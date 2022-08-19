using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000528 RID: 1320
	public class WindowsNameTransform : INameTransform
	{
		// Token: 0x06002A4E RID: 10830 RVA: 0x00140F3A File Offset: 0x0013F13A
		public WindowsNameTransform(string baseDirectory, bool allowParentTraversal = false)
		{
			if (baseDirectory == null)
			{
				throw new ArgumentNullException("baseDirectory", "Directory name is invalid");
			}
			this.BaseDirectory = baseDirectory;
			this.AllowParentTraversal = allowParentTraversal;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x00140F6C File Offset: 0x0013F16C
		public WindowsNameTransform()
		{
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x00140F7C File Offset: 0x0013F17C
		// (set) Token: 0x06002A51 RID: 10833 RVA: 0x00140F84 File Offset: 0x0013F184
		public string BaseDirectory
		{
			get
			{
				return this._baseDirectory;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._baseDirectory = Path.GetFullPath(value);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x00140FA0 File Offset: 0x0013F1A0
		// (set) Token: 0x06002A53 RID: 10835 RVA: 0x00140FA8 File Offset: 0x0013F1A8
		public bool AllowParentTraversal
		{
			get
			{
				return this._allowParentTraversal;
			}
			set
			{
				this._allowParentTraversal = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x00140FB1 File Offset: 0x0013F1B1
		// (set) Token: 0x06002A55 RID: 10837 RVA: 0x00140FB9 File Offset: 0x0013F1B9
		public bool TrimIncomingPaths
		{
			get
			{
				return this._trimIncomingPaths;
			}
			set
			{
				this._trimIncomingPaths = value;
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00140FC4 File Offset: 0x0013F1C4
		public string TransformDirectory(string name)
		{
			name = this.TransformFile(name);
			if (name.Length > 0)
			{
				while (name.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
				{
					name = name.Remove(name.Length - 1, 1);
				}
				return name;
			}
			throw new InvalidNameException("Cannot have an empty directory name");
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x0014101C File Offset: 0x0013F21C
		public string TransformFile(string name)
		{
			if (name != null)
			{
				name = WindowsNameTransform.MakeValidName(name, this._replacementChar);
				if (this._trimIncomingPaths)
				{
					name = Path.GetFileName(name);
				}
				if (this._baseDirectory != null)
				{
					name = Path.Combine(this._baseDirectory, name);
					if (!this._allowParentTraversal && !Path.GetFullPath(name).StartsWith(this._baseDirectory, StringComparison.InvariantCultureIgnoreCase))
					{
						throw new InvalidNameException("Parent traversal in paths is not allowed");
					}
				}
			}
			else
			{
				name = string.Empty;
			}
			return name;
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x0014108F File Offset: 0x0013F28F
		public static bool IsValidName(string name)
		{
			return name != null && name.Length <= 260 && string.Compare(name, WindowsNameTransform.MakeValidName(name, '_'), StringComparison.Ordinal) == 0;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x001410B8 File Offset: 0x0013F2B8
		public static string MakeValidName(string name, char replacement)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			name = PathUtils.DropPathRoot(name.Replace("/", Path.DirectorySeparatorChar.ToString()));
			while (name.Length > 0)
			{
				if (name[0] != Path.DirectorySeparatorChar)
				{
					break;
				}
				name = name.Remove(0, 1);
			}
			while (name.Length > 0 && name[name.Length - 1] == Path.DirectorySeparatorChar)
			{
				name = name.Remove(name.Length - 1, 1);
			}
			int i;
			for (i = name.IndexOf(string.Format("{0}{0}", Path.DirectorySeparatorChar), StringComparison.Ordinal); i >= 0; i = name.IndexOf(string.Format("{0}{0}", Path.DirectorySeparatorChar), StringComparison.Ordinal))
			{
				name = name.Remove(i, 1);
			}
			i = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars);
			if (i >= 0)
			{
				StringBuilder stringBuilder = new StringBuilder(name);
				while (i >= 0)
				{
					stringBuilder[i] = replacement;
					if (i >= name.Length)
					{
						i = -1;
					}
					else
					{
						i = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars, i + 1);
					}
				}
				name = stringBuilder.ToString();
			}
			if (name.Length > 260)
			{
				throw new PathTooLongException();
			}
			return name;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x001411EB File Offset: 0x0013F3EB
		// (set) Token: 0x06002A5B RID: 10843 RVA: 0x001411F4 File Offset: 0x0013F3F4
		public char Replacement
		{
			get
			{
				return this._replacementChar;
			}
			set
			{
				for (int i = 0; i < WindowsNameTransform.InvalidEntryChars.Length; i++)
				{
					if (WindowsNameTransform.InvalidEntryChars[i] == value)
					{
						throw new ArgumentException("invalid path character");
					}
				}
				if (value == Path.DirectorySeparatorChar || value == Path.AltDirectorySeparatorChar)
				{
					throw new ArgumentException("invalid replacement character");
				}
				this._replacementChar = value;
			}
		}

		// Token: 0x04002666 RID: 9830
		private const int MaxPath = 260;

		// Token: 0x04002667 RID: 9831
		private string _baseDirectory;

		// Token: 0x04002668 RID: 9832
		private bool _trimIncomingPaths;

		// Token: 0x04002669 RID: 9833
		private char _replacementChar = '_';

		// Token: 0x0400266A RID: 9834
		private bool _allowParentTraversal;

		// Token: 0x0400266B RID: 9835
		private static readonly char[] InvalidEntryChars = new char[]
		{
			'"',
			'<',
			'>',
			'|',
			'\0',
			'\u0001',
			'\u0002',
			'\u0003',
			'\u0004',
			'\u0005',
			'\u0006',
			'\a',
			'\b',
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			'\u000e',
			'\u000f',
			'\u0010',
			'\u0011',
			'\u0012',
			'\u0013',
			'\u0014',
			'\u0015',
			'\u0016',
			'\u0017',
			'\u0018',
			'\u0019',
			'\u001a',
			'\u001b',
			'\u001c',
			'\u001d',
			'\u001e',
			'\u001f',
			'*',
			'?',
			':'
		};
	}
}
