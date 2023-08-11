namespace UltimateSurvival.AI.Goals;

public class SatisfyHunger : Goal
{
	public override void OnStart()
	{
		base.GoalState.Add("Is Hungry", false);
	}

	public override void RecalculatePriority(AIBrain brain)
	{
		if (brain.Settings.Vitals.IsHungry)
		{
			m_Priority = m_PriorityRange.y;
		}
		else
		{
			m_Priority = m_PriorityRange.x;
		}
	}
}
