using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007E9 RID: 2025
	public class MemoryArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x060033EB RID: 13291 RVA: 0x00025D80 File Offset: 0x00023F80
		public MemoryArchiveStorage() : base(FileUpdateMode.Direct)
		{
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x00025D89 File Offset: 0x00023F89
		public MemoryArchiveStorage(FileUpdateMode updateMode) : base(updateMode)
		{
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x00025D92 File Offset: 0x00023F92
		public MemoryStream FinalStream
		{
			get
			{
				return this.finalStream_;
			}
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x00025D9A File Offset: 0x00023F9A
		public override Stream GetTemporaryOutput()
		{
			this.temporaryStream_ = new MemoryStream();
			return this.temporaryStream_;
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x00025DAD File Offset: 0x00023FAD
		public override Stream ConvertTemporaryToFinal()
		{
			if (this.temporaryStream_ == null)
			{
				throw new ZipException("No temporary stream has been created");
			}
			this.finalStream_ = new MemoryStream(this.temporaryStream_.ToArray());
			return this.finalStream_;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x00025DDE File Offset: 0x00023FDE
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			this.temporaryStream_ = new MemoryStream();
			stream.Position = 0L;
			StreamUtils.Copy(stream, this.temporaryStream_, new byte[4096]);
			return this.temporaryStream_;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x0019243C File Offset: 0x0019063C
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

		// Token: 0x060033F2 RID: 13298 RVA: 0x00025E0F File Offset: 0x0002400F
		public override void Dispose()
		{
			if (this.temporaryStream_ != null)
			{
				this.temporaryStream_.Dispose();
			}
		}

		// Token: 0x04002F52 RID: 12114
		private MemoryStream temporaryStream_;

		// Token: 0x04002F53 RID: 12115
		private MemoryStream finalStream_;
	}
}
