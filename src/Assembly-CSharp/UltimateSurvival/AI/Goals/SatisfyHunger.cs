using System;

namespace UltimateSurvival.AI.Goals
{
	// Token: 0x02000675 RID: 1653
	public class SatisfyHunger : Goal
	{
		// Token: 0x0600347E RID: 13438 RVA: 0x0016E1B5 File Offset: 0x0016C3B5
		public override void OnStart()
		{
			base.GoalState.Add("Is Hungry", false);
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x0016E1CD File Offset: 0x0016C3CD
		public override void RecalculatePriority(AIBrain brain)
		{
			if (brain.Settings.Vitals.IsHungry)
			{
				this.m_Priority = this.m_PriorityRange.y;
				return;
			}
			this.m_Priority = this.m_PriorityRange.x;
		}
	}
}
