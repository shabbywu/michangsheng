using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007CF RID: 1999
	public class NTTaggedData : ITaggedData
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x000251E2 File Offset: 0x000233E2
		public short TagID
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x0018EDB0 File Offset: 0x0018CFB0
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

		// Token: 0x060032EE RID: 13038 RVA: 0x0018EE78 File Offset: 0x0018D078
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

		// Token: 0x060032EF RID: 13039 RVA: 0x0018EF1C File Offset: 0x0018D11C
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060032F0 RID: 13040 RVA: 0x000251E6 File Offset: 0x000233E6
		// (set) Token: 0x060032F1 RID: 13041 RVA: 0x000251EE File Offset: 0x000233EE
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x0002520A File Offset: 0x0002340A
		// (set) Token: 0x060032F3 RID: 13043 RVA: 0x00025212 File Offset: 0x00023412
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x0002522E File Offset: 0x0002342E
		// (set) Token: 0x060032F5 RID: 13045 RVA: 0x00025236 File Offset: 0x00023436
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

		// Token: 0x04002EFD RID: 12029
		private DateTime _lastAccessTime = DateTime.FromFileTimeUtc(0L);

		// Token: 0x04002EFE RID: 12030
		private DateTime _lastModificationTime = DateTime.FromFileTimeUtc(0L);

		// Token: 0x04002EFF RID: 12031
		private DateTime _createTime = DateTime.FromFileTimeUtc(0L);
	}
}
