using System;
using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x02000836 RID: 2102
	public sealed class Crc32 : IChecksum
	{
		// Token: 0x0600370B RID: 14091 RVA: 0x000280FE File Offset: 0x000262FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint ComputeCrc32(uint oldCrc, byte bval)
		{
			return Crc32.crcTable[(int)((oldCrc ^ (uint)bval) & 255U)] ^ oldCrc >> 8;
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x00028113 File Offset: 0x00026313
		public Crc32()
		{
			this.Reset();
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x00028121 File Offset: 0x00026321
		public void Reset()
		{
			this.checkValue = Crc32.crcInit;
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x0002812E File Offset: 0x0002632E
		public long Value
		{
			get
			{
				return (long)((ulong)(this.checkValue ^ Crc32.crcXor));
			}
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x0002813D File Offset: 0x0002633D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Update(int bval)
		{
			this.checkValue = (Crc32.crcTable[(int)(checked((IntPtr)(unchecked((ulong)this.checkValue ^ (ulong)((long)bval)) & 255UL)))] ^ this.checkValue >> 8);
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x00028166 File Offset: 0x00026366
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(buffer, 0, buffer.Length);
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x00028181 File Offset: 0x00026381
		public void Update(ArraySegment<byte> segment)
		{
			this.Update(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x0019CD9C File Offset: 0x0019AF9C
		private void Update(byte[] data, int offset, int count)
		{
			int num = count % 16;
			int num2 = offset + count - num;
			while (offset != num2)
			{
				this.checkValue = CrcUtilities.UpdateDataForReversedPoly(data, offset, Crc32.crcTable, this.checkValue);
				offset += 16;
			}
			if (num != 0)
			{
				this.SlowUpdateLoop(data, offset, num2 + num);
			}
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x0002819E File Offset: 0x0002639E
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SlowUpdateLoop(byte[] data, int offset, int end)
		{
			while (offset != end)
			{
				this.Update((int)data[offset++]);
			}
		}

		// Token: 0x0400312E RID: 12590
		private static readonly uint crcInit = uint.MaxValue;

		// Token: 0x0400312F RID: 12591
		private static readonly uint crcXor = uint.MaxValue;

		// Token: 0x04003130 RID: 12592
		private static readonly uint[] crcTable = CrcUtilities.GenerateSlicingLookupTable(3988292384U, true);

		// Token: 0x04003131 RID: 12593
		private uint checkValue;
	}
}
