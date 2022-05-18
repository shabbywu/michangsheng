using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007CD RID: 1997
	public class ExtendedUnixData : ITaggedData
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000250BF File Offset: 0x000232BF
		public short TagID
		{
			get
			{
				return 21589;
			}
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x0018EB8C File Offset: 0x0018CD8C
		public void SetData(byte[] data, int index, int count)
		{
			using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					this._flags = (ExtendedUnixData.Flags)zipHelperStream.ReadByte();
					if ((this._flags & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags)0)
					{
						int seconds = zipHelperStream.ReadLEInt();
						this._modificationTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(0, 0, 0, seconds, 0);
						if (count <= 5)
						{
							return;
						}
					}
					if ((this._flags & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags)0)
					{
						int seconds2 = zipHelperStream.ReadLEInt();
						this._lastAccessTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(0, 0, 0, seconds2, 0);
					}
					if ((this._flags & ExtendedUnixData.Flags.CreateTime) != (ExtendedUnixData.Flags)0)
					{
						int seconds3 = zipHelperStream.ReadLEInt();
						this._createTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(0, 0, 0, seconds3, 0);
					}
				}
			}
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x0018EC9C File Offset: 0x0018CE9C
		public byte[] GetData()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					zipHelperStream.IsStreamOwner = false;
					zipHelperStream.WriteByte((byte)this._flags);
					if ((this._flags & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags)0)
					{
						int value = (int)(this._modificationTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
						zipHelperStream.WriteLEInt(value);
					}
					if ((this._flags & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags)0)
					{
						int value2 = (int)(this._lastAccessTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
						zipHelperStream.WriteLEInt(value2);
					}
					if ((this._flags & ExtendedUnixData.Flags.CreateTime) != (ExtendedUnixData.Flags)0)
					{
						int value3 = (int)(this._createTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
						zipHelperStream.WriteLEInt(value3);
					}
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000250C6 File Offset: 0x000232C6
		public static bool IsValidValue(DateTime value)
		{
			return value >= new DateTime(1901, 12, 13, 20, 45, 52) || value <= new DateTime(2038, 1, 19, 3, 14, 7);
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000250FD File Offset: 0x000232FD
		// (set) Token: 0x060032E4 RID: 13028 RVA: 0x00025105 File Offset: 0x00023305
		public DateTime ModificationTime
		{
			get
			{
				return this._modificationTime;
			}
			set
			{
				if (!ExtendedUnixData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.ModificationTime;
				this._modificationTime = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x0002512F File Offset: 0x0002332F
		// (set) Token: 0x060032E6 RID: 13030 RVA: 0x00025137 File Offset: 0x00023337
		public DateTime AccessTime
		{
			get
			{
				return this._lastAccessTime;
			}
			set
			{
				if (!ExtendedUnixData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.AccessTime;
				this._lastAccessTime = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x00025161 File Offset: 0x00023361
		// (set) Token: 0x060032E8 RID: 13032 RVA: 0x00025169 File Offset: 0x00023369
		public DateTime CreateTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				if (!ExtendedUnixData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.CreateTime;
				this._createTime = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x00025193 File Offset: 0x00023393
		// (set) Token: 0x060032EA RID: 13034 RVA: 0x0002519B File Offset: 0x0002339B
		public ExtendedUnixData.Flags Include
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x04002EF5 RID: 12021
		private ExtendedUnixData.Flags _flags;

		// Token: 0x04002EF6 RID: 12022
		private DateTime _modificationTime = new DateTime(1970, 1, 1);

		// Token: 0x04002EF7 RID: 12023
		private DateTime _lastAccessTime = new DateTime(1970, 1, 1);

		// Token: 0x04002EF8 RID: 12024
		private DateTime _createTime = new DateTime(1970, 1, 1);

		// Token: 0x020007CE RID: 1998
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x04002EFA RID: 12026
			ModificationTime = 1,
			// Token: 0x04002EFB RID: 12027
			AccessTime = 2,
			// Token: 0x04002EFC RID: 12028
			CreateTime = 4
		}
	}
}
