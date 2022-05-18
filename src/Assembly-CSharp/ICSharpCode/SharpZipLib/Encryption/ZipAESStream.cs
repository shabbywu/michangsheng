using System;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200081F RID: 2079
	internal class ZipAESStream : CryptoStream
	{
		// Token: 0x0600368A RID: 13962 RVA: 0x00027BF5 File Offset: 0x00025DF5
		public ZipAESStream(Stream stream, ZipAESTransform transform, CryptoStreamMode mode) : base(stream, transform, mode)
		{
			this._stream = stream;
			this._transform = transform;
			this._slideBuffer = new byte[1024];
			if (mode != CryptoStreamMode.Read)
			{
				throw new Exception("ZipAESStream only for read");
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x00027C2C File Offset: 0x00025E2C
		private bool HasBufferedData
		{
			get
			{
				return this._transformBuffer != null && this._transformBufferStartPos < this._transformBufferFreePos;
			}
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x0019BCD8 File Offset: 0x00199ED8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num = 0;
			if (this.HasBufferedData)
			{
				num = this.ReadBufferedData(buffer, offset, count);
				if (num == count)
				{
					return num;
				}
				offset += num;
				count -= num;
			}
			if (this._slideBuffer != null)
			{
				num += this.ReadAndTransform(buffer, offset, count);
			}
			return num;
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x0019BD24 File Offset: 0x00199F24
		private int ReadAndTransform(byte[] buffer, int offset, int count)
		{
			int i = 0;
			while (i < count)
			{
				int count2 = count - i;
				int num = this._slideBufFreePos - this._slideBufStartPos;
				int num2 = 26 - num;
				if (this._slideBuffer.Length - this._slideBufFreePos < num2)
				{
					int num3 = 0;
					int j = this._slideBufStartPos;
					while (j < this._slideBufFreePos)
					{
						this._slideBuffer[num3] = this._slideBuffer[j];
						j++;
						num3++;
					}
					this._slideBufFreePos -= this._slideBufStartPos;
					this._slideBufStartPos = 0;
				}
				int num4 = StreamUtils.ReadRequestedBytes(this._stream, this._slideBuffer, this._slideBufFreePos, num2);
				this._slideBufFreePos += num4;
				num = this._slideBufFreePos - this._slideBufStartPos;
				if (num < 26)
				{
					if (num > 10)
					{
						int blockSize = num - 10;
						i += this.TransformAndBufferBlock(buffer, offset, count2, blockSize);
					}
					else if (num < 10)
					{
						throw new Exception("Internal error missed auth code");
					}
					byte[] authCode = this._transform.GetAuthCode();
					for (int k = 0; k < 10; k++)
					{
						if (authCode[k] != this._slideBuffer[this._slideBufStartPos + k])
						{
							throw new Exception("AES Authentication Code does not match. This is a super-CRC check on the data in the file after compression and encryption. \r\nThe file may be damaged.");
						}
					}
					this._slideBuffer = null;
					break;
				}
				int num5 = this.TransformAndBufferBlock(buffer, offset, count2, 16);
				i += num5;
				offset += num5;
			}
			return i;
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x0019BE84 File Offset: 0x0019A084
		private int ReadBufferedData(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._transformBufferFreePos - this._transformBufferStartPos);
			Array.Copy(this._transformBuffer, this._transformBufferStartPos, buffer, offset, num);
			this._transformBufferStartPos += num;
			return num;
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x0019BEC8 File Offset: 0x0019A0C8
		private int TransformAndBufferBlock(byte[] buffer, int offset, int count, int blockSize)
		{
			bool flag = blockSize > count;
			if (flag && this._transformBuffer == null)
			{
				this._transformBuffer = new byte[16];
			}
			byte[] outputBuffer = flag ? this._transformBuffer : buffer;
			int outputOffset = flag ? 0 : offset;
			this._transform.TransformBlock(this._slideBuffer, this._slideBufStartPos, blockSize, outputBuffer, outputOffset);
			this._slideBufStartPos += blockSize;
			if (!flag)
			{
				return blockSize;
			}
			Array.Copy(this._transformBuffer, 0, buffer, offset, count);
			this._transformBufferStartPos = count;
			this._transformBufferFreePos = blockSize;
			return count;
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x0001C722 File Offset: 0x0001A922
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040030F6 RID: 12534
		private const int AUTH_CODE_LENGTH = 10;

		// Token: 0x040030F7 RID: 12535
		private const int CRYPTO_BLOCK_SIZE = 16;

		// Token: 0x040030F8 RID: 12536
		private const int BLOCK_AND_AUTH = 26;

		// Token: 0x040030F9 RID: 12537
		private Stream _stream;

		// Token: 0x040030FA RID: 12538
		private ZipAESTransform _transform;

		// Token: 0x040030FB RID: 12539
		private byte[] _slideBuffer;

		// Token: 0x040030FC RID: 12540
		private int _slideBufStartPos;

		// Token: 0x040030FD RID: 12541
		private int _slideBufFreePos;

		// Token: 0x040030FE RID: 12542
		private byte[] _transformBuffer;

		// Token: 0x040030FF RID: 12543
		private int _transformBufferFreePos;

		// Token: 0x04003100 RID: 12544
		private int _transformBufferStartPos;
	}
}
