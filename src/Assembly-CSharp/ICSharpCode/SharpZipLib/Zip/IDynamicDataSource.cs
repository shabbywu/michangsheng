using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007E3 RID: 2019
	public interface IDynamicDataSource
	{
		// Token: 0x060033D2 RID: 13266
		Stream GetSource(ZipEntry entry, string name);
	}
}
