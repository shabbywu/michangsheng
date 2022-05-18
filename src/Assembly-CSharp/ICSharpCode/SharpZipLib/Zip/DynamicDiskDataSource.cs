using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007E5 RID: 2021
	public class DynamicDiskDataSource : IDynamicDataSource
	{
		// Token: 0x060033D5 RID: 13269 RVA: 0x001922F4 File Offset: 0x001904F4
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
