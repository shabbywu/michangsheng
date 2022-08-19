using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000573 RID: 1395
	internal class PkzipClassicEncryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x06002DF9 RID: 11769 RVA: 0x00150A73 File Offset: 0x0014EC73
		internal PkzipClassicEncryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x00150A84 File Offset: 0x0014EC84
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x00150AA8 File Offset: 0x0014ECA8
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

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x00024C5F File Offset: 0x00022E5F
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x00024C5F File Offset: 0x00022E5F
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x00150AE4 File Offset: 0x0014ECE4
		public void Dispose()
		{
			base.Reset();
		}
	}
}
