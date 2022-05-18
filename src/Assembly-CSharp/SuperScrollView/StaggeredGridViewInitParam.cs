using System;

namespace SuperScrollView
{
	// Token: 0x020009FF RID: 2559
	public class StaggeredGridViewInitParam
	{
		// Token: 0x060041EF RID: 16879 RVA: 0x0002F25F File Offset: 0x0002D45F
		public static StaggeredGridViewInitParam CopyDefaultInitParam()
		{
			return new StaggeredGridViewInitParam();
		}

		// Token: 0x04003A87 RID: 14983
		public float mDistanceForRecycle0 = 300f;

		// Token: 0x04003A88 RID: 14984
		public float mDistanceForNew0 = 200f;

		// Token: 0x04003A89 RID: 14985
		public float mDistanceForRecycle1 = 300f;

		// Token: 0x04003A8A RID: 14986
		public float mDistanceForNew1 = 200f;

		// Token: 0x04003A8B RID: 14987
		public float mItemDefaultWithPaddingSize = 20f;
	}
}
