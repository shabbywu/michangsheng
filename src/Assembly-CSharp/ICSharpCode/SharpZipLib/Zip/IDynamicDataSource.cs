using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000541 RID: 1345
	public interface IDynamicDataSource
	{
		// Token: 0x06002B7B RID: 11131
		Stream GetSource(ZipEntry entry, string name);
	}
}
