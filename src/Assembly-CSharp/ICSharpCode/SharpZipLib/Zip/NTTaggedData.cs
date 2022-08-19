using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000536 RID: 1334
	public class NTTaggedData : ITaggedData
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x00142574 File Offset: 0x00140774
		public short TagID
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00142578 File Offset: 0x00140778
		public void SetData(byte[] data, int index, int count)
		{
			using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					zipHelperStream.ReadLEInt();
					while (zipHelperStream.Position < zipHelperStream.Length)
					{
						int num = zipHelperStream.ReadLEShort();
						int num2 = zipHelperStream.ReadLEShort();
						if (num == 1)
						{
							if (num2 >= 24)
							{
								long fileTime = zipHelperStream.ReadLELong();
								this._lastModificationTime = DateTime.FromFileTimeUtc(fileTime);
								long fileTime2 = zipHelperStream.ReadLELong();
								this._lastAccessTime = DateTime.FromFileTimeUtc(fileTime2);
								long fileTime3 = zipHelperStream.ReadLELong();
								this._createTime = DateTime.FromFileTimeUtc(fileTime3);
								break;
							}
							break;
						}
						else
						{
							zipHelperStream.Seek((long)num2, SeekOrigin.Current);
						}
					}
				}
			}
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x00142640 File Offset: 0x00140840
		public byte[] GetData()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					zipHelperStream.IsStreamOwner = false;
					zipHelperStream.WriteLEInt(0);
					zipHelperStream.WriteLEShort(1);
					zipHelperStream.WriteLEShort(24);
					zipHelperStream.WriteLELong(this._lastModificationTime.ToFileTimeUtc());
					zipHelperStream.WriteLELong(this._lastAccessTime.ToFileTimeUtc());
					zipHelperStream.WriteLELong(this._createTime.ToFileTimeUtc());
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x001426E4 File Offset: 0x001408E4
		public static bool IsValidValue(DateTime value)
		{
			bool result = true;
			try
			{
				value.ToFileTimeUtc();
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x00142714 File Offset: 0x00140914
		// (set) Token: 0x06002ADA RID: 10970 RVA: 0x0014271C File Offset: 0x0014091C
		public DateTime LastModificationTime
		{
			get
			{
				return this._lastModificationTime;
			}
			set
			{
				if (!NTTaggedData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lastModificationTime = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x00142738 File Offset: 0x00140938
		// (set) Token: 0x06002ADC RID: 10972 RVA: 0x00142740 File Offset: 0x00140940
		public DateTime CreateTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				if (!NTTaggedData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._createTime = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x0014275C File Offset: 0x0014095C
		// (set) Token: 0x06002ADE RID: 10974 RVA: 0x00142764 File Offset: 0x00140964
		public DateTime LastAccessTime
		{
			get
			{
				return this._lastAccessTime;
			}
			set
			{
				if (!NTTaggedData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lastAccessTime = value;
			}
		}

		// Token: 0x040026F6 RID: 9974
		private DateTime _lastAccessTime = DateTime.FromFileTimeUtc(0L);

		// Token: 0x040026F7 RID: 9975
		private DateTime _lastModificationTime = DateTime.FromFileTimeUtc(0L);

		// Token: 0x040026F8 RID: 9976
		private DateTime _createTime = DateTime.FromFileTimeUtc(0L);
	}
}
