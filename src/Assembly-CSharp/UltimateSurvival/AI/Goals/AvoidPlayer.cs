using System;

namespace UltimateSurvival.AI.Goals
{
	// Token: 0x02000672 RID: 1650
	public class AvoidPlayer : Goal
	{
		// Token: 0x06003473 RID: 13427 RVA: 0x0016E118 File Offset: 0x0016C318
		public override void OnStart()
		{
			base.GoalState.Add("Player in sight", false);
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x0016E130 File Offset: 0x0016C330
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
