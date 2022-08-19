using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000535 RID: 1333
	public class ExtendedUnixData : ITaggedData
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x0014222B File Offset: 0x0014042B
		public short TagID
		{
			get
			{
				return 21589;
			}
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x00142234 File Offset: 0x00140434
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

		// Token: 0x06002ACA RID: 10954 RVA: 0x00142344 File Offset: 0x00140544
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

		// Token: 0x06002ACB RID: 10955 RVA: 0x00142458 File Offset: 0x00140658
		public static bool IsValidValue(DateTime value)
		{
			return value >= new DateTime(1901, 12, 13, 20, 45, 52) || value <= new DateTime(2038, 1, 19, 3, 14, 7);
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x0014248F File Offset: 0x0014068F
		// (set) Token: 0x06002ACD RID: 10957 RVA: 0x00142497 File Offset: 0x00140697
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x001424C1 File Offset: 0x001406C1
		// (set) Token: 0x06002ACF RID: 10959 RVA: 0x001424C9 File Offset: 0x001406C9
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

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x001424F3 File Offset: 0x001406F3
		// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x001424FB File Offset: 0x001406FB
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

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x00142525 File Offset: 0x00140725
		// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x0014252D File Offset: 0x0014072D
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

		// Token: 0x040026F2 RID: 9970
		private ExtendedUnixData.Flags _flags;

		// Token: 0x040026F3 RID: 9971
		private DateTime _modificationTime = new DateTime(1970, 1, 1);

		// Token: 0x040026F4 RID: 9972
		private DateTime _lastAccessTime = new DateTime(1970, 1, 1);

		// Token: 0x040026F5 RID: 9973
		private DateTime _createTime = new DateTime(1970, 1, 1);

		// Token: 0x02001482 RID: 5250
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x04006C46 RID: 27718
			ModificationTime = 1,
			// Token: 0x04006C47 RID: 27719
			AccessTime = 2,
			// Token: 0x04006C48 RID: 27720
			CreateTime = 4
		}
	}
}
