using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x02000574 RID: 1396
	internal class PkzipClassicDecryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x06002E01 RID: 11777 RVA: 0x00150A73 File Offset: 0x0014EC73
		internal PkzipClassicDecryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x00150AEC File Offset: 0x0014ECEC
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x00150B10 File Offset: 0x0014ED10
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

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00024C5F File Offset: 0x00022E5F
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x00024C5F File Offset: 0x00022E5F
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x00150AE4 File Offset: 0x0014ECE4
		public void Dispose()
		{
			base.Reset();
		}
	}
}
