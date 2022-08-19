using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000544 RID: 1348
	public interface IArchiveStorage
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06002B80 RID: 11136
		FileUpdateMode UpdateMode { get; }

		// Token: 0x06002B81 RID: 11137
		Stream GetTemporaryOutput();

		// Token: 0x06002B82 RID: 11138
		Stream ConvertTemporaryToFinal();

		// Token: 0x06002B83 RID: 11139
		Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x06002B84 RID: 11140
		Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x06002B85 RID: 11141
		void Dispose();
	}
}
