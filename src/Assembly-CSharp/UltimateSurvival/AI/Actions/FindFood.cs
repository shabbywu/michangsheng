using System;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000987 RID: 2439
	public class FindFood : PointBased
	{
		// Token: 0x06003E5A RID: 15962 RVA: 0x001B6E54 File Offset: 0x001B5054
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

		// Token: 0x06003E5B RID: 15963 RVA: 0x0002CE51 File Offset: 0x0002B051
		public override bool CanActivate(AIBrain brain)
		{
			return base.CanActivate(brain) && brain.Settings.Vitals.IsHungry;
		}
	}
}
