using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200052C RID: 1324
	[Flags]
	public enum GeneralBitFlags
	{
		// Token: 0x04002688 RID: 9864
		Encrypted = 1,
		// Token: 0x04002689 RID: 9865
		Method = 6,
		// Token: 0x0400268A RID: 9866
		Descriptor = 8,
		// Token: 0x0400268B RID: 9867
		ReservedPKware4 = 16,
		// Token: 0x0400268C RID: 9868
		Patched = 32,
		// Token: 0x0400268D RID: 9869
		StrongEncryption = 64,
		// Token: 0x0400268E RID: 9870
		Unused7 = 128,
		// Token: 0x0400268F RID: 9871
		Unused8 = 256,
		// Token: 0x04002690 RID: 9872
		Unused9 = 512,
		// Token: 0x04002691 RID: 9873
		Unused10 = 1024,
		// Token: 0x04002692 RID: 9874
		UnicodeText = 2048,
		// Token: 0x04002693 RID: 9875
		EnhancedCompress = 4096,
		// Token: 0x04002694 RID: 9876
		HeaderMasked = 8192,
		// Token: 0x04002695 RID: 9877
		ReservedPkware14 = 16384,
		// Token: 0x04002696 RID: 9878
		ReservedPkware15 = 32768
	}
}
