using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200055C RID: 1372
	public class InflaterInputStream : Stream
	{
		// Token: 0x06002CA2 RID: 11426 RVA: 0x0014BF4E File Offset: 0x0014A14E
		public InflaterInputStream(Stream baseInputStream) : this(baseInputStream, new Inflater(), 4096)
		{
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x0014BF61 File Offset: 0x0014A161
		public InflaterInputStream(Stream baseInputStream, Inflater inf) : this(baseInputStream, inf, 4096)
		{
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x0014BF70 File Offset: 0x0014A170
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

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x0014BFD0 File Offset: 0x0014A1D0
		// (set) Token: 0x06002CA6 RID: 11430 RVA: 0x0014BFD8 File Offset: 0x0014A1D8
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x06002CA7 RID: 11431 RVA: 0x0014BFE4 File Offset: 0x0014A1E4
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

		// Token: 0x06002CA8 RID: 11432 RVA: 0x0014C061 File Offset: 0x0014A261
		protected void StopDecrypting()
		{
			this.inputBuffer.CryptoTransform = null;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x0014C06F File Offset: 0x0014A26F
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

		// Token: 0x06002CAA RID: 11434 RVA: 0x0014C084 File Offset: 0x0014A284
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

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x0014C0D4 File Offset: 0x0014A2D4
		public override bool CanRead
		{
			get
			{
				return this.baseInputStream.CanRead;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x0014C0E1 File Offset: 0x0014A2E1
		public override long Length
		{
			get
			{
				throw new NotSupportedException("InflaterInputStream Length is not supported");
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x0014C0ED File Offset: 0x0014A2ED
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x0014C0FA File Offset: 0x0014A2FA
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

		// Token: 0x06002CB1 RID: 11441 RVA: 0x0014C106 File Offset: 0x0014A306
		public override void Flush()
		{
			this.baseInputStream.Flush();
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x0014C113 File Offset: 0x0014A313
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek not supported");
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x0014C11F File Offset: 0x0014A31F
		public override void SetLength(long value)
		{
			throw new NotSupportedException("InflaterInputStream SetLength not supported");
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x0014C12B File Offset: 0x0014A32B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("InflaterInputStream Write not supported");
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x0014C137 File Offset: 0x0014A337
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("InflaterInputStream WriteByte not supported");
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x0014C143 File Offset: 0x0014A343
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

		// Token: 0x06002CB7 RID: 11447 RVA: 0x0014C168 File Offset: 0x0014A368
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

		// Token: 0x04002800 RID: 10240
		protected Inflater inf;

		// Token: 0x04002801 RID: 10241
		protected InflaterInputBuffer inputBuffer;

		// Token: 0x04002802 RID: 10242
		private Stream baseInputStream;

		// Token: 0x04002803 RID: 10243
		protected long csize;

		// Token: 0x04002804 RID: 10244
		private bool isClosed;
	}
}
