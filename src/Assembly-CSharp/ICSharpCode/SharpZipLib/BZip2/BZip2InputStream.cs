using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x0200083C RID: 2108
	public class BZip2InputStream : Stream
	{
		// Token: 0x06003726 RID: 14118 RVA: 0x0019D0F4 File Offset: 0x0019B2F4
		public BZip2InputStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			for (int i = 0; i < 6; i++)
			{
				this.limit[i] = new int[258];
				this.baseArray[i] = new int[258];
				this.perm[i] = new int[258];
			}
			this.baseStream = stream;
			this.bsLive = 0;
			this.bsBuff = 0;
			this.Initialize();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000281EE File Offset: 0x000263EE
		// (set) Token: 0x06003728 RID: 14120 RVA: 0x000281F6 File Offset: 0x000263F6
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x000281FF File Offset: 0x000263FF
		public override bool CanRead
		{
			get
			{
				return this.baseStream.CanRead;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x0002820C File Offset: 0x0002640C
		public override long Length
		{
			get
			{
				return this.baseStream.Length;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x00028219 File Offset: 0x00026419
		// (set) Token: 0x0600372E RID: 14126 RVA: 0x00028226 File Offset: 0x00026426
		public override long Position
		{
			get
			{
				return this.baseStream.Position;
			}
			set
			{
				throw new NotSupportedException("BZip2InputStream position cannot be set");
			}
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x00028232 File Offset: 0x00026432
		public override void Flush()
		{
			this.baseStream.Flush();
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x0002823F File Offset: 0x0002643F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("BZip2InputStream Seek not supported");
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x0002824B File Offset: 0x0002644B
		public override void SetLength(long value)
		{
			throw new NotSupportedException("BZip2InputStream SetLength not supported");
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x00028257 File Offset: 0x00026457
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("BZip2InputStream Write not supported");
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x00028263 File Offset: 0x00026463
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("BZip2InputStream WriteByte not supported");
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x0019D230 File Offset: 0x0019B430
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < count; i++)
			{
				int num = this.ReadByte();
				if (num == -1)
				{
					return i;
				}
				buffer[offset + i] = (byte)num;
			}
			return count;
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x0002826F File Offset: 0x0002646F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.IsStreamOwner)
			{
				this.baseStream.Dispose();
			}
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x0019D26C File Offset: 0x0019B46C
		public override int ReadByte()
		{
			if (this.streamEnd)
			{
				return -1;
			}
			int result = this.currentChar;
			switch (this.currentState)
			{
			case 3:
				this.SetupRandPartB();
				break;
			case 4:
				this.SetupRandPartC();
				break;
			case 6:
				this.SetupNoRandPartB();
				break;
			case 7:
				this.SetupNoRandPartC();
				break;
			}
			return result;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x0019D2D8 File Offset: 0x0019B4D8
		private void MakeMaps()
		{
			this.nInUse = 0;
			for (int i = 0; i < 256; i++)
			{
				if (this.inUse[i])
				{
					this.seqToUnseq[this.nInUse] = (byte)i;
					this.unseqToSeq[i] = (byte)this.nInUse;
					this.nInUse++;
				}
			}
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x0019D334 File Offset: 0x0019B534
		private void Initialize()
		{
			int num = (int)this.BsGetUChar();
			char c = this.BsGetUChar();
			char c2 = this.BsGetUChar();
			char c3 = this.BsGetUChar();
			if (num != 66 || c != 'Z' || c2 != 'h' || c3 < '1' || c3 > '9')
			{
				this.streamEnd = true;
				return;
			}
			this.SetDecompressStructureSizes((int)(c3 - '0'));
			this.computedCombinedCRC = 0U;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x0019D390 File Offset: 0x0019B590
		private void InitBlock()
		{
			char c = this.BsGetUChar();
			char c2 = this.BsGetUChar();
			char c3 = this.BsGetUChar();
			char c4 = this.BsGetUChar();
			char c5 = this.BsGetUChar();
			char c6 = this.BsGetUChar();
			if (c == '\u0017' && c2 == 'r' && c3 == 'E' && c4 == '8' && c5 == 'P' && c6 == '\u0090')
			{
				this.Complete();
				return;
			}
			if (c != '1' || c2 != 'A' || c3 != 'Y' || c4 != '&' || c5 != 'S' || c6 != 'Y')
			{
				BZip2InputStream.BadBlockHeader();
				this.streamEnd = true;
				return;
			}
			this.storedBlockCRC = this.BsGetInt32();
			this.blockRandomised = (this.BsR(1) == 1);
			this.GetAndMoveToFrontDecode();
			this.mCrc.Reset();
			this.currentState = 1;
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x0019D454 File Offset: 0x0019B654
		private void EndBlock()
		{
			this.computedBlockCRC = (int)this.mCrc.Value;
			if (this.storedBlockCRC != this.computedBlockCRC)
			{
				BZip2InputStream.CrcError();
			}
			this.computedCombinedCRC = ((this.computedCombinedCRC << 1 & uint.MaxValue) | this.computedCombinedCRC >> 31);
			this.computedCombinedCRC ^= (uint)this.computedBlockCRC;
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x00028287 File Offset: 0x00026487
		private void Complete()
		{
			this.storedCombinedCRC = this.BsGetInt32();
			if (this.storedCombinedCRC != (int)this.computedCombinedCRC)
			{
				BZip2InputStream.CrcError();
			}
			this.streamEnd = true;
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x0019D4B4 File Offset: 0x0019B6B4
		private void FillBuffer()
		{
			int num = 0;
			try
			{
				num = this.baseStream.ReadByte();
			}
			catch (Exception)
			{
				BZip2InputStream.CompressedStreamEOF();
			}
			if (num == -1)
			{
				BZip2InputStream.CompressedStreamEOF();
			}
			this.bsBuff = (this.bsBuff << 8 | (num & 255));
			this.bsLive += 8;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000282AF File Offset: 0x000264AF
		private int BsR(int n)
		{
			while (this.bsLive < n)
			{
				this.FillBuffer();
			}
			int result = this.bsBuff >> this.bsLive - n & (1 << n) - 1;
			this.bsLive -= n;
			return result;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000282EB File Offset: 0x000264EB
		private char BsGetUChar()
		{
			return (char)this.BsR(8);
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000282F5 File Offset: 0x000264F5
		private int BsGetIntVS(int numBits)
		{
			return this.BsR(numBits);
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000282FE File Offset: 0x000264FE
		private int BsGetInt32()
		{
			return ((this.BsR(8) << 8 | this.BsR(8)) << 8 | this.BsR(8)) << 8 | this.BsR(8);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x0019D518 File Offset: 0x0019B718
		private void RecvDecodingTables()
		{
			char[][] array = new char[6][];
			for (int i = 0; i < 6; i++)
			{
				array[i] = new char[258];
			}
			bool[] array2 = new bool[16];
			for (int j = 0; j < 16; j++)
			{
				array2[j] = (this.BsR(1) == 1);
			}
			for (int k = 0; k < 16; k++)
			{
				if (array2[k])
				{
					for (int l = 0; l < 16; l++)
					{
						this.inUse[k * 16 + l] = (this.BsR(1) == 1);
					}
				}
				else
				{
					for (int m = 0; m < 16; m++)
					{
						this.inUse[k * 16 + m] = false;
					}
				}
			}
			this.MakeMaps();
			int num = this.nInUse + 2;
			int num2 = this.BsR(3);
			int num3 = this.BsR(15);
			for (int n = 0; n < num3; n++)
			{
				int num4 = 0;
				while (this.BsR(1) == 1)
				{
					num4++;
				}
				this.selectorMtf[n] = (byte)num4;
			}
			byte[] array3 = new byte[6];
			for (int num5 = 0; num5 < num2; num5++)
			{
				array3[num5] = (byte)num5;
			}
			for (int num6 = 0; num6 < num3; num6++)
			{
				int num7 = (int)this.selectorMtf[num6];
				byte b = array3[num7];
				while (num7 > 0)
				{
					array3[num7] = array3[num7 - 1];
					num7--;
				}
				array3[0] = b;
				this.selector[num6] = b;
			}
			for (int num8 = 0; num8 < num2; num8++)
			{
				int num9 = this.BsR(5);
				for (int num10 = 0; num10 < num; num10++)
				{
					while (this.BsR(1) == 1)
					{
						if (this.BsR(1) == 0)
						{
							num9++;
						}
						else
						{
							num9--;
						}
					}
					array[num8][num10] = (char)num9;
				}
			}
			for (int num11 = 0; num11 < num2; num11++)
			{
				int num12 = 32;
				int num13 = 0;
				for (int num14 = 0; num14 < num; num14++)
				{
					num13 = Math.Max(num13, (int)array[num11][num14]);
					num12 = Math.Min(num12, (int)array[num11][num14]);
				}
				BZip2InputStream.HbCreateDecodeTables(this.limit[num11], this.baseArray[num11], this.perm[num11], array[num11], num12, num13, num);
				this.minLens[num11] = num12;
			}
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x0019D764 File Offset: 0x0019B964
		private void GetAndMoveToFrontDecode()
		{
			byte[] array = new byte[256];
			int num = 100000 * this.blockSize100k;
			this.origPtr = this.BsGetIntVS(24);
			this.RecvDecodingTables();
			int num2 = this.nInUse + 1;
			int num3 = -1;
			int num4 = 0;
			for (int i = 0; i <= 255; i++)
			{
				this.unzftab[i] = 0;
			}
			for (int j = 0; j <= 255; j++)
			{
				array[j] = (byte)j;
			}
			this.last = -1;
			if (num4 == 0)
			{
				num3++;
				num4 = 50;
			}
			num4--;
			int num5 = (int)this.selector[num3];
			int num6 = this.minLens[num5];
			int k;
			int num7;
			for (k = this.BsR(num6); k > this.limit[num5][num6]; k = (k << 1 | num7))
			{
				if (num6 > 20)
				{
					throw new BZip2Exception("Bzip data error");
				}
				num6++;
				while (this.bsLive < 1)
				{
					this.FillBuffer();
				}
				num7 = (this.bsBuff >> this.bsLive - 1 & 1);
				this.bsLive--;
			}
			if (k - this.baseArray[num5][num6] < 0 || k - this.baseArray[num5][num6] >= 258)
			{
				throw new BZip2Exception("Bzip data error");
			}
			int num8 = this.perm[num5][k - this.baseArray[num5][num6]];
			while (num8 != num2)
			{
				if (num8 == 0 || num8 == 1)
				{
					int l = -1;
					int num9 = 1;
					do
					{
						if (num8 == 0)
						{
							l += num9;
						}
						else if (num8 == 1)
						{
							l += 2 * num9;
						}
						num9 <<= 1;
						if (num4 == 0)
						{
							num3++;
							num4 = 50;
						}
						num4--;
						num5 = (int)this.selector[num3];
						num6 = this.minLens[num5];
						for (k = this.BsR(num6); k > this.limit[num5][num6]; k = (k << 1 | num7))
						{
							num6++;
							while (this.bsLive < 1)
							{
								this.FillBuffer();
							}
							num7 = (this.bsBuff >> this.bsLive - 1 & 1);
							this.bsLive--;
						}
						num8 = this.perm[num5][k - this.baseArray[num5][num6]];
					}
					while (num8 == 0 || num8 == 1);
					l++;
					byte b = this.seqToUnseq[(int)array[0]];
					this.unzftab[(int)b] += l;
					while (l > 0)
					{
						this.last++;
						this.ll8[this.last] = b;
						l--;
					}
					if (this.last >= num)
					{
						BZip2InputStream.BlockOverrun();
					}
				}
				else
				{
					this.last++;
					if (this.last >= num)
					{
						BZip2InputStream.BlockOverrun();
					}
					byte b2 = array[num8 - 1];
					this.unzftab[(int)this.seqToUnseq[(int)b2]]++;
					this.ll8[this.last] = this.seqToUnseq[(int)b2];
					for (int m = num8 - 1; m > 0; m--)
					{
						array[m] = array[m - 1];
					}
					array[0] = b2;
					if (num4 == 0)
					{
						num3++;
						num4 = 50;
					}
					num4--;
					num5 = (int)this.selector[num3];
					num6 = this.minLens[num5];
					for (k = this.BsR(num6); k > this.limit[num5][num6]; k = (k << 1 | num7))
					{
						num6++;
						while (this.bsLive < 1)
						{
							this.FillBuffer();
						}
						num7 = (this.bsBuff >> this.bsLive - 1 & 1);
						this.bsLive--;
					}
					num8 = this.perm[num5][k - this.baseArray[num5][num6]];
				}
			}
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x0019DB3C File Offset: 0x0019BD3C
		private void SetupBlock()
		{
			int[] array = new int[257];
			array[0] = 0;
			Array.Copy(this.unzftab, 0, array, 1, 256);
			for (int i = 1; i <= 256; i++)
			{
				array[i] += array[i - 1];
			}
			for (int j = 0; j <= this.last; j++)
			{
				byte b = this.ll8[j];
				this.tt[array[(int)b]] = j;
				array[(int)b]++;
			}
			this.tPos = this.tt[this.origPtr];
			this.count = 0;
			this.i2 = 0;
			this.ch2 = 256;
			if (this.blockRandomised)
			{
				this.rNToGo = 0;
				this.rTPos = 0;
				this.SetupRandPartA();
				return;
			}
			this.SetupNoRandPartA();
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x0019DC10 File Offset: 0x0019BE10
		private void SetupRandPartA()
		{
			if (this.i2 <= this.last)
			{
				this.chPrev = this.ch2;
				this.ch2 = (int)this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				if (this.rNToGo == 0)
				{
					this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
					this.rTPos++;
					if (this.rTPos == 512)
					{
						this.rTPos = 0;
					}
				}
				this.rNToGo--;
				this.ch2 ^= ((this.rNToGo == 1) ? 1 : 0);
				this.i2++;
				this.currentChar = this.ch2;
				this.currentState = 3;
				this.mCrc.Update(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x0019DD0C File Offset: 0x0019BF0C
		private void SetupNoRandPartA()
		{
			if (this.i2 <= this.last)
			{
				this.chPrev = this.ch2;
				this.ch2 = (int)this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				this.i2++;
				this.currentChar = this.ch2;
				this.currentState = 6;
				this.mCrc.Update(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x0019DDA0 File Offset: 0x0019BFA0
		private void SetupRandPartB()
		{
			if (this.ch2 != this.chPrev)
			{
				this.currentState = 2;
				this.count = 1;
				this.SetupRandPartA();
				return;
			}
			this.count++;
			if (this.count >= 4)
			{
				this.z = this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				if (this.rNToGo == 0)
				{
					this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
					this.rTPos++;
					if (this.rTPos == 512)
					{
						this.rTPos = 0;
					}
				}
				this.rNToGo--;
				this.z ^= ((this.rNToGo == 1) ? 1 : 0);
				this.j2 = 0;
				this.currentState = 4;
				this.SetupRandPartC();
				return;
			}
			this.currentState = 2;
			this.SetupRandPartA();
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x0019DE98 File Offset: 0x0019C098
		private void SetupRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.Update(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 2;
			this.i2++;
			this.count = 0;
			this.SetupRandPartA();
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x0019DF04 File Offset: 0x0019C104
		private void SetupNoRandPartB()
		{
			if (this.ch2 != this.chPrev)
			{
				this.currentState = 5;
				this.count = 1;
				this.SetupNoRandPartA();
				return;
			}
			this.count++;
			if (this.count >= 4)
			{
				this.z = this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				this.currentState = 7;
				this.j2 = 0;
				this.SetupNoRandPartC();
				return;
			}
			this.currentState = 5;
			this.SetupNoRandPartA();
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x0019DF94 File Offset: 0x0019C194
		private void SetupNoRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.Update(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 5;
			this.i2++;
			this.count = 0;
			this.SetupNoRandPartA();
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x0019E000 File Offset: 0x0019C200
		private void SetDecompressStructureSizes(int newSize100k)
		{
			if (0 > newSize100k || newSize100k > 9 || 0 > this.blockSize100k || this.blockSize100k > 9)
			{
				throw new BZip2Exception("Invalid block size");
			}
			this.blockSize100k = newSize100k;
			if (newSize100k == 0)
			{
				return;
			}
			int num = 100000 * newSize100k;
			this.ll8 = new byte[num];
			this.tt = new int[num];
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x00028325 File Offset: 0x00026525
		private static void CompressedStreamEOF()
		{
			throw new EndOfStreamException("BZip2 input stream end of compressed stream");
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x00028331 File Offset: 0x00026531
		private static void BlockOverrun()
		{
			throw new BZip2Exception("BZip2 input stream block overrun");
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x0002833D File Offset: 0x0002653D
		private static void BadBlockHeader()
		{
			throw new BZip2Exception("BZip2 input stream bad block header");
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x00028349 File Offset: 0x00026549
		private static void CrcError()
		{
			throw new BZip2Exception("BZip2 input stream crc error");
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x0019E060 File Offset: 0x0019C260
		private static void HbCreateDecodeTables(int[] limit, int[] baseArray, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
		{
			int num = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if ((int)length[j] == i)
					{
						perm[num] = j;
						num++;
					}
				}
			}
			for (int k = 0; k < 23; k++)
			{
				baseArray[k] = 0;
			}
			for (int l = 0; l < alphaSize; l++)
			{
				baseArray[(int)(length[l] + '\u0001')]++;
			}
			for (int m = 1; m < 23; m++)
			{
				baseArray[m] += baseArray[m - 1];
			}
			for (int n = 0; n < 23; n++)
			{
				limit[n] = 0;
			}
			int num2 = 0;
			for (int num3 = minLen; num3 <= maxLen; num3++)
			{
				num2 += baseArray[num3 + 1] - baseArray[num3];
				limit[num3] = num2 - 1;
				num2 <<= 1;
			}
			for (int num4 = minLen + 1; num4 <= maxLen; num4++)
			{
				baseArray[num4] = (limit[num4 - 1] + 1 << 1) - baseArray[num4];
			}
		}

		// Token: 0x0400313E RID: 12606
		private const int START_BLOCK_STATE = 1;

		// Token: 0x0400313F RID: 12607
		private const int RAND_PART_A_STATE = 2;

		// Token: 0x04003140 RID: 12608
		private const int RAND_PART_B_STATE = 3;

		// Token: 0x04003141 RID: 12609
		private const int RAND_PART_C_STATE = 4;

		// Token: 0x04003142 RID: 12610
		private const int NO_RAND_PART_A_STATE = 5;

		// Token: 0x04003143 RID: 12611
		private const int NO_RAND_PART_B_STATE = 6;

		// Token: 0x04003144 RID: 12612
		private const int NO_RAND_PART_C_STATE = 7;

		// Token: 0x04003145 RID: 12613
		private int last;

		// Token: 0x04003146 RID: 12614
		private int origPtr;

		// Token: 0x04003147 RID: 12615
		private int blockSize100k;

		// Token: 0x04003148 RID: 12616
		private bool blockRandomised;

		// Token: 0x04003149 RID: 12617
		private int bsBuff;

		// Token: 0x0400314A RID: 12618
		private int bsLive;

		// Token: 0x0400314B RID: 12619
		private IChecksum mCrc = new BZip2Crc();

		// Token: 0x0400314C RID: 12620
		private bool[] inUse = new bool[256];

		// Token: 0x0400314D RID: 12621
		private int nInUse;

		// Token: 0x0400314E RID: 12622
		private byte[] seqToUnseq = new byte[256];

		// Token: 0x0400314F RID: 12623
		private byte[] unseqToSeq = new byte[256];

		// Token: 0x04003150 RID: 12624
		private byte[] selector = new byte[18002];

		// Token: 0x04003151 RID: 12625
		private byte[] selectorMtf = new byte[18002];

		// Token: 0x04003152 RID: 12626
		private int[] tt;

		// Token: 0x04003153 RID: 12627
		private byte[] ll8;

		// Token: 0x04003154 RID: 12628
		private int[] unzftab = new int[256];

		// Token: 0x04003155 RID: 12629
		private int[][] limit = new int[6][];

		// Token: 0x04003156 RID: 12630
		private int[][] baseArray = new int[6][];

		// Token: 0x04003157 RID: 12631
		private int[][] perm = new int[6][];

		// Token: 0x04003158 RID: 12632
		private int[] minLens = new int[6];

		// Token: 0x04003159 RID: 12633
		private readonly Stream baseStream;

		// Token: 0x0400315A RID: 12634
		private bool streamEnd;

		// Token: 0x0400315B RID: 12635
		private int currentChar = -1;

		// Token: 0x0400315C RID: 12636
		private int currentState = 1;

		// Token: 0x0400315D RID: 12637
		private int storedBlockCRC;

		// Token: 0x0400315E RID: 12638
		private int storedCombinedCRC;

		// Token: 0x0400315F RID: 12639
		private int computedBlockCRC;

		// Token: 0x04003160 RID: 12640
		private uint computedCombinedCRC;

		// Token: 0x04003161 RID: 12641
		private int count;

		// Token: 0x04003162 RID: 12642
		private int chPrev;

		// Token: 0x04003163 RID: 12643
		private int ch2;

		// Token: 0x04003164 RID: 12644
		private int tPos;

		// Token: 0x04003165 RID: 12645
		private int rNToGo;

		// Token: 0x04003166 RID: 12646
		private int rTPos;

		// Token: 0x04003167 RID: 12647
		private int i2;

		// Token: 0x04003168 RID: 12648
		private int j2;

		// Token: 0x04003169 RID: 12649
		private byte z;
	}
}
