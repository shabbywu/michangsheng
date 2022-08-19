using System;
using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x0200058D RID: 1421
	public sealed class Crc32 : IChecksum
	{
		// Token: 0x06002E95 RID: 11925 RVA: 0x00152222 File Offset: 0x00150422
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint ComputeCrc32(uint oldCrc, byte bval)
		{
			return Crc32.crcTable[(int)((oldCrc ^ (uint)bval) & 255U)] ^ oldCrc >> 8;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x00152237 File Offset: 0x00150437
		public Crc32()
		{
			this.Reset();
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x00152245 File Offset: 0x00150445
		public void Reset()
		{
			this.checkValue = Crc32.crcInit;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x00152252 File Offset: 0x00150452
		public long Value
		{
			get
			{
				return (long)((ulong)(this.checkValue ^ Crc32.crcXor));
			}
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x00152261 File Offset: 0x00150461
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Update(int bval)
		{
			this.checkValue = (Crc32.crcTable[(int)(checked((IntPtr)(unchecked((ulong)this.checkValue ^ (ulong)((long)bval)) & 255UL)))] ^ this.checkValue >> 8);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x0015228A File Offset: 0x0015048A
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(buffer, 0, buffer.Length);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x001522A5 File Offset: 0x001504A5
		public void Update(ArraySegment<byte> segment)
		{
			this.Update(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x001522C4 File Offset: 0x001504C4
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

		// Token: 0x06002E9D RID: 11933 RVA: 0x0015230E File Offset: 0x0015050E
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SlowUpdateLoop(byte[] data, int offset, int end)
		{
			while (offset != end)
			{
				this.Update((int)data[offset++]);
			}
		}

		// Token: 0x040028EF RID: 10479
		private static readonly uint crcInit = uint.MaxValue;

		// Token: 0x040028F0 RID: 10480
		private static readonly uint crcXor = uint.MaxValue;

		// Token: 0x040028F1 RID: 10481
		private static readonly uint[] crcTable = CrcUtilities.GenerateSlicingLookupTable(3988292384U, true);

		// Token: 0x040028F2 RID: 10482
		private uint checkValue;
	}
}
