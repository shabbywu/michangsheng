using System;

namespace UltimateSurvival
{
	// Token: 0x0200088B RID: 2187
	public class AIEventHandler : EntityEventHandler
	{
		// Token: 0x040032BE RID: 12990
		public Value<bool> IsHungry = new Value<bool>(false);

		// Token: 0x040032BF RID: 12991
		public Value<float> LastFedTime = new Value<float>(0f);

		// Token: 0x040032C0 RID: 12992
		public Activity Patrol = new Activity();

		// Token: 0x040032C1 RID: 12993
		public Activity Chase = new Activity();

		// Token: 0x040032C2 RID: 12994
		public Activity Attack = new Activity();

		// Token: 0x040032C3 RID: 12995
		public Activity RunAway = new Activity();
	}
}
