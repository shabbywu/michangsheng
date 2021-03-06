using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000803 RID: 2051
	public class OutputWindow
	{
		// Token: 0x06003526 RID: 13606 RVA: 0x00198174 File Offset: 0x00196374
		public void Write(int value)
		{
			int num = this.windowFilled;
			this.windowFilled = num + 1;
			if (num == 32768)
			{
				throw new InvalidOperationException("Window full");
			}
			byte[] array = this.window;
			num = this.windowEnd;
			this.windowEnd = num + 1;
			array[num] = (byte)value;
			this.windowEnd &= 32767;
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x001981D0 File Offset: 0x001963D0
		private void SlowRepeat(int repStart, int length, int distance)
		{
			while (length-- > 0)
			{
				byte[] array = this.window;
				int num = this.windowEnd;
				this.windowEnd = num + 1;
				array[num] = this.window[repStart++];
				this.windowEnd &= 32767;
				repStart &= 32767;
			}
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x00198228 File Offset: 0x00196428
		public void Repeat(int length, int distance)
		{
			if ((this.windowFilled += length) > 32768)
			{
				throw new InvalidOperationException("Window full");
			}
			int num = this.windowEnd - distance & 32767;
			int num2 = 32768 - length;
			if (num > num2 || this.windowEnd >= num2)
			{
				this.SlowRepeat(num, length, distance);
				return;
			}
			if (length <= distance)
			{
				Array.Copy(this.window, num, this.window, this.windowEnd, length);
				this.windowEnd += length;
				return;
			}
			while (length-- > 0)
			{
				byte[] array = this.window;
				int num3 = this.windowEnd;
				this.windowEnd = num3 + 1;
				array[num3] = this.window[num++];
			}
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x001982E0 File Offset: 0x001964E0
		public int CopyStored(StreamManipulator input, int length)
		{
			length = Math.Min(Math.Min(length, 32768 - this.windowFilled), input.AvailableBytes);
			int num = 32768 - this.windowEnd;
			int num2;
			if (length > num)
			{
				num2 = input.CopyBytes(this.window, this.windowEnd, num);
				if (num2 == num)
				{
					num2 += input.CopyBytes(this.window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyBytes(this.window, this.windowEnd, length);
			}
			this.windowEnd = (this.windowEnd + num2 & 32767);
			this.windowFilled += num2;
			return num2;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x00198384 File Offset: 0x00196584
		public void CopyDict(byte[] dictionary, int offset, int length)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (this.windowFilled > 0)
			{
				throw new InvalidOperationException();
			}
			if (length > 32768)
			{
				offset += length - 32768;
				length = 32768;
			}
			Array.Copy(dictionary, offset, this.window, 0, length);
			this.windowEnd = (length & 32767);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x00026CC0 File Offset: 0x00024EC0
		public int GetFreeSpace()
		{
			return 32768 - this.windowFilled;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x00026CCE File Offset: 0x00024ECE
		public int GetAvailable()
		{
			return this.windowFilled;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x001983E4 File Offset: 0x001965E4
		public int CopyOutput(byte[] output, int offset, int len)
		{
			int num = this.windowEnd;
			if (len > this.windowFilled)
			{
				len = this.windowFilled;
			}
			else
			{
				num = (this.windowEnd - this.windowFilled + len & 32767);
			}
			int num2 = len;
			int num3 = len - num;
			if (num3 > 0)
			{
				Array.Copy(this.window, 32768 - num3, output, offset, num3);
				offset += num3;
				len = num;
			}
			Array.Copy(this.window, num - len, output, offset, len);
			this.windowFilled -= num2;
			if (this.windowFilled < 0)
			{
				throw new InvalidOperationException();
			}
			return num2;
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x00198478 File Offset: 0x00196678
		public void Reset()
		{
			this.windowFilled = (this.windowEnd = 0);
		}

		// Token: 0x0400303E RID: 12350
		private const int WindowSize = 32768;

		// Token: 0x0400303F RID: 12351
		private const int WindowMask = 32767;

		// Token: 0x04003040 RID: 12352
		private byte[] window = new byte[32768];

		// Token: 0x04003041 RID: 12353
		private int windowEnd;

		// Token: 0x04003042 RID: 12354
		private int windowFilled;
	}
}
