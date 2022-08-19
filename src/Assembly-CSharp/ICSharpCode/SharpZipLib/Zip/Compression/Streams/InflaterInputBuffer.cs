using System;
using System.IO;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200055B RID: 1371
	public class InflaterInputBuffer
	{
		// Token: 0x06002C90 RID: 11408 RVA: 0x0014BBAB File Offset: 0x00149DAB
		public InflaterInputBuffer(Stream stream) : this(stream, 4096)
		{
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0014BBB9 File Offset: 0x00149DB9
		public InflaterInputBuffer(Stream stream, int bufferSize)
		{
			this.inputStream = stream;
			if (bufferSize < 1024)
			{
				bufferSize = 1024;
			}
			this.rawData = new byte[bufferSize];
			this.clearText = this.rawData;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x0014BBEF File Offset: 0x00149DEF
		public int RawLength
		{
			get
			{
				return this.rawLength;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x0014BBF7 File Offset: 0x00149DF7
		public byte[] RawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x0014BBFF File Offset: 0x00149DFF
		public int ClearTextLength
		{
			get
			{
				return this.clearTextLength;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x0014BC07 File Offset: 0x00149E07
		public byte[] ClearText
		{
			get
			{
				return this.clearText;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x0014BC0F File Offset: 0x00149E0F
		// (set) Token: 0x06002C97 RID: 11415 RVA: 0x0014BC17 File Offset: 0x00149E17
		public int Available
		{
			get
			{
				return this.available;
			}
			set
			{
				this.available = value;
			}
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x0014BC20 File Offset: 0x00149E20
		public void SetInflaterInput(Inflater inflater)
		{
			if (this.available > 0)
			{
				inflater.SetInput(this.clearText, this.clearTextLength - this.available, this.available);
				this.available = 0;
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x0014BC54 File Offset: 0x00149E54
		public void Fill()
		{
			this.rawLength = 0;
			int num = this.rawData.Length;
			while (num > 0 && this.inputStream.CanRead)
			{
				int num2 = this.inputStream.Read(this.rawData, this.rawLength, num);
				if (num2 <= 0)
				{
					break;
				}
				this.rawLength += num2;
				num -= num2;
			}
			if (this.cryptoTransform != null)
			{
				this.clearTextLength = this.cryptoTransform.TransformBlock(this.rawData, 0, this.rawLength, this.clearText, 0);
			}
			else
			{
				this.clearTextLength = this.rawLength;
			}
			this.available = this.clearTextLength;
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x0014BCFA File Offset: 0x00149EFA
		public int ReadRawBuffer(byte[] buffer)
		{
			return this.ReadRawBuffer(buffer, 0, buffer.Length);
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x0014BD08 File Offset: 0x00149F08
		public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = offset;
			int i = length;
			while (i > 0)
			{
				if (this.available <= 0)
				{
					this.Fill();
					if (this.available <= 0)
					{
						return 0;
					}
				}
				int num2 = Math.Min(i, this.available);
				Array.Copy(this.rawData, this.rawLength - this.available, outBuffer, num, num2);
				num += num2;
				i -= num2;
				this.available -= num2;
			}
			return length;
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x0014BD88 File Offset: 0x00149F88
		public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = offset;
			int i = length;
			while (i > 0)
			{
				if (this.available <= 0)
				{
					this.Fill();
					if (this.available <= 0)
					{
						return 0;
					}
				}
				int num2 = Math.Min(i, this.available);
				Array.Copy(this.clearText, this.clearTextLength - this.available, outBuffer, num, num2);
				num += num2;
				i -= num2;
				this.available -= num2;
			}
			return length;
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x0014BE08 File Offset: 0x0014A008
		public int ReadLeByte()
		{
			if (this.available <= 0)
			{
				this.Fill();
				if (this.available <= 0)
				{
					throw new ZipException("EOF in header");
				}
			}
			int result = (int)this.rawData[this.rawLength - this.available];
			this.available--;
			return result;
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x0014BE5A File Offset: 0x0014A05A
		public int ReadLeShort()
		{
			return this.ReadLeByte() | this.ReadLeByte() << 8;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x0014BE6B File Offset: 0x0014A06B
		public int ReadLeInt()
		{
			return this.ReadLeShort() | this.ReadLeShort() << 16;
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x0014BE7D File Offset: 0x0014A07D
		public long ReadLeLong()
		{
			return (long)((ulong)this.ReadLeInt() | (ulong)((ulong)((long)this.ReadLeInt()) << 32));
		}

		// Token: 0x17000357 RID: 855
		// (set) Token: 0x06002CA1 RID: 11425 RVA: 0x0014BE94 File Offset: 0x0014A094
		public ICryptoTransform CryptoTransform
		{
			set
			{
				this.cryptoTransform = value;
				if (this.cryptoTransform != null)
				{
					if (this.rawData == this.clearText)
					{
						if (this.internalClearText == null)
						{
							this.internalClearText = new byte[this.rawData.Length];
						}
						this.clearText = this.internalClearText;
					}
					this.clearTextLength = this.rawLength;
					if (this.available > 0)
					{
						this.cryptoTransform.TransformBlock(this.rawData, this.rawLength - this.available, this.available, this.clearText, this.rawLength - this.available);
						return;
					}
				}
				else
				{
					this.clearText = this.rawData;
					this.clearTextLength = this.rawLength;
				}
			}
		}

		// Token: 0x040027F7 RID: 10231
		private int rawLength;

		// Token: 0x040027F8 RID: 10232
		private byte[] rawData;

		// Token: 0x040027F9 RID: 10233
		private int clearTextLength;

		// Token: 0x040027FA RID: 10234
		private byte[] clearText;

		// Token: 0x040027FB RID: 10235
		private byte[] internalClearText;

		// Token: 0x040027FC RID: 10236
		private int available;

		// Token: 0x040027FD RID: 10237
		private ICryptoTransform cryptoTransform;

		// Token: 0x040027FE RID: 10238
		private Stream inputStream;
	}
}
