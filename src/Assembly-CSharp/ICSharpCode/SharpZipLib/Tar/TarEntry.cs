using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000563 RID: 1379
	public class TarEntry
	{
		// Token: 0x06002D1E RID: 11550 RVA: 0x0014D918 File Offset: 0x0014BB18
		private TarEntry()
		{
			this.header = new TarHeader();
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x0014D92B File Offset: 0x0014BB2B
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarEntry(byte[] headerBuffer) : this(headerBuffer, null)
		{
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x0014D935 File Offset: 0x0014BB35
		public TarEntry(byte[] headerBuffer, Encoding nameEncoding)
		{
			this.header = new TarHeader();
			this.header.ParseBuffer(headerBuffer, nameEncoding);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x0014D955 File Offset: 0x0014BB55
		public TarEntry(TarHeader header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			this.header = (TarHeader)header.Clone();
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x0014D97C File Offset: 0x0014BB7C
		public object Clone()
		{
			return new TarEntry
			{
				file = this.file,
				header = (TarHeader)this.header.Clone(),
				Name = this.Name
			};
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x0014D9B1 File Offset: 0x0014BBB1
		public static TarEntry CreateTarEntry(string name)
		{
			TarEntry tarEntry = new TarEntry();
			TarEntry.NameTarHeader(tarEntry.header, name);
			return tarEntry;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x0014D9C4 File Offset: 0x0014BBC4
		public static TarEntry CreateEntryFromFile(string fileName)
		{
			TarEntry tarEntry = new TarEntry();
			tarEntry.GetFileTarHeader(tarEntry.header, fileName);
			return tarEntry;
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x0014D9D8 File Offset: 0x0014BBD8
		public override bool Equals(object obj)
		{
			TarEntry tarEntry = obj as TarEntry;
			return tarEntry != null && this.Name.Equals(tarEntry.Name);
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x0014DA02 File Offset: 0x0014BC02
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x0014DA0F File Offset: 0x0014BC0F
		public bool IsDescendent(TarEntry toTest)
		{
			if (toTest == null)
			{
				throw new ArgumentNullException("toTest");
			}
			return toTest.Name.StartsWith(this.Name, StringComparison.Ordinal);
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x0014DA31 File Offset: 0x0014BC31
		public TarHeader TarHeader
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x0014DA39 File Offset: 0x0014BC39
		// (set) Token: 0x06002D2A RID: 11562 RVA: 0x0014DA46 File Offset: 0x0014BC46
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

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x0014DA54 File Offset: 0x0014BC54
		// (set) Token: 0x06002D2C RID: 11564 RVA: 0x0014DA61 File Offset: 0x0014BC61
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

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x0014DA6F File Offset: 0x0014BC6F
		// (set) Token: 0x06002D2E RID: 11566 RVA: 0x0014DA7C File Offset: 0x0014BC7C
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

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x0014DA8A File Offset: 0x0014BC8A
		// (set) Token: 0x06002D30 RID: 11568 RVA: 0x0014DA97 File Offset: 0x0014BC97
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

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x0014DAA5 File Offset: 0x0014BCA5
		// (set) Token: 0x06002D32 RID: 11570 RVA: 0x0014DAB2 File Offset: 0x0014BCB2
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

		// Token: 0x06002D33 RID: 11571 RVA: 0x0014DAC0 File Offset: 0x0014BCC0
		public void SetIds(int userId, int groupId)
		{
			this.UserId = userId;
			this.GroupId = groupId;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0014DAD0 File Offset: 0x0014BCD0
		public void SetNames(string userName, string groupName)
		{
			this.UserName = userName;
			this.GroupName = groupName;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x0014DAE0 File Offset: 0x0014BCE0
		// (set) Token: 0x06002D36 RID: 11574 RVA: 0x0014DAED File Offset: 0x0014BCED
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

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x0014DAFB File Offset: 0x0014BCFB
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x0014DB03 File Offset: 0x0014BD03
		// (set) Token: 0x06002D39 RID: 11577 RVA: 0x0014DB10 File Offset: 0x0014BD10
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

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x0014DB20 File Offset: 0x0014BD20
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

		// Token: 0x06002D3B RID: 11579 RVA: 0x0014DB70 File Offset: 0x0014BD70
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

		// Token: 0x06002D3C RID: 11580 RVA: 0x0014DCBC File Offset: 0x0014BEBC
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

		// Token: 0x06002D3D RID: 11581 RVA: 0x0014DD14 File Offset: 0x0014BF14
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public void WriteEntryHeader(byte[] outBuffer)
		{
			this.WriteEntryHeader(outBuffer, null);
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x0014DD1E File Offset: 0x0014BF1E
		public void WriteEntryHeader(byte[] outBuffer, Encoding nameEncoding)
		{
			this.header.WriteHeader(outBuffer, nameEncoding);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x0014DD2D File Offset: 0x0014BF2D
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static void AdjustEntryName(byte[] buffer, string newName)
		{
			TarEntry.AdjustEntryName(buffer, newName, null);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x0014DD37 File Offset: 0x0014BF37
		public static void AdjustEntryName(byte[] buffer, string newName, Encoding nameEncoding)
		{
			TarHeader.GetNameBytes(newName, buffer, 0, 100, nameEncoding);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0014DD48 File Offset: 0x0014BF48
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

		// Token: 0x04002827 RID: 10279
		private string file;

		// Token: 0x04002828 RID: 10280
		private TarHeader header;
	}
}
