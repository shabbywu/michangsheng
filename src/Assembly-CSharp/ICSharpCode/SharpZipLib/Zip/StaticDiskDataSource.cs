using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007E4 RID: 2020
	public class StaticDiskDataSource : IStaticDataSource
	{
		// Token: 0x060033D3 RID: 13267 RVA: 0x00025CD6 File Offset: 0x00023ED6
		public StaticDiskDataSource(string fileName)
		{
			this.fileName_ = fileName;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x00025CE5 File Offset: 0x00023EE5
		public Stream GetSource()
		{
			return File.Open(this.fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x04002F4D RID: 12109
		private readonly string fileName_;
	}
}
