using System;
using System.Text;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011DC RID: 4572
	internal class ByteBuffer
	{
		// Token: 0x06006FDB RID: 28635 RVA: 0x0004C04B File Offset: 0x0004A24B
		public ByteBuffer()
		{
			this._buffer = new byte[0];
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06006FDC RID: 28636 RVA: 0x0004C05F File Offset: 0x0004A25F
		public int Length
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x0004C069 File Offset: 0x0004A269
		public string GetString(Encoding enc)
		{
			return enc.GetString(this._buffer);
		}

		// Token: 0x06006FDE RID: 28638 RVA: 0x002A0A28 File Offset: 0x0029EC28
		public void Append(byte[] b, int length)
		{
			byte[] array = new byte[this._buffer.Length + length];
			Buffer.BlockCopy(this._buffer, 0, array, 0, this._buffer.Length);
			Buffer.BlockCopy(b, 0, array, this._buffer.Length, length);
			this._buffer = array;
		}

		// Token: 0x06006FDF RID: 28639 RVA: 0x002A0A74 File Offset: 0x0029EC74
		public byte[] RemoveFirst(int n)
		{
			byte[] array = new byte[n];
			Buffer.BlockCopy(this._buffer, 0, array, 0, n);
			byte[] array2 = new byte[this._buffer.Length - n];
			Buffer.BlockCopy(this._buffer, n, array2, 0, this._buffer.Length - n);
			this._buffer = array2;
			return array;
		}

		// Token: 0x040062D5 RID: 25301
		private byte[] _buffer;
	}
}
