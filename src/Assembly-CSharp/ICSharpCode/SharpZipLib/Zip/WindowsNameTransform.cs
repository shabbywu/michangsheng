using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007BE RID: 1982
	public class WindowsNameTransform : INameTransform
	{
		// Token: 0x06003265 RID: 12901 RVA: 0x00024AFB File Offset: 0x00022CFB
		public WindowsNameTransform(string baseDirectory, bool allowParentTraversal = false)
		{
			if (baseDirectory == null)
			{
				throw new ArgumentNullException("baseDirectory", "Directory name is invalid");
			}
			this.BaseDirectory = baseDirectory;
			this.AllowParentTraversal = allowParentTraversal;
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x00024B2D File Offset: 0x00022D2D
		public WindowsNameTransform()
		{
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x00024B3D File Offset: 0x00022D3D
		// (set) Token: 0x06003268 RID: 12904 RVA: 0x00024B45 File Offset: 0x00022D45
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

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x00024B61 File Offset: 0x00022D61
		// (set) Token: 0x0600326A RID: 12906 RVA: 0x00024B69 File Offset: 0x00022D69
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

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x00024B72 File Offset: 0x00022D72
		// (set) Token: 0x0600326C RID: 12908 RVA: 0x00024B7A File Offset: 0x00022D7A
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

		// Token: 0x0600326D RID: 12909 RVA: 0x0018DE68 File Offset: 0x0018C068
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

		// Token: 0x0600326E RID: 12910 RVA: 0x0018DEC0 File Offset: 0x0018C0C0
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

		// Token: 0x0600326F RID: 12911 RVA: 0x00024B83 File Offset: 0x00022D83
		public static bool IsValidName(string name)
		{
			return name != null && name.Length <= 260 && string.Compare(name, WindowsNameTransform.MakeValidName(name, '_'), StringComparison.Ordinal) == 0;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x0018DF34 File Offset: 0x0018C134
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

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x00024BA9 File Offset: 0x00022DA9
		// (set) Token: 0x06003272 RID: 12914 RVA: 0x0018E068 File Offset: 0x0018C268
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

		// Token: 0x04002E5A RID: 11866
		private const int MaxPath = 260;

		// Token: 0x04002E5B RID: 11867
		private string _baseDirectory;

		// Token: 0x04002E5C RID: 11868
		private bool _trimIncomingPaths;

		// Token: 0x04002E5D RID: 11869
		private char _replacementChar = '_';

		// Token: 0x04002E5E RID: 11870
		private bool _allowParentTraversal;

		// Token: 0x04002E5F RID: 11871
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
