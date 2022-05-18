using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007E7 RID: 2023
	public abstract class BaseArchiveStorage : IArchiveStorage
	{
		// Token: 0x060033DD RID: 13277 RVA: 0x00025CF5 File Offset: 0x00023EF5
		protected BaseArchiveStorage(FileUpdateMode updateMode)
		{
			this.updateMode_ = updateMode;
		}

		// Token: 0x060033DE RID: 13278
		public abstract Stream GetTemporaryOutput();

		// Token: 0x060033DF RID: 13279
		public abstract Stream ConvertTemporaryToFinal();

		// Token: 0x060033E0 RID: 13280
		public abstract Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x060033E1 RID: 13281
		public abstract Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x060033E2 RID: 13282
		public abstract void Dispose();

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x00025D04 File Offset: 0x00023F04
		public FileUpdateMode UpdateMode
		{
			get
			{
				return this.updateMode_;
			}
		}

		// Token: 0x04002F4E RID: 12110
		private readonly FileUpdateMode updateMode_;
	}
}
