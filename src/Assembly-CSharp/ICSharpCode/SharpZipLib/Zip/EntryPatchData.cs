using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007EB RID: 2027
	internal class EntryPatchData
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x00025E5A File Offset: 0x0002405A
		// (set) Token: 0x060033FB RID: 13307 RVA: 0x00025E62 File Offset: 0x00024062
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

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x00025E6B File Offset: 0x0002406B
		// (set) Token: 0x060033FD RID: 13309 RVA: 0x00025E73 File Offset: 0x00024073
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

		// Token: 0x04002F57 RID: 12119
		private long sizePatchOffset_;

		// Token: 0x04002F58 RID: 12120
		private long crcPatchOffset_;
	}
}
