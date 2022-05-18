using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007D5 RID: 2005
	public class TestStatus
	{
		// Token: 0x0600331B RID: 13083 RVA: 0x000254D3 File Offset: 0x000236D3
		public TestStatus(ZipFile file)
		{
			this.file_ = file;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000254E2 File Offset: 0x000236E2
		public TestOperation Operation
		{
			get
			{
				return this.operation_;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000254EA File Offset: 0x000236EA
		public ZipFile File
		{
			get
			{
				return this.file_;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000254F2 File Offset: 0x000236F2
		public ZipEntry Entry
		{
			get
			{
				return this.entry_;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x000254FA File Offset: 0x000236FA
		public int ErrorCount
		{
			get
			{
				return this.errorCount_;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x00025502 File Offset: 0x00023702
		public long BytesTested
		{
			get
			{
				return this.bytesTested_;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x0002550A File Offset: 0x0002370A
		public bool EntryValid
		{
			get
			{
				return this.entryValid_;
			}
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x00025512 File Offset: 0x00023712
		internal void AddError()
		{
			this.errorCount_++;
			this.entryValid_ = false;
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x00025529 File Offset: 0x00023729
		internal void SetOperation(TestOperation operation)
		{
			this.operation_ = operation;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x00025532 File Offset: 0x00023732
		internal void SetEntry(ZipEntry entry)
		{
			this.entry_ = entry;
			this.entryValid_ = true;
			this.bytesTested_ = 0L;
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x0002554A File Offset: 0x0002374A
		internal void SetBytesTested(long value)
		{
			this.bytesTested_ = value;
		}

		// Token: 0x04002F11 RID: 12049
		private readonly ZipFile file_;

		// Token: 0x04002F12 RID: 12050
		private ZipEntry entry_;

		// Token: 0x04002F13 RID: 12051
		private bool entryValid_;

		// Token: 0x04002F14 RID: 12052
		private int errorCount_;

		// Token: 0x04002F15 RID: 12053
		private long bytesTested_;

		// Token: 0x04002F16 RID: 12054
		private TestOperation operation_;
	}
}
