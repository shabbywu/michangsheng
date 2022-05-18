using System;
using System.IO;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000801 RID: 2049
	public class InflaterInputBuffer
	{
		// Token: 0x060034FE RID: 13566 RVA: 0x00026AF0 File Offset: 0x00024CF0
		public InflaterInputBuffer(Stream stream) : this(stream, 4096)
		{
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x00026AFE File Offset: 0x00024CFE
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

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x00026B34 File Offset: 0x00024D34
		public int RawLength
		{
			get
			{
				return this.rawLength;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06003501 RID: 13569 RVA: 0x00026B3C File Offset: 0x00024D3C
		public byte[] RawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x00026B44 File Offset: 0x00024D44
		public int ClearTextLength
		{
			get
			{
				return this.clearTextLength;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x00026B4C File Offset: 0x00024D4C
		public byte[] ClearText
		{
			get
			{
				return this.clearText;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x00026B54 File Offset: 0x00024D54
		// (set) Token: 0x06003505 RID: 13573 RVA: 0x00026B5C File Offset: 0x00024D5C
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

		// Token: 0x06003506 RID: 13574 RVA: 0x00026B65 File Offset: 0x00024D65
		public void SetInflaterInput(Inflater inflater)
		{
			if (this.available > 0)
			{
				inflater.SetInput(this.clearText, this.clearTextLength - this.available, this.available);
				this.available = 0;
			}
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x00197D14 File Offset: 0x00195F14
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

		// Token: 0x06003508 RID: 13576 RVA: 0x00026B96 File Offset: 0x00024D96
		public int ReadRawBuffer(byte[] buffer)
		{
			return this.ReadRawBuffer(buffer, 0, buffer.Length);
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x00197DBC File Offset: 0x00195FBC
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

		// Token: 0x0600350A RID: 13578 RVA: 0x00197E3C File Offset: 0x0019603C
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

		// Token: 0x0600350B RID: 13579 RVA: 0x00197EBC File Offset: 0x001960BC
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

		// Token: 0x0600350C RID: 13580 RVA: 0x00026BA3 File Offset: 0x00024DA3
		public int ReadLeShort()
		{
			return this.ReadLeByte() | this.ReadLeByte() << 8;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x00026BB4 File Offset: 0x00024DB4
		public int ReadLeInt()
		{
			return this.ReadLeShort() | this.ReadLeShort() << 16;
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x00026BC6 File Offset: 0x00024DC6
		public long ReadLeLong()
		{
			return (long)((ulong)this.ReadLeInt() | (ulong)((ulong)((long)this.ReadLeInt()) << 32));
		}

		// Token: 0x1700050E RID: 1294
		// (set) Token: 0x0600350F RID: 13583 RVA: 0x00197F10 File Offset: 0x00196110
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

		// Token: 0x04003030 RID: 12336
		private int rawLength;

		// Token: 0x04003031 RID: 12337
		private byte[] rawData;

		// Token: 0x04003032 RID: 12338
		private int clearTextLength;

		// Token: 0x04003033 RID: 12339
		private byte[] clearText;

		// Token: 0x04003034 RID: 12340
		private byte[] internalClearText;

		// Token: 0x04003035 RID: 12341
		private int available;

		// Token: 0x04003036 RID: 12342
		private ICryptoTransform cryptoTransform;

		// Token: 0x04003037 RID: 12343
		private Stream inputStream;
	}
}
