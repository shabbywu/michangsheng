using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000531 RID: 1329
	public class ZipEntryFactory : IEntryFactory
	{
		// Token: 0x06002AA6 RID: 10918 RVA: 0x00141E58 File Offset: 0x00140058
		public ZipEntryFactory()
		{
			this.nameTransform_ = new ZipNameTransform();
			this.isUnicodeText_ = ZipStrings.UseUnicode;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00141E88 File Offset: 0x00140088
		public ZipEntryFactory(ZipEntryFactory.TimeSetting timeSetting) : this()
		{
			this.timeSetting_ = timeSetting;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x00141E97 File Offset: 0x00140097
		public ZipEntryFactory(DateTime time) : this()
		{
			this.timeSetting_ = ZipEntryFactory.TimeSetting.Fixed;
			this.FixedDateTime = time;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x00141EAD File Offset: 0x001400AD
		// (set) Token: 0x06002AAA RID: 10922 RVA: 0x00141EB5 File Offset: 0x001400B5
		public INameTransform NameTransform
		{
			get
			{
				return this.nameTransform_;
			}
			set
			{
				if (value == null)
				{
					this.nameTransform_ = new ZipNameTransform();
					return;
				}
				this.nameTransform_ = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06002AAB RID: 10923 RVA: 0x00141ECD File Offset: 0x001400CD
		// (set) Token: 0x06002AAC RID: 10924 RVA: 0x00141ED5 File Offset: 0x001400D5
		public ZipEntryFactory.TimeSetting Setting
		{
			get
			{
				return this.timeSetting_;
			}
			set
			{
				this.timeSetting_ = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06002AAD RID: 10925 RVA: 0x00141EDE File Offset: 0x001400DE
		// (set) Token: 0x06002AAE RID: 10926 RVA: 0x00141EE6 File Offset: 0x001400E6
		public DateTime FixedDateTime
		{
			get
			{
				return this.fixedDateTime_;
			}
			set
			{
				if (value.Year < 1970)
				{
					throw new ArgumentException("Value is too old to be valid", "value");
				}
				this.fixedDateTime_ = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x00141F0D File Offset: 0x0014010D
		// (set) Token: 0x06002AB0 RID: 10928 RVA: 0x00141F15 File Offset: 0x00140115
		public int GetAttributes
		{
			get
			{
				return this.getAttributes_;
			}
			set
			{
				this.getAttributes_ = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x00141F1E File Offset: 0x0014011E
		// (set) Token: 0x06002AB2 RID: 10930 RVA: 0x00141F26 File Offset: 0x00140126
		public int SetAttributes
		{
			get
			{
				return this.setAttributes_;
			}
			set
			{
				this.setAttributes_ = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x00141F2F File Offset: 0x0014012F
		// (set) Token: 0x06002AB4 RID: 10932 RVA: 0x00141F37 File Offset: 0x00140137
		public bool IsUnicodeText
		{
			get
			{
				return this.isUnicodeText_;
			}
			set
			{
				this.isUnicodeText_ = value;
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x00141F40 File Offset: 0x00140140
		public ZipEntry MakeFileEntry(string fileName)
		{
			return this.MakeFileEntry(fileName, null, true);
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x00141F4B File Offset: 0x0014014B
		public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
		{
			return this.MakeFileEntry(fileName, null, useFileSystem);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x00141F58 File Offset: 0x00140158
		public ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem)
		{
			ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformFile((!string.IsNullOrEmpty(entryName)) ? entryName : fileName));
			zipEntry.IsUnicodeText = this.isUnicodeText_;
			int num = 0;
			bool flag = this.setAttributes_ != 0;
			FileInfo fileInfo = null;
			if (useFileSystem)
			{
				fileInfo = new FileInfo(fileName);
			}
			if (fileInfo != null && fileInfo.Exists)
			{
				switch (this.timeSetting_)
				{
				case ZipEntryFactory.TimeSetting.LastWriteTime:
					zipEntry.DateTime = fileInfo.LastWriteTime;
					break;
				case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
					zipEntry.DateTime = fileInfo.LastWriteTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.CreateTime:
					zipEntry.DateTime = fileInfo.CreationTime;
					break;
				case ZipEntryFactory.TimeSetting.CreateTimeUtc:
					zipEntry.DateTime = fileInfo.CreationTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTime:
					zipEntry.DateTime = fileInfo.LastAccessTime;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
					zipEntry.DateTime = fileInfo.LastAccessTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.Fixed:
					zipEntry.DateTime = this.fixedDateTime_;
					break;
				default:
					throw new ZipException("Unhandled time setting in MakeFileEntry");
				}
				zipEntry.Size = fileInfo.Length;
				flag = true;
				num = (int)(fileInfo.Attributes & (FileAttributes)this.getAttributes_);
			}
			else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
			{
				zipEntry.DateTime = this.fixedDateTime_;
			}
			if (flag)
			{
				num |= this.setAttributes_;
				zipEntry.ExternalFileAttributes = num;
			}
			return zipEntry;
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x00142098 File Offset: 0x00140298
		public ZipEntry MakeDirectoryEntry(string directoryName)
		{
			return this.MakeDirectoryEntry(directoryName, true);
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x001420A4 File Offset: 0x001402A4
		public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
		{
			ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformDirectory(directoryName));
			zipEntry.IsUnicodeText = this.isUnicodeText_;
			zipEntry.Size = 0L;
			int num = 0;
			DirectoryInfo directoryInfo = null;
			if (useFileSystem)
			{
				directoryInfo = new DirectoryInfo(directoryName);
			}
			if (directoryInfo != null && directoryInfo.Exists)
			{
				switch (this.timeSetting_)
				{
				case ZipEntryFactory.TimeSetting.LastWriteTime:
					zipEntry.DateTime = directoryInfo.LastWriteTime;
					break;
				case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
					zipEntry.DateTime = directoryInfo.LastWriteTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.CreateTime:
					zipEntry.DateTime = directoryInfo.CreationTime;
					break;
				case ZipEntryFactory.TimeSetting.CreateTimeUtc:
					zipEntry.DateTime = directoryInfo.CreationTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTime:
					zipEntry.DateTime = directoryInfo.LastAccessTime;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
					zipEntry.DateTime = directoryInfo.LastAccessTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.Fixed:
					zipEntry.DateTime = this.fixedDateTime_;
					break;
				default:
					throw new ZipException("Unhandled time setting in MakeDirectoryEntry");
				}
				num = (int)(directoryInfo.Attributes & (FileAttributes)this.getAttributes_);
			}
			else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
			{
				zipEntry.DateTime = this.fixedDateTime_;
			}
			num |= (this.setAttributes_ | 16);
			zipEntry.ExternalFileAttributes = num;
			return zipEntry;
		}

		// Token: 0x040026EA RID: 9962
		private INameTransform nameTransform_;

		// Token: 0x040026EB RID: 9963
		private DateTime fixedDateTime_ = DateTime.Now;

		// Token: 0x040026EC RID: 9964
		private ZipEntryFactory.TimeSetting timeSetting_;

		// Token: 0x040026ED RID: 9965
		private bool isUnicodeText_;

		// Token: 0x040026EE RID: 9966
		private int getAttributes_ = -1;

		// Token: 0x040026EF RID: 9967
		private int setAttributes_;

		// Token: 0x02001481 RID: 5249
		public enum TimeSetting
		{
			// Token: 0x04006C3E RID: 27710
			LastWriteTime,
			// Token: 0x04006C3F RID: 27711
			LastWriteTimeUtc,
			// Token: 0x04006C40 RID: 27712
			CreateTime,
			// Token: 0x04006C41 RID: 27713
			CreateTimeUtc,
			// Token: 0x04006C42 RID: 27714
			LastAccessTime,
			// Token: 0x04006C43 RID: 27715
			LastAccessTimeUtc,
			// Token: 0x04006C44 RID: 27716
			Fixed
		}
	}
}
