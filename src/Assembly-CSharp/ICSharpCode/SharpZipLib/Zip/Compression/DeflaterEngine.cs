using System;
using ICSharpCode.SharpZipLib.Checksum;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000553 RID: 1363
	public class DeflaterEngine
	{
		// Token: 0x06002C21 RID: 11297 RVA: 0x00148D88 File Offset: 0x00146F88
		public DeflaterEngine(DeflaterPending pending) : this(pending, false)
		{
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x00148D94 File Offset: 0x00146F94
		public DeflaterEngine(DeflaterPending pending, bool noAdlerCalculation)
		{
			this.pending = pending;
			this.huffman = new DeflaterHuffman(pending);
			if (!noAdlerCalculation)
			{
				this.adler = new Adler32();
			}
			this.window = new byte[65536];
			this.head = new short[32768];
			this.prev = new short[32768];
			this.blockStart = (this.strstart = 1);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x00148E08 File Offset: 0x00147008
		public bool Deflate(bool flush, bool finish)
		{
			for (;;)
			{
				this.FillWindow();
				bool flush2 = flush && this.inputOff == this.inputEnd;
				bool flag;
				switch (this.compressionFunction)
				{
				case 0:
					flag = this.DeflateStored(flush2, finish);
					goto IL_62;
				case 1:
					flag = this.DeflateFast(flush2, finish);
					goto IL_62;
				case 2:
					flag = this.DeflateSlow(flush2, finish);
					goto IL_62;
				}
				break;
				IL_62:
				if (!this.pending.IsFlushed || !flag)
				{
					return flag;
				}
			}
			throw new InvalidOperationException("unknown compressionFunction");
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x00148E88 File Offset: 0x00147088
		public void SetInput(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.inputOff < this.inputEnd)
			{
				throw new InvalidOperationException("Old input was not completely processed");
			}
			int num = offset + count;
			if (offset > num || num > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.inputBuf = buffer;
			this.inputOff = offset;
			this.inputEnd = num;
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x00148F08 File Offset: 0x00147108
		public bool NeedsInput()
		{
			return this.inputEnd == this.inputOff;
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x00148F18 File Offset: 0x00147118
		public void SetDictionary(byte[] buffer, int offset, int length)
		{
			Adler32 adler = this.adler;
			if (adler != null)
			{
				adler.Update(new ArraySegment<byte>(buffer, offset, length));
			}
			if (length < 3)
			{
				return;
			}
			if (length > 32506)
			{
				offset += length - 32506;
				length = 32506;
			}
			Array.Copy(buffer, offset, this.window, this.strstart, length);
			this.UpdateHash();
			length--;
			while (--length > 0)
			{
				this.InsertString();
				this.strstart++;
			}
			this.strstart += 2;
			this.blockStart = this.strstart;
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x00148FB8 File Offset: 0x001471B8
		public void Reset()
		{
			this.huffman.Reset();
			Adler32 adler = this.adler;
			if (adler != null)
			{
				adler.Reset();
			}
			this.blockStart = (this.strstart = 1);
			this.lookahead = 0;
			this.totalIn = 0L;
			this.prevAvailable = false;
			this.matchLen = 2;
			for (int i = 0; i < 32768; i++)
			{
				this.head[i] = 0;
			}
			for (int j = 0; j < 32768; j++)
			{
				this.prev[j] = 0;
			}
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x00149040 File Offset: 0x00147240
		public void ResetAdler()
		{
			Adler32 adler = this.adler;
			if (adler == null)
			{
				return;
			}
			adler.Reset();
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x00149052 File Offset: 0x00147252
		public int Adler
		{
			get
			{
				if (this.adler == null)
				{
					return 0;
				}
				return (int)this.adler.Value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x0014906A File Offset: 0x0014726A
		public long TotalIn
		{
			get
			{
				return this.totalIn;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x00149072 File Offset: 0x00147272
		// (set) Token: 0x06002C2C RID: 11308 RVA: 0x0014907A File Offset: 0x0014727A
		public DeflateStrategy Strategy
		{
			get
			{
				return this.strategy;
			}
			set
			{
				this.strategy = value;
			}
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x00149084 File Offset: 0x00147284
		public void SetLevel(int level)
		{
			if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.goodLength = DeflaterConstants.GOOD_LENGTH[level];
			this.max_lazy = DeflaterConstants.MAX_LAZY[level];
			this.niceLength = DeflaterConstants.NICE_LENGTH[level];
			this.max_chain = DeflaterConstants.MAX_CHAIN[level];
			if (DeflaterConstants.COMPR_FUNC[level] != this.compressionFunction)
			{
				switch (this.compressionFunction)
				{
				case 0:
					if (this.strstart > this.blockStart)
					{
						this.huffman.FlushStoredBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
						this.blockStart = this.strstart;
					}
					this.UpdateHash();
					break;
				case 1:
					if (this.strstart > this.blockStart)
					{
						this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
						this.blockStart = this.strstart;
					}
					break;
				case 2:
					if (this.prevAvailable)
					{
						this.huffman.TallyLit((int)(this.window[this.strstart - 1] & byte.MaxValue));
					}
					if (this.strstart > this.blockStart)
					{
						this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
						this.blockStart = this.strstart;
					}
					this.prevAvailable = false;
					this.matchLen = 2;
					break;
				}
				this.compressionFunction = DeflaterConstants.COMPR_FUNC[level];
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x0014921C File Offset: 0x0014741C
		public void FillWindow()
		{
			if (this.strstart >= 65274)
			{
				this.SlideWindow();
			}
			if (this.lookahead < 262 && this.inputOff < this.inputEnd)
			{
				int num = 65536 - this.lookahead - this.strstart;
				if (num > this.inputEnd - this.inputOff)
				{
					num = this.inputEnd - this.inputOff;
				}
				Array.Copy(this.inputBuf, this.inputOff, this.window, this.strstart + this.lookahead, num);
				Adler32 adler = this.adler;
				if (adler != null)
				{
					adler.Update(new ArraySegment<byte>(this.inputBuf, this.inputOff, num));
				}
				this.inputOff += num;
				this.totalIn += (long)num;
				this.lookahead += num;
			}
			if (this.lookahead >= 3)
			{
				this.UpdateHash();
			}
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x00149311 File Offset: 0x00147511
		private void UpdateHash()
		{
			this.ins_h = ((int)this.window[this.strstart] << 5 ^ (int)this.window[this.strstart + 1]);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x00149338 File Offset: 0x00147538
		private int InsertString()
		{
			int num = (this.ins_h << 5 ^ (int)this.window[this.strstart + 2]) & 32767;
			short num2 = this.prev[this.strstart & 32767] = this.head[num];
			this.head[num] = (short)this.strstart;
			this.ins_h = num;
			return (int)num2 & 65535;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x001493A0 File Offset: 0x001475A0
		private void SlideWindow()
		{
			Array.Copy(this.window, 32768, this.window, 0, 32768);
			this.matchStart -= 32768;
			this.strstart -= 32768;
			this.blockStart -= 32768;
			for (int i = 0; i < 32768; i++)
			{
				int num = (int)this.head[i] & 65535;
				this.head[i] = (short)((num >= 32768) ? (num - 32768) : 0);
			}
			for (int j = 0; j < 32768; j++)
			{
				int num2 = (int)this.prev[j] & 65535;
				this.prev[j] = (short)((num2 >= 32768) ? (num2 - 32768) : 0);
			}
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x00149474 File Offset: 0x00147674
		private bool FindLongestMatch(int curMatch)
		{
			int num = this.strstart;
			int num2 = num + Math.Min(258, this.lookahead) - 1;
			int num3 = Math.Max(num - 32506, 0);
			byte[] array = this.window;
			short[] array2 = this.prev;
			int num4 = this.max_chain;
			int num5 = Math.Min(this.niceLength, this.lookahead);
			this.matchLen = Math.Max(this.matchLen, 2);
			if (num + this.matchLen > num2)
			{
				return false;
			}
			byte b = array[num + this.matchLen - 1];
			byte b2 = array[num + this.matchLen];
			if (this.matchLen >= this.goodLength)
			{
				num4 >>= 2;
			}
			do
			{
				int num6 = curMatch;
				num = this.strstart;
				if (array[num6 + this.matchLen] == b2 && array[num6 + this.matchLen - 1] == b && array[num6] == array[num] && array[++num6] == array[++num])
				{
					switch ((num2 - num) % 8)
					{
					case 1:
						if (array[++num] == array[++num6])
						{
						}
						break;
					case 2:
						if (array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 3:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 4:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 5:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 6:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 7:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
							byte b3 = array[++num];
							byte b4 = array[++num6];
						}
						break;
					}
					if (array[num] == array[num6])
					{
						while (num != num2)
						{
							if (array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6])
							{
								goto IL_42C;
							}
						}
						num++;
						num6++;
					}
					IL_42C:
					if (num - this.strstart > this.matchLen)
					{
						this.matchStart = curMatch;
						this.matchLen = num - this.strstart;
						if (this.matchLen >= num5)
						{
							break;
						}
						b = array[num - 1];
						b2 = array[num];
					}
				}
			}
			while ((curMatch = ((int)array2[curMatch & 32767] & 65535)) > num3 && --num4 != 0);
			return this.matchLen >= 3;
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x00149918 File Offset: 0x00147B18
		private bool DeflateStored(bool flush, bool finish)
		{
			if (!flush && this.lookahead == 0)
			{
				return false;
			}
			this.strstart += this.lookahead;
			this.lookahead = 0;
			int num = this.strstart - this.blockStart;
			if (num >= DeflaterConstants.MAX_BLOCK_SIZE || (this.blockStart < 32768 && num >= 32506) || flush)
			{
				bool flag = finish;
				if (num > DeflaterConstants.MAX_BLOCK_SIZE)
				{
					num = DeflaterConstants.MAX_BLOCK_SIZE;
					flag = false;
				}
				this.huffman.FlushStoredBlock(this.window, this.blockStart, num, flag);
				this.blockStart += num;
				return !flag && num != 0;
			}
			return true;
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x001499C8 File Offset: 0x00147BC8
		private bool DeflateFast(bool flush, bool finish)
		{
			if (this.lookahead < 262 && !flush)
			{
				return false;
			}
			while (this.lookahead >= 262 || flush)
			{
				if (this.lookahead == 0)
				{
					this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
					this.blockStart = this.strstart;
					return false;
				}
				if (this.strstart > 65274)
				{
					this.SlideWindow();
				}
				int num;
				if (this.lookahead >= 3 && (num = this.InsertString()) != 0 && this.strategy != DeflateStrategy.HuffmanOnly && this.strstart - num <= 32506 && this.FindLongestMatch(num))
				{
					bool flag = this.huffman.TallyDist(this.strstart - this.matchStart, this.matchLen);
					this.lookahead -= this.matchLen;
					if (this.matchLen <= this.max_lazy && this.lookahead >= 3)
					{
						for (;;)
						{
							int num2 = this.matchLen - 1;
							this.matchLen = num2;
							if (num2 <= 0)
							{
								break;
							}
							this.strstart++;
							this.InsertString();
						}
						this.strstart++;
					}
					else
					{
						this.strstart += this.matchLen;
						if (this.lookahead >= 2)
						{
							this.UpdateHash();
						}
					}
					this.matchLen = 2;
					if (!flag)
					{
						continue;
					}
				}
				else
				{
					this.huffman.TallyLit((int)(this.window[this.strstart] & byte.MaxValue));
					this.strstart++;
					this.lookahead--;
				}
				if (this.huffman.IsFull())
				{
					bool flag2 = finish && this.lookahead == 0;
					this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, flag2);
					this.blockStart = this.strstart;
					return !flag2;
				}
			}
			return true;
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x00149BDC File Offset: 0x00147DDC
		private bool DeflateSlow(bool flush, bool finish)
		{
			if (this.lookahead < 262 && !flush)
			{
				return false;
			}
			while (this.lookahead >= 262 || flush)
			{
				if (this.lookahead == 0)
				{
					if (this.prevAvailable)
					{
						this.huffman.TallyLit((int)(this.window[this.strstart - 1] & byte.MaxValue));
					}
					this.prevAvailable = false;
					this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
					this.blockStart = this.strstart;
					return false;
				}
				if (this.strstart >= 65274)
				{
					this.SlideWindow();
				}
				int num = this.matchStart;
				int num2 = this.matchLen;
				if (this.lookahead >= 3)
				{
					int num3 = this.InsertString();
					if (this.strategy != DeflateStrategy.HuffmanOnly && num3 != 0 && this.strstart - num3 <= 32506 && this.FindLongestMatch(num3) && this.matchLen <= 5 && (this.strategy == DeflateStrategy.Filtered || (this.matchLen == 3 && this.strstart - this.matchStart > 4096)))
					{
						this.matchLen = 2;
					}
				}
				if (num2 >= 3 && this.matchLen <= num2)
				{
					this.huffman.TallyDist(this.strstart - 1 - num, num2);
					num2 -= 2;
					do
					{
						this.strstart++;
						this.lookahead--;
						if (this.lookahead >= 3)
						{
							this.InsertString();
						}
					}
					while (--num2 > 0);
					this.strstart++;
					this.lookahead--;
					this.prevAvailable = false;
					this.matchLen = 2;
				}
				else
				{
					if (this.prevAvailable)
					{
						this.huffman.TallyLit((int)(this.window[this.strstart - 1] & byte.MaxValue));
					}
					this.prevAvailable = true;
					this.strstart++;
					this.lookahead--;
				}
				if (this.huffman.IsFull())
				{
					int num4 = this.strstart - this.blockStart;
					if (this.prevAvailable)
					{
						num4--;
					}
					bool flag = finish && this.lookahead == 0 && !this.prevAvailable;
					this.huffman.FlushBlock(this.window, this.blockStart, num4, flag);
					this.blockStart += num4;
					return !flag;
				}
			}
			return true;
		}

		// Token: 0x04002788 RID: 10120
		private const int TooFar = 4096;

		// Token: 0x04002789 RID: 10121
		private int ins_h;

		// Token: 0x0400278A RID: 10122
		private short[] head;

		// Token: 0x0400278B RID: 10123
		private short[] prev;

		// Token: 0x0400278C RID: 10124
		private int matchStart;

		// Token: 0x0400278D RID: 10125
		private int matchLen;

		// Token: 0x0400278E RID: 10126
		private bool prevAvailable;

		// Token: 0x0400278F RID: 10127
		private int blockStart;

		// Token: 0x04002790 RID: 10128
		private int strstart;

		// Token: 0x04002791 RID: 10129
		private int lookahead;

		// Token: 0x04002792 RID: 10130
		private byte[] window;

		// Token: 0x04002793 RID: 10131
		private DeflateStrategy strategy;

		// Token: 0x04002794 RID: 10132
		private int max_chain;

		// Token: 0x04002795 RID: 10133
		private int max_lazy;

		// Token: 0x04002796 RID: 10134
		private int niceLength;

		// Token: 0x04002797 RID: 10135
		private int goodLength;

		// Token: 0x04002798 RID: 10136
		private int compressionFunction;

		// Token: 0x04002799 RID: 10137
		private byte[] inputBuf;

		// Token: 0x0400279A RID: 10138
		private long totalIn;

		// Token: 0x0400279B RID: 10139
		private int inputOff;

		// Token: 0x0400279C RID: 10140
		private int inputEnd;

		// Token: 0x0400279D RID: 10141
		private DeflaterPending pending;

		// Token: 0x0400279E RID: 10142
		private DeflaterHuffman huffman;

		// Token: 0x0400279F RID: 10143
		private Adler32 adler;
	}
}
