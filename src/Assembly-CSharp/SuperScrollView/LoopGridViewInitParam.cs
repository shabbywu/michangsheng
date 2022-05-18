using System;

namespace SuperScrollView
{
	// Token: 0x020009F2 RID: 2546
	public class LoopGridViewInitParam
	{
		// Token: 0x060040F0 RID: 16624 RVA: 0x0002EA82 File Offset: 0x0002CC82
		public static LoopGridViewInitParam CopyDefaultInitParam()
		{
			return new LoopGridViewInitParam();
		}

		// Token: 0x040039D2 RID: 14802
		public float mSmoothDumpRate = 0.3f;

		// Token: 0x040039D3 RID: 14803
		public float mSnapFinishThreshold = 0.01f;

		// Token: 0x040039D4 RID: 14804
		public float mSnapVecThreshold = 145f;
	}
}
