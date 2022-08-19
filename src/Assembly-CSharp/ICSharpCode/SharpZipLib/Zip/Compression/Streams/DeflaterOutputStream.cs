using System;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Encryption;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200055A RID: 1370
	public class DeflaterOutputStream : Stream
	{
		// Token: 0x06002C72 RID: 11378 RVA: 0x0014B773 File Offset: 0x00149973
		public DeflaterOutputStream(Stream baseOutputStream) : this(baseOutputStream, new Deflater(), 512)
		{
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x0014B786 File Offset: 0x00149986
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater) : this(baseOutputStream, deflater, 512)
		{
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x0014B798 File Offset: 0x00149998
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater, int bufferSize)
		{
			if (baseOutputStream == null)
			{
				throw new ArgumentNullException("baseOutputStream");
			}
			if (!baseOutputStream.CanWrite)
			{
				throw new ArgumentException("Must support writing", "baseOutputStream");
			}
			if (bufferSize < 512)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.baseOutputStream_ = baseOutputStream;
			this.buffer_ = new byte[bufferSize];
			if (deflater == null)
			{
				throw new ArgumentNullException("deflater");
			}
			this.deflater_ = deflater;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x0014B814 File Offset: 0x00149A14
		public virtual void Finish()
		{
			this.deflater_.Finish();
			while (!this.deflater_.IsFinished)
			{
				int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
				if (num <= 0)
				{
					break;
				}
				if (this.cryptoTransform_ != null)
				{
					this.EncryptBlock(this.buffer_, 0, num);
				}
				this.baseOutputStream_.Write(this.buffer_, 0, num);
			}
			if (!this.deflater_.IsFinished)
			{
				throw new SharpZipBaseException("Can't deflate all input?");
			}
			this.baseOutputStream_.Flush();
			if (this.cryptoTransform_ != null)
			{
				if (this.cryptoTransform_ is ZipAESTransform)
				{
					this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
				}
				this.cryptoTransform_.Dispose();
				this.cryptoTransform_ = null;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x0014B8E3 File Offset: 0x00149AE3
		// (set) Token: 0x06002C77 RID: 11383 RVA: 0x0014B8EB File Offset: 0x00149AEB
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x0014B8F4 File Offset: 0x00149AF4
		public bool CanPatchEntries
		{
			get
			{
				return this.baseOutputStream_.CanSeek;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x0014B901 File Offset: 0x00149B01
		// (set) Token: 0x06002C7A RID: 11386 RVA: 0x0014B909 File Offset: 0x00149B09
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					this.password = null;
					return;
				}
				this.password = value;
			}
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x0014B925 File Offset: 0x00149B25
		protected void EncryptBlock(byte[] buffer, int offset, int length)
		{
			this.cryptoTransform_.TransformBlock(buffer, 0, length, buffer, 0);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x0014B938 File Offset: 0x00149B38
		protected void InitializePassword(string password)
		{
			PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
			byte[] rgbKey = PkzipClassic.GenerateKeys(ZipStrings.ConvertToArray(password));
			this.cryptoTransform_ = pkzipClassicManaged.CreateEncryptor(rgbKey, null);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x0014B968 File Offset: 0x00149B68
		protected void InitializeAESPassword(ZipEntry entry, string rawPassword, out byte[] salt, out byte[] pwdVerifier)
		{
			salt = new byte[entry.AESSaltLen];
			if (DeflaterOutputStream._aesRnd == null)
			{
				DeflaterOutputStream._aesRnd = RandomNumberGenerator.Create();
			}
			DeflaterOutputStream._aesRnd.GetBytes(salt);
			int blockSize = entry.AESKeySize / 8;
			this.cryptoTransform_ = new ZipAESTransform(rawPassword, salt, blockSize, true);
			pwdVerifier = ((ZipAESTransform)this.cryptoTransform_).PwdVerifier;
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x0014B9CB File Offset: 0x00149BCB
		protected void Deflate()
		{
			this.Deflate(false);
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x0014B9D4 File Offset: 0x00149BD4
		private void Deflate(bool flushing)
		{
			while (flushing || !this.deflater_.IsNeedingInput)
			{
				int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
				if (num <= 0)
				{
					break;
				}
				if (this.cryptoTransform_ != null)
				{
					this.EncryptBlock(this.buffer_, 0, num);
				}
				this.baseOutputStream_.Write(this.buffer_, 0, num);
			}
			if (!this.deflater_.IsNeedingInput)
			{
				throw new SharpZipBaseException("DeflaterOutputStream can't deflate all input?");
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x0014BA53 File Offset: 0x00149C53
		public override bool CanWrite
		{
			get
			{
				return this.baseOutputStream_.CanWrite;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x0014BA60 File Offset: 0x00149C60
		public override long Length
		{
			get
			{
				return this.baseOutputStream_.Length;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x0014BA6D File Offset: 0x00149C6D
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x0014BA7A File Offset: 0x00149C7A
		public override long Position
		{
			get
			{
				return this.baseOutputStream_.Position;
			}
			set
			{
				throw new NotSupportedException("Position property not supported");
			}
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x0014BA86 File Offset: 0x00149C86
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("DeflaterOutputStream Seek not supported");
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x0014BA92 File Offset: 0x00149C92
		public override void SetLength(long value)
		{
			throw new NotSupportedException("DeflaterOutputStream SetLength not supported");
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x0014BA9E File Offset: 0x00149C9E
		public override int ReadByte()
		{
			throw new NotSupportedException("DeflaterOutputStream ReadByte not supported");
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x0014BAAA File Offset: 0x00149CAA
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("DeflaterOutputStream Read not supported");
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x0014BAB6 File Offset: 0x00149CB6
		public override void Flush()
		{
			this.deflater_.Flush();
			this.Deflate(true);
			this.baseOutputStream_.Flush();
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x0014BAD8 File Offset: 0x00149CD8
		protected override void Dispose(bool disposing)
		{
			if (!this.isClosed_)
			{
				this.isClosed_ = true;
				try
				{
					this.Finish();
					if (this.cryptoTransform_ != null)
					{
						this.GetAuthCodeIfAES();
						this.cryptoTransform_.Dispose();
						this.cryptoTransform_ = null;
					}
				}
				finally
				{
					if (this.IsStreamOwner)
					{
						this.baseOutputStream_.Dispose();
					}
				}
			}
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x0014BB40 File Offset: 0x00149D40
		protected void GetAuthCodeIfAES()
		{
			if (this.cryptoTransform_ is ZipAESTransform)
			{
				this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
			}
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x0014BB68 File Offset: 0x00149D68
		public override void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x0014BB89 File Offset: 0x00149D89
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.deflater_.SetInput(buffer, offset, count);
			this.Deflate();
		}

		// Token: 0x040027EF RID: 10223
		private string password;

		// Token: 0x040027F0 RID: 10224
		private ICryptoTransform cryptoTransform_;

		// Token: 0x040027F1 RID: 10225
		protected byte[] AESAuthCode;

		// Token: 0x040027F2 RID: 10226
		private byte[] buffer_;

		// Token: 0x040027F3 RID: 10227
		protected Deflater deflater_;

		// Token: 0x040027F4 RID: 10228
		protected Stream baseOutputStream_;

		// Token: 0x040027F5 RID: 10229
		private bool isClosed_;

		// Token: 0x040027F6 RID: 10230
		private static RandomNumberGenerator _aesRnd = RandomNumberGenerator.Create();
	}
}
