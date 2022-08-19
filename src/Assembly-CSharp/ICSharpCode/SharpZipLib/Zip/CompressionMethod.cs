using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200052A RID: 1322
	public enum CompressionMethod
	{
		// Token: 0x04002671 RID: 9841
		Stored,
		// Token: 0x04002672 RID: 9842
		Deflated = 8,
		// Token: 0x04002673 RID: 9843
		Deflate64,
		// Token: 0x04002674 RID: 9844
		BZip2 = 12,
		// Token: 0x04002675 RID: 9845
		LZMA = 14,
		// Token: 0x04002676 RID: 9846
		PPMd = 98,
		// Token: 0x04002677 RID: 9847
		WinZipAES
	}
}
