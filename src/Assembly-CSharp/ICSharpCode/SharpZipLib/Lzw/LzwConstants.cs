using System;

namespace ICSharpCode.SharpZipLib.Lzw
{
	// Token: 0x02000569 RID: 1385
	public sealed class LzwConstants
	{
		// Token: 0x06002DC4 RID: 11716 RVA: 0x000027FC File Offset: 0x000009FC
		private LzwConstants()
		{
		}

		// Token: 0x04002887 RID: 10375
		public const int MAGIC = 8093;

		// Token: 0x04002888 RID: 10376
		public const int MAX_BITS = 16;

		// Token: 0x04002889 RID: 10377
		public const int BIT_MASK = 31;

		// Token: 0x0400288A RID: 10378
		public const int EXTENDED_MASK = 32;

		// Token: 0x0400288B RID: 10379
		public const int RESERVED_MASK = 96;

		// Token: 0x0400288C RID: 10380
		public const int BLOCK_MODE_MASK = 128;

		// Token: 0x0400288D RID: 10381
		public const int HDR_SIZE = 3;

		// Token: 0x0400288E RID: 10382
		public const int INIT_BITS = 9;
	}
}
