using System;

namespace UltimateSurvival
{
	// Token: 0x020005C9 RID: 1481
	public class AIEventHandler : EntityEventHandler
	{
		// Token: 0x04002A28 RID: 10792
		public Value<bool> IsHungry = new Value<bool>(false);

		// Token: 0x04002A29 RID: 10793
		public Value<float> LastFedTime = new Value<float>(0f);

		// Token: 0x04002A2A RID: 10794
		public Activity Patrol = new Activity();

		// Token: 0x04002A2B RID: 10795
		public Activity Chase = new Activity();

		// Token: 0x04002A2C RID: 10796
		public Activity Attack = new Activity();

		// Token: 0x04002A2D RID: 10797
		public Activity RunAway = new Activity();
	}
}
