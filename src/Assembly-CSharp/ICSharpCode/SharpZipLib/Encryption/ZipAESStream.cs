using System;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000576 RID: 1398
	internal class ZipAESStream : CryptoStream
	{
		// Token: 0x06002E14 RID: 11796 RVA: 0x00150C54 File Offset: 0x0014EE54
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

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x00150C8B File Offset: 0x0014EE8B
		private bool HasBufferedData
		{
			get
			{
				return this._transformBuffer != null && this._transformBufferStartPos < this._transformBufferFreePos;
			}
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x00150CA8 File Offset: 0x0014EEA8
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

		// Token: 0x06002E17 RID: 11799 RVA: 0x00150CF4 File Offset: 0x0014EEF4
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

		// Token: 0x06002E18 RID: 11800 RVA: 0x00150E54 File Offset: 0x0014F054
		private int ReadBufferedData(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._transformBufferFreePos - this._transformBufferStartPos);
			Array.Copy(this._transformBuffer, this._transformBufferStartPos, buffer, offset, num);
			this._transformBufferStartPos += num;
			return num;
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x00150E98 File Offset: 0x0014F098
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

		// Token: 0x06002E1A RID: 11802 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040028B7 RID: 10423
		private const int AUTH_CODE_LENGTH = 10;

		// Token: 0x040028B8 RID: 10424
		private const int CRYPTO_BLOCK_SIZE = 16;

		// Token: 0x040028B9 RID: 10425
		private const int BLOCK_AND_AUTH = 26;

		// Token: 0x040028BA RID: 10426
		private Stream _stream;

		// Token: 0x040028BB RID: 10427
		private ZipAESTransform _transform;

		// Token: 0x040028BC RID: 10428
		private byte[] _slideBuffer;

		// Token: 0x040028BD RID: 10429
		private int _slideBufStartPos;

		// Token: 0x040028BE RID: 10430
		private int _slideBufFreePos;

		// Token: 0x040028BF RID: 10431
		private byte[] _transformBuffer;

		// Token: 0x040028C0 RID: 10432
		private int _transformBufferFreePos;

		// Token: 0x040028C1 RID: 10433
		private int _transformBufferStartPos;
	}
}
