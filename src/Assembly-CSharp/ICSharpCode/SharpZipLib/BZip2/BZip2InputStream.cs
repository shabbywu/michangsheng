using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x02000593 RID: 1427
	public class BZip2InputStream : Stream
	{
		// Token: 0x06002EB0 RID: 11952 RVA: 0x0015266C File Offset: 0x0015086C
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

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x001527A6 File Offset: 0x001509A6
		// (set) Token: 0x06002EB2 RID: 11954 RVA: 0x001527AE File Offset: 0x001509AE
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x001527B7 File Offset: 0x001509B7
		public override bool CanRead
		{
			get
			{
				return this.baseStream.CanRead;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x001527C4 File Offset: 0x001509C4
		public override long Length
		{
			get
			{
				return this.baseStream.Length;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x001527D1 File Offset: 0x001509D1
		// (set) Token: 0x06002EB8 RID: 11960 RVA: 0x001527DE File Offset: 0x001509DE
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

		// Token: 0x06002EB9 RID: 11961 RVA: 0x001527EA File Offset: 0x001509EA
		public override void Flush()
		{
			this.baseStream.Flush();
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x001527F7 File Offset: 0x001509F7
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("BZip2InputStream Seek not supported");
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x00152803 File Offset: 0x00150A03
		public override void SetLength(long value)
		{
			throw new NotSupportedException("BZip2InputStream SetLength not supported");
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x0015280F File Offset: 0x00150A0F
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("BZip2InputStream Write not supported");
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0015281B File Offset: 0x00150A1B
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("BZip2InputStream WriteByte not supported");
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x00152828 File Offset: 0x00150A28
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

		// Token: 0x06002EBF RID: 11967 RVA: 0x00152864 File Offset: 0x00150A64
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.IsStreamOwner)
			{
				this.baseStream.Dispose();
			}
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x0015287C File Offset: 0x00150A7C
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

		// Token: 0x06002EC1 RID: 11969 RVA: 0x001528E8 File Offset: 0x00150AE8
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

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00152944 File Offset: 0x00150B44
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

		// Token: 0x06002EC3 RID: 11971 RVA: 0x001529A0 File Offset: 0x00150BA0
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

		// Token: 0x06002EC4 RID: 11972 RVA: 0x00152A64 File Offset: 0x00150C64
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

		// Token: 0x06002EC5 RID: 11973 RVA: 0x00152AC3 File Offset: 0x00150CC3
		private void Complete()
		{
			this.storedCombinedCRC = this.BsGetInt32();
			if (this.storedCombinedCRC != (int)this.computedCombinedCRC)
			{
				BZip2InputStream.CrcError();
			}
			this.streamEnd = true;
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x00152AEC File Offset: 0x00150CEC
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

		// Token: 0x06002EC7 RID: 11975 RVA: 0x00152B50 File Offset: 0x00150D50
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

		// Token: 0x06002EC8 RID: 11976 RVA: 0x00152B8C File Offset: 0x00150D8C
		private char BsGetUChar()
		{
			return (char)this.BsR(8);
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x00152B96 File Offset: 0x00150D96
		private int BsGetIntVS(int numBits)
		{
			return this.BsR(numBits);
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x00152B9F File Offset: 0x00150D9F
		private int BsGetInt32()
		{
			return ((this.BsR(8) << 8 | this.BsR(8)) << 8 | this.BsR(8)) << 8 | this.BsR(8);
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x00152BC8 File Offset: 0x00150DC8
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

		// Token: 0x06002ECC RID: 11980 RVA: 0x00152E14 File Offset: 0x00151014
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

		// Token: 0x06002ECD RID: 11981 RVA: 0x001531EC File Offset: 0x001513EC
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

		// Token: 0x06002ECE RID: 11982 RVA: 0x001532C0 File Offset: 0x001514C0
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

		// Token: 0x06002ECF RID: 11983 RVA: 0x001533BC File Offset: 0x001515BC
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

		// Token: 0x06002ED0 RID: 11984 RVA: 0x00153450 File Offset: 0x00151650
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

		// Token: 0x06002ED1 RID: 11985 RVA: 0x00153548 File Offset: 0x00151748
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

		// Token: 0x06002ED2 RID: 11986 RVA: 0x001535B4 File Offset: 0x001517B4
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

		// Token: 0x06002ED3 RID: 11987 RVA: 0x00153644 File Offset: 0x00151844
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

		// Token: 0x06002ED4 RID: 11988 RVA: 0x001536B0 File Offset: 0x001518B0
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

		// Token: 0x06002ED5 RID: 11989 RVA: 0x0015370F File Offset: 0x0015190F
		private static void CompressedStreamEOF()
		{
			throw new EndOfStreamException("BZip2 input stream end of compressed stream");
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x0015371B File Offset: 0x0015191B
		private static void BlockOverrun()
		{
			throw new BZip2Exception("BZip2 input stream block overrun");
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x00153727 File Offset: 0x00151927
		private static void BadBlockHeader()
		{
			throw new BZip2Exception("BZip2 input stream bad block header");
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x00153733 File Offset: 0x00151933
		private static void CrcError()
		{
			throw new BZip2Exception("BZip2 input stream crc error");
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x00153740 File Offset: 0x00151940
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

		// Token: 0x040028FF RID: 10495
		private const int START_BLOCK_STATE = 1;

		// Token: 0x04002900 RID: 10496
		private const int RAND_PART_A_STATE = 2;

		// Token: 0x04002901 RID: 10497
		private const int RAND_PART_B_STATE = 3;

		// Token: 0x04002902 RID: 10498
		private const int RAND_PART_C_STATE = 4;

		// Token: 0x04002903 RID: 10499
		private const int NO_RAND_PART_A_STATE = 5;

		// Token: 0x04002904 RID: 10500
		private const int NO_RAND_PART_B_STATE = 6;

		// Token: 0x04002905 RID: 10501
		private const int NO_RAND_PART_C_STATE = 7;

		// Token: 0x04002906 RID: 10502
		private int last;

		// Token: 0x04002907 RID: 10503
		private int origPtr;

		// Token: 0x04002908 RID: 10504
		private int blockSize100k;

		// Token: 0x04002909 RID: 10505
		private bool blockRandomised;

		// Token: 0x0400290A RID: 10506
		private int bsBuff;

		// Token: 0x0400290B RID: 10507
		private int bsLive;

		// Token: 0x0400290C RID: 10508
		private IChecksum mCrc = new BZip2Crc();

		// Token: 0x0400290D RID: 10509
		private bool[] inUse = new bool[256];

		// Token: 0x0400290E RID: 10510
		private int nInUse;

		// Token: 0x0400290F RID: 10511
		private byte[] seqToUnseq = new byte[256];

		// Token: 0x04002910 RID: 10512
		private byte[] unseqToSeq = new byte[256];

		// Token: 0x04002911 RID: 10513
		private byte[] selector = new byte[18002];

		// Token: 0x04002912 RID: 10514
		private byte[] selectorMtf = new byte[18002];

		// Token: 0x04002913 RID: 10515
		private int[] tt;

		// Token: 0x04002914 RID: 10516
		private byte[] ll8;

		// Token: 0x04002915 RID: 10517
		private int[] unzftab = new int[256];

		// Token: 0x04002916 RID: 10518
		private int[][] limit = new int[6][];

		// Token: 0x04002917 RID: 10519
		private int[][] baseArray = new int[6][];

		// Token: 0x04002918 RID: 10520
		private int[][] perm = new int[6][];

		// Token: 0x04002919 RID: 10521
		private int[] minLens = new int[6];

		// Token: 0x0400291A RID: 10522
		private readonly Stream baseStream;

		// Token: 0x0400291B RID: 10523
		private bool streamEnd;

		// Token: 0x0400291C RID: 10524
		private int currentChar = -1;

		// Token: 0x0400291D RID: 10525
		private int currentState = 1;

		// Token: 0x0400291E RID: 10526
		private int storedBlockCRC;

		// Token: 0x0400291F RID: 10527
		private int storedCombinedCRC;

		// Token: 0x04002920 RID: 10528
		private int computedBlockCRC;

		// Token: 0x04002921 RID: 10529
		private uint computedCombinedCRC;

		// Token: 0x04002922 RID: 10530
		private int count;

		// Token: 0x04002923 RID: 10531
		private int chPrev;

		// Token: 0x04002924 RID: 10532
		private int ch2;

		// Token: 0x04002925 RID: 10533
		private int tPos;

		// Token: 0x04002926 RID: 10534
		private int rNToGo;

		// Token: 0x04002927 RID: 10535
		private int rTPos;

		// Token: 0x04002928 RID: 10536
		private int i2;

		// Token: 0x04002929 RID: 10537
		private int j2;

		// Token: 0x0400292A RID: 10538
		private byte z;
	}
}
