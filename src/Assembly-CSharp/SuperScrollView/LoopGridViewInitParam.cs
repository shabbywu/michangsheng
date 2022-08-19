using System;

namespace SuperScrollView
{
	// Token: 0x020006C7 RID: 1735
	public class LoopGridViewInitParam
	{
		// Token: 0x060036D3 RID: 14035 RVA: 0x00176B8E File Offset: 0x00174D8E
		public static LoopGridViewInitParam CopyDefaultInitParam()
		{
			return new LoopGridViewInitParam();
		}

		// Token: 0x04002FCD RID: 12237
		public float mSmoothDumpRate = 0.3f;

		// Token: 0x04002FCE RID: 12238
		public float mSnapFinishThreshold = 0.01f;

		// Token: 0x04002FCF RID: 12239
		public float mSnapVecThreshold = 145f;
	}
}
