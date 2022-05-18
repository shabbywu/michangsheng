using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000802 RID: 2050
	public class InflaterInputStream : Stream
	{
		// Token: 0x06003510 RID: 13584 RVA: 0x00026BDA File Offset: 0x00024DDA
		public InflaterInputStream(Stream baseInputStream) : this(baseInputStream, new Inflater(), 4096)
		{
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x00026BED File Offset: 0x00024DED
		public InflaterInputStream(Stream baseInputStream, Inflater inf) : this(baseInputStream, inf, 4096)
		{
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x00197FCC File Offset: 0x001961CC
		public InflaterInputStream(Stream baseInputStream, Inflater inflater, int bufferSize)
		{
			if (baseInputStream == null)
			{
				throw new ArgumentNullException("baseInputStream");
			}
			if (inflater == null)
			{
				throw new ArgumentNullException("inflater");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.baseInputStream = baseInputStream;
			this.inf = inflater;
			this.inputBuffer = new InflaterInputBuffer(baseInputStream, bufferSize);
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x00026BFC File Offset: 0x00024DFC
		// (set) Token: 0x06003514 RID: 13588 RVA: 0x00026C04 File Offset: 0x00024E04
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x06003515 RID: 13589 RVA: 0x0019802C File Offset: 0x0019622C
		public long Skip(long count)
		{
			if (count <= 0L)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.baseInputStream.CanSeek)
			{
				this.baseInputStream.Seek(count, SeekOrigin.Current);
				return count;
			}
			int num = 2048;
			if (count < (long)num)
			{
				num = (int)count;
			}
			byte[] buffer = new byte[num];
			int num2 = 1;
			long num3 = count;
			while (num3 > 0L && num2 > 0)
			{
				if (num3 < (long)num)
				{
					num = (int)num3;
				}
				num2 = this.baseInputStream.Read(buffer, 0, num);
				num3 -= (long)num2;
			}
			return count - num3;
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x00026C0D File Offset: 0x00024E0D
		protected void StopDecrypting()
		{
			this.inputBuffer.CryptoTransform = null;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x00026C1B File Offset: 0x00024E1B
		public virtual int Available
		{
			get
			{
				if (!this.inf.IsFinished)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x001980AC File Offset: 0x001962AC
		protected void Fill()
		{
			if (this.inputBuffer.Available <= 0)
			{
				this.inputBuffer.Fill();
				if (this.inputBuffer.Available <= 0)
				{
					throw new SharpZipBaseException("Unexpected EOF");
				}
			}
			this.inputBuffer.SetInflaterInput(this.inf);
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x00026C2D File Offset: 0x00024E2D
		public override bool CanRead
		{
			get
			{
				return this.baseInputStream.CanRead;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x00026C3A File Offset: 0x00024E3A
		public override long Length
		{
			get
			{
				throw new NotSupportedException("InflaterInputStream Length is not supported");
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x00026C46 File Offset: 0x00024E46
		// (set) Token: 0x0600351E RID: 13598 RVA: 0x00026C53 File Offset: 0x00024E53
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

		// Token: 0x0600351F RID: 13599 RVA: 0x00026C5F File Offset: 0x00024E5F
		public override void Flush()
		{
			this.baseInputStream.Flush();
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x00026C6C File Offset: 0x00024E6C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek not supported");
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x00026C78 File Offset: 0x00024E78
		public override void SetLength(long value)
		{
			throw new NotSupportedException("InflaterInputStream SetLength not supported");
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x00026C84 File Offset: 0x00024E84
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("InflaterInputStream Write not supported");
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x00026C90 File Offset: 0x00024E90
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("InflaterInputStream WriteByte not supported");
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x00026C9C File Offset: 0x00024E9C
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

		// Token: 0x06003525 RID: 13605 RVA: 0x001980FC File Offset: 0x001962FC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.inf.IsNeedingDictionary)
			{
				throw new SharpZipBaseException("Need a dictionary");
			}
			int num = count;
			for (;;)
			{
				int num2 = this.inf.Inflate(buffer, offset, num);
				offset += num2;
				num -= num2;
				if (num == 0 || this.inf.IsFinished)
				{
					goto IL_65;
				}
				if (this.inf.IsNeedingInput)
				{
					this.Fill();
				}
				else if (num2 == 0)
				{
					break;
				}
			}
			throw new ZipException("Invalid input data");
			IL_65:
			return count - num;
		}

		// Token: 0x04003039 RID: 12345
		protected Inflater inf;

		// Token: 0x0400303A RID: 12346
		protected InflaterInputBuffer inputBuffer;

		// Token: 0x0400303B RID: 12347
		private Stream baseInputStream;

		// Token: 0x0400303C RID: 12348
		protected long csize;

		// Token: 0x0400303D RID: 12349
		private bool isClosed;
	}
}
