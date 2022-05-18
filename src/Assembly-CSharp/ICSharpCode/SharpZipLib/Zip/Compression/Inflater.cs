using System;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x020007FB RID: 2043
	public class Inflater
	{
		// Token: 0x060034AA RID: 13482 RVA: 0x00026732 File Offset: 0x00024932
		public Inflater() : this(false)
		{
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x0002673B File Offset: 0x0002493B
		public Inflater(bool noHeader)
		{
			this.noHeader = noHeader;
			if (!noHeader)
			{
				this.adler = new Adler32();
			}
			this.input = new StreamManipulator();
			this.outputWindow = new OutputWindow();
			this.mode = (noHeader ? 2 : 0);
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x001966A0 File Offset: 0x001948A0
		public void Reset()
		{
			this.mode = (this.noHeader ? 2 : 0);
			this.totalIn = 0L;
			this.totalOut = 0L;
			this.input.Reset();
			this.outputWindow.Reset();
			this.dynHeader = null;
			this.litlenTree = null;
			this.distTree = null;
			this.isLastBlock = false;
			Adler32 adler = this.adler;
			if (adler == null)
			{
				return;
			}
			adler.Reset();
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x00196714 File Offset: 0x00194914
		private bool DecodeHeader()
		{
			int num = this.input.PeekBits(16);
			if (num < 0)
			{
				return false;
			}
			this.input.DropBits(16);
			num = ((num << 8 | num >> 8) & 65535);
			if (num % 31 != 0)
			{
				throw new SharpZipBaseException("Header checksum illegal");
			}
			if ((num & 3840) != 2048)
			{
				throw new SharpZipBaseException("Compression Method unknown");
			}
			if ((num & 32) == 0)
			{
				this.mode = 2;
			}
			else
			{
				this.mode = 1;
				this.neededBits = 32;
			}
			return true;
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x0019679C File Offset: 0x0019499C
		private bool DecodeDict()
		{
			while (this.neededBits > 0)
			{
				int num = this.input.PeekBits(8);
				if (num < 0)
				{
					return false;
				}
				this.input.DropBits(8);
				this.readAdler = (this.readAdler << 8 | num);
				this.neededBits -= 8;
			}
			return false;
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x001967F4 File Offset: 0x001949F4
		private bool DecodeHuffman()
		{
			int i = this.outputWindow.GetFreeSpace();
			while (i >= 258)
			{
				int symbol;
				switch (this.mode)
				{
				case 7:
					while (((symbol = this.litlenTree.GetSymbol(this.input)) & -256) == 0)
					{
						this.outputWindow.Write(symbol);
						if (--i < 258)
						{
							return true;
						}
					}
					if (symbol >= 257)
					{
						try
						{
							this.repLength = Inflater.CPLENS[symbol - 257];
							this.neededBits = Inflater.CPLEXT[symbol - 257];
						}
						catch (Exception)
						{
							throw new SharpZipBaseException("Illegal rep length code");
						}
						goto IL_C4;
					}
					if (symbol < 0)
					{
						return false;
					}
					this.distTree = null;
					this.litlenTree = null;
					this.mode = 2;
					return true;
				case 8:
					goto IL_C4;
				case 9:
					goto IL_113;
				case 10:
					break;
				default:
					throw new SharpZipBaseException("Inflater unknown mode");
				}
				IL_154:
				if (this.neededBits > 0)
				{
					this.mode = 10;
					int num = this.input.PeekBits(this.neededBits);
					if (num < 0)
					{
						return false;
					}
					this.input.DropBits(this.neededBits);
					this.repDist += num;
				}
				this.outputWindow.Repeat(this.repLength, this.repDist);
				i -= this.repLength;
				this.mode = 7;
				continue;
				IL_113:
				symbol = this.distTree.GetSymbol(this.input);
				if (symbol < 0)
				{
					return false;
				}
				try
				{
					this.repDist = Inflater.CPDIST[symbol];
					this.neededBits = Inflater.CPDEXT[symbol];
				}
				catch (Exception)
				{
					throw new SharpZipBaseException("Illegal rep dist code");
				}
				goto IL_154;
				IL_C4:
				if (this.neededBits > 0)
				{
					this.mode = 8;
					int num2 = this.input.PeekBits(this.neededBits);
					if (num2 < 0)
					{
						return false;
					}
					this.input.DropBits(this.neededBits);
					this.repLength += num2;
				}
				this.mode = 9;
				goto IL_113;
			}
			return true;
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x001969FC File Offset: 0x00194BFC
		private bool DecodeChksum()
		{
			while (this.neededBits > 0)
			{
				int num = this.input.PeekBits(8);
				if (num < 0)
				{
					return false;
				}
				this.input.DropBits(8);
				this.readAdler = (this.readAdler << 8 | num);
				this.neededBits -= 8;
			}
			Adler32 adler = this.adler;
			if ((int)((adler != null) ? new long?(adler.Value) : null).Value != this.readAdler)
			{
				object[] array = new object[4];
				array[0] = "Adler chksum doesn't match: ";
				int num2 = 1;
				Adler32 adler2 = this.adler;
				array[num2] = (int)((adler2 != null) ? new long?(adler2.Value) : null).Value;
				array[2] = " vs. ";
				array[3] = this.readAdler;
				throw new SharpZipBaseException(string.Concat(array));
			}
			this.mode = 12;
			return false;
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x00196AEC File Offset: 0x00194CEC
		private bool Decode()
		{
			switch (this.mode)
			{
			case 0:
				return this.DecodeHeader();
			case 1:
				return this.DecodeDict();
			case 2:
				if (this.isLastBlock)
				{
					if (this.noHeader)
					{
						this.mode = 12;
						return false;
					}
					this.input.SkipToByteBoundary();
					this.neededBits = 32;
					this.mode = 11;
					return true;
				}
				else
				{
					int num = this.input.PeekBits(3);
					if (num < 0)
					{
						return false;
					}
					this.input.DropBits(3);
					this.isLastBlock |= ((num & 1) != 0);
					switch (num >> 1)
					{
					case 0:
						this.input.SkipToByteBoundary();
						this.mode = 3;
						break;
					case 1:
						this.litlenTree = InflaterHuffmanTree.defLitLenTree;
						this.distTree = InflaterHuffmanTree.defDistTree;
						this.mode = 7;
						break;
					case 2:
						this.dynHeader = new InflaterDynHeader(this.input);
						this.mode = 6;
						break;
					default:
						throw new SharpZipBaseException("Unknown block type " + num);
					}
					return true;
				}
				break;
			case 3:
				if ((this.uncomprLen = this.input.PeekBits(16)) < 0)
				{
					return false;
				}
				this.input.DropBits(16);
				this.mode = 4;
				break;
			case 4:
				break;
			case 5:
				goto IL_1B2;
			case 6:
				if (!this.dynHeader.AttemptRead())
				{
					return false;
				}
				this.litlenTree = this.dynHeader.LiteralLengthTree;
				this.distTree = this.dynHeader.DistanceTree;
				this.mode = 7;
				goto IL_230;
			case 7:
			case 8:
			case 9:
			case 10:
				goto IL_230;
			case 11:
				return this.DecodeChksum();
			case 12:
				return false;
			default:
				throw new SharpZipBaseException("Inflater.Decode unknown mode");
			}
			int num2 = this.input.PeekBits(16);
			if (num2 < 0)
			{
				return false;
			}
			this.input.DropBits(16);
			if (num2 != (this.uncomprLen ^ 65535))
			{
				throw new SharpZipBaseException("broken uncompressed block");
			}
			this.mode = 5;
			IL_1B2:
			int num3 = this.outputWindow.CopyStored(this.input, this.uncomprLen);
			this.uncomprLen -= num3;
			if (this.uncomprLen == 0)
			{
				this.mode = 2;
				return true;
			}
			return !this.input.IsNeedingInput;
			IL_230:
			return this.DecodeHuffman();
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x0002677B File Offset: 0x0002497B
		public void SetDictionary(byte[] buffer)
		{
			this.SetDictionary(buffer, 0, buffer.Length);
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x00196D3C File Offset: 0x00194F3C
		public void SetDictionary(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.IsNeedingDictionary)
			{
				throw new InvalidOperationException("Dictionary is not needed");
			}
			Adler32 adler = this.adler;
			if (adler != null)
			{
				adler.Update(new ArraySegment<byte>(buffer, index, count));
			}
			if (this.adler != null && (int)this.adler.Value != this.readAdler)
			{
				throw new SharpZipBaseException("Wrong adler checksum");
			}
			Adler32 adler2 = this.adler;
			if (adler2 != null)
			{
				adler2.Reset();
			}
			this.outputWindow.CopyDict(buffer, index, count);
			this.mode = 2;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x00026788 File Offset: 0x00024988
		public void SetInput(byte[] buffer)
		{
			this.SetInput(buffer, 0, buffer.Length);
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x00026795 File Offset: 0x00024995
		public void SetInput(byte[] buffer, int index, int count)
		{
			this.input.SetInput(buffer, index, count);
			this.totalIn += (long)count;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000267B4 File Offset: 0x000249B4
		public int Inflate(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.Inflate(buffer, 0, buffer.Length);
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x00196DF0 File Offset: 0x00194FF0
		public int Inflate(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "count cannot be negative");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset cannot be negative");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentException("count exceeds buffer bounds");
			}
			if (count == 0)
			{
				if (!this.IsFinished)
				{
					this.Decode();
				}
				return 0;
			}
			int num = 0;
			for (;;)
			{
				if (this.mode != 11)
				{
					int num2 = this.outputWindow.CopyOutput(buffer, offset, count);
					if (num2 > 0)
					{
						Adler32 adler = this.adler;
						if (adler != null)
						{
							adler.Update(new ArraySegment<byte>(buffer, offset, num2));
						}
						offset += num2;
						num += num2;
						this.totalOut += (long)num2;
						count -= num2;
						if (count == 0)
						{
							break;
						}
					}
				}
				if (!this.Decode() && (this.outputWindow.GetAvailable() <= 0 || this.mode == 11))
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060034B8 RID: 13496 RVA: 0x000267CF File Offset: 0x000249CF
		public bool IsNeedingInput
		{
			get
			{
				return this.input.IsNeedingInput;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000267DC File Offset: 0x000249DC
		public bool IsNeedingDictionary
		{
			get
			{
				return this.mode == 1 && this.neededBits == 0;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060034BA RID: 13498 RVA: 0x000267F2 File Offset: 0x000249F2
		public bool IsFinished
		{
			get
			{
				return this.mode == 12 && this.outputWindow.GetAvailable() == 0;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x0002680E File Offset: 0x00024A0E
		public int Adler
		{
			get
			{
				if (this.IsNeedingDictionary)
				{
					return this.readAdler;
				}
				if (this.adler != null)
				{
					return (int)this.adler.Value;
				}
				return 0;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x00026835 File Offset: 0x00024A35
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x0002683D File Offset: 0x00024A3D
		public long TotalIn
		{
			get
			{
				return this.totalIn - (long)this.RemainingInput;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x0002684D File Offset: 0x00024A4D
		public int RemainingInput
		{
			get
			{
				return this.input.AvailableBytes;
			}
		}

		// Token: 0x04002FE6 RID: 12262
		private static readonly int[] CPLENS = new int[]
		{
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			13,
			15,
			17,
			19,
			23,
			27,
			31,
			35,
			43,
			51,
			59,
			67,
			83,
			99,
			115,
			131,
			163,
			195,
			227,
			258
		};

		// Token: 0x04002FE7 RID: 12263
		private static readonly int[] CPLEXT = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			5,
			5,
			5,
			5,
			0
		};

		// Token: 0x04002FE8 RID: 12264
		private static readonly int[] CPDIST = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			7,
			9,
			13,
			17,
			25,
			33,
			49,
			65,
			97,
			129,
			193,
			257,
			385,
			513,
			769,
			1025,
			1537,
			2049,
			3073,
			4097,
			6145,
			8193,
			12289,
			16385,
			24577
		};

		// Token: 0x04002FE9 RID: 12265
		private static readonly int[] CPDEXT = new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			10,
			10,
			11,
			11,
			12,
			12,
			13,
			13
		};

		// Token: 0x04002FEA RID: 12266
		private const int DECODE_HEADER = 0;

		// Token: 0x04002FEB RID: 12267
		private const int DECODE_DICT = 1;

		// Token: 0x04002FEC RID: 12268
		private const int DECODE_BLOCKS = 2;

		// Token: 0x04002FED RID: 12269
		private const int DECODE_STORED_LEN1 = 3;

		// Token: 0x04002FEE RID: 12270
		private const int DECODE_STORED_LEN2 = 4;

		// Token: 0x04002FEF RID: 12271
		private const int DECODE_STORED = 5;

		// Token: 0x04002FF0 RID: 12272
		private const int DECODE_DYN_HEADER = 6;

		// Token: 0x04002FF1 RID: 12273
		private const int DECODE_HUFFMAN = 7;

		// Token: 0x04002FF2 RID: 12274
		private const int DECODE_HUFFMAN_LENBITS = 8;

		// Token: 0x04002FF3 RID: 12275
		private const int DECODE_HUFFMAN_DIST = 9;

		// Token: 0x04002FF4 RID: 12276
		private const int DECODE_HUFFMAN_DISTBITS = 10;

		// Token: 0x04002FF5 RID: 12277
		private const int DECODE_CHKSUM = 11;

		// Token: 0x04002FF6 RID: 12278
		private const int FINISHED = 12;

		// Token: 0x04002FF7 RID: 12279
		private int mode;

		// Token: 0x04002FF8 RID: 12280
		private int readAdler;

		// Token: 0x04002FF9 RID: 12281
		private int neededBits;

		// Token: 0x04002FFA RID: 12282
		private int repLength;

		// Token: 0x04002FFB RID: 12283
		private int repDist;

		// Token: 0x04002FFC RID: 12284
		private int uncomprLen;

		// Token: 0x04002FFD RID: 12285
		private bool isLastBlock;

		// Token: 0x04002FFE RID: 12286
		private long totalOut;

		// Token: 0x04002FFF RID: 12287
		private long totalIn;

		// Token: 0x04003000 RID: 12288
		private bool noHeader;

		// Token: 0x04003001 RID: 12289
		private readonly StreamManipulator input;

		// Token: 0x04003002 RID: 12290
		private OutputWindow outputWindow;

		// Token: 0x04003003 RID: 12291
		private InflaterDynHeader dynHeader;

		// Token: 0x04003004 RID: 12292
		private InflaterHuffmanTree litlenTree;

		// Token: 0x04003005 RID: 12293
		private InflaterHuffmanTree distTree;

		// Token: 0x04003006 RID: 12294
		private Adler32 adler;
	}
}
