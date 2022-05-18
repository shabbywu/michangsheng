using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007E8 RID: 2024
	public class DiskArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x060033E4 RID: 13284 RVA: 0x00025D0C File Offset: 0x00023F0C
		public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode) : base(updateMode)
		{
			if (file.Name == null)
			{
				throw new ZipException("Cant handle non file archives");
			}
			this.fileName_ = file.Name;
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x00025D34 File Offset: 0x00023F34
		public DiskArchiveStorage(ZipFile file) : this(file, FileUpdateMode.Safe)
		{
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x00025D3E File Offset: 0x00023F3E
		public override Stream GetTemporaryOutput()
		{
			this.temporaryName_ = PathUtils.GetTempFileName(this.temporaryName_);
			this.temporaryStream_ = File.Open(this.temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			return this.temporaryStream_;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x00192314 File Offset: 0x00190514
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

		// Token: 0x060033E8 RID: 13288 RVA: 0x001923B8 File Offset: 0x001905B8
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			stream.Dispose();
			this.temporaryName_ = PathUtils.GetTempFileName(this.fileName_);
			File.Copy(this.fileName_, this.temporaryName_, true);
			this.temporaryStream_ = new FileStream(this.temporaryName_, FileMode.Open, FileAccess.ReadWrite);
			return this.temporaryStream_;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x00192408 File Offset: 0x00190608
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

		// Token: 0x060033EA RID: 13290 RVA: 0x00025D6B File Offset: 0x00023F6B
		public override void Dispose()
		{
			if (this.temporaryStream_ != null)
			{
				this.temporaryStream_.Dispose();
			}
		}

		// Token: 0x04002F4F RID: 12111
		private Stream temporaryStream_;

		// Token: 0x04002F50 RID: 12112
		private readonly string fileName_;

		// Token: 0x04002F51 RID: 12113
		private string temporaryName_;
	}
}
