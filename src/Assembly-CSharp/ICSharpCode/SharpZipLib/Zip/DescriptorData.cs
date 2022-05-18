using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007EA RID: 2026
	public class DescriptorData
	{
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x00025E24 File Offset: 0x00024024
		// (set) Token: 0x060033F4 RID: 13300 RVA: 0x00025E2C File Offset: 0x0002402C
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

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x00025E35 File Offset: 0x00024035
		// (set) Token: 0x060033F6 RID: 13302 RVA: 0x00025E3D File Offset: 0x0002403D
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

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x00025E46 File Offset: 0x00024046
		// (set) Token: 0x060033F8 RID: 13304 RVA: 0x00025E4E File Offset: 0x0002404E
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

		// Token: 0x04002F54 RID: 12116
		private long size;

		// Token: 0x04002F55 RID: 12117
		private long compressedSize;

		// Token: 0x04002F56 RID: 12118
		private long crc;
	}
}
