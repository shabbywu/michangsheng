using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000809 RID: 2057
	public class TarEntry
	{
		// Token: 0x0600358C RID: 13708 RVA: 0x000271AD File Offset: 0x000253AD
		private TarEntry()
		{
			this.header = new TarHeader();
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000271C0 File Offset: 0x000253C0
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarEntry(byte[] headerBuffer) : this(headerBuffer, null)
		{
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000271CA File Offset: 0x000253CA
		public TarEntry(byte[] headerBuffer, Encoding nameEncoding)
		{
			this.header = new TarHeader();
			this.header.ParseBuffer(headerBuffer, nameEncoding);
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000271EA File Offset: 0x000253EA
		public TarEntry(TarHeader header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			this.header = (TarHeader)header.Clone();
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x00027211 File Offset: 0x00025411
		public object Clone()
		{
			return new TarEntry
			{
				file = this.file,
				header = (TarHeader)this.header.Clone(),
				Name = this.Name
			};
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x00027246 File Offset: 0x00025446
		public static TarEntry CreateTarEntry(string name)
		{
			TarEntry tarEntry = new TarEntry();
			TarEntry.NameTarHeader(tarEntry.header, name);
			return tarEntry;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x00027259 File Offset: 0x00025459
		public static TarEntry CreateEntryFromFile(string fileName)
		{
			TarEntry tarEntry = new TarEntry();
			tarEntry.GetFileTarHeader(tarEntry.header, fileName);
			return tarEntry;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x001993C0 File Offset: 0x001975C0
		public override bool Equals(object obj)
		{
			TarEntry tarEntry = obj as TarEntry;
			return tarEntry != null && this.Name.Equals(tarEntry.Name);
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x0002726D File Offset: 0x0002546D
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x0002727A File Offset: 0x0002547A
		public bool IsDescendent(TarEntry toTest)
		{
			if (toTest == null)
			{
				throw new ArgumentNullException("toTest");
			}
			return toTest.Name.StartsWith(this.Name, StringComparison.Ordinal);
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x0002729C File Offset: 0x0002549C
		public TarHeader TarHeader
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000272A4 File Offset: 0x000254A4
		// (set) Token: 0x06003598 RID: 13720 RVA: 0x000272B1 File Offset: 0x000254B1
		public string Name
		{
			get
			{
				return this.header.Name;
			}
			set
			{
				this.header.Name = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x000272BF File Offset: 0x000254BF
		// (set) Token: 0x0600359A RID: 13722 RVA: 0x000272CC File Offset: 0x000254CC
		public int UserId
		{
			get
			{
				return this.header.UserId;
			}
			set
			{
				this.header.UserId = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000272DA File Offset: 0x000254DA
		// (set) Token: 0x0600359C RID: 13724 RVA: 0x000272E7 File Offset: 0x000254E7
		public int GroupId
		{
			get
			{
				return this.header.GroupId;
			}
			set
			{
				this.header.GroupId = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x000272F5 File Offset: 0x000254F5
		// (set) Token: 0x0600359E RID: 13726 RVA: 0x00027302 File Offset: 0x00025502
		public string UserName
		{
			get
			{
				return this.header.UserName;
			}
			set
			{
				this.header.UserName = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x00027310 File Offset: 0x00025510
		// (set) Token: 0x060035A0 RID: 13728 RVA: 0x0002731D File Offset: 0x0002551D
		public string GroupName
		{
			get
			{
				return this.header.GroupName;
			}
			set
			{
				this.header.GroupName = value;
			}
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x0002732B File Offset: 0x0002552B
		public void SetIds(int userId, int groupId)
		{
			this.UserId = userId;
			this.GroupId = groupId;
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x0002733B File Offset: 0x0002553B
		public void SetNames(string userName, string groupName)
		{
			this.UserName = userName;
			this.GroupName = groupName;
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x0002734B File Offset: 0x0002554B
		// (set) Token: 0x060035A4 RID: 13732 RVA: 0x00027358 File Offset: 0x00025558
		public DateTime ModTime
		{
			get
			{
				return this.header.ModTime;
			}
			set
			{
				this.header.ModTime = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x00027366 File Offset: 0x00025566
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x0002736E File Offset: 0x0002556E
		// (set) Token: 0x060035A7 RID: 13735 RVA: 0x0002737B File Offset: 0x0002557B
		public long Size
		{
			get
			{
				return this.header.Size;
			}
			set
			{
				this.header.Size = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x001993EC File Offset: 0x001975EC
		public bool IsDirectory
		{
			get
			{
				if (this.file != null)
				{
					return Directory.Exists(this.file);
				}
				return this.header != null && (this.header.TypeFlag == 53 || this.Name.EndsWith("/", StringComparison.Ordinal));
			}
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x0019943C File Offset: 0x0019763C
		public void GetFileTarHeader(TarHeader header, string file)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			this.file = file;
			string text = file;
			if (text.IndexOf(Directory.GetCurrentDirectory(), StringComparison.Ordinal) == 0)
			{
				text = text.Substring(Directory.GetCurrentDirectory().Length);
			}
			text = text.Replace(Path.DirectorySeparatorChar, '/');
			while (text.StartsWith("/", StringComparison.Ordinal))
			{
				text = text.Substring(1);
			}
			header.LinkName = string.Empty;
			header.Name = text;
			if (Directory.Exists(file))
			{
				header.Mode = 1003;
				header.TypeFlag = 53;
				if (header.Name.Length == 0 || header.Name[header.Name.Length - 1] != '/')
				{
					header.Name += "/";
				}
				header.Size = 0L;
			}
			else
			{
				header.Mode = 33216;
				header.TypeFlag = 48;
				header.Size = new FileInfo(file.Replace('/', Path.DirectorySeparatorChar)).Length;
			}
			header.ModTime = System.IO.File.GetLastWriteTime(file.Replace('/', Path.DirectorySeparatorChar)).ToUniversalTime();
			header.DevMajor = 0;
			header.DevMinor = 0;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x00199588 File Offset: 0x00197788
		public TarEntry[] GetDirectoryEntries()
		{
			if (this.file == null || !Directory.Exists(this.file))
			{
				return new TarEntry[0];
			}
			string[] fileSystemEntries = Directory.GetFileSystemEntries(this.file);
			TarEntry[] array = new TarEntry[fileSystemEntries.Length];
			for (int i = 0; i < fileSystemEntries.Length; i++)
			{
				array[i] = TarEntry.CreateEntryFromFile(fileSystemEntries[i]);
			}
			return array;
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x00027389 File Offset: 0x00025589
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public void WriteEntryHeader(byte[] outBuffer)
		{
			this.WriteEntryHeader(outBuffer, null);
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x00027393 File Offset: 0x00025593
		public void WriteEntryHeader(byte[] outBuffer, Encoding nameEncoding)
		{
			this.header.WriteHeader(outBuffer, nameEncoding);
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000273A2 File Offset: 0x000255A2
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static void AdjustEntryName(byte[] buffer, string newName)
		{
			TarEntry.AdjustEntryName(buffer, newName, null);
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000273AC File Offset: 0x000255AC
		public static void AdjustEntryName(byte[] buffer, string newName, Encoding nameEncoding)
		{
			TarHeader.GetNameBytes(newName, buffer, 0, 100, nameEncoding);
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x001995E0 File Offset: 0x001977E0
		public static void NameTarHeader(TarHeader header, string name)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool flag = name.EndsWith("/", StringComparison.Ordinal);
			header.Name = name;
			header.Mode = (flag ? 1003 : 33216);
			header.UserId = 0;
			header.GroupId = 0;
			header.Size = 0L;
			header.ModTime = DateTime.UtcNow;
			header.TypeFlag = (flag ? 53 : 48);
			header.LinkName = string.Empty;
			header.UserName = string.Empty;
			header.GroupName = string.Empty;
			header.DevMajor = 0;
			header.DevMinor = 0;
		}

		// Token: 0x04003060 RID: 12384
		private string file;

		// Token: 0x04003061 RID: 12385
		private TarHeader header;
	}
}
