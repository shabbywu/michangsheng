using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200055E RID: 1374
	public class StreamManipulator
	{
		// Token: 0x06002CC2 RID: 11458 RVA: 0x0014C534 File Offset: 0x0014A734
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

		// Token: 0x06002CC3 RID: 11459 RVA: 0x0014C5D4 File Offset: 0x0014A7D4
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

		// Token: 0x06002CC4 RID: 11460 RVA: 0x0014C5FC File Offset: 0x0014A7FC
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

		// Token: 0x06002CC5 RID: 11461 RVA: 0x0014C625 File Offset: 0x0014A825
		public void DropBits(int bitCount)
		{
			this.buffer_ >>= bitCount;
			this.bitsInBuffer_ -= bitCount;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x0014C646 File Offset: 0x0014A846
		public int GetBits(int bitCount)
		{
			int num = this.PeekBits(bitCount);
			if (num >= 0)
			{
				this.DropBits(bitCount);
			}
			return num;
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x0014C65A File Offset: 0x0014A85A
		public int AvailableBits
		{
			get
			{
				return this.bitsInBuffer_;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x0014C662 File Offset: 0x0014A862
		public int AvailableBytes
		{
			get
			{
				return this.windowEnd_ - this.windowStart_ + (this.bitsInBuffer_ >> 3);
			}
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x0014C67A File Offset: 0x0014A87A
		public void SkipToByteBoundary()
		{
			this.buffer_ >>= (this.bitsInBuffer_ & 7);
			this.bitsInBuffer_ &= -8;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x0014C6A3 File Offset: 0x0014A8A3
		public bool IsNeedingInput
		{
			get
			{
				return this.windowStart_ == this.windowEnd_;
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0014C6B4 File Offset: 0x0014A8B4
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

		// Token: 0x06002CCC RID: 11468 RVA: 0x0014C7A8 File Offset: 0x0014A9A8
		public void Reset()
		{
			this.buffer_ = 0U;
			this.windowStart_ = (this.windowEnd_ = (this.bitsInBuffer_ = 0));
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x0014C7D8 File Offset: 0x0014A9D8
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

		// Token: 0x0400280A RID: 10250
		private byte[] window_;

		// Token: 0x0400280B RID: 10251
		private int windowStart_;

		// Token: 0x0400280C RID: 10252
		private int windowEnd_;

		// Token: 0x0400280D RID: 10253
		private uint buffer_;

		// Token: 0x0400280E RID: 10254
		private int bitsInBuffer_;
	}
}
