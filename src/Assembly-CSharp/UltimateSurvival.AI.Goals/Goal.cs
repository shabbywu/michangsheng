using UnityEngine;

namespace UltimateSurvival.AI.Goals;

public class Goal : ScriptableObject
{
	[SerializeField]
	[ShowOnly]
	protected float m_Priority;

	[SerializeField]
	protected Vector2 m_PriorityRange;

	private StateData m_GoalState = new StateData();

	public float Priority => m_Priority;

	public StateData GoalState => m_GoalState;

	public virtual void OnStart()
	{
	}

	public virtual void RecalculatePriority(AIBrain brain)
	{
	}
}
