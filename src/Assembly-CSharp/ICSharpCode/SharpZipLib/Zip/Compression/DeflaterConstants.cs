using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000551 RID: 1361
	public static class DeflaterConstants
	{
		// Token: 0x04002769 RID: 10089
		public const bool DEBUGGING = false;

		// Token: 0x0400276A RID: 10090
		public const int STORED_BLOCK = 0;

		// Token: 0x0400276B RID: 10091
		public const int STATIC_TREES = 1;

		// Token: 0x0400276C RID: 10092
		public const int DYN_TREES = 2;

		// Token: 0x0400276D RID: 10093
		public const int PRESET_DICT = 32;

		// Token: 0x0400276E RID: 10094
		public const int DEFAULT_MEM_LEVEL = 8;

		// Token: 0x0400276F RID: 10095
		public const int MAX_MATCH = 258;

		// Token: 0x04002770 RID: 10096
		public const int MIN_MATCH = 3;

		// Token: 0x04002771 RID: 10097
		public const int MAX_WBITS = 15;

		// Token: 0x04002772 RID: 10098
		public const int WSIZE = 32768;

		// Token: 0x04002773 RID: 10099
		public const int WMASK = 32767;

		// Token: 0x04002774 RID: 10100
		public const int HASH_BITS = 15;

		// Token: 0x04002775 RID: 10101
		public const int HASH_SIZE = 32768;

		// Token: 0x04002776 RID: 10102
		public const int HASH_MASK = 32767;

		// Token: 0x04002777 RID: 10103
		public const int HASH_SHIFT = 5;

		// Token: 0x04002778 RID: 10104
		public const int MIN_LOOKAHEAD = 262;

		// Token: 0x04002779 RID: 10105
		public const int MAX_DIST = 32506;

		// Token: 0x0400277A RID: 10106
		public const int PENDING_BUF_SIZE = 65536;

		// Token: 0x0400277B RID: 10107
		public static int MAX_BLOCK_SIZE = Math.Min(65535, 65531);

		// Token: 0x0400277C RID: 10108
		public const int DEFLATE_STORED = 0;

		// Token: 0x0400277D RID: 10109
		public const int DEFLATE_FAST = 1;

		// Token: 0x0400277E RID: 10110
		public const int DEFLATE_SLOW = 2;

		// Token: 0x0400277F RID: 10111
		public static int[] GOOD_LENGTH = new int[]
		{
			0,
			4,
			4,
			4,
			4,
			8,
			8,
			8,
			32,
			32
		};

		// Token: 0x04002780 RID: 10112
		public static int[] MAX_LAZY = new int[]
		{
			0,
			4,
			5,
			6,
			4,
			16,
			16,
			32,
			128,
			258
		};

		// Token: 0x04002781 RID: 10113
		public static int[] NICE_LENGTH = new int[]
		{
			0,
			8,
			16,
			32,
			16,
			32,
			128,
			128,
			258,
			258
		};

		// Token: 0x04002782 RID: 10114
		public static int[] MAX_CHAIN = new int[]
		{
			0,
			4,
			8,
			32,
			16,
			32,
			128,
			256,
			1024,
			4096
		};

		// Token: 0x04002783 RID: 10115
		public static int[] COMPR_FUNC = new int[]
		{
			0,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			2
		};
	}
}
