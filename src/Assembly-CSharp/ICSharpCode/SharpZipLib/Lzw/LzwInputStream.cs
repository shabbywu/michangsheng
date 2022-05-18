using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Lzw
{
	// Token: 0x02000813 RID: 2067
	public class LzwInputStream : Stream
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x00027994 File Offset: 0x00025B94
		// (set) Token: 0x06003640 RID: 13888 RVA: 0x0002799C File Offset: 0x00025B9C
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x06003641 RID: 13889 RVA: 0x0019AB54 File Offset: 0x00198D54
		public LzwInputStream(Stream baseInputStream)
		{
			this.baseInputStream = baseInputStream;
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000279A5 File Offset: 0x00025BA5
		public override int ReadByte()
		{
			if (this.Read(this.one, 0, 1) == 1)
			{
				return (int)(this.one[0] & byte.MaxValue);
			}
			return -1;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x0019ABA4 File Offset: 0x00198DA4
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

		// Token: 0x06003644 RID: 13892 RVA: 0x0019AFE4 File Offset: 0x001991E4
		private int ResetBuf(int bitPosition)
		{
			int num = bitPosition >> 3;
			Array.Copy(this.data, num, this.data, 0, this.end - num);
			this.end -= num;
			return 0;
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x0019B020 File Offset: 0x00199220
		private void Fill()
		{
			this.got = this.baseInputStream.Read(this.data, this.end, this.data.Length - 1 - this.end);
			if (this.got > 0)
			{
				this.end += this.got;
			}
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x0019B078 File Offset: 0x00199278
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

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x000279C8 File Offset: 0x00025BC8
		public override bool CanRead
		{
			get
			{
				return this.baseInputStream.CanRead;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x000279D5 File Offset: 0x00025BD5
		public override long Length
		{
			get
			{
				return (long)this.got;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000279DE File Offset: 0x00025BDE
		// (set) Token: 0x0600364C RID: 13900 RVA: 0x00026C53 File Offset: 0x00024E53
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

		// Token: 0x0600364D RID: 13901 RVA: 0x000279EB File Offset: 0x00025BEB
		public override void Flush()
		{
			this.baseInputStream.Flush();
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x00026C6C File Offset: 0x00024E6C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek not supported");
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x00026C78 File Offset: 0x00024E78
		public override void SetLength(long value)
		{
			throw new NotSupportedException("InflaterInputStream SetLength not supported");
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x00026C84 File Offset: 0x00024E84
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("InflaterInputStream Write not supported");
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x00026C90 File Offset: 0x00024E90
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("InflaterInputStream WriteByte not supported");
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000279F8 File Offset: 0x00025BF8
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

		// Token: 0x040030CA RID: 12490
		private Stream baseInputStream;

		// Token: 0x040030CB RID: 12491
		private bool isClosed;

		// Token: 0x040030CC RID: 12492
		private readonly byte[] one = new byte[1];

		// Token: 0x040030CD RID: 12493
		private bool headerParsed;

		// Token: 0x040030CE RID: 12494
		private const int TBL_CLEAR = 256;

		// Token: 0x040030CF RID: 12495
		private const int TBL_FIRST = 257;

		// Token: 0x040030D0 RID: 12496
		private int[] tabPrefix;

		// Token: 0x040030D1 RID: 12497
		private byte[] tabSuffix;

		// Token: 0x040030D2 RID: 12498
		private readonly int[] zeros = new int[256];

		// Token: 0x040030D3 RID: 12499
		private byte[] stack;

		// Token: 0x040030D4 RID: 12500
		private bool blockMode;

		// Token: 0x040030D5 RID: 12501
		private int nBits;

		// Token: 0x040030D6 RID: 12502
		private int maxBits;

		// Token: 0x040030D7 RID: 12503
		private int maxMaxCode;

		// Token: 0x040030D8 RID: 12504
		private int maxCode;

		// Token: 0x040030D9 RID: 12505
		private int bitMask;

		// Token: 0x040030DA RID: 12506
		private int oldCode;

		// Token: 0x040030DB RID: 12507
		private byte finChar;

		// Token: 0x040030DC RID: 12508
		private int stackP;

		// Token: 0x040030DD RID: 12509
		private int freeEnt;

		// Token: 0x040030DE RID: 12510
		private readonly byte[] data = new byte[8192];

		// Token: 0x040030DF RID: 12511
		private int bitPos;

		// Token: 0x040030E0 RID: 12512
		private int end;

		// Token: 0x040030E1 RID: 12513
		private int got;

		// Token: 0x040030E2 RID: 12514
		private bool eof;

		// Token: 0x040030E3 RID: 12515
		private const int EXTRA = 64;
	}
}
