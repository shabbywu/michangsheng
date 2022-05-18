using System;

namespace SuperScrollView
{
	// Token: 0x020009FA RID: 2554
	public class LoopListViewInitParam
	{
		// Token: 0x06004177 RID: 16759 RVA: 0x0002EE87 File Offset: 0x0002D087
		public static LoopListViewInitParam CopyDefaultInitParam()
		{
			return new LoopListViewInitParam();
		}

		// Token: 0x04003A30 RID: 14896
		public float mDistanceForRecycle0 = 300f;

		// Token: 0x04003A31 RID: 14897
		public float mDistanceForNew0 = 200f;

		// Token: 0x04003A32 RID: 14898
		public float mDistanceForRecycle1 = 300f;

		// Token: 0x04003A33 RID: 14899
		public float mDistanceForNew1 = 200f;

		// Token: 0x04003A34 RID: 14900
		public float mSmoothDumpRate = 0.3f;

		// Token: 0x04003A35 RID: 14901
		public float mSnapFinishThreshold = 0.01f;

		// Token: 0x04003A36 RID: 14902
		public float mSnapVecThreshold = 145f;

		// Token: 0x04003A37 RID: 14903
		public float mItemDefaultWithPaddingSize = 20f;
	}
}
