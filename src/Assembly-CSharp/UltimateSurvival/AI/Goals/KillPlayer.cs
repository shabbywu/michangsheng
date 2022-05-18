using System;

namespace UltimateSurvival.AI.Goals
{
	// Token: 0x02000981 RID: 2433
	public class KillPlayer : Goal
	{
		// Token: 0x06003E2B RID: 15915 RVA: 0x0002CC80 File Offset: 0x0002AE80
		public override void OnStart()
		{
			this.m_Priority = 10f;
			base.GoalState.Add("Is Player Dead", true);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x0002CC1E File Offset: 0x0002AE1E
		public override void RecalculatePriority(AIBrain brain)
		{
			if (brain.Settings.Detection.HasTarget())
			{
				this.m_Priority = this.m_PriorityRange.y;
				return;
			}
			this.m_Priority = this.m_PriorityRange.x;
		}
	}
}
