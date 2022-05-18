using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x020007F3 RID: 2035
	public class Deflater
	{
		// Token: 0x06003467 RID: 13415 RVA: 0x00026444 File Offset: 0x00024644
		public Deflater() : this(-1, false)
		{
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x0002644E File Offset: 0x0002464E
		public Deflater(int level) : this(level, false)
		{
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x00194520 File Offset: 0x00192720
		public Deflater(int level, bool noZlibHeaderOrFooter)
		{
			if (level == -1)
			{
				level = 6;
			}
			else if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.pending = new DeflaterPending();
			this.engine = new DeflaterEngine(this.pending, noZlibHeaderOrFooter);
			this.noZlibHeaderOrFooter = noZlibHeaderOrFooter;
			this.SetStrategy(DeflateStrategy.Default);
			this.SetLevel(level);
			this.Reset();
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x00026458 File Offset: 0x00024658
		public void Reset()
		{
			this.state = (this.noZlibHeaderOrFooter ? 16 : 0);
			this.totalOut = 0L;
			this.pending.Reset();
			this.engine.Reset();
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x0002648B File Offset: 0x0002468B
		public int Adler
		{
			get
			{
				return this.engine.Adler;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x00026498 File Offset: 0x00024698
		public long TotalIn
		{
			get
			{
				return this.engine.TotalIn;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000264A5 File Offset: 0x000246A5
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000264AD File Offset: 0x000246AD
		public void Flush()
		{
			this.state |= 4;
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000264BD File Offset: 0x000246BD
		public void Finish()
		{
			this.state |= 12;
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000264CE File Offset: 0x000246CE
		public bool IsFinished
		{
			get
			{
				return this.state == 30 && this.pending.IsFlushed;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000264E7 File Offset: 0x000246E7
		public bool IsNeedingInput
		{
			get
			{
				return this.engine.NeedsInput();
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000264F4 File Offset: 0x000246F4
		public void SetInput(byte[] input)
		{
			this.SetInput(input, 0, input.Length);
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x00026501 File Offset: 0x00024701
		public void SetInput(byte[] input, int offset, int count)
		{
			if ((this.state & 8) != 0)
			{
				throw new InvalidOperationException("Finish() already called");
			}
			this.engine.SetInput(input, offset, count);
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x00026526 File Offset: 0x00024726
		public void SetLevel(int level)
		{
			if (level == -1)
			{
				level = 6;
			}
			else if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			if (this.level != level)
			{
				this.level = level;
				this.engine.SetLevel(level);
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x00026561 File Offset: 0x00024761
		public int GetLevel()
		{
			return this.level;
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x00026569 File Offset: 0x00024769
		public void SetStrategy(DeflateStrategy strategy)
		{
			this.engine.Strategy = strategy;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x00026577 File Offset: 0x00024777
		public int Deflate(byte[] output)
		{
			return this.Deflate(output, 0, output.Length);
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00194588 File Offset: 0x00192788
		public int Deflate(byte[] output, int offset, int length)
		{
			int num = length;
			if (this.state == 127)
			{
				throw new InvalidOperationException("Deflater closed");
			}
			if (this.state < 16)
			{
				int num2 = 30720;
				int num3 = this.level - 1 >> 1;
				if (num3 < 0 || num3 > 3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				if ((this.state & 1) != 0)
				{
					num2 |= 32;
				}
				num2 += 31 - num2 % 31;
				this.pending.WriteShortMSB(num2);
				if ((this.state & 1) != 0)
				{
					int adler = this.engine.Adler;
					this.engine.ResetAdler();
					this.pending.WriteShortMSB(adler >> 16);
					this.pending.WriteShortMSB(adler & 65535);
				}
				this.state = (16 | (this.state & 12));
			}
			for (;;)
			{
				int num4 = this.pending.Flush(output, offset, length);
				offset += num4;
				this.totalOut += (long)num4;
				length -= num4;
				if (length == 0 || this.state == 30)
				{
					goto IL_1D3;
				}
				if (!this.engine.Deflate((this.state & 4) != 0, (this.state & 8) != 0))
				{
					int num5 = this.state;
					if (num5 == 16)
					{
						break;
					}
					if (num5 != 20)
					{
						if (num5 == 28)
						{
							this.pending.AlignToByte();
							if (!this.noZlibHeaderOrFooter)
							{
								int adler2 = this.engine.Adler;
								this.pending.WriteShortMSB(adler2 >> 16);
								this.pending.WriteShortMSB(adler2 & 65535);
							}
							this.state = 30;
						}
					}
					else
					{
						if (this.level != 0)
						{
							for (int i = 8 + (-this.pending.BitCount & 7); i > 0; i -= 10)
							{
								this.pending.WriteBits(2, 10);
							}
						}
						this.state = 16;
					}
				}
			}
			return num - length;
			IL_1D3:
			return num - length;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x00026584 File Offset: 0x00024784
		public void SetDictionary(byte[] dictionary)
		{
			this.SetDictionary(dictionary, 0, dictionary.Length);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x00026591 File Offset: 0x00024791
		public void SetDictionary(byte[] dictionary, int index, int count)
		{
			if (this.state != 0)
			{
				throw new InvalidOperationException();
			}
			this.state = 1;
			this.engine.SetDictionary(dictionary, index, count);
		}

		// Token: 0x04002F76 RID: 12150
		public const int BEST_COMPRESSION = 9;

		// Token: 0x04002F77 RID: 12151
		public const int BEST_SPEED = 1;

		// Token: 0x04002F78 RID: 12152
		public const int DEFAULT_COMPRESSION = -1;

		// Token: 0x04002F79 RID: 12153
		public const int NO_COMPRESSION = 0;

		// Token: 0x04002F7A RID: 12154
		public const int DEFLATED = 8;

		// Token: 0x04002F7B RID: 12155
		private const int IS_SETDICT = 1;

		// Token: 0x04002F7C RID: 12156
		private const int IS_FLUSHING = 4;

		// Token: 0x04002F7D RID: 12157
		private const int IS_FINISHING = 8;

		// Token: 0x04002F7E RID: 12158
		private const int INIT_STATE = 0;

		// Token: 0x04002F7F RID: 12159
		private const int SETDICT_STATE = 1;

		// Token: 0x04002F80 RID: 12160
		private const int BUSY_STATE = 16;

		// Token: 0x04002F81 RID: 12161
		private const int FLUSHING_STATE = 20;

		// Token: 0x04002F82 RID: 12162
		private const int FINISHING_STATE = 28;

		// Token: 0x04002F83 RID: 12163
		private const int FINISHED_STATE = 30;

		// Token: 0x04002F84 RID: 12164
		private const int CLOSED_STATE = 127;

		// Token: 0x04002F85 RID: 12165
		private int level;

		// Token: 0x04002F86 RID: 12166
		private bool noZlibHeaderOrFooter;

		// Token: 0x04002F87 RID: 12167
		private int state;

		// Token: 0x04002F88 RID: 12168
		private long totalOut;

		// Token: 0x04002F89 RID: 12169
		private DeflaterPending pending;

		// Token: 0x04002F8A RID: 12170
		private DeflaterEngine engine;

		// Token: 0x020007F4 RID: 2036
		public enum CompressionLevel
		{
			// Token: 0x04002F8C RID: 12172
			BEST_COMPRESSION = 9,
			// Token: 0x04002F8D RID: 12173
			BEST_SPEED = 1,
			// Token: 0x04002F8E RID: 12174
			DEFAULT_COMPRESSION = -1,
			// Token: 0x04002F8F RID: 12175
			NO_COMPRESSION,
			// Token: 0x04002F90 RID: 12176
			DEFLATED = 8
		}
	}
}
