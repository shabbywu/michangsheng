namespace UltimateSurvival.AI.Actions;

public class FindFood : PointBased
{
	public override void OnStart(AIBrain brain)
	{
		m_Priority = 8;
		m_IsInterruptable = true;
		m_RepeatType = ET.ActionRepeatType.Single;
		base.Preconditions.Add("Is Hungry", true);
		base.Effects.Add("Next To Food", true);
		base.OnStart(brain);
		ChangePatrolPoint();
	}

	public override bool CanActivate(AIBrain brain)
	{
		if (base.CanActivate(brain))
		{
			return brain.Settings.Vitals.IsHungry;
		}
		return false;
	}
}
