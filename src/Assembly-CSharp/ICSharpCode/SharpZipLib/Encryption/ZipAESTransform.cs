using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000577 RID: 1399
	internal class ZipAESTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x06002E1B RID: 11803 RVA: 0x00150F28 File Offset: 0x0014F128
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

		// Token: 0x06002E1C RID: 11804 RVA: 0x0015103C File Offset: 0x0014F23C
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

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x001510FA File Offset: 0x0014F2FA
		public byte[] PwdVerifier
		{
			get
			{
				return this._pwdVerifier;
			}
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x00151102 File Offset: 0x0014F302
		public byte[] GetAuthCode()
		{
			if (this._authCode == null)
			{
				this._authCode = this._hmacsha1.GetHashAndReset();
			}
			return this._authCode;
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x00151123 File Offset: 0x0014F323
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputCount > 0)
			{
				throw new NotImplementedException("TransformFinalBlock is not implemented and inputCount is greater than 0");
			}
			return new byte[0];
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x0015113A File Offset: 0x0014F33A
		public int InputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x0015113A File Offset: 0x0014F33A
		public int OutputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06002E23 RID: 11811 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x00151142 File Offset: 0x0014F342
		public void Dispose()
		{
			this._encryptor.Dispose();
		}

		// Token: 0x040028C2 RID: 10434
		private const int PWD_VER_LENGTH = 2;

		// Token: 0x040028C3 RID: 10435
		private const int KEY_ROUNDS = 1000;

		// Token: 0x040028C4 RID: 10436
		private const int ENCRYPT_BLOCK = 16;

		// Token: 0x040028C5 RID: 10437
		private int _blockSize;

		// Token: 0x040028C6 RID: 10438
		private readonly ICryptoTransform _encryptor;

		// Token: 0x040028C7 RID: 10439
		private readonly byte[] _counterNonce;

		// Token: 0x040028C8 RID: 10440
		private byte[] _encryptBuffer;

		// Token: 0x040028C9 RID: 10441
		private int _encrPos;

		// Token: 0x040028CA RID: 10442
		private byte[] _pwdVerifier;

		// Token: 0x040028CB RID: 10443
		private IncrementalHash _hmacsha1;

		// Token: 0x040028CC RID: 10444
		private byte[] _authCode;

		// Token: 0x040028CD RID: 10445
		private bool _writeMode;
	}
}
