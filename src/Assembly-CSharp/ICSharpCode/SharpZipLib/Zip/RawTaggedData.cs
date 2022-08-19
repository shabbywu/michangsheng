using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000534 RID: 1332
	public class RawTaggedData : ITaggedData
	{
		// Token: 0x06002AC1 RID: 10945 RVA: 0x001421CF File Offset: 0x001403CF
		public RawTaggedData(short tag)
		{
			this._tag = tag;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x001421DE File Offset: 0x001403DE
		// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x001421E6 File Offset: 0x001403E6
		public short TagID
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x001421EF File Offset: 0x001403EF
		public void SetData(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._data = new byte[count];
			Array.Copy(data, offset, this._data, 0, count);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0014221A File Offset: 0x0014041A
		public byte[] GetData()
		{
			return this._data;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x0014221A File Offset: 0x0014041A
		// (set) Token: 0x06002AC7 RID: 10951 RVA: 0x00142222 File Offset: 0x00140422
		public byte[] Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x040026F0 RID: 9968
		private short _tag;

		// Token: 0x040026F1 RID: 9969
		private byte[] _data;
	}
}
