using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200052B RID: 1323
	public enum EncryptionAlgorithm
	{
		// Token: 0x04002679 RID: 9849
		None,
		// Token: 0x0400267A RID: 9850
		PkzipClassic,
		// Token: 0x0400267B RID: 9851
		Des = 26113,
		// Token: 0x0400267C RID: 9852
		RC2,
		// Token: 0x0400267D RID: 9853
		TripleDes168,
		// Token: 0x0400267E RID: 9854
		TripleDes112 = 26121,
		// Token: 0x0400267F RID: 9855
		Aes128 = 26126,
		// Token: 0x04002680 RID: 9856
		Aes192,
		// Token: 0x04002681 RID: 9857
		Aes256,
		// Token: 0x04002682 RID: 9858
		RC2Corrected = 26370,
		// Token: 0x04002683 RID: 9859
		Blowfish = 26400,
		// Token: 0x04002684 RID: 9860
		Twofish,
		// Token: 0x04002685 RID: 9861
		RC4 = 26625,
		// Token: 0x04002686 RID: 9862
		Unknown = 65535
	}
}
