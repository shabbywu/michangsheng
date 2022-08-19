using System;

namespace SuperScrollView
{
	// Token: 0x020006D1 RID: 1745
	public class StaggeredGridViewInitParam
	{
		// Token: 0x060037CD RID: 14285 RVA: 0x0017EC82 File Offset: 0x0017CE82
		public static StaggeredGridViewInitParam CopyDefaultInitParam()
		{
			return new StaggeredGridViewInitParam();
		}

		// Token: 0x0400306F RID: 12399
		public float mDistanceForRecycle0 = 300f;

		// Token: 0x04003070 RID: 12400
		public float mDistanceForNew0 = 200f;

		// Token: 0x04003071 RID: 12401
		public float mDistanceForRecycle1 = 300f;

		// Token: 0x04003072 RID: 12402
		public float mDistanceForNew1 = 200f;

		// Token: 0x04003073 RID: 12403
		public float mItemDefaultWithPaddingSize = 20f;
	}
}
