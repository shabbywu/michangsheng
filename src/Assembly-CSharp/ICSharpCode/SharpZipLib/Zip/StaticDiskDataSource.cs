using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000542 RID: 1346
	public class StaticDiskDataSource : IStaticDataSource
	{
		// Token: 0x06002B7C RID: 11132 RVA: 0x00145F90 File Offset: 0x00144190
		public StaticDiskDataSource(string fileName)
		{
			this.fileName_ = fileName;
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x00145F9F File Offset: 0x0014419F
		public Stream GetSource()
		{
			return File.Open(this.fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x0400272B RID: 10027
		private readonly string fileName_;
	}
}
