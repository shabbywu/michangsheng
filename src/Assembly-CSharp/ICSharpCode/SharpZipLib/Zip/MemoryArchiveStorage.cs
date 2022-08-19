using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000547 RID: 1351
	public class MemoryArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x06002B94 RID: 11156 RVA: 0x00146181 File Offset: 0x00144381
		public MemoryArchiveStorage() : base(FileUpdateMode.Direct)
		{
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x0014618A File Offset: 0x0014438A
		public MemoryArchiveStorage(FileUpdateMode updateMode) : base(updateMode)
		{
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x00146193 File Offset: 0x00144393
		public MemoryStream FinalStream
		{
			get
			{
				return this.finalStream_;
			}
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x0014619B File Offset: 0x0014439B
		public override Stream GetTemporaryOutput()
		{
			this.temporaryStream_ = new MemoryStream();
			return this.temporaryStream_;
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x001461AE File Offset: 0x001443AE
		public override Stream ConvertTemporaryToFinal()
		{
			if (this.temporaryStream_ == null)
			{
				throw new ZipException("No temporary stream has been created");
			}
			this.finalStream_ = new MemoryStream(this.temporaryStream_.ToArray());
			return this.finalStream_;
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x001461DF File Offset: 0x001443DF
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			this.temporaryStream_ = new MemoryStream();
			stream.Position = 0L;
			StreamUtils.Copy(stream, this.temporaryStream_, new byte[4096]);
			return this.temporaryStream_;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x00146210 File Offset: 0x00144410
		public override Stream OpenForDirectUpdate(Stream stream)
		{
			Stream stream2;
			if (stream == null || !stream.CanWrite)
			{
				stream2 = new MemoryStream();
				if (stream != null)
				{
					stream.Position = 0L;
					StreamUtils.Copy(stream, stream2, new byte[4096]);
					stream.Dispose();
				}
			}
			else
			{
				stream2 = stream;
			}
			return stream2;
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x00146255 File Offset: 0x00144455
		public override void Dispose()
		{
			if (this.temporaryStream_ != null)
			{
				this.temporaryStream_.Dispose();
			}
		}

		// Token: 0x04002730 RID: 10032
		private MemoryStream temporaryStream_;

		// Token: 0x04002731 RID: 10033
		private MemoryStream finalStream_;
	}
}
