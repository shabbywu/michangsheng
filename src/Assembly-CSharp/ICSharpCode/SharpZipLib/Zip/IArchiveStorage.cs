using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007E6 RID: 2022
	public interface IArchiveStorage
	{
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060033D7 RID: 13271
		FileUpdateMode UpdateMode { get; }

		// Token: 0x060033D8 RID: 13272
		Stream GetTemporaryOutput();

		// Token: 0x060033D9 RID: 13273
		Stream ConvertTemporaryToFinal();

		// Token: 0x060033DA RID: 13274
		Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x060033DB RID: 13275
		Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x060033DC RID: 13276
		void Dispose();
	}
}
