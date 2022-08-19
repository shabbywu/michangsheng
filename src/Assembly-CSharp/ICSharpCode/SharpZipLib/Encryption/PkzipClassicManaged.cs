using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000575 RID: 1397
	public sealed class PkzipClassicManaged : PkzipClassic
	{
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x00150B4A File Offset: 0x0014ED4A
		// (set) Token: 0x06002E0A RID: 11786 RVA: 0x00150B4D File Offset: 0x0014ED4D
		public override int BlockSize
		{
			get
			{
				return 8;
			}
			set
			{
				if (value != 8)
				{
					throw new CryptographicException("Block size is invalid");
				}
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x00150B5E File Offset: 0x0014ED5E
		public override KeySizes[] LegalKeySizes
		{
			get
			{
				return new KeySizes[]
				{
					new KeySizes(96, 96, 0)
				};
			}
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x00004095 File Offset: 0x00002295
		public override void GenerateIV()
		{
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x00150B73 File Offset: 0x0014ED73
		public override KeySizes[] LegalBlockSizes
		{
			get
			{
				return new KeySizes[]
				{
					new KeySizes(8, 8, 0)
				};
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x00150B86 File Offset: 0x0014ED86
		// (set) Token: 0x06002E0F RID: 11791 RVA: 0x00150BA6 File Offset: 0x0014EDA6
		public override byte[] Key
		{
			get
			{
				if (this.key_ == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.key_.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 12)
				{
					throw new CryptographicException("Key size is illegal");
				}
				this.key_ = (byte[])value.Clone();
			}
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x00150BDC File Offset: 0x0014EDDC
		public override void GenerateKey()
		{
			this.key_ = new byte[12];
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				rngcryptoServiceProvider.GetBytes(this.key_);
			}
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x00150C24 File Offset: 0x0014EE24
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicEncryptCryptoTransform(this.Key);
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x00150C38 File Offset: 0x0014EE38
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicDecryptCryptoTransform(this.Key);
		}

		// Token: 0x040028B6 RID: 10422
		private byte[] key_;
	}
}
