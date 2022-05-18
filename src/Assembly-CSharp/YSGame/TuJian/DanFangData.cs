using System;

namespace YSGame.TuJian
{
	// Token: 0x02000DD8 RID: 3544
	public class DanFangData
	{
		// Token: 0x06005563 RID: 21859 RVA: 0x00239790 File Offset: 0x00237990
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

		// Token: 0x04005538 RID: 21816
		public int ItemID;

		// Token: 0x04005539 RID: 21817
		public int YaoYinID;

		// Token: 0x0400553A RID: 21818
		public int ZhuYao1ID;

		// Token: 0x0400553B RID: 21819
		public int ZhuYao2ID;

		// Token: 0x0400553C RID: 21820
		public int FuYao1ID;

		// Token: 0x0400553D RID: 21821
		public int FuYao2ID;

		// Token: 0x0400553E RID: 21822
		public int YaoYinCount;

		// Token: 0x0400553F RID: 21823
		public int ZhuYao1Count;

		// Token: 0x04005540 RID: 21824
		public int ZhuYao2Count;

		// Token: 0x04005541 RID: 21825
		public int FuYao1Count;

		// Token: 0x04005542 RID: 21826
		public int FuYao2Count;

		// Token: 0x04005543 RID: 21827
		public int CastTime;

		// Token: 0x04005544 RID: 21828
		public int YaoCaiTypeCount;
	}
}
