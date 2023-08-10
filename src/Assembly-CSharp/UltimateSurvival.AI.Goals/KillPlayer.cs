namespace UltimateSurvival.AI.Goals;

public class KillPlayer : Goal
{
	public override void OnStart()
	{
		m_Priority = 10f;
		base.GoalState.Add("Is Player Dead", true);
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
