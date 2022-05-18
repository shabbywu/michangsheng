using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007BD RID: 1981
	public interface IEntryFactory
	{
		// Token: 0x0600325E RID: 12894
		ZipEntry MakeFileEntry(string fileName);

		// Token: 0x0600325F RID: 12895
		ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

		// Token: 0x06003260 RID: 12896
		ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem);

		// Token: 0x06003261 RID: 12897
		ZipEntry MakeDirectoryEntry(string directoryName);

		// Token: 0x06003262 RID: 12898
		ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06003263 RID: 12899
		// (set) Token: 0x06003264 RID: 12900
		INameTransform NameTransform { get; set; }
	}
}
