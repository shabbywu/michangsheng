using System;

namespace UltimateSurvival.AI.Goals
{
	// Token: 0x02000982 RID: 2434
	public class SatisfyHunger : Goal
	{
		// Token: 0x06003E2E RID: 15918 RVA: 0x0002CCA3 File Offset: 0x0002AEA3
		public override void OnStart()
		{
			base.GoalState.Add("Is Hungry", false);
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x0002CCBB File Offset: 0x0002AEBB
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
