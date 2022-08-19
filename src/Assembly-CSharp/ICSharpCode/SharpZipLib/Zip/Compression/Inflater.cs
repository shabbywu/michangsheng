using System;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000556 RID: 1366
	public class Inflater
	{
		// Token: 0x06002C44 RID: 11332 RVA: 0x0014A5AA File Offset: 0x001487AA
		public Inflater() : this(false)
		{
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x0014A5B3 File Offset: 0x001487B3
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

		// Token: 0x06002C46 RID: 11334 RVA: 0x0014A5F4 File Offset: 0x001487F4
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

		// Token: 0x06002C47 RID: 11335 RVA: 0x0014A668 File Offset: 0x00148868
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

		// Token: 0x06002C48 RID: 11336 RVA: 0x0014A6F0 File Offset: 0x001488F0
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

		// Token: 0x06002C49 RID: 11337 RVA: 0x0014A748 File Offset: 0x00148948
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

		// Token: 0x06002C4A RID: 11338 RVA: 0x0014A950 File Offset: 0x00148B50
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

		// Token: 0x06002C4B RID: 11339 RVA: 0x0014AA40 File Offset: 0x00148C40
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

		// Token: 0x06002C4C RID: 11340 RVA: 0x0014AC90 File Offset: 0x00148E90
		public void SetDictionary(byte[] buffer)
		{
			this.SetDictionary(buffer, 0, buffer.Length);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x0014ACA0 File Offset: 0x00148EA0
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

		// Token: 0x06002C4E RID: 11342 RVA: 0x0014AD52 File Offset: 0x00148F52
		public void SetInput(byte[] buffer)
		{
			this.SetInput(buffer, 0, buffer.Length);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x0014AD5F File Offset: 0x00148F5F
		public void SetInput(byte[] buffer, int index, int count)
		{
			this.input.SetInput(buffer, index, count);
			this.totalIn += (long)count;
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x0014AD7E File Offset: 0x00148F7E
		public int Inflate(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.Inflate(buffer, 0, buffer.Length);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x0014AD9C File Offset: 0x00148F9C
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

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x0014AE81 File Offset: 0x00149081
		public bool IsNeedingInput
		{
			get
			{
				return this.input.IsNeedingInput;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06002C53 RID: 11347 RVA: 0x0014AE8E File Offset: 0x0014908E
		public bool IsNeedingDictionary
		{
			get
			{
				return this.mode == 1 && this.neededBits == 0;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x0014AEA4 File Offset: 0x001490A4
		public bool IsFinished
		{
			get
			{
				return this.mode == 12 && this.outputWindow.GetAvailable() == 0;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06002C55 RID: 11349 RVA: 0x0014AEC0 File Offset: 0x001490C0
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

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x0014AEE7 File Offset: 0x001490E7
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x0014AEEF File Offset: 0x001490EF
		public long TotalIn
		{
			get
			{
				return this.totalIn - (long)this.RemainingInput;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x0014AEFF File Offset: 0x001490FF
		public int RemainingInput
		{
			get
			{
				return this.input.AvailableBytes;
			}
		}

		// Token: 0x040027B6 RID: 10166
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

		// Token: 0x040027B7 RID: 10167
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

		// Token: 0x040027B8 RID: 10168
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

		// Token: 0x040027B9 RID: 10169
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

		// Token: 0x040027BA RID: 10170
		private const int DECODE_HEADER = 0;

		// Token: 0x040027BB RID: 10171
		private const int DECODE_DICT = 1;

		// Token: 0x040027BC RID: 10172
		private const int DECODE_BLOCKS = 2;

		// Token: 0x040027BD RID: 10173
		private const int DECODE_STORED_LEN1 = 3;

		// Token: 0x040027BE RID: 10174
		private const int DECODE_STORED_LEN2 = 4;

		// Token: 0x040027BF RID: 10175
		private const int DECODE_STORED = 5;

		// Token: 0x040027C0 RID: 10176
		private const int DECODE_DYN_HEADER = 6;

		// Token: 0x040027C1 RID: 10177
		private const int DECODE_HUFFMAN = 7;

		// Token: 0x040027C2 RID: 10178
		private const int DECODE_HUFFMAN_LENBITS = 8;

		// Token: 0x040027C3 RID: 10179
		private const int DECODE_HUFFMAN_DIST = 9;

		// Token: 0x040027C4 RID: 10180
		private const int DECODE_HUFFMAN_DISTBITS = 10;

		// Token: 0x040027C5 RID: 10181
		private const int DECODE_CHKSUM = 11;

		// Token: 0x040027C6 RID: 10182
		private const int FINISHED = 12;

		// Token: 0x040027C7 RID: 10183
		private int mode;

		// Token: 0x040027C8 RID: 10184
		private int readAdler;

		// Token: 0x040027C9 RID: 10185
		private int neededBits;

		// Token: 0x040027CA RID: 10186
		private int repLength;

		// Token: 0x040027CB RID: 10187
		private int repDist;

		// Token: 0x040027CC RID: 10188
		private int uncomprLen;

		// Token: 0x040027CD RID: 10189
		private bool isLastBlock;

		// Token: 0x040027CE RID: 10190
		private long totalOut;

		// Token: 0x040027CF RID: 10191
		private long totalIn;

		// Token: 0x040027D0 RID: 10192
		private bool noHeader;

		// Token: 0x040027D1 RID: 10193
		private readonly StreamManipulator input;

		// Token: 0x040027D2 RID: 10194
		private OutputWindow outputWindow;

		// Token: 0x040027D3 RID: 10195
		private InflaterDynHeader dynHeader;

		// Token: 0x040027D4 RID: 10196
		private InflaterHuffmanTree litlenTree;

		// Token: 0x040027D5 RID: 10197
		private InflaterHuffmanTree distTree;

		// Token: 0x040027D6 RID: 10198
		private Adler32 adler;
	}
}
