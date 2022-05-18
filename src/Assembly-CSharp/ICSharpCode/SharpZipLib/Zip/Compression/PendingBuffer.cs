using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x020007FF RID: 2047
	public class PendingBuffer
	{
		// Token: 0x060034D2 RID: 13522 RVA: 0x0002694F File Offset: 0x00024B4F
		public PendingBuffer() : this(4096)
		{
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x0002695C File Offset: 0x00024B5C
		public PendingBuffer(int bufferSize)
		{
			this.buffer = new byte[bufferSize];
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x001976C0 File Offset: 0x001958C0
		public void Reset()
		{
			this.start = (this.end = (this.bitCount = 0));
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x001976E8 File Offset: 0x001958E8
		public void WriteByte(int value)
		{
			byte[] array = this.buffer;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x00197710 File Offset: 0x00195910
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

		// Token: 0x060034D7 RID: 13527 RVA: 0x00197754 File Offset: 0x00195954
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

		// Token: 0x060034D8 RID: 13528 RVA: 0x00026970 File Offset: 0x00024B70
		public void WriteBlock(byte[] block, int offset, int length)
		{
			Array.Copy(block, offset, this.buffer, this.end, length);
			this.end += length;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x00026994 File Offset: 0x00024B94
		public int BitCount
		{
			get
			{
				return this.bitCount;
			}
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x001977D4 File Offset: 0x001959D4
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

		// Token: 0x060034DB RID: 13531 RVA: 0x00197844 File Offset: 0x00195A44
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

		// Token: 0x060034DC RID: 13532 RVA: 0x001978E0 File Offset: 0x00195AE0
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

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x0002699C File Offset: 0x00024B9C
		public bool IsFlushed
		{
			get
			{
				return this.end == 0;
			}
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x00197924 File Offset: 0x00195B24
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

		// Token: 0x060034DF RID: 13535 RVA: 0x001979DC File Offset: 0x00195BDC
		public byte[] ToByteArray()
		{
			this.AlignToByte();
			byte[] array = new byte[this.end - this.start];
			Array.Copy(this.buffer, this.start, array, 0, array.Length);
			this.start = 0;
			this.end = 0;
			return array;
		}

		// Token: 0x04003022 RID: 12322
		private readonly byte[] buffer;

		// Token: 0x04003023 RID: 12323
		private int start;

		// Token: 0x04003024 RID: 12324
		private int end;

		// Token: 0x04003025 RID: 12325
		private uint bits;

		// Token: 0x04003026 RID: 12326
		private int bitCount;
	}
}
