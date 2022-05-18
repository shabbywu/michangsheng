using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007CC RID: 1996
	public class RawTaggedData : ITaggedData
	{
		// Token: 0x060032D8 RID: 13016 RVA: 0x00025063 File Offset: 0x00023263
		public RawTaggedData(short tag)
		{
			this._tag = tag;
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x00025072 File Offset: 0x00023272
		// (set) Token: 0x060032DA RID: 13018 RVA: 0x0002507A File Offset: 0x0002327A
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

		// Token: 0x060032DB RID: 13019 RVA: 0x00025083 File Offset: 0x00023283
		public void SetData(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._data = new byte[count];
			Array.Copy(data, offset, this._data, 0, count);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000250AE File Offset: 0x000232AE
		public byte[] GetData()
		{
			return this._data;
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x000250AE File Offset: 0x000232AE
		// (set) Token: 0x060032DE RID: 13022 RVA: 0x000250B6 File Offset: 0x000232B6
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

		// Token: 0x04002EF3 RID: 12019
		private short _tag;

		// Token: 0x04002EF4 RID: 12020
		private byte[] _data;
	}
}
