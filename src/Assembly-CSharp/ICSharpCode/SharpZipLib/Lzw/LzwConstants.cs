using System;

namespace ICSharpCode.SharpZipLib.Lzw
{
	// Token: 0x02000811 RID: 2065
	public sealed class LzwConstants
	{
		// Token: 0x0600363A RID: 13882 RVA: 0x0000403D File Offset: 0x0000223D
		private LzwConstants()
		{
		}

		// Token: 0x040030C1 RID: 12481
		public const int MAGIC = 8093;

		// Token: 0x040030C2 RID: 12482
		public const int MAX_BITS = 16;

		// Token: 0x040030C3 RID: 12483
		public const int BIT_MASK = 31;

		// Token: 0x040030C4 RID: 12484
		public const int EXTENDED_MASK = 32;

		// Token: 0x040030C5 RID: 12485
		public const int RESERVED_MASK = 96;

		// Token: 0x040030C6 RID: 12486
		public const int BLOCK_MODE_MASK = 128;

		// Token: 0x040030C7 RID: 12487
		public const int HDR_SIZE = 3;

		// Token: 0x040030C8 RID: 12488
		public const int INIT_BITS = 9;
	}
}
