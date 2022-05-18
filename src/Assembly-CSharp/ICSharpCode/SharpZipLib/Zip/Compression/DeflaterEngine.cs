using System;
using ICSharpCode.SharpZipLib.Checksum;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x020007F7 RID: 2039
	public class DeflaterEngine
	{
		// Token: 0x0600347C RID: 13436 RVA: 0x000265B6 File Offset: 0x000247B6
		public DeflaterEngine(DeflaterPending pending) : this(pending, false)
		{
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x00194800 File Offset: 0x00192A00
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

		// Token: 0x0600347E RID: 13438 RVA: 0x00194874 File Offset: 0x00192A74
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

		// Token: 0x0600347F RID: 13439 RVA: 0x001948F4 File Offset: 0x00192AF4
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

		// Token: 0x06003480 RID: 13440 RVA: 0x000265C0 File Offset: 0x000247C0
		public bool NeedsInput()
		{
			return this.inputEnd == this.inputOff;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x00194974 File Offset: 0x00192B74
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

		// Token: 0x06003482 RID: 13442 RVA: 0x00194A14 File Offset: 0x00192C14
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

		// Token: 0x06003483 RID: 13443 RVA: 0x000265D0 File Offset: 0x000247D0
		public void ResetAdler()
		{
			Adler32 adler = this.adler;
			if (adler == null)
			{
				return;
			}
			adler.Reset();
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000265E2 File Offset: 0x000247E2
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

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000265FA File Offset: 0x000247FA
		public long TotalIn
		{
			get
			{
				return this.totalIn;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x00026602 File Offset: 0x00024802
		// (set) Token: 0x06003487 RID: 13447 RVA: 0x0002660A File Offset: 0x0002480A
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

		// Token: 0x06003488 RID: 13448 RVA: 0x00194A9C File Offset: 0x00192C9C
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

		// Token: 0x06003489 RID: 13449 RVA: 0x00194C34 File Offset: 0x00192E34
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

		// Token: 0x0600348A RID: 13450 RVA: 0x00026613 File Offset: 0x00024813
		private void UpdateHash()
		{
			this.ins_h = ((int)this.window[this.strstart] << 5 ^ (int)this.window[this.strstart + 1]);
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x00194D2C File Offset: 0x00192F2C
		private int InsertString()
		{
			int num = (this.ins_h << 5 ^ (int)this.window[this.strstart + 2]) & 32767;
			short num2 = this.prev[this.strstart & 32767] = this.head[num];
			this.head[num] = (short)this.strstart;
			this.ins_h = num;
			return (int)num2 & 65535;
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x00194D94 File Offset: 0x00192F94
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

		// Token: 0x0600348D RID: 13453 RVA: 0x00194E68 File Offset: 0x00193068
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

		// Token: 0x0600348E RID: 13454 RVA: 0x0019530C File Offset: 0x0019350C
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

		// Token: 0x0600348F RID: 13455 RVA: 0x001953BC File Offset: 0x001935BC
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

		// Token: 0x06003490 RID: 13456 RVA: 0x001955D0 File Offset: 0x001937D0
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

		// Token: 0x04002FB0 RID: 12208
		private const int TooFar = 4096;

		// Token: 0x04002FB1 RID: 12209
		private int ins_h;

		// Token: 0x04002FB2 RID: 12210
		private short[] head;

		// Token: 0x04002FB3 RID: 12211
		private short[] prev;

		// Token: 0x04002FB4 RID: 12212
		private int matchStart;

		// Token: 0x04002FB5 RID: 12213
		private int matchLen;

		// Token: 0x04002FB6 RID: 12214
		private bool prevAvailable;

		// Token: 0x04002FB7 RID: 12215
		private int blockStart;

		// Token: 0x04002FB8 RID: 12216
		private int strstart;

		// Token: 0x04002FB9 RID: 12217
		private int lookahead;

		// Token: 0x04002FBA RID: 12218
		private byte[] window;

		// Token: 0x04002FBB RID: 12219
		private DeflateStrategy strategy;

		// Token: 0x04002FBC RID: 12220
		private int max_chain;

		// Token: 0x04002FBD RID: 12221
		private int max_lazy;

		// Token: 0x04002FBE RID: 12222
		private int niceLength;

		// Token: 0x04002FBF RID: 12223
		private int goodLength;

		// Token: 0x04002FC0 RID: 12224
		private int compressionFunction;

		// Token: 0x04002FC1 RID: 12225
		private byte[] inputBuf;

		// Token: 0x04002FC2 RID: 12226
		private long totalIn;

		// Token: 0x04002FC3 RID: 12227
		private int inputOff;

		// Token: 0x04002FC4 RID: 12228
		private int inputEnd;

		// Token: 0x04002FC5 RID: 12229
		private DeflaterPending pending;

		// Token: 0x04002FC6 RID: 12230
		private DeflaterHuffman huffman;

		// Token: 0x04002FC7 RID: 12231
		private Adler32 adler;
	}
}
