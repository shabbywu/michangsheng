using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200055D RID: 1373
	public class OutputWindow
	{
		// Token: 0x06002CB8 RID: 11448 RVA: 0x0014C1E0 File Offset: 0x0014A3E0
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

		// Token: 0x06002CB9 RID: 11449 RVA: 0x0014C23C File Offset: 0x0014A43C
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

		// Token: 0x06002CBA RID: 11450 RVA: 0x0014C294 File Offset: 0x0014A494
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

		// Token: 0x06002CBB RID: 11451 RVA: 0x0014C34C File Offset: 0x0014A54C
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

		// Token: 0x06002CBC RID: 11452 RVA: 0x0014C3F0 File Offset: 0x0014A5F0
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

		// Token: 0x06002CBD RID: 11453 RVA: 0x0014C450 File Offset: 0x0014A650
		public int GetFreeSpace()
		{
			return 32768 - this.windowFilled;
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x0014C45E File Offset: 0x0014A65E
		public int GetAvailable()
		{
			return this.windowFilled;
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x0014C468 File Offset: 0x0014A668
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

		// Token: 0x06002CC0 RID: 11456 RVA: 0x0014C4FC File Offset: 0x0014A6FC
		public void Reset()
		{
			this.windowFilled = (this.windowEnd = 0);
		}

		// Token: 0x04002805 RID: 10245
		private const int WindowSize = 32768;

		// Token: 0x04002806 RID: 10246
		private const int WindowMask = 32767;

		// Token: 0x04002807 RID: 10247
		private byte[] window = new byte[32768];

		// Token: 0x04002808 RID: 10248
		private int windowEnd;

		// Token: 0x04002809 RID: 10249
		private int windowFilled;
	}
}
