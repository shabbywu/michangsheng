using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200053C RID: 1340
	public class TestStatus
	{
		// Token: 0x06002B04 RID: 11012 RVA: 0x00142E04 File Offset: 0x00141004
		public TestStatus(ZipFile file)
		{
			this.file_ = file;
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x00142E13 File Offset: 0x00141013
		public TestOperation Operation
		{
			get
			{
				return this.operation_;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x00142E1B File Offset: 0x0014101B
		public ZipFile File
		{
			get
			{
				return this.file_;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x00142E23 File Offset: 0x00141023
		public ZipEntry Entry
		{
			get
			{
				return this.entry_;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x00142E2B File Offset: 0x0014102B
		public int ErrorCount
		{
			get
			{
				return this.errorCount_;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x00142E33 File Offset: 0x00141033
		public long BytesTested
		{
			get
			{
				return this.bytesTested_;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x00142E3B File Offset: 0x0014103B
		public bool EntryValid
		{
			get
			{
				return this.entryValid_;
			}
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x00142E43 File Offset: 0x00141043
		internal void AddError()
		{
			this.errorCount_++;
			this.entryValid_ = false;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x00142E5A File Offset: 0x0014105A
		internal void SetOperation(TestOperation operation)
		{
			this.operation_ = operation;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x00142E63 File Offset: 0x00141063
		internal void SetEntry(ZipEntry entry)
		{
			this.entry_ = entry;
			this.entryValid_ = true;
			this.bytesTested_ = 0L;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x00142E7B File Offset: 0x0014107B
		internal void SetBytesTested(long value)
		{
			this.bytesTested_ = value;
		}

		// Token: 0x0400270A RID: 9994
		private readonly ZipFile file_;

		// Token: 0x0400270B RID: 9995
		private ZipEntry entry_;

		// Token: 0x0400270C RID: 9996
		private bool entryValid_;

		// Token: 0x0400270D RID: 9997
		private int errorCount_;

		// Token: 0x0400270E RID: 9998
		private long bytesTested_;

		// Token: 0x0400270F RID: 9999
		private TestOperation operation_;
	}
}
