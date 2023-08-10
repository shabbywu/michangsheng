namespace UltimateSurvival.AI.Goals;

public class AvoidPlayer : Goal
{
	public override void OnStart()
	{
		base.GoalState.Add("Player in sight", false);
	}

	public override void RecalculatePriority(AIBrain brain)
	{
		if (brain.Settings.Detection.HasTarget())
		{
			m_Priority = m_PriorityRange.y;
		}
		else
		{
			m_Priority = m_PriorityRange.x;
		}
	}
}
