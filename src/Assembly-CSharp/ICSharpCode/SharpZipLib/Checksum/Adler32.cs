using System;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x02000834 RID: 2100
	public sealed class Adler32 : IChecksum
	{
		// Token: 0x060036FB RID: 14075 RVA: 0x00028006 File Offset: 0x00026206
		public Adler32()
		{
			this.Reset();
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x00028014 File Offset: 0x00026214
		public void Reset()
		{
			this.checkValue = 1U;
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x0002801D File Offset: 0x0002621D
		public long Value
		{
			get
			{
				return (long)((ulong)this.checkValue);
			}
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x0019CC74 File Offset: 0x0019AE74
		public void Update(int bval)
		{
			uint num = this.checkValue & 65535U;
			uint num2 = this.checkValue >> 16;
			num = (num + (uint)(bval & 255)) % Adler32.BASE;
			num2 = (num + num2) % Adler32.BASE;
			this.checkValue = (num2 << 16) + num;
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x00028026 File Offset: 0x00026226
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(new ArraySegment<byte>(buffer, 0, buffer.Length));
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x0019CCC0 File Offset: 0x0019AEC0
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

		// Token: 0x04003129 RID: 12585
		private static readonly uint BASE = 65521U;

		// Token: 0x0400312A RID: 12586
		private uint checkValue;
	}
}
