using System;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000566 RID: 1382
	public class TarHeader
	{
		// Token: 0x06002D4C RID: 11596 RVA: 0x0014DF94 File Offset: 0x0014C194
		public TarHeader()
		{
			this.Magic = "ustar";
			this.Version = " ";
			this.Name = "";
			this.LinkName = "";
			this.UserId = TarHeader.defaultUserId;
			this.GroupId = TarHeader.defaultGroupId;
			this.UserName = TarHeader.defaultUser;
			this.GroupName = TarHeader.defaultGroupName;
			this.Size = 0L;
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x0014E007 File Offset: 0x0014C207
		// (set) Token: 0x06002D4E RID: 11598 RVA: 0x0014E00F File Offset: 0x0014C20F
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x0014E007 File Offset: 0x0014C207
		[Obsolete("Use the Name property instead", true)]
		public string GetName()
		{
			return this.name;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x0014E026 File Offset: 0x0014C226
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x0014E02E File Offset: 0x0014C22E
		public int Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x0014E037 File Offset: 0x0014C237
		// (set) Token: 0x06002D53 RID: 11603 RVA: 0x0014E03F File Offset: 0x0014C23F
		public int UserId
		{
			get
			{
				return this.userId;
			}
			set
			{
				this.userId = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x0014E048 File Offset: 0x0014C248
		// (set) Token: 0x06002D55 RID: 11605 RVA: 0x0014E050 File Offset: 0x0014C250
		public int GroupId
		{
			get
			{
				return this.groupId;
			}
			set
			{
				this.groupId = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x0014E059 File Offset: 0x0014C259
		// (set) Token: 0x06002D57 RID: 11607 RVA: 0x0014E061 File Offset: 0x0014C261
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot be less than zero");
				}
				this.size = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x0014E07F File Offset: 0x0014C27F
		// (set) Token: 0x06002D59 RID: 11609 RVA: 0x0014E088 File Offset: 0x0014C288
		public DateTime ModTime
		{
			get
			{
				return this.modTime;
			}
			set
			{
				if (value < TarHeader.dateTime1970)
				{
					throw new ArgumentOutOfRangeException("value", "ModTime cannot be before Jan 1st 1970");
				}
				this.modTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x0014E0E7 File Offset: 0x0014C2E7
		public int Checksum
		{
			get
			{
				return this.checksum;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x0014E0EF File Offset: 0x0014C2EF
		public bool IsChecksumValid
		{
			get
			{
				return this.isChecksumValid;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x0014E0F7 File Offset: 0x0014C2F7
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x0014E0FF File Offset: 0x0014C2FF
		public byte TypeFlag
		{
			get
			{
				return this.typeFlag;
			}
			set
			{
				this.typeFlag = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x0014E108 File Offset: 0x0014C308
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x0014E110 File Offset: 0x0014C310
		public string LinkName
		{
			get
			{
				return this.linkName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.linkName = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x0014E127 File Offset: 0x0014C327
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x0014E12F File Offset: 0x0014C32F
		public string Magic
		{
			get
			{
				return this.magic;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.magic = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x0014E146 File Offset: 0x0014C346
		// (set) Token: 0x06002D63 RID: 11619 RVA: 0x0014E14E File Offset: 0x0014C34E
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.version = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x0014E165 File Offset: 0x0014C365
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x0014E170 File Offset: 0x0014C370
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				if (value != null)
				{
					this.userName = value.Substring(0, Math.Min(32, value.Length));
					return;
				}
				string text = "user";
				if (text.Length > 32)
				{
					text = text.Substring(0, 32);
				}
				this.userName = text;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x0014E1BC File Offset: 0x0014C3BC
		// (set) Token: 0x06002D67 RID: 11623 RVA: 0x0014E1C4 File Offset: 0x0014C3C4
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
			set
			{
				if (value == null)
				{
					this.groupName = "None";
					return;
				}
				this.groupName = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x0014E1DC File Offset: 0x0014C3DC
		// (set) Token: 0x06002D69 RID: 11625 RVA: 0x0014E1E4 File Offset: 0x0014C3E4
		public int DevMajor
		{
			get
			{
				return this.devMajor;
			}
			set
			{
				this.devMajor = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x0014E1ED File Offset: 0x0014C3ED
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x0014E1F5 File Offset: 0x0014C3F5
		public int DevMinor
		{
			get
			{
				return this.devMinor;
			}
			set
			{
				this.devMinor = value;
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x0014E1FE File Offset: 0x0014C3FE
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x0014E208 File Offset: 0x0014C408
		public void ParseBuffer(byte[] header, Encoding nameEncoding)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			int num = 0;
			this.name = TarHeader.ParseName(header, num, 100, nameEncoding).ToString();
			num += 100;
			this.mode = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.UserId = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.GroupId = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.Size = TarHeader.ParseBinaryOrOctal(header, num, 12);
			num += 12;
			this.ModTime = TarHeader.GetDateTimeFromCTime(TarHeader.ParseOctal(header, num, 12));
			num += 12;
			this.checksum = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.TypeFlag = header[num++];
			this.LinkName = TarHeader.ParseName(header, num, 100, nameEncoding).ToString();
			num += 100;
			this.Magic = TarHeader.ParseName(header, num, 6, nameEncoding).ToString();
			num += 6;
			if (this.Magic == "ustar")
			{
				this.Version = TarHeader.ParseName(header, num, 2, nameEncoding).ToString();
				num += 2;
				this.UserName = TarHeader.ParseName(header, num, 32, nameEncoding).ToString();
				num += 32;
				this.GroupName = TarHeader.ParseName(header, num, 32, nameEncoding).ToString();
				num += 32;
				this.DevMajor = (int)TarHeader.ParseOctal(header, num, 8);
				num += 8;
				this.DevMinor = (int)TarHeader.ParseOctal(header, num, 8);
				num += 8;
				string text = TarHeader.ParseName(header, num, 155, nameEncoding).ToString();
				if (!string.IsNullOrEmpty(text))
				{
					this.Name = text + "/" + this.Name;
				}
			}
			this.isChecksumValid = (this.Checksum == TarHeader.MakeCheckSum(header));
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x0014E3C4 File Offset: 0x0014C5C4
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public void ParseBuffer(byte[] header)
		{
			this.ParseBuffer(header, null);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x0014E3CE File Offset: 0x0014C5CE
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public void WriteHeader(byte[] outBuffer)
		{
			this.WriteHeader(outBuffer, null);
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x0014E3D8 File Offset: 0x0014C5D8
		public void WriteHeader(byte[] outBuffer, Encoding nameEncoding)
		{
			if (outBuffer == null)
			{
				throw new ArgumentNullException("outBuffer");
			}
			int i = 0;
			i = TarHeader.GetNameBytes(this.Name, outBuffer, i, 100, nameEncoding);
			i = TarHeader.GetOctalBytes((long)this.mode, outBuffer, i, 8);
			i = TarHeader.GetOctalBytes((long)this.UserId, outBuffer, i, 8);
			i = TarHeader.GetOctalBytes((long)this.GroupId, outBuffer, i, 8);
			i = TarHeader.GetBinaryOrOctalBytes(this.Size, outBuffer, i, 12);
			i = TarHeader.GetOctalBytes((long)TarHeader.GetCTime(this.ModTime), outBuffer, i, 12);
			int offset = i;
			for (int j = 0; j < 8; j++)
			{
				outBuffer[i++] = 32;
			}
			outBuffer[i++] = this.TypeFlag;
			i = TarHeader.GetNameBytes(this.LinkName, outBuffer, i, 100, nameEncoding);
			i = TarHeader.GetAsciiBytes(this.Magic, 0, outBuffer, i, 6, nameEncoding);
			i = TarHeader.GetNameBytes(this.Version, outBuffer, i, 2, nameEncoding);
			i = TarHeader.GetNameBytes(this.UserName, outBuffer, i, 32, nameEncoding);
			i = TarHeader.GetNameBytes(this.GroupName, outBuffer, i, 32, nameEncoding);
			if (this.TypeFlag == 51 || this.TypeFlag == 52)
			{
				i = TarHeader.GetOctalBytes((long)this.DevMajor, outBuffer, i, 8);
				i = TarHeader.GetOctalBytes((long)this.DevMinor, outBuffer, i, 8);
			}
			while (i < outBuffer.Length)
			{
				outBuffer[i++] = 0;
			}
			this.checksum = TarHeader.ComputeCheckSum(outBuffer);
			TarHeader.GetCheckSumOctalBytes((long)this.checksum, outBuffer, offset, 8);
			this.isChecksumValid = true;
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x0014E53A File Offset: 0x0014C73A
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x0014E548 File Offset: 0x0014C748
		public override bool Equals(object obj)
		{
			TarHeader tarHeader = obj as TarHeader;
			return tarHeader != null && (this.name == tarHeader.name && this.mode == tarHeader.mode && this.UserId == tarHeader.UserId && this.GroupId == tarHeader.GroupId && this.Size == tarHeader.Size && this.ModTime == tarHeader.ModTime && this.Checksum == tarHeader.Checksum && this.TypeFlag == tarHeader.TypeFlag && this.LinkName == tarHeader.LinkName && this.Magic == tarHeader.Magic && this.Version == tarHeader.Version && this.UserName == tarHeader.UserName && this.GroupName == tarHeader.GroupName && this.DevMajor == tarHeader.DevMajor) && this.DevMinor == tarHeader.DevMinor;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x0014E675 File Offset: 0x0014C875
		internal static void SetValueDefaults(int userId, string userName, int groupId, string groupName)
		{
			TarHeader.userIdAsSet = userId;
			TarHeader.defaultUserId = userId;
			TarHeader.userNameAsSet = userName;
			TarHeader.defaultUser = userName;
			TarHeader.groupIdAsSet = groupId;
			TarHeader.defaultGroupId = groupId;
			TarHeader.groupNameAsSet = groupName;
			TarHeader.defaultGroupName = groupName;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x0014E6A7 File Offset: 0x0014C8A7
		internal static void RestoreSetValues()
		{
			TarHeader.defaultUserId = TarHeader.userIdAsSet;
			TarHeader.defaultUser = TarHeader.userNameAsSet;
			TarHeader.defaultGroupId = TarHeader.groupIdAsSet;
			TarHeader.defaultGroupName = TarHeader.groupNameAsSet;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x0014E6D4 File Offset: 0x0014C8D4
		private static long ParseBinaryOrOctal(byte[] header, int offset, int length)
		{
			if (header[offset] >= 128)
			{
				long num = 0L;
				for (int i = length - 8; i < length; i++)
				{
					num = (num << 8 | (long)((ulong)header[offset + i]));
				}
				return num;
			}
			return TarHeader.ParseOctal(header, offset, length);
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x0014E714 File Offset: 0x0014C914
		public static long ParseOctal(byte[] header, int offset, int length)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			long num = 0L;
			bool flag = true;
			int num2 = offset + length;
			int num3 = offset;
			while (num3 < num2 && header[num3] != 0)
			{
				if (header[num3] != 32 && header[num3] != 48)
				{
					goto IL_38;
				}
				if (!flag)
				{
					if (header[num3] != 32)
					{
						goto IL_38;
					}
					break;
				}
				IL_46:
				num3++;
				continue;
				IL_38:
				flag = false;
				num = (num << 3) + (long)(header[num3] - 48);
				goto IL_46;
			}
			return num;
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x0014E770 File Offset: 0x0014C970
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static StringBuilder ParseName(byte[] header, int offset, int length)
		{
			return TarHeader.ParseName(header, offset, length, null);
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x0014E77C File Offset: 0x0014C97C
		public static StringBuilder ParseName(byte[] header, int offset, int length, Encoding encoding)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be less than zero");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Cannot be less than zero");
			}
			if (offset + length > header.Length)
			{
				throw new ArgumentException("Exceeds header size", "length");
			}
			StringBuilder stringBuilder = new StringBuilder(length);
			int num = 0;
			if (encoding == null)
			{
				for (int i = offset; i < offset + length; i++)
				{
					if (header[i] == 0)
					{
						break;
					}
					stringBuilder.Append((char)header[i]);
				}
			}
			else
			{
				int num2 = offset;
				while (num2 < offset + length && header[num2] != 0)
				{
					num2++;
					num++;
				}
				stringBuilder.Append(encoding.GetString(header, offset, num));
			}
			return stringBuilder;
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x0014E82A File Offset: 0x0014CA2A
		public static int GetNameBytes(StringBuilder name, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			return TarHeader.GetNameBytes(name.ToString(), nameOffset, buffer, bufferOffset, length, null);
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x0014E83D File Offset: 0x0014CA3D
		public static int GetNameBytes(string name, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			return TarHeader.GetNameBytes(name, nameOffset, buffer, bufferOffset, length, null);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x0014E84C File Offset: 0x0014CA4C
		public static int GetNameBytes(string name, int nameOffset, byte[] buffer, int bufferOffset, int length, Encoding encoding)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int i;
			if (encoding != null)
			{
				char[] array = name.ToCharArray(nameOffset, Math.Min(name.Length - nameOffset, length));
				byte[] bytes = encoding.GetBytes(array, 0, array.Length);
				i = Math.Min(bytes.Length, length);
				Array.Copy(bytes, 0, buffer, bufferOffset, i);
			}
			else
			{
				for (i = 0; i < length; i++)
				{
					if (nameOffset + i >= name.Length)
					{
						break;
					}
					buffer[bufferOffset + i] = (byte)name[nameOffset + i];
				}
			}
			while (i < length)
			{
				buffer[bufferOffset + i] = 0;
				i++;
			}
			return bufferOffset + length;
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x0014E8EE File Offset: 0x0014CAEE
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length)
		{
			return TarHeader.GetNameBytes(name, buffer, offset, length, null);
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x0014E8FA File Offset: 0x0014CAFA
		public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length, Encoding encoding)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return TarHeader.GetNameBytes(name.ToString(), 0, buffer, offset, length, encoding);
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x0014E929 File Offset: 0x0014CB29
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static int GetNameBytes(string name, byte[] buffer, int offset, int length)
		{
			return TarHeader.GetNameBytes(name, buffer, offset, length, null);
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x0014E935 File Offset: 0x0014CB35
		public static int GetNameBytes(string name, byte[] buffer, int offset, int length, Encoding encoding)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return TarHeader.GetNameBytes(name, 0, buffer, offset, length, encoding);
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x0014E95F File Offset: 0x0014CB5F
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static int GetAsciiBytes(string toAdd, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			return TarHeader.GetAsciiBytes(toAdd, nameOffset, buffer, bufferOffset, length, null);
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x0014E970 File Offset: 0x0014CB70
		public static int GetAsciiBytes(string toAdd, int nameOffset, byte[] buffer, int bufferOffset, int length, Encoding encoding)
		{
			if (toAdd == null)
			{
				throw new ArgumentNullException("toAdd");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int i;
			if (encoding == null)
			{
				for (i = 0; i < length; i++)
				{
					if (nameOffset + i >= toAdd.Length)
					{
						break;
					}
					buffer[bufferOffset + i] = (byte)toAdd[nameOffset + i];
				}
			}
			else
			{
				char[] chars = toAdd.ToCharArray();
				byte[] bytes = encoding.GetBytes(chars, nameOffset, Math.Min(toAdd.Length - nameOffset, length));
				i = Math.Min(bytes.Length, length);
				Array.Copy(bytes, 0, buffer, bufferOffset, i);
			}
			while (i < length)
			{
				buffer[bufferOffset + i] = 0;
				i++;
			}
			return bufferOffset + length;
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x0014EA10 File Offset: 0x0014CC10
		public static int GetOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int i = length - 1;
			buffer[offset + i] = 0;
			i--;
			if (value > 0L)
			{
				long num = value;
				while (i >= 0)
				{
					if (num <= 0L)
					{
						break;
					}
					buffer[offset + i] = 48 + (byte)(num & 7L);
					num >>= 3;
					i--;
				}
			}
			while (i >= 0)
			{
				buffer[offset + i] = 48;
				i--;
			}
			return offset + length;
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x0014EA78 File Offset: 0x0014CC78
		private static int GetBinaryOrOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			if (value > 8589934591L)
			{
				for (int i = length - 1; i > 0; i--)
				{
					buffer[offset + i] = (byte)value;
					value >>= 8;
				}
				buffer[offset] = 128;
				return offset + length;
			}
			return TarHeader.GetOctalBytes(value, buffer, offset, length);
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x0014EAC0 File Offset: 0x0014CCC0
		private static void GetCheckSumOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			TarHeader.GetOctalBytes(value, buffer, offset, length - 1);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x0014EAD0 File Offset: 0x0014CCD0
		private static int ComputeCheckSum(byte[] buffer)
		{
			int num = 0;
			for (int i = 0; i < buffer.Length; i++)
			{
				num += (int)buffer[i];
			}
			return num;
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x0014EAF4 File Offset: 0x0014CCF4
		private static int MakeCheckSum(byte[] buffer)
		{
			int num = 0;
			for (int i = 0; i < 148; i++)
			{
				num += (int)buffer[i];
			}
			for (int j = 0; j < 8; j++)
			{
				num += 32;
			}
			for (int k = 156; k < buffer.Length; k++)
			{
				num += (int)buffer[k];
			}
			return num;
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0014EB44 File Offset: 0x0014CD44
		private static int GetCTime(DateTime dateTime)
		{
			return (int)((dateTime.Ticks - TarHeader.dateTime1970.Ticks) / 10000000L);
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x0014EB70 File Offset: 0x0014CD70
		private static DateTime GetDateTimeFromCTime(long ticks)
		{
			DateTime result;
			try
			{
				result = new DateTime(TarHeader.dateTime1970.Ticks + ticks * 10000000L);
			}
			catch (ArgumentOutOfRangeException)
			{
				result = TarHeader.dateTime1970;
			}
			return result;
		}

		// Token: 0x04002836 RID: 10294
		public const int NAMELEN = 100;

		// Token: 0x04002837 RID: 10295
		public const int MODELEN = 8;

		// Token: 0x04002838 RID: 10296
		public const int UIDLEN = 8;

		// Token: 0x04002839 RID: 10297
		public const int GIDLEN = 8;

		// Token: 0x0400283A RID: 10298
		public const int CHKSUMLEN = 8;

		// Token: 0x0400283B RID: 10299
		public const int CHKSUMOFS = 148;

		// Token: 0x0400283C RID: 10300
		public const int SIZELEN = 12;

		// Token: 0x0400283D RID: 10301
		public const int MAGICLEN = 6;

		// Token: 0x0400283E RID: 10302
		public const int VERSIONLEN = 2;

		// Token: 0x0400283F RID: 10303
		public const int MODTIMELEN = 12;

		// Token: 0x04002840 RID: 10304
		public const int UNAMELEN = 32;

		// Token: 0x04002841 RID: 10305
		public const int GNAMELEN = 32;

		// Token: 0x04002842 RID: 10306
		public const int DEVLEN = 8;

		// Token: 0x04002843 RID: 10307
		public const int PREFIXLEN = 155;

		// Token: 0x04002844 RID: 10308
		public const byte LF_OLDNORM = 0;

		// Token: 0x04002845 RID: 10309
		public const byte LF_NORMAL = 48;

		// Token: 0x04002846 RID: 10310
		public const byte LF_LINK = 49;

		// Token: 0x04002847 RID: 10311
		public const byte LF_SYMLINK = 50;

		// Token: 0x04002848 RID: 10312
		public const byte LF_CHR = 51;

		// Token: 0x04002849 RID: 10313
		public const byte LF_BLK = 52;

		// Token: 0x0400284A RID: 10314
		public const byte LF_DIR = 53;

		// Token: 0x0400284B RID: 10315
		public const byte LF_FIFO = 54;

		// Token: 0x0400284C RID: 10316
		public const byte LF_CONTIG = 55;

		// Token: 0x0400284D RID: 10317
		public const byte LF_GHDR = 103;

		// Token: 0x0400284E RID: 10318
		public const byte LF_XHDR = 120;

		// Token: 0x0400284F RID: 10319
		public const byte LF_ACL = 65;

		// Token: 0x04002850 RID: 10320
		public const byte LF_GNU_DUMPDIR = 68;

		// Token: 0x04002851 RID: 10321
		public const byte LF_EXTATTR = 69;

		// Token: 0x04002852 RID: 10322
		public const byte LF_META = 73;

		// Token: 0x04002853 RID: 10323
		public const byte LF_GNU_LONGLINK = 75;

		// Token: 0x04002854 RID: 10324
		public const byte LF_GNU_LONGNAME = 76;

		// Token: 0x04002855 RID: 10325
		public const byte LF_GNU_MULTIVOL = 77;

		// Token: 0x04002856 RID: 10326
		public const byte LF_GNU_NAMES = 78;

		// Token: 0x04002857 RID: 10327
		public const byte LF_GNU_SPARSE = 83;

		// Token: 0x04002858 RID: 10328
		public const byte LF_GNU_VOLHDR = 86;

		// Token: 0x04002859 RID: 10329
		public const string TMAGIC = "ustar";

		// Token: 0x0400285A RID: 10330
		public const string GNU_TMAGIC = "ustar  ";

		// Token: 0x0400285B RID: 10331
		private const long timeConversionFactor = 10000000L;

		// Token: 0x0400285C RID: 10332
		private static readonly DateTime dateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		// Token: 0x0400285D RID: 10333
		private string name;

		// Token: 0x0400285E RID: 10334
		private int mode;

		// Token: 0x0400285F RID: 10335
		private int userId;

		// Token: 0x04002860 RID: 10336
		private int groupId;

		// Token: 0x04002861 RID: 10337
		private long size;

		// Token: 0x04002862 RID: 10338
		private DateTime modTime;

		// Token: 0x04002863 RID: 10339
		private int checksum;

		// Token: 0x04002864 RID: 10340
		private bool isChecksumValid;

		// Token: 0x04002865 RID: 10341
		private byte typeFlag;

		// Token: 0x04002866 RID: 10342
		private string linkName;

		// Token: 0x04002867 RID: 10343
		private string magic;

		// Token: 0x04002868 RID: 10344
		private string version;

		// Token: 0x04002869 RID: 10345
		private string userName;

		// Token: 0x0400286A RID: 10346
		private string groupName;

		// Token: 0x0400286B RID: 10347
		private int devMajor;

		// Token: 0x0400286C RID: 10348
		private int devMinor;

		// Token: 0x0400286D RID: 10349
		internal static int userIdAsSet;

		// Token: 0x0400286E RID: 10350
		internal static int groupIdAsSet;

		// Token: 0x0400286F RID: 10351
		internal static string userNameAsSet;

		// Token: 0x04002870 RID: 10352
		internal static string groupNameAsSet = "None";

		// Token: 0x04002871 RID: 10353
		internal static int defaultUserId;

		// Token: 0x04002872 RID: 10354
		internal static int defaultGroupId;

		// Token: 0x04002873 RID: 10355
		internal static string defaultGroupName = "None";

		// Token: 0x04002874 RID: 10356
		internal static string defaultUser;
	}
}
