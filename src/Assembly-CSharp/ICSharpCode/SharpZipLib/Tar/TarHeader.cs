using System;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200080C RID: 2060
	public class TarHeader
	{
		// Token: 0x060035BA RID: 13754 RVA: 0x001997AC File Offset: 0x001979AC
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

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x0002743B File Offset: 0x0002563B
		// (set) Token: 0x060035BC RID: 13756 RVA: 0x00027443 File Offset: 0x00025643
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

		// Token: 0x060035BD RID: 13757 RVA: 0x0002743B File Offset: 0x0002563B
		[Obsolete("Use the Name property instead", true)]
		public string GetName()
		{
			return this.name;
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060035BE RID: 13758 RVA: 0x0002745A File Offset: 0x0002565A
		// (set) Token: 0x060035BF RID: 13759 RVA: 0x00027462 File Offset: 0x00025662
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

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x0002746B File Offset: 0x0002566B
		// (set) Token: 0x060035C1 RID: 13761 RVA: 0x00027473 File Offset: 0x00025673
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

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x0002747C File Offset: 0x0002567C
		// (set) Token: 0x060035C3 RID: 13763 RVA: 0x00027484 File Offset: 0x00025684
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

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x0002748D File Offset: 0x0002568D
		// (set) Token: 0x060035C5 RID: 13765 RVA: 0x00027495 File Offset: 0x00025695
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

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000274B3 File Offset: 0x000256B3
		// (set) Token: 0x060035C7 RID: 13767 RVA: 0x00199820 File Offset: 0x00197A20
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

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000274BB File Offset: 0x000256BB
		public int Checksum
		{
			get
			{
				return this.checksum;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x000274C3 File Offset: 0x000256C3
		public bool IsChecksumValid
		{
			get
			{
				return this.isChecksumValid;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x000274CB File Offset: 0x000256CB
		// (set) Token: 0x060035CB RID: 13771 RVA: 0x000274D3 File Offset: 0x000256D3
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

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x000274DC File Offset: 0x000256DC
		// (set) Token: 0x060035CD RID: 13773 RVA: 0x000274E4 File Offset: 0x000256E4
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

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060035CE RID: 13774 RVA: 0x000274FB File Offset: 0x000256FB
		// (set) Token: 0x060035CF RID: 13775 RVA: 0x00027503 File Offset: 0x00025703
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

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x0002751A File Offset: 0x0002571A
		// (set) Token: 0x060035D1 RID: 13777 RVA: 0x00027522 File Offset: 0x00025722
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

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x00027539 File Offset: 0x00025739
		// (set) Token: 0x060035D3 RID: 13779 RVA: 0x00199880 File Offset: 0x00197A80
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

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060035D4 RID: 13780 RVA: 0x00027541 File Offset: 0x00025741
		// (set) Token: 0x060035D5 RID: 13781 RVA: 0x00027549 File Offset: 0x00025749
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

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x00027561 File Offset: 0x00025761
		// (set) Token: 0x060035D7 RID: 13783 RVA: 0x00027569 File Offset: 0x00025769
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

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x00027572 File Offset: 0x00025772
		// (set) Token: 0x060035D9 RID: 13785 RVA: 0x0002757A File Offset: 0x0002577A
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

		// Token: 0x060035DA RID: 13786 RVA: 0x00027583 File Offset: 0x00025783
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x001998CC File Offset: 0x00197ACC
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

		// Token: 0x060035DC RID: 13788 RVA: 0x0002758B File Offset: 0x0002578B
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public void ParseBuffer(byte[] header)
		{
			this.ParseBuffer(header, null);
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x00027595 File Offset: 0x00025795
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public void WriteHeader(byte[] outBuffer)
		{
			this.WriteHeader(outBuffer, null);
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x00199A88 File Offset: 0x00197C88
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

		// Token: 0x060035DF RID: 13791 RVA: 0x0002759F File Offset: 0x0002579F
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x00199BEC File Offset: 0x00197DEC
		public override bool Equals(object obj)
		{
			TarHeader tarHeader = obj as TarHeader;
			return tarHeader != null && (this.name == tarHeader.name && this.mode == tarHeader.mode && this.UserId == tarHeader.UserId && this.GroupId == tarHeader.GroupId && this.Size == tarHeader.Size && this.ModTime == tarHeader.ModTime && this.Checksum == tarHeader.Checksum && this.TypeFlag == tarHeader.TypeFlag && this.LinkName == tarHeader.LinkName && this.Magic == tarHeader.Magic && this.Version == tarHeader.Version && this.UserName == tarHeader.UserName && this.GroupName == tarHeader.GroupName && this.DevMajor == tarHeader.DevMajor) && this.DevMinor == tarHeader.DevMinor;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000275AC File Offset: 0x000257AC
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

		// Token: 0x060035E2 RID: 13794 RVA: 0x000275DE File Offset: 0x000257DE
		internal static void RestoreSetValues()
		{
			TarHeader.defaultUserId = TarHeader.userIdAsSet;
			TarHeader.defaultUser = TarHeader.userNameAsSet;
			TarHeader.defaultGroupId = TarHeader.groupIdAsSet;
			TarHeader.defaultGroupName = TarHeader.groupNameAsSet;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x00199D1C File Offset: 0x00197F1C
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

		// Token: 0x060035E4 RID: 13796 RVA: 0x00199D5C File Offset: 0x00197F5C
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

		// Token: 0x060035E5 RID: 13797 RVA: 0x00027608 File Offset: 0x00025808
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static StringBuilder ParseName(byte[] header, int offset, int length)
		{
			return TarHeader.ParseName(header, offset, length, null);
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x00199DB8 File Offset: 0x00197FB8
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

		// Token: 0x060035E7 RID: 13799 RVA: 0x00027613 File Offset: 0x00025813
		public static int GetNameBytes(StringBuilder name, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			return TarHeader.GetNameBytes(name.ToString(), nameOffset, buffer, bufferOffset, length, null);
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x00027626 File Offset: 0x00025826
		public static int GetNameBytes(string name, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			return TarHeader.GetNameBytes(name, nameOffset, buffer, bufferOffset, length, null);
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x00199E68 File Offset: 0x00198068
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

		// Token: 0x060035EA RID: 13802 RVA: 0x00027634 File Offset: 0x00025834
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length)
		{
			return TarHeader.GetNameBytes(name, buffer, offset, length, null);
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x00027640 File Offset: 0x00025840
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

		// Token: 0x060035EC RID: 13804 RVA: 0x0002766F File Offset: 0x0002586F
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static int GetNameBytes(string name, byte[] buffer, int offset, int length)
		{
			return TarHeader.GetNameBytes(name, buffer, offset, length, null);
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x0002767B File Offset: 0x0002587B
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

		// Token: 0x060035EE RID: 13806 RVA: 0x000276A5 File Offset: 0x000258A5
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static int GetAsciiBytes(string toAdd, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			return TarHeader.GetAsciiBytes(toAdd, nameOffset, buffer, bufferOffset, length, null);
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x00199F0C File Offset: 0x0019810C
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

		// Token: 0x060035F0 RID: 13808 RVA: 0x00199FAC File Offset: 0x001981AC
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

		// Token: 0x060035F1 RID: 13809 RVA: 0x0019A014 File Offset: 0x00198214
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

		// Token: 0x060035F2 RID: 13810 RVA: 0x000276B3 File Offset: 0x000258B3
		private static void GetCheckSumOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			TarHeader.GetOctalBytes(value, buffer, offset, length - 1);
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x0019A05C File Offset: 0x0019825C
		private static int ComputeCheckSum(byte[] buffer)
		{
			int num = 0;
			for (int i = 0; i < buffer.Length; i++)
			{
				num += (int)buffer[i];
			}
			return num;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x0019A080 File Offset: 0x00198280
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

		// Token: 0x060035F5 RID: 13813 RVA: 0x0019A0D0 File Offset: 0x001982D0
		private static int GetCTime(DateTime dateTime)
		{
			return (int)((dateTime.Ticks - TarHeader.dateTime1970.Ticks) / 10000000L);
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x0019A0FC File Offset: 0x001982FC
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

		// Token: 0x0400306F RID: 12399
		public const int NAMELEN = 100;

		// Token: 0x04003070 RID: 12400
		public const int MODELEN = 8;

		// Token: 0x04003071 RID: 12401
		public const int UIDLEN = 8;

		// Token: 0x04003072 RID: 12402
		public const int GIDLEN = 8;

		// Token: 0x04003073 RID: 12403
		public const int CHKSUMLEN = 8;

		// Token: 0x04003074 RID: 12404
		public const int CHKSUMOFS = 148;

		// Token: 0x04003075 RID: 12405
		public const int SIZELEN = 12;

		// Token: 0x04003076 RID: 12406
		public const int MAGICLEN = 6;

		// Token: 0x04003077 RID: 12407
		public const int VERSIONLEN = 2;

		// Token: 0x04003078 RID: 12408
		public const int MODTIMELEN = 12;

		// Token: 0x04003079 RID: 12409
		public const int UNAMELEN = 32;

		// Token: 0x0400307A RID: 12410
		public const int GNAMELEN = 32;

		// Token: 0x0400307B RID: 12411
		public const int DEVLEN = 8;

		// Token: 0x0400307C RID: 12412
		public const int PREFIXLEN = 155;

		// Token: 0x0400307D RID: 12413
		public const byte LF_OLDNORM = 0;

		// Token: 0x0400307E RID: 12414
		public const byte LF_NORMAL = 48;

		// Token: 0x0400307F RID: 12415
		public const byte LF_LINK = 49;

		// Token: 0x04003080 RID: 12416
		public const byte LF_SYMLINK = 50;

		// Token: 0x04003081 RID: 12417
		public const byte LF_CHR = 51;

		// Token: 0x04003082 RID: 12418
		public const byte LF_BLK = 52;

		// Token: 0x04003083 RID: 12419
		public const byte LF_DIR = 53;

		// Token: 0x04003084 RID: 12420
		public const byte LF_FIFO = 54;

		// Token: 0x04003085 RID: 12421
		public const byte LF_CONTIG = 55;

		// Token: 0x04003086 RID: 12422
		public const byte LF_GHDR = 103;

		// Token: 0x04003087 RID: 12423
		public const byte LF_XHDR = 120;

		// Token: 0x04003088 RID: 12424
		public const byte LF_ACL = 65;

		// Token: 0x04003089 RID: 12425
		public const byte LF_GNU_DUMPDIR = 68;

		// Token: 0x0400308A RID: 12426
		public const byte LF_EXTATTR = 69;

		// Token: 0x0400308B RID: 12427
		public const byte LF_META = 73;

		// Token: 0x0400308C RID: 12428
		public const byte LF_GNU_LONGLINK = 75;

		// Token: 0x0400308D RID: 12429
		public const byte LF_GNU_LONGNAME = 76;

		// Token: 0x0400308E RID: 12430
		public const byte LF_GNU_MULTIVOL = 77;

		// Token: 0x0400308F RID: 12431
		public const byte LF_GNU_NAMES = 78;

		// Token: 0x04003090 RID: 12432
		public const byte LF_GNU_SPARSE = 83;

		// Token: 0x04003091 RID: 12433
		public const byte LF_GNU_VOLHDR = 86;

		// Token: 0x04003092 RID: 12434
		public const string TMAGIC = "ustar";

		// Token: 0x04003093 RID: 12435
		public const string GNU_TMAGIC = "ustar  ";

		// Token: 0x04003094 RID: 12436
		private const long timeConversionFactor = 10000000L;

		// Token: 0x04003095 RID: 12437
		private static readonly DateTime dateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		// Token: 0x04003096 RID: 12438
		private string name;

		// Token: 0x04003097 RID: 12439
		private int mode;

		// Token: 0x04003098 RID: 12440
		private int userId;

		// Token: 0x04003099 RID: 12441
		private int groupId;

		// Token: 0x0400309A RID: 12442
		private long size;

		// Token: 0x0400309B RID: 12443
		private DateTime modTime;

		// Token: 0x0400309C RID: 12444
		private int checksum;

		// Token: 0x0400309D RID: 12445
		private bool isChecksumValid;

		// Token: 0x0400309E RID: 12446
		private byte typeFlag;

		// Token: 0x0400309F RID: 12447
		private string linkName;

		// Token: 0x040030A0 RID: 12448
		private string magic;

		// Token: 0x040030A1 RID: 12449
		private string version;

		// Token: 0x040030A2 RID: 12450
		private string userName;

		// Token: 0x040030A3 RID: 12451
		private string groupName;

		// Token: 0x040030A4 RID: 12452
		private int devMajor;

		// Token: 0x040030A5 RID: 12453
		private int devMinor;

		// Token: 0x040030A6 RID: 12454
		internal static int userIdAsSet;

		// Token: 0x040030A7 RID: 12455
		internal static int groupIdAsSet;

		// Token: 0x040030A8 RID: 12456
		internal static string userNameAsSet;

		// Token: 0x040030A9 RID: 12457
		internal static string groupNameAsSet = "None";

		// Token: 0x040030AA RID: 12458
		internal static int defaultUserId;

		// Token: 0x040030AB RID: 12459
		internal static int defaultGroupId;

		// Token: 0x040030AC RID: 12460
		internal static string defaultGroupName = "None";

		// Token: 0x040030AD RID: 12461
		internal static string defaultUser;
	}
}
