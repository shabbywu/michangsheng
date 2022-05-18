using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000804 RID: 2052
	public class StreamManipulator
	{
		// Token: 0x06003530 RID: 13616 RVA: 0x00198498 File Offset: 0x00196698
		public int PeekBits(int bitCount)
		{
			if (this.bitsInBuffer_ < bitCount)
			{
				if (this.windowStart_ == this.windowEnd_)
				{
					return -1;
				}
				uint num = this.buffer_;
				byte[] array = this.window_;
				int num2 = this.windowStart_;
				this.windowStart_ = num2 + 1;
				uint num3 = array[num2] & 255U;
				byte[] array2 = this.window_;
				num2 = this.windowStart_;
				this.windowStart_ = num2 + 1;
				this.buffer_ = (num | (num3 | (array2[num2] & 255U) << 8) << this.bitsInBuffer_);
				this.bitsInBuffer_ += 16;
			}
			return (int)((ulong)this.buffer_ & (ulong)((long)((1 << bitCount) - 1)));
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x00198538 File Offset: 0x00196738
		public bool TryGetBits(int bitCount, ref int output, int outputOffset = 0)
		{
			int num = this.PeekBits(bitCount);
			if (num < 0)
			{
				return false;
			}
			output = num + outputOffset;
			this.DropBits(bitCount);
			return true;
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x00198560 File Offset: 0x00196760
		public bool TryGetBits(int bitCount, ref byte[] array, int index)
		{
			int num = this.PeekBits(bitCount);
			if (num < 0)
			{
				return false;
			}
			array[index] = (byte)num;
			this.DropBits(bitCount);
			return true;
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x00026CEE File Offset: 0x00024EEE
		public void DropBits(int bitCount)
		{
			this.buffer_ >>= bitCount;
			this.bitsInBuffer_ -= bitCount;
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x00026D0F File Offset: 0x00024F0F
		public int GetBits(int bitCount)
		{
			int num = this.PeekBits(bitCount);
			if (num >= 0)
			{
				this.DropBits(bitCount);
			}
			return num;
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x00026D23 File Offset: 0x00024F23
		public int AvailableBits
		{
			get
			{
				return this.bitsInBuffer_;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x00026D2B File Offset: 0x00024F2B
		public int AvailableBytes
		{
			get
			{
				return this.windowEnd_ - this.windowStart_ + (this.bitsInBuffer_ >> 3);
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x00026D43 File Offset: 0x00024F43
		public void SkipToByteBoundary()
		{
			this.buffer_ >>= (this.bitsInBuffer_ & 7);
			this.bitsInBuffer_ &= -8;
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x00026D6C File Offset: 0x00024F6C
		public bool IsNeedingInput
		{
			get
			{
				return this.windowStart_ == this.windowEnd_;
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x0019858C File Offset: 0x0019678C
		public int CopyBytes(byte[] output, int offset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if ((this.bitsInBuffer_ & 7) != 0)
			{
				throw new InvalidOperationException("Bit buffer is not byte aligned!");
			}
			int num = 0;
			while (this.bitsInBuffer_ > 0 && length > 0)
			{
				output[offset++] = (byte)this.buffer_;
				this.buffer_ >>= 8;
				this.bitsInBuffer_ -= 8;
				length--;
				num++;
			}
			if (length == 0)
			{
				return num;
			}
			int num2 = this.windowEnd_ - this.windowStart_;
			if (length > num2)
			{
				length = num2;
			}
			Array.Copy(this.window_, this.windowStart_, output, offset, length);
			this.windowStart_ += length;
			if ((this.windowStart_ - this.windowEnd_ & 1) != 0)
			{
				byte[] array = this.window_;
				int num3 = this.windowStart_;
				this.windowStart_ = num3 + 1;
				this.buffer_ = (array[num3] & 255U);
				this.bitsInBuffer_ = 8;
			}
			return num + length;
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x00198680 File Offset: 0x00196880
		public void Reset()
		{
			this.buffer_ = 0U;
			this.windowStart_ = (this.windowEnd_ = (this.bitsInBuffer_ = 0));
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x001986B0 File Offset: 0x001968B0
		public void SetInput(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			if (this.windowStart_ < this.windowEnd_)
			{
				throw new InvalidOperationException("Old input was not completely processed");
			}
			int num = offset + count;
			if (offset > num || num > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((count & 1) != 0)
			{
				this.buffer_ |= (uint)((uint)(buffer[offset++] & byte.MaxValue) << this.bitsInBuffer_);
				this.bitsInBuffer_ += 8;
			}
			this.window_ = buffer;
			this.windowStart_ = offset;
			this.windowEnd_ = num;
		}

		// Token: 0x04003043 RID: 12355
		private byte[] window_;

		// Token: 0x04003044 RID: 12356
		private int windowStart_;

		// Token: 0x04003045 RID: 12357
		private int windowEnd_;

		// Token: 0x04003046 RID: 12358
		private uint buffer_;

		// Token: 0x04003047 RID: 12359
		private int bitsInBuffer_;
	}
}
