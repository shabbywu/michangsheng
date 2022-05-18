using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007C6 RID: 1990
	public class ZipEntry
	{
		// Token: 0x0600327C RID: 12924 RVA: 0x00024C0E File Offset: 0x00022E0E
		public ZipEntry(string name) : this(name, 0, 51, CompressionMethod.Deflated)
		{
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x00024C1B File Offset: 0x00022E1B
		internal ZipEntry(string name, int versionRequiredToExtract) : this(name, versionRequiredToExtract, 51, CompressionMethod.Deflated)
		{
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x0018E0C0 File Offset: 0x0018C2C0
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

		// Token: 0x0600327F RID: 12927 RVA: 0x0018E15C File Offset: 0x0018C35C
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

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x00024C28 File Offset: 0x00022E28
		public bool HasCrc
		{
			get
			{
				return (this.known & ZipEntry.Known.Crc) > ZipEntry.Known.None;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x00024C35 File Offset: 0x00022E35
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x00024C42 File Offset: 0x00022E42
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

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x00024C65 File Offset: 0x00022E65
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x00024C76 File Offset: 0x00022E76
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

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x00024CA0 File Offset: 0x00022EA0
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x00024CA8 File Offset: 0x00022EA8
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

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x00024CB1 File Offset: 0x00022EB1
		// (set) Token: 0x06003288 RID: 12936 RVA: 0x00024CB9 File Offset: 0x00022EB9
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

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x00024CC2 File Offset: 0x00022EC2
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x00024CCA File Offset: 0x00022ECA
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

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x00024CD3 File Offset: 0x00022ED3
		// (set) Token: 0x0600328C RID: 12940 RVA: 0x00024CDB File Offset: 0x00022EDB
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

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x00024CE4 File Offset: 0x00022EE4
		// (set) Token: 0x0600328E RID: 12942 RVA: 0x00024CF9 File Offset: 0x00022EF9
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

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x00024D11 File Offset: 0x00022F11
		public int VersionMadeBy
		{
			get
			{
				return (int)(this.versionMadeBy & 255);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x00024D1F File Offset: 0x00022F1F
		public bool IsDOSEntry
		{
			get
			{
				return this.HostSystem == 0 || this.HostSystem == 10;
			}
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x0018E280 File Offset: 0x0018C480
		private bool HasDosAttributes(int attributes)
		{
			bool flag = false;
			if ((this.known & ZipEntry.Known.ExternalAttributes) != ZipEntry.Known.None)
			{
				flag |= ((this.HostSystem == 0 || this.HostSystem == 10) && (this.ExternalFileAttributes & attributes) == attributes);
			}
			return flag;
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x00024D35 File Offset: 0x00022F35
		// (set) Token: 0x06003293 RID: 12947 RVA: 0x00024D45 File Offset: 0x00022F45
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

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x0018E2C0 File Offset: 0x0018C4C0
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

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x0018E344 File Offset: 0x0018C544
		public bool CanDecompress
		{
			get
			{
				return this.Version <= 51 && (this.Version == 10 || this.Version == 11 || this.Version == 20 || this.Version == 45 || this.Version == 46 || this.Version == 51) && this.IsCompressionMethodSupported();
			}
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x00024D72 File Offset: 0x00022F72
		public void ForceZip64()
		{
			this.forceZip64_ = true;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x00024D7B File Offset: 0x00022F7B
		public bool IsZip64Forced()
		{
			return this.forceZip64_;
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x0018E3A0 File Offset: 0x0018C5A0
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

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x00024D83 File Offset: 0x00022F83
		public bool CentralHeaderRequiresZip64
		{
			get
			{
				return this.LocalHeaderRequiresZip64 || this.offset >= (long)((ulong)-1);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x0018E404 File Offset: 0x0018C604
		// (set) Token: 0x0600329B RID: 12955 RVA: 0x0018E4E4 File Offset: 0x0018C6E4
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

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x00024D9C File Offset: 0x00022F9C
		// (set) Token: 0x0600329D RID: 12957 RVA: 0x00024DA4 File Offset: 0x00022FA4
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

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x00024DBB File Offset: 0x00022FBB
		// (set) Token: 0x0600329F RID: 12959 RVA: 0x00024DC3 File Offset: 0x00022FC3
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

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x00024DCC File Offset: 0x00022FCC
		// (set) Token: 0x060032A1 RID: 12961 RVA: 0x00024DE1 File Offset: 0x00022FE1
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

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x00024DF8 File Offset: 0x00022FF8
		// (set) Token: 0x060032A3 RID: 12963 RVA: 0x00024E0D File Offset: 0x0002300D
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

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x00024E24 File Offset: 0x00023024
		// (set) Token: 0x060032A5 RID: 12965 RVA: 0x00024E3D File Offset: 0x0002303D
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x00024E73 File Offset: 0x00023073
		// (set) Token: 0x060032A7 RID: 12967 RVA: 0x00024E7B File Offset: 0x0002307B
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x00024E97 File Offset: 0x00023097
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

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x00024EAB File Offset: 0x000230AB
		// (set) Token: 0x060032AA RID: 12970 RVA: 0x0018E57C File Offset: 0x0018C77C
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

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x0018E5C8 File Offset: 0x0018C7C8
		// (set) Token: 0x060032AC RID: 12972 RVA: 0x0018E624 File Offset: 0x0018C824
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

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x00024EB3 File Offset: 0x000230B3
		internal byte AESEncryptionStrength
		{
			get
			{
				return (byte)this._aesEncryptionStrength;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060032AE RID: 12974 RVA: 0x00024EBC File Offset: 0x000230BC
		internal int AESSaltLen
		{
			get
			{
				return this.AESKeySize / 16;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x00024EC7 File Offset: 0x000230C7
		internal int AESOverheadSize
		{
			get
			{
				return 12 + this.AESSaltLen;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060032B0 RID: 12976 RVA: 0x00024ED2 File Offset: 0x000230D2
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

		// Token: 0x060032B1 RID: 12977 RVA: 0x0018E674 File Offset: 0x0018C874
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

		// Token: 0x060032B2 RID: 12978 RVA: 0x0018E760 File Offset: 0x0018C960
		private DateTime? GetDateTime(ZipExtraData extraData)
		{
			ExtendedUnixData data = extraData.GetData<ExtendedUnixData>();
			if (data != null && data.Include.HasFlag(ExtendedUnixData.Flags.ModificationTime))
			{
				return new DateTime?(data.ModificationTime);
			}
			return null;
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x0018E7A4 File Offset: 0x0018C9A4
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

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x00024EEF File Offset: 0x000230EF
		// (set) Token: 0x060032B5 RID: 12981 RVA: 0x00024EF7 File Offset: 0x000230F7
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

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x0018E828 File Offset: 0x0018CA28
		public bool IsDirectory
		{
			get
			{
				int length = this.name.Length;
				return (length > 0 && (this.name[length - 1] == '/' || this.name[length - 1] == '\\')) || this.HasDosAttributes(16);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x00024F20 File Offset: 0x00023120
		public bool IsFile
		{
			get
			{
				return !this.IsDirectory && !this.HasDosAttributes(8);
			}
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x00024F36 File Offset: 0x00023136
		public bool IsCompressionMethodSupported()
		{
			return ZipEntry.IsCompressionMethodSupported(this.CompressionMethod);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x0018E874 File Offset: 0x0018CA74
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

		// Token: 0x060032BA RID: 12986 RVA: 0x00024DBB File Offset: 0x00022FBB
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x00024F43 File Offset: 0x00023143
		public static bool IsCompressionMethodSupported(CompressionMethod method)
		{
			return method == CompressionMethod.Deflated || method == CompressionMethod.Stored || method == CompressionMethod.BZip2;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x0018E8C4 File Offset: 0x0018CAC4
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

		// Token: 0x04002ECB RID: 11979
		private ZipEntry.Known known;

		// Token: 0x04002ECC RID: 11980
		private int externalFileAttributes;

		// Token: 0x04002ECD RID: 11981
		private ushort versionMadeBy;

		// Token: 0x04002ECE RID: 11982
		private string name;

		// Token: 0x04002ECF RID: 11983
		private ulong size;

		// Token: 0x04002ED0 RID: 11984
		private ulong compressedSize;

		// Token: 0x04002ED1 RID: 11985
		private ushort versionToExtract;

		// Token: 0x04002ED2 RID: 11986
		private uint crc;

		// Token: 0x04002ED3 RID: 11987
		private DateTime dateTime;

		// Token: 0x04002ED4 RID: 11988
		private CompressionMethod method;

		// Token: 0x04002ED5 RID: 11989
		private byte[] extra;

		// Token: 0x04002ED6 RID: 11990
		private string comment;

		// Token: 0x04002ED7 RID: 11991
		private int flags;

		// Token: 0x04002ED8 RID: 11992
		private long zipFileIndex;

		// Token: 0x04002ED9 RID: 11993
		private long offset;

		// Token: 0x04002EDA RID: 11994
		private bool forceZip64_;

		// Token: 0x04002EDB RID: 11995
		private byte cryptoCheckValue_;

		// Token: 0x04002EDC RID: 11996
		private int _aesVer;

		// Token: 0x04002EDD RID: 11997
		private int _aesEncryptionStrength;

		// Token: 0x020007C7 RID: 1991
		[Flags]
		private enum Known : byte
		{
			// Token: 0x04002EDF RID: 11999
			None = 0,
			// Token: 0x04002EE0 RID: 12000
			Size = 1,
			// Token: 0x04002EE1 RID: 12001
			CompressedSize = 2,
			// Token: 0x04002EE2 RID: 12002
			Crc = 4,
			// Token: 0x04002EE3 RID: 12003
			Time = 8,
			// Token: 0x04002EE4 RID: 12004
			ExternalAttributes = 16
		}
	}
}
