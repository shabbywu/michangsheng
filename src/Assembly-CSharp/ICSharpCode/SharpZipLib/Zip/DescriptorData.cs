using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000548 RID: 1352
	public class DescriptorData
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x0014626A File Offset: 0x0014446A
		// (set) Token: 0x06002B9D RID: 11165 RVA: 0x00146272 File Offset: 0x00144472
		public long CompressedSize
		{
			get
			{
				return this.compressedSize;
			}
			set
			{
				this.compressedSize = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x0014627B File Offset: 0x0014447B
		// (set) Token: 0x06002B9F RID: 11167 RVA: 0x00146283 File Offset: 0x00144483
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x0014628C File Offset: 0x0014448C
		// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x00146294 File Offset: 0x00144494
		public long Crc
		{
			get
			{
				return this.crc;
			}
			set
			{
				this.crc = (value & (long)((ulong)-1));
			}
		}

		// Token: 0x04002732 RID: 10034
		private long size;

		// Token: 0x04002733 RID: 10035
		private long compressedSize;

		// Token: 0x04002734 RID: 10036
		private long crc;
	}
}
