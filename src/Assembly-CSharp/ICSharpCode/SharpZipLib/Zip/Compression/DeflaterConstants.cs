using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x020007F5 RID: 2037
	public static class DeflaterConstants
	{
		// Token: 0x04002F91 RID: 12177
		public const bool DEBUGGING = false;

		// Token: 0x04002F92 RID: 12178
		public const int STORED_BLOCK = 0;

		// Token: 0x04002F93 RID: 12179
		public const int STATIC_TREES = 1;

		// Token: 0x04002F94 RID: 12180
		public const int DYN_TREES = 2;

		// Token: 0x04002F95 RID: 12181
		public const int PRESET_DICT = 32;

		// Token: 0x04002F96 RID: 12182
		public const int DEFAULT_MEM_LEVEL = 8;

		// Token: 0x04002F97 RID: 12183
		public const int MAX_MATCH = 258;

		// Token: 0x04002F98 RID: 12184
		public const int MIN_MATCH = 3;

		// Token: 0x04002F99 RID: 12185
		public const int MAX_WBITS = 15;

		// Token: 0x04002F9A RID: 12186
		public const int WSIZE = 32768;

		// Token: 0x04002F9B RID: 12187
		public const int WMASK = 32767;

		// Token: 0x04002F9C RID: 12188
		public const int HASH_BITS = 15;

		// Token: 0x04002F9D RID: 12189
		public const int HASH_SIZE = 32768;

		// Token: 0x04002F9E RID: 12190
		public const int HASH_MASK = 32767;

		// Token: 0x04002F9F RID: 12191
		public const int HASH_SHIFT = 5;

		// Token: 0x04002FA0 RID: 12192
		public const int MIN_LOOKAHEAD = 262;

		// Token: 0x04002FA1 RID: 12193
		public const int MAX_DIST = 32506;

		// Token: 0x04002FA2 RID: 12194
		public const int PENDING_BUF_SIZE = 65536;

		// Token: 0x04002FA3 RID: 12195
		public static int MAX_BLOCK_SIZE = Math.Min(65535, 65531);

		// Token: 0x04002FA4 RID: 12196
		public const int DEFLATE_STORED = 0;

		// Token: 0x04002FA5 RID: 12197
		public const int DEFLATE_FAST = 1;

		// Token: 0x04002FA6 RID: 12198
		public const int DEFLATE_SLOW = 2;

		// Token: 0x04002FA7 RID: 12199
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

		// Token: 0x04002FA8 RID: 12200
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

		// Token: 0x04002FA9 RID: 12201
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

		// Token: 0x04002FAA RID: 12202
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

		// Token: 0x04002FAB RID: 12203
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
