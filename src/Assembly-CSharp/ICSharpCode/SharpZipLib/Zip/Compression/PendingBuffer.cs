using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000559 RID: 1369
	public class PendingBuffer
	{
		// Token: 0x06002C64 RID: 11364 RVA: 0x0014B3B1 File Offset: 0x001495B1
		public PendingBuffer() : this(4096)
		{
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x0014B3BE File Offset: 0x001495BE
		public PendingBuffer(int bufferSize)
		{
			this.buffer = new byte[bufferSize];
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x0014B3D4 File Offset: 0x001495D4
		public void Reset()
		{
			this.start = (this.end = (this.bitCount = 0));
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x0014B3FC File Offset: 0x001495FC
		public void WriteByte(int value)
		{
			byte[] array = this.buffer;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x0014B424 File Offset: 0x00149624
		public void WriteShort(int value)
		{
			byte[] array = this.buffer;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array2[num] = (byte)(value >> 8);
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x0014B468 File Offset: 0x00149668
		public void WriteInt(int value)
		{
			byte[] array = this.buffer;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array2[num] = (byte)(value >> 8);
			byte[] array3 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array3[num] = (byte)(value >> 16);
			byte[] array4 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array4[num] = (byte)(value >> 24);
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x0014B4E5 File Offset: 0x001496E5
		public void WriteBlock(byte[] block, int offset, int length)
		{
			Array.Copy(block, offset, this.buffer, this.end, length);
			this.end += length;
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x0014B509 File Offset: 0x00149709
		public int BitCount
		{
			get
			{
				return this.bitCount;
			}
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x0014B514 File Offset: 0x00149714
		public void AlignToByte()
		{
			if (this.bitCount > 0)
			{
				byte[] array = this.buffer;
				int num = this.end;
				this.end = num + 1;
				array[num] = (byte)this.bits;
				if (this.bitCount > 8)
				{
					byte[] array2 = this.buffer;
					num = this.end;
					this.end = num + 1;
					array2[num] = (byte)(this.bits >> 8);
				}
			}
			this.bits = 0U;
			this.bitCount = 0;
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x0014B584 File Offset: 0x00149784
		public void WriteBits(int b, int count)
		{
			this.bits |= (uint)((uint)b << this.bitCount);
			this.bitCount += count;
			if (this.bitCount >= 16)
			{
				byte[] array = this.buffer;
				int num = this.end;
				this.end = num + 1;
				array[num] = (byte)this.bits;
				byte[] array2 = this.buffer;
				num = this.end;
				this.end = num + 1;
				array2[num] = (byte)(this.bits >> 8);
				this.bits >>= 16;
				this.bitCount -= 16;
			}
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x0014B620 File Offset: 0x00149820
		public void WriteShortMSB(int s)
		{
			byte[] array = this.buffer;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)(s >> 8);
			byte[] array2 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array2[num] = (byte)s;
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x0014B663 File Offset: 0x00149863
		public bool IsFlushed
		{
			get
			{
				return this.end == 0;
			}
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x0014B670 File Offset: 0x00149870
		public int Flush(byte[] output, int offset, int length)
		{
			if (this.bitCount >= 8)
			{
				byte[] array = this.buffer;
				int num = this.end;
				this.end = num + 1;
				array[num] = (byte)this.bits;
				this.bits >>= 8;
				this.bitCount -= 8;
			}
			if (length > this.end - this.start)
			{
				length = this.end - this.start;
				Array.Copy(this.buffer, this.start, output, offset, length);
				this.start = 0;
				this.end = 0;
			}
			else
			{
				Array.Copy(this.buffer, this.start, output, offset, length);
				this.start += length;
			}
			return length;
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x0014B728 File Offset: 0x00149928
		public byte[] ToByteArray()
		{
			this.AlignToByte();
			byte[] array = new byte[this.end - this.start];
			Array.Copy(this.buffer, this.start, array, 0, array.Length);
			this.start = 0;
			this.end = 0;
			return array;
		}

		// Token: 0x040027E9 RID: 10217
		private readonly byte[] buffer;

		// Token: 0x040027EA RID: 10218
		private int start;

		// Token: 0x040027EB RID: 10219
		private int end;

		// Token: 0x040027EC RID: 10220
		private uint bits;

		// Token: 0x040027ED RID: 10221
		private int bitCount;
	}
}
