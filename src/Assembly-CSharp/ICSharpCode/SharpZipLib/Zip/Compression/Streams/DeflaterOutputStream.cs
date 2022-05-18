using System;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Encryption;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000800 RID: 2048
	public class DeflaterOutputStream : Stream
	{
		// Token: 0x060034E0 RID: 13536 RVA: 0x000269A7 File Offset: 0x00024BA7
		public DeflaterOutputStream(Stream baseOutputStream) : this(baseOutputStream, new Deflater(), 512)
		{
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000269BA File Offset: 0x00024BBA
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater) : this(baseOutputStream, deflater, 512)
		{
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x00197A28 File Offset: 0x00195C28
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

		// Token: 0x060034E3 RID: 13539 RVA: 0x00197AA4 File Offset: 0x00195CA4
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

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000269C9 File Offset: 0x00024BC9
		// (set) Token: 0x060034E5 RID: 13541 RVA: 0x000269D1 File Offset: 0x00024BD1
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000269DA File Offset: 0x00024BDA
		public bool CanPatchEntries
		{
			get
			{
				return this.baseOutputStream_.CanSeek;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000269E7 File Offset: 0x00024BE7
		// (set) Token: 0x060034E8 RID: 13544 RVA: 0x000269EF File Offset: 0x00024BEF
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

		// Token: 0x060034E9 RID: 13545 RVA: 0x00026A0B File Offset: 0x00024C0B
		protected void EncryptBlock(byte[] buffer, int offset, int length)
		{
			this.cryptoTransform_.TransformBlock(buffer, 0, length, buffer, 0);
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x00197B74 File Offset: 0x00195D74
		protected void InitializePassword(string password)
		{
			PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
			byte[] rgbKey = PkzipClassic.GenerateKeys(ZipStrings.ConvertToArray(password));
			this.cryptoTransform_ = pkzipClassicManaged.CreateEncryptor(rgbKey, null);
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x00197BA4 File Offset: 0x00195DA4
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

		// Token: 0x060034EC RID: 13548 RVA: 0x00026A1E File Offset: 0x00024C1E
		protected void Deflate()
		{
			this.Deflate(false);
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x00197C08 File Offset: 0x00195E08
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

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060034F0 RID: 13552 RVA: 0x00026A27 File Offset: 0x00024C27
		public override bool CanWrite
		{
			get
			{
				return this.baseOutputStream_.CanWrite;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060034F1 RID: 13553 RVA: 0x00026A34 File Offset: 0x00024C34
		public override long Length
		{
			get
			{
				return this.baseOutputStream_.Length;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x00026A41 File Offset: 0x00024C41
		// (set) Token: 0x060034F3 RID: 13555 RVA: 0x00026A4E File Offset: 0x00024C4E
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

		// Token: 0x060034F4 RID: 13556 RVA: 0x00026A5A File Offset: 0x00024C5A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("DeflaterOutputStream Seek not supported");
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x00026A66 File Offset: 0x00024C66
		public override void SetLength(long value)
		{
			throw new NotSupportedException("DeflaterOutputStream SetLength not supported");
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x00026A72 File Offset: 0x00024C72
		public override int ReadByte()
		{
			throw new NotSupportedException("DeflaterOutputStream ReadByte not supported");
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x00026A7E File Offset: 0x00024C7E
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("DeflaterOutputStream Read not supported");
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x00026A8A File Offset: 0x00024C8A
		public override void Flush()
		{
			this.deflater_.Flush();
			this.Deflate(true);
			this.baseOutputStream_.Flush();
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x00197C88 File Offset: 0x00195E88
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

		// Token: 0x060034FA RID: 13562 RVA: 0x00026AA9 File Offset: 0x00024CA9
		protected void GetAuthCodeIfAES()
		{
			if (this.cryptoTransform_ is ZipAESTransform)
			{
				this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
			}
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x00197CF0 File Offset: 0x00195EF0
		public override void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x00026ACE File Offset: 0x00024CCE
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.deflater_.SetInput(buffer, offset, count);
			this.Deflate();
		}

		// Token: 0x04003028 RID: 12328
		private string password;

		// Token: 0x04003029 RID: 12329
		private ICryptoTransform cryptoTransform_;

		// Token: 0x0400302A RID: 12330
		protected byte[] AESAuthCode;

		// Token: 0x0400302B RID: 12331
		private byte[] buffer_;

		// Token: 0x0400302C RID: 12332
		protected Deflater deflater_;

		// Token: 0x0400302D RID: 12333
		protected Stream baseOutputStream_;

		// Token: 0x0400302E RID: 12334
		private bool isClosed_;

		// Token: 0x0400302F RID: 12335
		private static RandomNumberGenerator _aesRnd = RandomNumberGenerator.Create();
	}
}
