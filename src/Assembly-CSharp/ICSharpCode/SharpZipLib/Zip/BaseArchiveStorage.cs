using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000545 RID: 1349
	public abstract class BaseArchiveStorage : IArchiveStorage
	{
		// Token: 0x06002B86 RID: 11142 RVA: 0x00145FCD File Offset: 0x001441CD
		protected BaseArchiveStorage(FileUpdateMode updateMode)
		{
			this.updateMode_ = updateMode;
		}

		// Token: 0x06002B87 RID: 11143
		public abstract Stream GetTemporaryOutput();

		// Token: 0x06002B88 RID: 11144
		public abstract Stream ConvertTemporaryToFinal();

		// Token: 0x06002B89 RID: 11145
		public abstract Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x06002B8A RID: 11146
		public abstract Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x06002B8B RID: 11147
		public abstract void Dispose();

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x00145FDC File Offset: 0x001441DC
		public FileUpdateMode UpdateMode
		{
			get
			{
				return this.updateMode_;
			}
		}

		// Token: 0x0400272C RID: 10028
		private readonly FileUpdateMode updateMode_;
	}
}
