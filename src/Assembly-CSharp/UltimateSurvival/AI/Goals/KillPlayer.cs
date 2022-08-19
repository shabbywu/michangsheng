using System;

namespace UltimateSurvival.AI.Goals
{
	// Token: 0x02000674 RID: 1652
	public class KillPlayer : Goal
	{
		// Token: 0x0600347B RID: 13435 RVA: 0x0016E192 File Offset: 0x0016C392
		public override void OnStart()
		{
			this.m_Priority = 10f;
			base.GoalState.Add("Is Player Dead", true);
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x0016E130 File Offset: 0x0016C330
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
