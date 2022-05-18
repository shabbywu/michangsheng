using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007C0 RID: 1984
	public enum CompressionMethod
	{
		// Token: 0x04002E65 RID: 11877
		Stored,
		// Token: 0x04002E66 RID: 11878
		Deflated = 8,
		// Token: 0x04002E67 RID: 11879
		Deflate64,
		// Token: 0x04002E68 RID: 11880
		BZip2 = 12,
		// Token: 0x04002E69 RID: 11881
		LZMA = 14,
		// Token: 0x04002E6A RID: 11882
		PPMd = 98,
		// Token: 0x04002E6B RID: 11883
		WinZipAES
	}
}
