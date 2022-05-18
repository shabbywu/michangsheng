using System;
using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x02000835 RID: 2101
	public sealed class BZip2Crc : IChecksum
	{
		// Token: 0x06003702 RID: 14082 RVA: 0x00028052 File Offset: 0x00026252
		public BZip2Crc()
		{
			this.Reset();
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x00028060 File Offset: 0x00026260
		public void Reset()
		{
			this.checkValue = uint.MaxValue;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x00028069 File Offset: 0x00026269
		public long Value
		{
			get
			{
				return (long)((ulong)(~(ulong)this.checkValue));
			}
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x00028073 File Offset: 0x00026273
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Update(int bval)
		{
			this.checkValue = (BZip2Crc.crcTable[(int)((byte)((ulong)(this.checkValue >> 24 & 255U) ^ (ulong)((long)bval)))] ^ this.checkValue << 8);
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x0002809E File Offset: 0x0002629E
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(buffer, 0, buffer.Length);
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000280B9 File Offset: 0x000262B9
		public void Update(ArraySegment<byte> segment)
		{
			this.Update(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x0019CD50 File Offset: 0x0019AF50
		private void Update(byte[] data, int offset, int count)
		{
			int num = count % 16;
			int num2 = offset + count - num;
			while (offset != num2)
			{
				this.checkValue = CrcUtilities.UpdateDataForNormalPoly(data, offset, BZip2Crc.crcTable, this.checkValue);
				offset += 16;
			}
			if (num != 0)
			{
				this.SlowUpdateLoop(data, offset, num2 + num);
			}
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000280D6 File Offset: 0x000262D6
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SlowUpdateLoop(byte[] data, int offset, int end)
		{
			while (offset != end)
			{
				this.Update((int)data[offset++]);
			}
		}

		// Token: 0x0400312B RID: 12587
		private const uint crcInit = 4294967295U;

		// Token: 0x0400312C RID: 12588
		private static readonly uint[] crcTable = CrcUtilities.GenerateSlicingLookupTable(79764919U, false);

		// Token: 0x0400312D RID: 12589
		private uint checkValue;
	}
}
