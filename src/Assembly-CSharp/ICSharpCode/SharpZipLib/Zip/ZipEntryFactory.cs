using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007C8 RID: 1992
	public class ZipEntryFactory : IEntryFactory
	{
		// Token: 0x060032BD RID: 12989 RVA: 0x00024F53 File Offset: 0x00023153
		public ZipEntryFactory()
		{
			this.nameTransform_ = new ZipNameTransform();
			this.isUnicodeText_ = ZipStrings.UseUnicode;
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x00024F83 File Offset: 0x00023183
		public ZipEntryFactory(ZipEntryFactory.TimeSetting timeSetting) : this()
		{
			this.timeSetting_ = timeSetting;
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x00024F92 File Offset: 0x00023192
		public ZipEntryFactory(DateTime time) : this()
		{
			this.timeSetting_ = ZipEntryFactory.TimeSetting.Fixed;
			this.FixedDateTime = time;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x00024FA8 File Offset: 0x000231A8
		// (set) Token: 0x060032C1 RID: 12993 RVA: 0x00024FB0 File Offset: 0x000231B0
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

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x00024FC8 File Offset: 0x000231C8
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x00024FD0 File Offset: 0x000231D0
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

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x00024FD9 File Offset: 0x000231D9
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x00024FE1 File Offset: 0x000231E1
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

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x00025008 File Offset: 0x00023208
		// (set) Token: 0x060032C7 RID: 12999 RVA: 0x00025010 File Offset: 0x00023210
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

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x00025019 File Offset: 0x00023219
		// (set) Token: 0x060032C9 RID: 13001 RVA: 0x00025021 File Offset: 0x00023221
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

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x0002502A File Offset: 0x0002322A
		// (set) Token: 0x060032CB RID: 13003 RVA: 0x00025032 File Offset: 0x00023232
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

		// Token: 0x060032CC RID: 13004 RVA: 0x0002503B File Offset: 0x0002323B
		public ZipEntry MakeFileEntry(string fileName)
		{
			return this.MakeFileEntry(fileName, null, true);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x00025046 File Offset: 0x00023246
		public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
		{
			return this.MakeFileEntry(fileName, null, useFileSystem);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x0018E928 File Offset: 0x0018CB28
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

		// Token: 0x060032CF RID: 13007 RVA: 0x00025051 File Offset: 0x00023251
		public ZipEntry MakeDirectoryEntry(string directoryName)
		{
			return this.MakeDirectoryEntry(directoryName, true);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x0018EA68 File Offset: 0x0018CC68
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

		// Token: 0x04002EE5 RID: 12005
		private INameTransform nameTransform_;

		// Token: 0x04002EE6 RID: 12006
		private DateTime fixedDateTime_ = DateTime.Now;

		// Token: 0x04002EE7 RID: 12007
		private ZipEntryFactory.TimeSetting timeSetting_;

		// Token: 0x04002EE8 RID: 12008
		private bool isUnicodeText_;

		// Token: 0x04002EE9 RID: 12009
		private int getAttributes_ = -1;

		// Token: 0x04002EEA RID: 12010
		private int setAttributes_;

		// Token: 0x020007C9 RID: 1993
		public enum TimeSetting
		{
			// Token: 0x04002EEC RID: 12012
			LastWriteTime,
			// Token: 0x04002EED RID: 12013
			LastWriteTimeUtc,
			// Token: 0x04002EEE RID: 12014
			CreateTime,
			// Token: 0x04002EEF RID: 12015
			CreateTimeUtc,
			// Token: 0x04002EF0 RID: 12016
			LastAccessTime,
			// Token: 0x04002EF1 RID: 12017
			LastAccessTimeUtc,
			// Token: 0x04002EF2 RID: 12018
			Fixed
		}
	}
}
