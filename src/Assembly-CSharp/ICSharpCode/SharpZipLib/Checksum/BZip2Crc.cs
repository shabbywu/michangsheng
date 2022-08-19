using System;
using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x0200058C RID: 1420
	public sealed class BZip2Crc : IChecksum
	{
		// Token: 0x06002E8C RID: 11916 RVA: 0x0015212C File Offset: 0x0015032C
		public BZip2Crc()
		{
			this.Reset();
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x0015213A File Offset: 0x0015033A
		public void Reset()
		{
			this.checkValue = uint.MaxValue;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x00152143 File Offset: 0x00150343
		public long Value
		{
			get
			{
				return (long)((ulong)(~(ulong)this.checkValue));
			}
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x0015214D File Offset: 0x0015034D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Update(int bval)
		{
			this.checkValue = (BZip2Crc.crcTable[(int)((byte)((ulong)(this.checkValue >> 24 & 255U) ^ (ulong)((long)bval)))] ^ this.checkValue << 8);
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x00152178 File Offset: 0x00150378
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(buffer, 0, buffer.Length);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x00152193 File Offset: 0x00150393
		public void Update(ArraySegment<byte> segment)
		{
			this.Update(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x001521B0 File Offset: 0x001503B0
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

		// Token: 0x06002E93 RID: 11923 RVA: 0x001521FA File Offset: 0x001503FA
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SlowUpdateLoop(byte[] data, int offset, int end)
		{
			while (offset != end)
			{
				this.Update((int)data[offset++]);
			}
		}

		// Token: 0x040028EC RID: 10476
		private const uint crcInit = 4294967295U;

		// Token: 0x040028ED RID: 10477
		private static readonly uint[] crcTable = CrcUtilities.GenerateSlicingLookupTable(79764919U, false);

		// Token: 0x040028EE RID: 10478
		private uint checkValue;
	}
}
