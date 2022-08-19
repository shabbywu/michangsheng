using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000543 RID: 1347
	public class DynamicDiskDataSource : IDynamicDataSource
	{
		// Token: 0x06002B7E RID: 11134 RVA: 0x00145FB0 File Offset: 0x001441B0
		public Stream GetSource(ZipEntry entry, string name)
		{
			Stream result = null;
			if (name != null)
			{
				result = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			return result;
		}
	}
}
