using System;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x0200058B RID: 1419
	public sealed class Adler32 : IChecksum
	{
		// Token: 0x06002E85 RID: 11909 RVA: 0x00152001 File Offset: 0x00150201
		public Adler32()
		{
			this.Reset();
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x0015200F File Offset: 0x0015020F
		public void Reset()
		{
			this.checkValue = 1U;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x00152018 File Offset: 0x00150218
		public long Value
		{
			get
			{
				return (long)((ulong)this.checkValue);
			}
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x00152024 File Offset: 0x00150224
		public void Update(int bval)
		{
			uint num = this.checkValue & 65535U;
			uint num2 = this.checkValue >> 16;
			num = (num + (uint)(bval & 255)) % Adler32.BASE;
			num2 = (num + num2) % Adler32.BASE;
			this.checkValue = (num2 << 16) + num;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x0015206E File Offset: 0x0015026E
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(new ArraySegment<byte>(buffer, 0, buffer.Length));
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x00152090 File Offset: 0x00150290
		public void Update(ArraySegment<byte> segment)
		{
			uint num = this.checkValue & 65535U;
			uint num2 = this.checkValue >> 16;
			int i = segment.Count;
			int offset = segment.Offset;
			while (i > 0)
			{
				int num3 = 3800;
				if (num3 > i)
				{
					num3 = i;
				}
				i -= num3;
				while (--num3 >= 0)
				{
					num += (uint)(segment.Array[offset++] & byte.MaxValue);
					num2 += num;
				}
				num %= Adler32.BASE;
				num2 %= Adler32.BASE;
			}
			this.checkValue = (num2 << 16 | num);
		}

		// Token: 0x040028EA RID: 10474
		private static readonly uint BASE = 65521U;

		// Token: 0x040028EB RID: 10475
		private uint checkValue;
	}
}
