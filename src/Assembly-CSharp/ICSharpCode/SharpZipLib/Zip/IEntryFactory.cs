using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000527 RID: 1319
	public interface IEntryFactory
	{
		// Token: 0x06002A47 RID: 10823
		ZipEntry MakeFileEntry(string fileName);

		// Token: 0x06002A48 RID: 10824
		ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

		// Token: 0x06002A49 RID: 10825
		ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem);

		// Token: 0x06002A4A RID: 10826
		ZipEntry MakeDirectoryEntry(string directoryName);

		// Token: 0x06002A4B RID: 10827
		ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06002A4C RID: 10828
		// (set) Token: 0x06002A4D RID: 10829
		INameTransform NameTransform { get; set; }
	}
}
