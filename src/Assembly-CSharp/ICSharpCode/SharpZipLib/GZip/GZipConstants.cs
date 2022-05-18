using System;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000815 RID: 2069
	public sealed class GZipConstants
	{
		// Token: 0x06003655 RID: 13909 RVA: 0x0000403D File Offset: 0x0000223D
		private GZipConstants()
		{
		}

		// Token: 0x040030E4 RID: 12516
		public const int GZIP_MAGIC = 8075;

		// Token: 0x040030E5 RID: 12517
		public const int FTEXT = 1;

		// Token: 0x040030E6 RID: 12518
		public const int FHCRC = 2;

		// Token: 0x040030E7 RID: 12519
		public const int FEXTRA = 4;

		// Token: 0x040030E8 RID: 12520
		public const int FNAME = 8;

		// Token: 0x040030E9 RID: 12521
		public const int FCOMMENT = 16;
	}
}
