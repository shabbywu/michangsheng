using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000820 RID: 2080
	internal class ZipAESTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x06003691 RID: 13969 RVA: 0x0019BF58 File Offset: 0x0019A158
		public ZipAESTransform(string key, byte[] saltBytes, int blockSize, bool writeMode)
		{
			if (blockSize != 16 && blockSize != 32)
			{
				throw new Exception("Invalid blocksize " + blockSize + ". Must be 16 or 32.");
			}
			if (saltBytes.Length != blockSize / 2)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Invalid salt len. Must be ",
					blockSize / 2,
					" for blocksize ",
					blockSize
				}));
			}
			this._blockSize = blockSize;
			this._encryptBuffer = new byte[this._blockSize];
			this._encrPos = 16;
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, saltBytes, 1000);
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.ECB;
			this._counterNonce = new byte[this._blockSize];
			byte[] bytes = rfc2898DeriveBytes.GetBytes(this._blockSize);
			byte[] bytes2 = rfc2898DeriveBytes.GetBytes(this._blockSize);
			this._encryptor = aes.CreateEncryptor(bytes, new byte[16]);
			this._pwdVerifier = rfc2898DeriveBytes.GetBytes(2);
			this._hmacsha1 = IncrementalHash.CreateHMAC(HashAlgorithmName.SHA1, bytes2);
			this._writeMode = writeMode;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x0019C06C File Offset: 0x0019A26C
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (!this._writeMode)
			{
				this._hmacsha1.AppendData(inputBuffer, inputOffset, inputCount);
			}
			for (int i = 0; i < inputCount; i++)
			{
				if (this._encrPos == 16)
				{
					int num = 0;
					for (;;)
					{
						byte[] counterNonce = this._counterNonce;
						int num2 = num;
						byte b = counterNonce[num2] + 1;
						counterNonce[num2] = b;
						if (b != 0)
						{
							break;
						}
						num++;
					}
					this._encryptor.TransformBlock(this._counterNonce, 0, this._blockSize, this._encryptBuffer, 0);
					this._encrPos = 0;
				}
				int num3 = i + outputOffset;
				byte b2 = inputBuffer[i + inputOffset];
				byte[] encryptBuffer = this._encryptBuffer;
				int encrPos = this._encrPos;
				this._encrPos = encrPos + 1;
				outputBuffer[num3] = (b2 ^ encryptBuffer[encrPos]);
			}
			if (this._writeMode)
			{
				this._hmacsha1.AppendData(outputBuffer, outputOffset, inputCount);
			}
			return inputCount;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x00027C46 File Offset: 0x00025E46
		public byte[] PwdVerifier
		{
			get
			{
				return this._pwdVerifier;
			}
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x00027C4E File Offset: 0x00025E4E
		public byte[] GetAuthCode()
		{
			if (this._authCode == null)
			{
				this._authCode = this._hmacsha1.GetHashAndReset();
			}
			return this._authCode;
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x00027C6F File Offset: 0x00025E6F
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputCount > 0)
			{
				throw new NotImplementedException("TransformFinalBlock is not implemented and inputCount is greater than 0");
			}
			return new byte[0];
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06003696 RID: 13974 RVA: 0x00027C86 File Offset: 0x00025E86
		public int InputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x00027C86 File Offset: 0x00025E86
		public int OutputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x0000A093 File Offset: 0x00008293
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x0000A093 File Offset: 0x00008293
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x00027C8E File Offset: 0x00025E8E
		public void Dispose()
		{
			this._encryptor.Dispose();
		}

		// Token: 0x04003101 RID: 12545
		private const int PWD_VER_LENGTH = 2;

		// Token: 0x04003102 RID: 12546
		private const int KEY_ROUNDS = 1000;

		// Token: 0x04003103 RID: 12547
		private const int ENCRYPT_BLOCK = 16;

		// Token: 0x04003104 RID: 12548
		private int _blockSize;

		// Token: 0x04003105 RID: 12549
		private readonly ICryptoTransform _encryptor;

		// Token: 0x04003106 RID: 12550
		private readonly byte[] _counterNonce;

		// Token: 0x04003107 RID: 12551
		private byte[] _encryptBuffer;

		// Token: 0x04003108 RID: 12552
		private int _encrPos;

		// Token: 0x04003109 RID: 12553
		private byte[] _pwdVerifier;

		// Token: 0x0400310A RID: 12554
		private IncrementalHash _hmacsha1;

		// Token: 0x0400310B RID: 12555
		private byte[] _authCode;

		// Token: 0x0400310C RID: 12556
		private bool _writeMode;
	}
}
