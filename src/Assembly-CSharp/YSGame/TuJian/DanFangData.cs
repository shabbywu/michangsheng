using System;

namespace YSGame.TuJian
{
	// Token: 0x02000A9D RID: 2717
	public class DanFangData
	{
		// Token: 0x06004C1F RID: 19487 RVA: 0x002082A4 File Offset: 0x002064A4
		public void CalcYaoCaiTypeCount()
		{
			if (this.YaoYinCount > 0)
			{
				this.YaoCaiTypeCount++;
			}
			if (this.ZhuYao1Count > 0)
			{
				this.YaoCaiTypeCount++;
			}
			if (this.ZhuYao2Count > 0)
			{
				this.YaoCaiTypeCount++;
			}
			if (this.FuYao1Count > 0)
			{
				this.YaoCaiTypeCount++;
			}
			if (this.FuYao2Count > 0)
			{
				this.YaoCaiTypeCount++;
			}
		}

		// Token: 0x04004B5F RID: 19295
		public int ItemID;

		// Token: 0x04004B60 RID: 19296
		public int YaoYinID;

		// Token: 0x04004B61 RID: 19297
		public int ZhuYao1ID;

		// Token: 0x04004B62 RID: 19298
		public int ZhuYao2ID;

		// Token: 0x04004B63 RID: 19299
		public int FuYao1ID;

		// Token: 0x04004B64 RID: 19300
		public int FuYao2ID;

		// Token: 0x04004B65 RID: 19301
		public int YaoYinCount;

		// Token: 0x04004B66 RID: 19302
		public int ZhuYao1Count;

		// Token: 0x04004B67 RID: 19303
		public int ZhuYao2Count;

		// Token: 0x04004B68 RID: 19304
		public int FuYao1Count;

		// Token: 0x04004B69 RID: 19305
		public int FuYao2Count;

		// Token: 0x04004B6A RID: 19306
		public int CastTime;

		// Token: 0x04004B6B RID: 19307
		public int YaoCaiTypeCount;
	}
}
