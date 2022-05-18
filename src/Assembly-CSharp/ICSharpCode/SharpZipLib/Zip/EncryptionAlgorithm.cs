using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007C1 RID: 1985
	public enum EncryptionAlgorithm
	{
		// Token: 0x04002E6D RID: 11885
		None,
		// Token: 0x04002E6E RID: 11886
		PkzipClassic,
		// Token: 0x04002E6F RID: 11887
		Des = 26113,
		// Token: 0x04002E70 RID: 11888
		RC2,
		// Token: 0x04002E71 RID: 11889
		TripleDes168,
		// Token: 0x04002E72 RID: 11890
		TripleDes112 = 26121,
		// Token: 0x04002E73 RID: 11891
		Aes128 = 26126,
		// Token: 0x04002E74 RID: 11892
		Aes192,
		// Token: 0x04002E75 RID: 11893
		Aes256,
		// Token: 0x04002E76 RID: 11894
		RC2Corrected = 26370,
		// Token: 0x04002E77 RID: 11895
		Blowfish = 26400,
		// Token: 0x04002E78 RID: 11896
		Twofish,
		// Token: 0x04002E79 RID: 11897
		RC4 = 26625,
		// Token: 0x04002E7A RID: 11898
		Unknown = 65535
	}
}
