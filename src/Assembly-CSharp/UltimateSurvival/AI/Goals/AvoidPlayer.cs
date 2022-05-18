using System;

namespace UltimateSurvival.AI.Goals
{
	// Token: 0x0200097F RID: 2431
	public class AvoidPlayer : Goal
	{
		// Token: 0x06003E23 RID: 15907 RVA: 0x0002CC06 File Offset: 0x0002AE06
		public override void OnStart()
		{
			base.GoalState.Add("Player in sight", false);
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x0002CC1E File Offset: 0x0002AE1E
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
