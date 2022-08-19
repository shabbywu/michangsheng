using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000530 RID: 1328
	public class ZipEntry
	{
		// Token: 0x06002A65 RID: 10853 RVA: 0x001412A7 File Offset: 0x0013F4A7
		public ZipEntry(string name) : this(name, 0, 51, CompressionMethod.Deflated)
		{
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x001412B4 File Offset: 0x0013F4B4
		internal ZipEntry(string name, int versionRequiredToExtract) : this(name, versionRequiredToExtract, 51, CompressionMethod.Deflated)
		{
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x001412C4 File Offset: 0x0013F4C4
		internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo, CompressionMethod method)
		{
			this.externalFileAttributes = -1;
			this.method = CompressionMethod.Deflated;
			this.zipFileIndex = -1L;
			base..ctor();
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length > 65535)
			{
				throw new ArgumentException("Name is too long", "name");
			}
			if (versionRequiredToExtract != 0 && versionRequiredToExtract < 10)
			{
				throw new ArgumentOutOfRangeException("versionRequiredToExtract");
			}
			this.DateTime = DateTime.Now;
			this.name = name;
			this.versionMadeBy = (ushort)madeByInfo;
			this.versionToExtract = (ushort)versionRequiredToExtract;
			this.method = method;
			this.IsUnicodeText = ZipStrings.UseUnicode;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x00141360 File Offset: 0x0013F560
		[Obsolete("Use Clone instead")]
		public ZipEntry(ZipEntry entry)
		{
			this.externalFileAttributes = -1;
			this.method = CompressionMethod.Deflated;
			this.zipFileIndex = -1L;
			base..ctor();
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.known = entry.known;
			this.name = entry.name;
			this.size = entry.size;
			this.compressedSize = entry.compressedSize;
			this.crc = entry.crc;
			this.dateTime = entry.DateTime;
			this.method = entry.method;
			this.comment = entry.comment;
			this.versionToExtract = entry.versionToExtract;
			this.versionMadeBy = entry.versionMadeBy;
			this.externalFileAttributes = entry.externalFileAttributes;
			this.flags = entry.flags;
			this.zipFileIndex = entry.zipFileIndex;
			this.offset = entry.offset;
			this.forceZip64_ = entry.forceZip64_;
			if (entry.extra != null)
			{
				this.extra = new byte[entry.extra.Length];
				Array.Copy(entry.extra, 0, this.extra, 0, entry.extra.Length);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x00141481 File Offset: 0x0013F681
		public bool HasCrc
		{
			get
			{
				return (this.known & ZipEntry.Known.Crc) > ZipEntry.Known.None;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x0014148E File Offset: 0x0013F68E
		// (set) Token: 0x06002A6B RID: 10859 RVA: 0x0014149B File Offset: 0x0013F69B
		public bool IsCrypted
		{
			get
			{
				return (this.flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 1;
					return;
				}
				this.flags &= -2;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06002A6C RID: 10860 RVA: 0x001414BE File Offset: 0x0013F6BE
		// (set) Token: 0x06002A6D RID: 10861 RVA: 0x001414CF File Offset: 0x0013F6CF
		public bool IsUnicodeText
		{
			get
			{
				return (this.flags & 2048) != 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 2048;
					return;
				}
				this.flags &= -2049;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x001414F9 File Offset: 0x0013F6F9
		// (set) Token: 0x06002A6F RID: 10863 RVA: 0x00141501 File Offset: 0x0013F701
		internal byte CryptoCheckValue
		{
			get
			{
				return this.cryptoCheckValue_;
			}
			set
			{
				this.cryptoCheckValue_ = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06002A70 RID: 10864 RVA: 0x0014150A File Offset: 0x0013F70A
		// (set) Token: 0x06002A71 RID: 10865 RVA: 0x00141512 File Offset: 0x0013F712
		public int Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x0014151B File Offset: 0x0013F71B
		// (set) Token: 0x06002A73 RID: 10867 RVA: 0x00141523 File Offset: 0x0013F723
		public long ZipFileIndex
		{
			get
			{
				return this.zipFileIndex;
			}
			set
			{
				this.zipFileIndex = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x0014152C File Offset: 0x0013F72C
		// (set) Token: 0x06002A75 RID: 10869 RVA: 0x00141534 File Offset: 0x0013F734
		public long Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x0014153D File Offset: 0x0013F73D
		// (set) Token: 0x06002A77 RID: 10871 RVA: 0x00141552 File Offset: 0x0013F752
		public int ExternalFileAttributes
		{
			get
			{
				if ((this.known & ZipEntry.Known.ExternalAttributes) == ZipEntry.Known.None)
				{
					return -1;
				}
				return this.externalFileAttributes;
			}
			set
			{
				this.externalFileAttributes = value;
				this.known |= ZipEntry.Known.ExternalAttributes;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06002A78 RID: 10872 RVA: 0x0014156A File Offset: 0x0013F76A
		public int VersionMadeBy
		{
			get
			{
				return (int)(this.versionMadeBy & 255);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x00141578 File Offset: 0x0013F778
		public bool IsDOSEntry
		{
			get
			{
				return this.HostSystem == 0 || this.HostSystem == 10;
			}
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00141590 File Offset: 0x0013F790
		private bool HasDosAttributes(int attributes)
		{
			bool flag = false;
			if ((this.known & ZipEntry.Known.ExternalAttributes) != ZipEntry.Known.None)
			{
				flag |= ((this.HostSystem == 0 || this.HostSystem == 10) && (this.ExternalFileAttributes & attributes) == attributes);
			}
			return flag;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x001415CE File Offset: 0x0013F7CE
		// (set) Token: 0x06002A7C RID: 10876 RVA: 0x001415DE File Offset: 0x0013F7DE
		public int HostSystem
		{
			get
			{
				return this.versionMadeBy >> 8 & 255;
			}
			set
			{
				this.versionMadeBy &= 255;
				this.versionMadeBy |= (ushort)((value & 255) << 8);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x0014160C File Offset: 0x0013F80C
		public int Version
		{
			get
			{
				if (this.versionToExtract != 0)
				{
					return (int)(this.versionToExtract & 255);
				}
				int result = 10;
				if (this.AESKeySize > 0)
				{
					result = 51;
				}
				else if (this.CentralHeaderRequiresZip64)
				{
					result = 45;
				}
				else if (CompressionMethod.Deflated == this.method)
				{
					result = 20;
				}
				else if (CompressionMethod.BZip2 == this.method)
				{
					result = 46;
				}
				else if (this.IsDirectory)
				{
					result = 20;
				}
				else if (this.IsCrypted)
				{
					result = 20;
				}
				else if (this.HasDosAttributes(8))
				{
					result = 11;
				}
				return result;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x00141690 File Offset: 0x0013F890
		public bool CanDecompress
		{
			get
			{
				return this.Version <= 51 && (this.Version == 10 || this.Version == 11 || this.Version == 20 || this.Version == 45 || this.Version == 46 || this.Version == 51) && this.IsCompressionMethodSupported();
			}
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x001416EB File Offset: 0x0013F8EB
		public void ForceZip64()
		{
			this.forceZip64_ = true;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x001416F4 File Offset: 0x0013F8F4
		public bool IsZip64Forced()
		{
			return this.forceZip64_;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x001416FC File Offset: 0x0013F8FC
		public bool LocalHeaderRequiresZip64
		{
			get
			{
				bool flag = this.forceZip64_;
				if (!flag)
				{
					ulong num = this.compressedSize;
					if (this.versionToExtract == 0 && this.IsCrypted)
					{
						num += (ulong)((long)this.EncryptionOverheadSize);
					}
					flag = ((this.size >= (ulong)-1 || num >= (ulong)-1) && (this.versionToExtract == 0 || this.versionToExtract >= 45));
				}
				return flag;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x00141760 File Offset: 0x0013F960
		public bool CentralHeaderRequiresZip64
		{
			get
			{
				return this.LocalHeaderRequiresZip64 || this.offset >= (long)((ulong)-1);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x0014177C File Offset: 0x0013F97C
		// (set) Token: 0x06002A84 RID: 10884 RVA: 0x0014185C File Offset: 0x0013FA5C
		public long DosTime
		{
			get
			{
				if ((this.known & ZipEntry.Known.Time) == ZipEntry.Known.None)
				{
					return 0L;
				}
				uint num = (uint)this.DateTime.Year;
				uint num2 = (uint)this.DateTime.Month;
				uint num3 = (uint)this.DateTime.Day;
				uint num4 = (uint)this.DateTime.Hour;
				uint num5 = (uint)this.DateTime.Minute;
				uint num6 = (uint)this.DateTime.Second;
				if (num < 1980U)
				{
					num = 1980U;
					num2 = 1U;
					num3 = 1U;
					num4 = 0U;
					num5 = 0U;
					num6 = 0U;
				}
				else if (num > 2107U)
				{
					num = 2107U;
					num2 = 12U;
					num3 = 31U;
					num4 = 23U;
					num5 = 59U;
					num6 = 59U;
				}
				return (long)((ulong)((num - 1980U & 127U) << 25 | num2 << 21 | num3 << 16 | num4 << 11 | num5 << 5 | num6 >> 1));
			}
			set
			{
				uint num = (uint)value;
				uint second = Math.Min(59U, 2U * (num & 31U));
				uint minute = Math.Min(59U, num >> 5 & 63U);
				uint hour = Math.Min(23U, num >> 11 & 31U);
				uint month = Math.Max(1U, Math.Min(12U, (uint)(value >> 21) & 15U));
				uint year = (num >> 25 & 127U) + 1980U;
				int day = Math.Max(1, Math.Min(DateTime.DaysInMonth((int)year, (int)month), (int)(value >> 16 & 31L)));
				this.DateTime = new DateTime((int)year, (int)month, day, (int)hour, (int)minute, (int)second, DateTimeKind.Unspecified);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x001418F1 File Offset: 0x0013FAF1
		// (set) Token: 0x06002A86 RID: 10886 RVA: 0x001418F9 File Offset: 0x0013FAF9
		public DateTime DateTime
		{
			get
			{
				return this.dateTime;
			}
			set
			{
				this.dateTime = value;
				this.known |= ZipEntry.Known.Time;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x00141910 File Offset: 0x0013FB10
		// (set) Token: 0x06002A88 RID: 10888 RVA: 0x00141918 File Offset: 0x0013FB18
		public string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x00141921 File Offset: 0x0013FB21
		// (set) Token: 0x06002A8A RID: 10890 RVA: 0x00141936 File Offset: 0x0013FB36
		public long Size
		{
			get
			{
				if ((this.known & ZipEntry.Known.Size) == ZipEntry.Known.None)
				{
					return -1L;
				}
				return (long)this.size;
			}
			set
			{
				this.size = (ulong)value;
				this.known |= ZipEntry.Known.Size;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x0014194D File Offset: 0x0013FB4D
		// (set) Token: 0x06002A8C RID: 10892 RVA: 0x00141962 File Offset: 0x0013FB62
		public long CompressedSize
		{
			get
			{
				if ((this.known & ZipEntry.Known.CompressedSize) == ZipEntry.Known.None)
				{
					return -1L;
				}
				return (long)this.compressedSize;
			}
			set
			{
				this.compressedSize = (ulong)value;
				this.known |= ZipEntry.Known.CompressedSize;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x00141979 File Offset: 0x0013FB79
		// (set) Token: 0x06002A8E RID: 10894 RVA: 0x00141992 File Offset: 0x0013FB92
		public long Crc
		{
			get
			{
				if ((this.known & ZipEntry.Known.Crc) == ZipEntry.Known.None)
				{
					return -1L;
				}
				return (long)((ulong)this.crc & (ulong)-1);
			}
			set
			{
				if (((ulong)this.crc & 18446744069414584320UL) != 0UL)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.crc = (uint)value;
				this.known |= ZipEntry.Known.Crc;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x001419C8 File Offset: 0x0013FBC8
		// (set) Token: 0x06002A90 RID: 10896 RVA: 0x001419D0 File Offset: 0x0013FBD0
		public CompressionMethod CompressionMethod
		{
			get
			{
				return this.method;
			}
			set
			{
				if (!ZipEntry.IsCompressionMethodSupported(value))
				{
					throw new NotSupportedException("Compression method not supported");
				}
				this.method = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x001419EC File Offset: 0x0013FBEC
		internal CompressionMethod CompressionMethodForHeader
		{
			get
			{
				if (this.AESKeySize <= 0)
				{
					return this.method;
				}
				return CompressionMethod.WinZipAES;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x00141A00 File Offset: 0x0013FC00
		// (set) Token: 0x06002A93 RID: 10899 RVA: 0x00141A08 File Offset: 0x0013FC08
		public byte[] ExtraData
		{
			get
			{
				return this.extra;
			}
			set
			{
				if (value == null)
				{
					this.extra = null;
					return;
				}
				if (value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.extra = new byte[value.Length];
				Array.Copy(value, 0, this.extra, 0, value.Length);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x00141A54 File Offset: 0x0013FC54
		// (set) Token: 0x06002A95 RID: 10901 RVA: 0x00141AB0 File Offset: 0x0013FCB0
		public int AESKeySize
		{
			get
			{
				switch (this._aesEncryptionStrength)
				{
				case 0:
					return 0;
				case 1:
					return 128;
				case 2:
					return 192;
				case 3:
					return 256;
				default:
					throw new ZipException("Invalid AESEncryptionStrength " + this._aesEncryptionStrength);
				}
			}
			set
			{
				if (value == 0)
				{
					this._aesEncryptionStrength = 0;
					return;
				}
				if (value == 128)
				{
					this._aesEncryptionStrength = 1;
					return;
				}
				if (value != 256)
				{
					throw new ZipException("AESKeySize must be 0, 128 or 256: " + value);
				}
				this._aesEncryptionStrength = 3;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x00141AFF File Offset: 0x0013FCFF
		internal byte AESEncryptionStrength
		{
			get
			{
				return (byte)this._aesEncryptionStrength;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x00141B08 File Offset: 0x0013FD08
		internal int AESSaltLen
		{
			get
			{
				return this.AESKeySize / 16;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x00141B13 File Offset: 0x0013FD13
		internal int AESOverheadSize
		{
			get
			{
				return 12 + this.AESSaltLen;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x00141B1E File Offset: 0x0013FD1E
		internal int EncryptionOverheadSize
		{
			get
			{
				if (!this.IsCrypted)
				{
					return 0;
				}
				if (this._aesEncryptionStrength == 0)
				{
					return 12;
				}
				return this.AESOverheadSize;
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x00141B3C File Offset: 0x0013FD3C
		internal void ProcessExtraData(bool localHeader)
		{
			ZipExtraData zipExtraData = new ZipExtraData(this.extra);
			if (zipExtraData.Find(1))
			{
				this.forceZip64_ = true;
				if (zipExtraData.ValueLength < 4)
				{
					throw new ZipException("Extra data extended Zip64 information length is invalid");
				}
				if (this.size == (ulong)-1)
				{
					this.size = (ulong)zipExtraData.ReadLong();
				}
				if (this.compressedSize == (ulong)-1)
				{
					this.compressedSize = (ulong)zipExtraData.ReadLong();
				}
				if (!localHeader && this.offset == (long)((ulong)-1))
				{
					this.offset = zipExtraData.ReadLong();
				}
			}
			else if ((this.versionToExtract & 255) >= 45 && (this.size == (ulong)-1 || this.compressedSize == (ulong)-1))
			{
				throw new ZipException("Zip64 Extended information required but is missing.");
			}
			this.DateTime = (this.GetDateTime(zipExtraData) ?? this.DateTime);
			if (this.method == CompressionMethod.WinZipAES)
			{
				this.ProcessAESExtraData(zipExtraData);
			}
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x00141C28 File Offset: 0x0013FE28
		private DateTime? GetDateTime(ZipExtraData extraData)
		{
			ExtendedUnixData data = extraData.GetData<ExtendedUnixData>();
			if (data != null && data.Include.HasFlag(ExtendedUnixData.Flags.ModificationTime))
			{
				return new DateTime?(data.ModificationTime);
			}
			return null;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x00141C6C File Offset: 0x0013FE6C
		private void ProcessAESExtraData(ZipExtraData extraData)
		{
			if (!extraData.Find(39169))
			{
				throw new ZipException("AES Extra Data missing");
			}
			this.versionToExtract = 51;
			int valueLength = extraData.ValueLength;
			if (valueLength < 7)
			{
				throw new ZipException("AES Extra Data Length " + valueLength + " invalid.");
			}
			int aesVer = extraData.ReadShort();
			extraData.ReadShort();
			int aesEncryptionStrength = extraData.ReadByte();
			int num = extraData.ReadShort();
			this._aesVer = aesVer;
			this._aesEncryptionStrength = aesEncryptionStrength;
			this.method = (CompressionMethod)num;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06002A9D RID: 10909 RVA: 0x00141CF0 File Offset: 0x0013FEF0
		// (set) Token: 0x06002A9E RID: 10910 RVA: 0x00141CF8 File Offset: 0x0013FEF8
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				if (value != null && value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value", "cannot exceed 65535");
				}
				this.comment = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x00141D24 File Offset: 0x0013FF24
		public bool IsDirectory
		{
			get
			{
				int length = this.name.Length;
				return (length > 0 && (this.name[length - 1] == '/' || this.name[length - 1] == '\\')) || this.HasDosAttributes(16);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x00141D6F File Offset: 0x0013FF6F
		public bool IsFile
		{
			get
			{
				return !this.IsDirectory && !this.HasDosAttributes(8);
			}
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x00141D85 File Offset: 0x0013FF85
		public bool IsCompressionMethodSupported()
		{
			return ZipEntry.IsCompressionMethodSupported(this.CompressionMethod);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x00141D94 File Offset: 0x0013FF94
		public object Clone()
		{
			ZipEntry zipEntry = (ZipEntry)base.MemberwiseClone();
			if (this.extra != null)
			{
				zipEntry.extra = new byte[this.extra.Length];
				Array.Copy(this.extra, 0, zipEntry.extra, 0, this.extra.Length);
			}
			return zipEntry;
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x00141910 File Offset: 0x0013FB10
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x00141DE4 File Offset: 0x0013FFE4
		public static bool IsCompressionMethodSupported(CompressionMethod method)
		{
			return method == CompressionMethod.Deflated || method == CompressionMethod.Stored || method == CompressionMethod.BZip2;
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x00141DF4 File Offset: 0x0013FFF4
		public static string CleanName(string name)
		{
			if (name == null)
			{
				return string.Empty;
			}
			if (Path.IsPathRooted(name))
			{
				name = name.Substring(Path.GetPathRoot(name).Length);
			}
			name = name.Replace("\\", "/");
			while (name.Length > 0 && name[0] == '/')
			{
				name = name.Remove(0, 1);
			}
			return name;
		}

		// Token: 0x040026D7 RID: 9943
		private ZipEntry.Known known;

		// Token: 0x040026D8 RID: 9944
		private int externalFileAttributes;

		// Token: 0x040026D9 RID: 9945
		private ushort versionMadeBy;

		// Token: 0x040026DA RID: 9946
		private string name;

		// Token: 0x040026DB RID: 9947
		private ulong size;

		// Token: 0x040026DC RID: 9948
		private ulong compressedSize;

		// Token: 0x040026DD RID: 9949
		private ushort versionToExtract;

		// Token: 0x040026DE RID: 9950
		private uint crc;

		// Token: 0x040026DF RID: 9951
		private DateTime dateTime;

		// Token: 0x040026E0 RID: 9952
		private CompressionMethod method;

		// Token: 0x040026E1 RID: 9953
		private byte[] extra;

		// Token: 0x040026E2 RID: 9954
		private string comment;

		// Token: 0x040026E3 RID: 9955
		private int flags;

		// Token: 0x040026E4 RID: 9956
		private long zipFileIndex;

		// Token: 0x040026E5 RID: 9957
		private long offset;

		// Token: 0x040026E6 RID: 9958
		private bool forceZip64_;

		// Token: 0x040026E7 RID: 9959
		private byte cryptoCheckValue_;

		// Token: 0x040026E8 RID: 9960
		private int _aesVer;

		// Token: 0x040026E9 RID: 9961
		private int _aesEncryptionStrength;

		// Token: 0x02001480 RID: 5248
		[Flags]
		private enum Known : byte
		{
			// Token: 0x04006C37 RID: 27703
			None = 0,
			// Token: 0x04006C38 RID: 27704
			Size = 1,
			// Token: 0x04006C39 RID: 27705
			CompressedSize = 2,
			// Token: 0x04006C3A RID: 27706
			Crc = 4,
			// Token: 0x04006C3B RID: 27707
			Time = 8,
			// Token: 0x04006C3C RID: 27708
			ExternalAttributes = 16
		}
	}
}
