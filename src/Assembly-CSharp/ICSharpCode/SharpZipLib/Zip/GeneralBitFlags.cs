using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007C2 RID: 1986
	[Flags]
	public enum GeneralBitFlags
	{
		// Token: 0x04002E7C RID: 11900
		Encrypted = 1,
		// Token: 0x04002E7D RID: 11901
		Method = 6,
		// Token: 0x04002E7E RID: 11902
		Descriptor = 8,
		// Token: 0x04002E7F RID: 11903
		ReservedPKware4 = 16,
		// Token: 0x04002E80 RID: 11904
		Patched = 32,
		// Token: 0x04002E81 RID: 11905
		StrongEncryption = 64,
		// Token: 0x04002E82 RID: 11906
		Unused7 = 128,
		// Token: 0x04002E83 RID: 11907
		Unused8 = 256,
		// Token: 0x04002E84 RID: 11908
		Unused9 = 512,
		// Token: 0x04002E85 RID: 11909
		Unused10 = 1024,
		// Token: 0x04002E86 RID: 11910
		UnicodeText = 2048,
		// Token: 0x04002E87 RID: 11911
		EnhancedCompress = 4096,
		// Token: 0x04002E88 RID: 11912
		HeaderMasked = 8192,
		// Token: 0x04002E89 RID: 11913
		ReservedPkware14 = 16384,
		// Token: 0x04002E8A RID: 11914
		ReservedPkware15 = 32768
	}
}
