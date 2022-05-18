using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200081C RID: 2076
	internal class PkzipClassicEncryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x0600366F RID: 13935 RVA: 0x00027B1F File Offset: 0x00025D1F
		internal PkzipClassicEncryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x0019BBD0 File Offset: 0x00199DD0
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x0019BBF4 File Offset: 0x00199DF4
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = inputOffset; i < inputOffset + inputCount; i++)
			{
				byte ch = inputBuffer[i];
				outputBuffer[outputOffset++] = (inputBuffer[i] ^ base.TransformByte());
				base.UpdateKeys(ch);
			}
			return inputCount;
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06003672 RID: 13938 RVA: 0x0000A093 File Offset: 0x00008293
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x0000A093 File Offset: 0x00008293
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x0000A093 File Offset: 0x00008293
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x0000A093 File Offset: 0x00008293
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x00027B2E File Offset: 0x00025D2E
		public void Dispose()
		{
			base.Reset();
		}
	}
}
