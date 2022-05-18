using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200081D RID: 2077
	internal class PkzipClassicDecryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x06003677 RID: 13943 RVA: 0x00027B1F File Offset: 0x00025D1F
		internal PkzipClassicDecryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x0019BC30 File Offset: 0x00199E30
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x0019BC54 File Offset: 0x00199E54
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = inputOffset; i < inputOffset + inputCount; i++)
			{
				byte b = inputBuffer[i] ^ base.TransformByte();
				outputBuffer[outputOffset++] = b;
				base.UpdateKeys(b);
			}
			return inputCount;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x0000A093 File Offset: 0x00008293
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x0000A093 File Offset: 0x00008293
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x0000A093 File Offset: 0x00008293
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x0000A093 File Offset: 0x00008293
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x00027B2E File Offset: 0x00025D2E
		public void Dispose()
		{
			base.Reset();
		}
	}
}
