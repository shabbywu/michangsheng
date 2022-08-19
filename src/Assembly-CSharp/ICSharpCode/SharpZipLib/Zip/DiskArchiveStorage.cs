using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000546 RID: 1350
	public class DiskArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x06002B8D RID: 11149 RVA: 0x00145FE4 File Offset: 0x001441E4
		public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode) : base(updateMode)
		{
			if (file.Name == null)
			{
				throw new ZipException("Cant handle non file archives");
			}
			this.fileName_ = file.Name;
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x0014600C File Offset: 0x0014420C
		public DiskArchiveStorage(ZipFile file) : this(file, FileUpdateMode.Safe)
		{
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x00146016 File Offset: 0x00144216
		public override Stream GetTemporaryOutput()
		{
			this.temporaryName_ = PathUtils.GetTempFileName(this.temporaryName_);
			this.temporaryStream_ = File.Open(this.temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			return this.temporaryStream_;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x00146044 File Offset: 0x00144244
		public override Stream ConvertTemporaryToFinal()
		{
			if (this.temporaryStream_ == null)
			{
				throw new ZipException("No temporary stream has been created");
			}
			Stream result = null;
			string tempFileName = PathUtils.GetTempFileName(this.fileName_);
			bool flag = false;
			try
			{
				this.temporaryStream_.Dispose();
				File.Move(this.fileName_, tempFileName);
				File.Move(this.temporaryName_, this.fileName_);
				flag = true;
				File.Delete(tempFileName);
				result = File.Open(this.fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (Exception)
			{
				result = null;
				if (!flag)
				{
					File.Move(tempFileName, this.fileName_);
					File.Delete(this.temporaryName_);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x001460E8 File Offset: 0x001442E8
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			stream.Dispose();
			this.temporaryName_ = PathUtils.GetTempFileName(this.fileName_);
			File.Copy(this.fileName_, this.temporaryName_, true);
			this.temporaryStream_ = new FileStream(this.temporaryName_, FileMode.Open, FileAccess.ReadWrite);
			return this.temporaryStream_;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00146138 File Offset: 0x00144338
		public override Stream OpenForDirectUpdate(Stream stream)
		{
			Stream result;
			if (stream == null || !stream.CanWrite)
			{
				if (stream != null)
				{
					stream.Dispose();
				}
				result = new FileStream(this.fileName_, FileMode.Open, FileAccess.ReadWrite);
			}
			else
			{
				result = stream;
			}
			return result;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x0014616C File Offset: 0x0014436C
		public override void Dispose()
		{
			if (this.temporaryStream_ != null)
			{
				this.temporaryStream_.Dispose();
			}
		}

		// Token: 0x0400272D RID: 10029
		private Stream temporaryStream_;

		// Token: 0x0400272E RID: 10030
		private readonly string fileName_;

		// Token: 0x0400272F RID: 10031
		private string temporaryName_;
	}
}
