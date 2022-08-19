using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000549 RID: 1353
	internal class EntryPatchData
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x001462A0 File Offset: 0x001444A0
		// (set) Token: 0x06002BA4 RID: 11172 RVA: 0x001462A8 File Offset: 0x001444A8
		public long SizePatchOffset
		{
			get
			{
				return this.sizePatchOffset_;
			}
			set
			{
				this.sizePatchOffset_ = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x001462B1 File Offset: 0x001444B1
		// (set) Token: 0x06002BA6 RID: 11174 RVA: 0x001462B9 File Offset: 0x001444B9
		public long CrcPatchOffset
		{
			get
			{
				return this.crcPatchOffset_;
			}
			set
			{
				this.crcPatchOffset_ = value;
			}
		}

		// Token: 0x04002735 RID: 10037
		private long sizePatchOffset_;

		// Token: 0x04002736 RID: 10038
		private long crcPatchOffset_;
	}
}
