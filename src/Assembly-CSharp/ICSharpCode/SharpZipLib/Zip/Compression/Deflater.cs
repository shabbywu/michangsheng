using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000550 RID: 1360
	public class Deflater
	{
		// Token: 0x06002C0C RID: 11276 RVA: 0x00148933 File Offset: 0x00146B33
		public Deflater() : this(-1, false)
		{
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x0014893D File Offset: 0x00146B3D
		public Deflater(int level) : this(level, false)
		{
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x00148948 File Offset: 0x00146B48
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

		// Token: 0x06002C0F RID: 11279 RVA: 0x001489B0 File Offset: 0x00146BB0
		public void Reset()
		{
			this.state = (this.noZlibHeaderOrFooter ? 16 : 0);
			this.totalOut = 0L;
			this.pending.Reset();
			this.engine.Reset();
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x001489E3 File Offset: 0x00146BE3
		public int Adler
		{
			get
			{
				return this.engine.Adler;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x001489F0 File Offset: 0x00146BF0
		public long TotalIn
		{
			get
			{
				return this.engine.TotalIn;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x001489FD File Offset: 0x00146BFD
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x00148A05 File Offset: 0x00146C05
		public void Flush()
		{
			this.state |= 4;
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x00148A15 File Offset: 0x00146C15
		public void Finish()
		{
			this.state |= 12;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x00148A26 File Offset: 0x00146C26
		public bool IsFinished
		{
			get
			{
				return this.state == 30 && this.pending.IsFlushed;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x00148A3F File Offset: 0x00146C3F
		public bool IsNeedingInput
		{
			get
			{
				return this.engine.NeedsInput();
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x00148A4C File Offset: 0x00146C4C
		public void SetInput(byte[] input)
		{
			this.SetInput(input, 0, input.Length);
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x00148A59 File Offset: 0x00146C59
		public void SetInput(byte[] input, int offset, int count)
		{
			if ((this.state & 8) != 0)
			{
				throw new InvalidOperationException("Finish() already called");
			}
			this.engine.SetInput(input, offset, count);
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x00148A7E File Offset: 0x00146C7E
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

		// Token: 0x06002C1A RID: 11290 RVA: 0x00148AB9 File Offset: 0x00146CB9
		public int GetLevel()
		{
			return this.level;
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x00148AC1 File Offset: 0x00146CC1
		public void SetStrategy(DeflateStrategy strategy)
		{
			this.engine.Strategy = strategy;
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x00148ACF File Offset: 0x00146CCF
		public int Deflate(byte[] output)
		{
			return this.Deflate(output, 0, output.Length);
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x00148ADC File Offset: 0x00146CDC
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

		// Token: 0x06002C1E RID: 11294 RVA: 0x00148CBF File Offset: 0x00146EBF
		public void SetDictionary(byte[] dictionary)
		{
			this.SetDictionary(dictionary, 0, dictionary.Length);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x00148CCC File Offset: 0x00146ECC
		public void SetDictionary(byte[] dictionary, int index, int count)
		{
			if (this.state != 0)
			{
				throw new InvalidOperationException();
			}
			this.state = 1;
			this.engine.SetDictionary(dictionary, index, count);
		}

		// Token: 0x04002754 RID: 10068
		public const int BEST_COMPRESSION = 9;

		// Token: 0x04002755 RID: 10069
		public const int BEST_SPEED = 1;

		// Token: 0x04002756 RID: 10070
		public const int DEFAULT_COMPRESSION = -1;

		// Token: 0x04002757 RID: 10071
		public const int NO_COMPRESSION = 0;

		// Token: 0x04002758 RID: 10072
		public const int DEFLATED = 8;

		// Token: 0x04002759 RID: 10073
		private const int IS_SETDICT = 1;

		// Token: 0x0400275A RID: 10074
		private const int IS_FLUSHING = 4;

		// Token: 0x0400275B RID: 10075
		private const int IS_FINISHING = 8;

		// Token: 0x0400275C RID: 10076
		private const int INIT_STATE = 0;

		// Token: 0x0400275D RID: 10077
		private const int SETDICT_STATE = 1;

		// Token: 0x0400275E RID: 10078
		private const int BUSY_STATE = 16;

		// Token: 0x0400275F RID: 10079
		private const int FLUSHING_STATE = 20;

		// Token: 0x04002760 RID: 10080
		private const int FINISHING_STATE = 28;

		// Token: 0x04002761 RID: 10081
		private const int FINISHED_STATE = 30;

		// Token: 0x04002762 RID: 10082
		private const int CLOSED_STATE = 127;

		// Token: 0x04002763 RID: 10083
		private int level;

		// Token: 0x04002764 RID: 10084
		private bool noZlibHeaderOrFooter;

		// Token: 0x04002765 RID: 10085
		private int state;

		// Token: 0x04002766 RID: 10086
		private long totalOut;

		// Token: 0x04002767 RID: 10087
		private DeflaterPending pending;

		// Token: 0x04002768 RID: 10088
		private DeflaterEngine engine;

		// Token: 0x0200148D RID: 5261
		public enum CompressionLevel
		{
			// Token: 0x04006C65 RID: 27749
			BEST_COMPRESSION = 9,
			// Token: 0x04006C66 RID: 27750
			BEST_SPEED = 1,
			// Token: 0x04006C67 RID: 27751
			DEFAULT_COMPRESSION = -1,
			// Token: 0x04006C68 RID: 27752
			NO_COMPRESSION,
			// Token: 0x04006C69 RID: 27753
			DEFLATED = 8
		}
	}
}
