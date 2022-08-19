using System;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x0200067A RID: 1658
	public class FindFood : PointBased
	{
		// Token: 0x060034AA RID: 13482 RVA: 0x0016E8A8 File Offset: 0x0016CAA8
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 8;
			this.m_IsInterruptable = true;
			this.m_RepeatType = ET.ActionRepeatType.Single;
			base.Preconditions.Add("Is Hungry", true);
			base.Effects.Add("Next To Food", true);
			base.OnStart(brain);
			base.ChangePatrolPoint();
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x0016E903 File Offset: 0x0016CB03
		public override bool CanActivate(AIBrain brain)
		{
			return base.CanActivate(brain) && brain.Settings.Vitals.IsHungry;
		}
	}
}
