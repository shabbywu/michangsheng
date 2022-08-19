using System;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x0200056D RID: 1389
	public sealed class GZipConstants
	{
		// Token: 0x06002DDF RID: 11743 RVA: 0x000027FC File Offset: 0x000009FC
		private GZipConstants()
		{
		}

		// Token: 0x040028AA RID: 10410
		public const int GZIP_MAGIC = 8075;

		// Token: 0x040028AB RID: 10411
		public const int FTEXT = 1;

		// Token: 0x040028AC RID: 10412
		public const int FHCRC = 2;

		// Token: 0x040028AD RID: 10413
		public const int FEXTRA = 4;

		// Token: 0x040028AE RID: 10414
		public const int FNAME = 8;

		// Token: 0x040028AF RID: 10415
		public const int FCOMMENT = 16;
	}
}
