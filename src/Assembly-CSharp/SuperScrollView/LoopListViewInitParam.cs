using System;

namespace SuperScrollView
{
	// Token: 0x020006CD RID: 1741
	public class LoopListViewInitParam
	{
		// Token: 0x06003757 RID: 14167 RVA: 0x00179A92 File Offset: 0x00177C92
		public static LoopListViewInitParam CopyDefaultInitParam()
		{
			return new LoopListViewInitParam();
		}

		// Token: 0x04003020 RID: 12320
		public float mDistanceForRecycle0 = 300f;

		// Token: 0x04003021 RID: 12321
		public float mDistanceForNew0 = 200f;

		// Token: 0x04003022 RID: 12322
		public float mDistanceForRecycle1 = 300f;

		// Token: 0x04003023 RID: 12323
		public float mDistanceForNew1 = 200f;

		// Token: 0x04003024 RID: 12324
		public float mSmoothDumpRate = 0.3f;

		// Token: 0x04003025 RID: 12325
		public float mSnapFinishThreshold = 0.01f;

		// Token: 0x04003026 RID: 12326
		public float mSnapVecThreshold = 145f;

		// Token: 0x04003027 RID: 12327
		public float mItemDefaultWithPaddingSize = 20f;
	}
}
