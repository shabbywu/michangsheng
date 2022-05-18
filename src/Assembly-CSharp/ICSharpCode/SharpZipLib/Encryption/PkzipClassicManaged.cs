using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200081E RID: 2078
	public sealed class PkzipClassicManaged : PkzipClassic
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x00027B36 File Offset: 0x00025D36
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x00027B39 File Offset: 0x00025D39
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

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x00027B4A File Offset: 0x00025D4A
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

		// Token: 0x06003682 RID: 13954 RVA: 0x000042DD File Offset: 0x000024DD
		public override void GenerateIV()
		{
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x00027B5F File Offset: 0x00025D5F
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

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06003684 RID: 13956 RVA: 0x00027B72 File Offset: 0x00025D72
		// (set) Token: 0x06003685 RID: 13957 RVA: 0x00027B92 File Offset: 0x00025D92
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

		// Token: 0x06003686 RID: 13958 RVA: 0x0019BC90 File Offset: 0x00199E90
		public override void GenerateKey()
		{
			this.key_ = new byte[12];
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				rngcryptoServiceProvider.GetBytes(this.key_);
			}
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x00027BC5 File Offset: 0x00025DC5
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicEncryptCryptoTransform(this.Key);
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x00027BD9 File Offset: 0x00025DD9
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicDecryptCryptoTransform(this.Key);
		}

		// Token: 0x040030F5 RID: 12533
		private byte[] key_;
	}
}
