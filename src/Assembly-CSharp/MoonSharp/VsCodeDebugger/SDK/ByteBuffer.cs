using System;
using System.Text;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DB0 RID: 3504
	internal class ByteBuffer
	{
		// Token: 0x06006395 RID: 25493 RVA: 0x0027B417 File Offset: 0x00279617
		public ByteBuffer()
		{
			this._buffer = new byte[0];
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x0027B42B File Offset: 0x0027962B
		public int Length
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x0027B435 File Offset: 0x00279635
		public string GetString(Encoding enc)
		{
			return enc.GetString(this._buffer);
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x0027B444 File Offset: 0x00279644
		public void Append(byte[] b, int length)
		{
			byte[] array = new byte[this._buffer.Length + length];
			Buffer.BlockCopy(this._buffer, 0, array, 0, this._buffer.Length);
			Buffer.BlockCopy(b, 0, array, this._buffer.Length, length);
			this._buffer = array;
		}

		// Token: 0x06006399 RID: 25497 RVA: 0x0027B490 File Offset: 0x00279690
		public byte[] RemoveFirst(int n)
		{
			byte[] array = new byte[n];
			Buffer.BlockCopy(this._buffer, 0, array, 0, n);
			byte[] array2 = new byte[this._buffer.Length - n];
			Buffer.BlockCopy(this._buffer, n, array2, 0, this._buffer.Length - n);
			this._buffer = array2;
			return array;
		}

		// Token: 0x040055EE RID: 21998
		private byte[] _buffer;
	}
}
