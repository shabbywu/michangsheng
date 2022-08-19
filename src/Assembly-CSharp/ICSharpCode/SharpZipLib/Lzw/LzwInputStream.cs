using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Lzw
{
	// Token: 0x0200056B RID: 1387
	public class LzwInputStream : Stream
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x0014F86E File Offset: 0x0014DA6E
		// (set) Token: 0x06002DCA RID: 11722 RVA: 0x0014F876 File Offset: 0x0014DA76
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x06002DCB RID: 11723 RVA: 0x0014F880 File Offset: 0x0014DA80
		public LzwInputStream(Stream baseInputStream)
		{
			this.baseInputStream = baseInputStream;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x0014F8CD File Offset: 0x0014DACD
		public override int ReadByte()
		{
			if (this.Read(this.one, 0, 1) == 1)
			{
				return (int)(this.one[0] & byte.MaxValue);
			}
			return -1;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x0014F8F0 File Offset: 0x0014DAF0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.headerParsed)
			{
				this.ParseHeader();
			}
			if (this.eof)
			{
				return 0;
			}
			int num = offset;
			int[] array = this.tabPrefix;
			byte[] array2 = this.tabSuffix;
			byte[] array3 = this.stack;
			int num2 = this.nBits;
			int num3 = this.maxCode;
			int num4 = this.maxMaxCode;
			int num5 = this.bitMask;
			int num6 = this.oldCode;
			byte b = this.finChar;
			int num7 = this.stackP;
			int num8 = this.freeEnt;
			byte[] array4 = this.data;
			int i = this.bitPos;
			int num9 = array3.Length - num7;
			if (num9 > 0)
			{
				int num10 = (num9 >= count) ? count : num9;
				Array.Copy(array3, num7, buffer, offset, num10);
				offset += num10;
				count -= num10;
				num7 += num10;
			}
			if (count == 0)
			{
				this.stackP = num7;
				return offset - num;
			}
			int j;
			for (;;)
			{
				IL_C6:
				if (this.end < 64)
				{
					this.Fill();
				}
				int num11 = (this.got > 0) ? (this.end - this.end % num2 << 3) : ((this.end << 3) - (num2 - 1));
				while (i < num11)
				{
					if (count == 0)
					{
						goto Block_8;
					}
					if (num8 > num3)
					{
						int num12 = num2 << 3;
						i = i - 1 + num12 - (i - 1 + num12) % num12;
						num2++;
						num3 = ((num2 == this.maxBits) ? num4 : ((1 << num2) - 1));
						num5 = (1 << num2) - 1;
						i = this.ResetBuf(i);
						goto IL_C6;
					}
					int num13 = i >> 3;
					j = (((int)(array4[num13] & byte.MaxValue) | (int)(array4[num13 + 1] & byte.MaxValue) << 8 | (int)(array4[num13 + 2] & byte.MaxValue) << 16) >> (i & 7) & num5);
					i += num2;
					if (num6 == -1)
					{
						if (j >= 256)
						{
							goto Block_12;
						}
						b = (byte)(num6 = j);
						buffer[offset++] = b;
						count--;
					}
					else
					{
						if (j == 256 && this.blockMode)
						{
							Array.Copy(this.zeros, 0, array, 0, this.zeros.Length);
							num8 = 256;
							int num14 = num2 << 3;
							i = i - 1 + num14 - (i - 1 + num14) % num14;
							num2 = 9;
							num3 = (1 << num2) - 1;
							num5 = num3;
							i = this.ResetBuf(i);
							goto IL_C6;
						}
						int num15 = j;
						num7 = array3.Length;
						if (j >= num8)
						{
							if (j > num8)
							{
								goto Block_16;
							}
							array3[--num7] = b;
							j = num6;
						}
						while (j >= 256)
						{
							array3[--num7] = array2[j];
							j = array[j];
						}
						b = array2[j];
						buffer[offset++] = b;
						count--;
						num9 = array3.Length - num7;
						int num16 = (num9 >= count) ? count : num9;
						Array.Copy(array3, num7, buffer, offset, num16);
						offset += num16;
						count -= num16;
						num7 += num16;
						if (num8 < num4)
						{
							array[num8] = num6;
							array2[num8] = b;
							num8++;
						}
						num6 = num15;
						if (count == 0)
						{
							goto Block_20;
						}
					}
				}
				i = this.ResetBuf(i);
				if (this.got <= 0)
				{
					goto Block_22;
				}
			}
			Block_8:
			this.nBits = num2;
			this.maxCode = num3;
			this.maxMaxCode = num4;
			this.bitMask = num5;
			this.oldCode = num6;
			this.finChar = b;
			this.stackP = num7;
			this.freeEnt = num8;
			this.bitPos = i;
			return offset - num;
			Block_12:
			throw new LzwException("corrupt input: " + j + " > 255");
			Block_16:
			throw new LzwException(string.Concat(new object[]
			{
				"corrupt input: code=",
				j,
				", freeEnt=",
				num8
			}));
			Block_20:
			this.nBits = num2;
			this.maxCode = num3;
			this.bitMask = num5;
			this.oldCode = num6;
			this.finChar = b;
			this.stackP = num7;
			this.freeEnt = num8;
			this.bitPos = i;
			return offset - num;
			Block_22:
			this.nBits = num2;
			this.maxCode = num3;
			this.bitMask = num5;
			this.oldCode = num6;
			this.finChar = b;
			this.stackP = num7;
			this.freeEnt = num8;
			this.bitPos = i;
			this.eof = true;
			return offset - num;
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x0014FD30 File Offset: 0x0014DF30
		private int ResetBuf(int bitPosition)
		{
			int num = bitPosition >> 3;
			Array.Copy(this.data, num, this.data, 0, this.end - num);
			this.end -= num;
			return 0;
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x0014FD6C File Offset: 0x0014DF6C
		private void Fill()
		{
			this.got = this.baseInputStream.Read(this.data, this.end, this.data.Length - 1 - this.end);
			if (this.got > 0)
			{
				this.end += this.got;
			}
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x0014FDC4 File Offset: 0x0014DFC4
		private void ParseHeader()
		{
			this.headerParsed = true;
			byte[] array = new byte[3];
			if (this.baseInputStream.Read(array, 0, array.Length) < 0)
			{
				throw new LzwException("Failed to read LZW header");
			}
			if (array[0] != 31 || array[1] != 157)
			{
				throw new LzwException(string.Format("Wrong LZW header. Magic bytes don't match. 0x{0:x2} 0x{1:x2}", array[0], array[1]));
			}
			this.blockMode = ((array[2] & 128) > 0);
			this.maxBits = (int)(array[2] & 31);
			if (this.maxBits > 16)
			{
				throw new LzwException(string.Concat(new object[]
				{
					"Stream compressed with ",
					this.maxBits,
					" bits, but decompression can only handle ",
					16,
					" bits."
				}));
			}
			if ((array[2] & 96) > 0)
			{
				throw new LzwException("Unsupported bits set in the header.");
			}
			this.maxMaxCode = 1 << this.maxBits;
			this.nBits = 9;
			this.maxCode = (1 << this.nBits) - 1;
			this.bitMask = this.maxCode;
			this.oldCode = -1;
			this.finChar = 0;
			this.freeEnt = (this.blockMode ? 257 : 256);
			this.tabPrefix = new int[1 << this.maxBits];
			this.tabSuffix = new byte[1 << this.maxBits];
			this.stack = new byte[1 << this.maxBits];
			this.stackP = this.stack.Length;
			for (int i = 255; i >= 0; i--)
			{
				this.tabSuffix[i] = (byte)i;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x0014FF75 File Offset: 0x0014E175
		public override bool CanRead
		{
			get
			{
				return this.baseInputStream.CanRead;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x0014FF82 File Offset: 0x0014E182
		public override long Length
		{
			get
			{
				return (long)this.got;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x0014FF8B File Offset: 0x0014E18B
		// (set) Token: 0x06002DD6 RID: 11734 RVA: 0x0014C0FA File Offset: 0x0014A2FA
		public override long Position
		{
			get
			{
				return this.baseInputStream.Position;
			}
			set
			{
				throw new NotSupportedException("InflaterInputStream Position not supported");
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x0014FF98 File Offset: 0x0014E198
		public override void Flush()
		{
			this.baseInputStream.Flush();
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0014C113 File Offset: 0x0014A313
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek not supported");
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x0014C11F File Offset: 0x0014A31F
		public override void SetLength(long value)
		{
			throw new NotSupportedException("InflaterInputStream SetLength not supported");
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x0014C12B File Offset: 0x0014A32B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("InflaterInputStream Write not supported");
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x0014C137 File Offset: 0x0014A337
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("InflaterInputStream WriteByte not supported");
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x0014FFA5 File Offset: 0x0014E1A5
		protected override void Dispose(bool disposing)
		{
			if (!this.isClosed)
			{
				this.isClosed = true;
				if (this.IsStreamOwner)
				{
					this.baseInputStream.Dispose();
				}
			}
		}

		// Token: 0x04002890 RID: 10384
		private Stream baseInputStream;

		// Token: 0x04002891 RID: 10385
		private bool isClosed;

		// Token: 0x04002892 RID: 10386
		private readonly byte[] one = new byte[1];

		// Token: 0x04002893 RID: 10387
		private bool headerParsed;

		// Token: 0x04002894 RID: 10388
		private const int TBL_CLEAR = 256;

		// Token: 0x04002895 RID: 10389
		private const int TBL_FIRST = 257;

		// Token: 0x04002896 RID: 10390
		private int[] tabPrefix;

		// Token: 0x04002897 RID: 10391
		private byte[] tabSuffix;

		// Token: 0x04002898 RID: 10392
		private readonly int[] zeros = new int[256];

		// Token: 0x04002899 RID: 10393
		private byte[] stack;

		// Token: 0x0400289A RID: 10394
		private bool blockMode;

		// Token: 0x0400289B RID: 10395
		private int nBits;

		// Token: 0x0400289C RID: 10396
		private int maxBits;

		// Token: 0x0400289D RID: 10397
		private int maxMaxCode;

		// Token: 0x0400289E RID: 10398
		private int maxCode;

		// Token: 0x0400289F RID: 10399
		private int bitMask;

		// Token: 0x040028A0 RID: 10400
		private int oldCode;

		// Token: 0x040028A1 RID: 10401
		private byte finChar;

		// Token: 0x040028A2 RID: 10402
		private int stackP;

		// Token: 0x040028A3 RID: 10403
		private int freeEnt;

		// Token: 0x040028A4 RID: 10404
		private readonly byte[] data = new byte[8192];

		// Token: 0x040028A5 RID: 10405
		private int bitPos;

		// Token: 0x040028A6 RID: 10406
		private int end;

		// Token: 0x040028A7 RID: 10407
		private int got;

		// Token: 0x040028A8 RID: 10408
		private bool eof;

		// Token: 0x040028A9 RID: 10409
		private const int EXTRA = 64;
	}
}
