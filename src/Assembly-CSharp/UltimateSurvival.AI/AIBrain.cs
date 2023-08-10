using System.Collections.Generic;
using System.Linq;
using UltimateSurvival.AI.Actions;
using UltimateSurvival.AI.Goals;
using UnityEngine;

namespace UltimateSurvival.AI;

[RequireComponent(typeof(AISettings))]
public class AIBrain : AIBehaviour
{
	[SerializeField]
	private List<Action> m_AvailableActions;

	[SerializeField]
	private List<Goal> m_AvailableGoals;

	[SerializeField]
	private Action m_Fallback;

	[SerializeField]
	private float m_MinPlanInterval;

	[SerializeField]
	private float m_MinGoalPriorityCheckInterval;

	private Queue<Action> m_ActionQueue;

	private Planner m_Planner;

	private Goal m_CurrentGoal;

	private StateData m_WorldState;

	private AISettings m_Settings;

	private float m_LastPlanTime;

	private float m_LastGoalPriorityCheckTime;

	public List<Action> AvailableActions => m_AvailableActions;

	public Action Fallback => m_Fallback;

	public Queue<Action> ActionQueue => m_ActionQueue;

	public Goal CurrentGoal => m_CurrentGoal;

	public StateData WorldState => m_WorldState;

	public AISettings Settings => m_Settings;

	private void Start()
	{
		m_Fallback = Object.Instantiate<Action>(m_Fallback);
		for (int i = 0; i < m_AvailableActions.Count; i++)
		{
			m_AvailableActions[i] = Object.Instantiate<Action>(m_AvailableActions[i]);
		}
		for (int j = 0; j < m_AvailableGoals.Count; j++)
		{
			m_AvailableGoals[j] = Object.Instantiate<Goal>(m_AvailableGoals[j]);
		}
		m_Planner = new Planner();
		m_Settings = ((Component)this).GetComponent<AISettings>();
		CreateNewWorldState();
		InitializeData();
	}

	private void Update()
	{
		bool flag = IsReplanNeededBecauseOfGoals();
		bool num = m_ActionQueue == null || m_ActionQueue.Count == 0;
		if (num || flag)
		{
			Replan();
		}
		if (num)
		{
			return;
		}
		Action action = m_ActionQueue.Peek();
		bool flag2 = Time.time - m_LastPlanTime > m_MinPlanInterval && action.IsInterruptable;
		if (m_ActionQueue.Count > 1 && flag2)
		{
			Action obj = m_ActionQueue.ToArray()[1];
			bool flag3 = obj.Priority > action.Priority;
			bool flag4 = obj.CanActivate(this);
			if (flag3 && flag4)
			{
				m_LastPlanTime = Time.time;
				m_ActionQueue.Dequeue();
				action = m_ActionQueue.Peek();
			}
		}
		if (action.IsActive)
		{
			if (!action.StillValid(this))
			{
				action.IsActive = false;
				m_ActionQueue.Clear();
				return;
			}
			action.OnUpdate(this);
			if (action.IsDone(this))
			{
				action.OnCompletion(this);
				action.IsActive = false;
				m_ActionQueue.Dequeue();
			}
		}
		else if (!action.IsActive)
		{
			if (!action.CanActivate(this))
			{
				m_ActionQueue.Clear();
				return;
			}
			action.Activate(this);
			action.IsActive = true;
		}
	}

	private bool IsReplanNeededBecauseOfGoals()
	{
		if (m_AvailableGoals == null || m_AvailableGoals.Count == 0)
		{
			return false;
		}
		if (m_ActionQueue == null || m_ActionQueue.Count == 0)
		{
			return false;
		}
		bool result = false;
		if (Time.time - m_LastGoalPriorityCheckTime > m_MinGoalPriorityCheckInterval)
		{
			m_LastGoalPriorityCheckTime = Time.time;
			for (int i = 0; i < m_AvailableGoals.Count; i++)
			{
				m_AvailableGoals[i].RecalculatePriority(this);
			}
			m_AvailableGoals = m_AvailableGoals.OrderByDescending((Goal x) => x.Priority).ToList();
			if ((Object)(object)m_AvailableGoals[0] != (Object)(object)CurrentGoal)
			{
				m_ActionQueue.Peek().OnDeactivation(this);
				result = true;
			}
		}
		return result;
	}

	private void Replan()
	{
		for (int i = 0; i < m_AvailableActions.Count; i++)
		{
			m_AvailableActions[i].IsActive = false;
		}
		if (!m_Planner.Plan(m_AvailableGoals, m_AvailableActions, this, out m_ActionQueue, out m_CurrentGoal))
		{
			FallBack();
			return;
		}
		m_Fallback.OnDeactivation(this);
		m_Fallback.IsActive = false;
		m_LastPlanTime = Time.time;
	}

	private void FallBack()
	{
		if (m_ActionQueue == null || (Object)(object)m_ActionQueue.Peek() != (Object)(object)m_Fallback)
		{
			m_ActionQueue = new Queue<Action>();
			m_ActionQueue.Enqueue(m_Fallback);
			m_Fallback.Activate(this);
			m_Fallback.IsActive = true;
		}
	}

	private void CreateNewWorldState()
	{
		m_WorldState = new StateData();
		m_WorldState.Add("Is Player Dead", false);
		m_WorldState.Add("Can Attack Player", false);
		m_WorldState.Add("Player in sight", false);
		m_WorldState.Add("Next To Food", false);
		m_WorldState.Add("Is Hungry", false);
	}

	private void InitializeData()
	{
		if (m_AvailableActions.Count == 0)
		{
			Debug.LogError((object)("No actions set for " + ((Object)((Component)this).gameObject).name + " entity"), (Object)(object)this);
			return;
		}
		if (m_AvailableGoals.Count == 0)
		{
			Debug.LogError((object)("No goals set for " + ((Object)((Component)this).gameObject).name + " entity"), (Object)(object)this);
			return;
		}
		for (int i = 0; i < m_AvailableActions.Count; i++)
		{
			m_AvailableActions[i].OnStart(this);
			m_AvailableActions[i].IsActive = false;
		}
		for (int j = 0; j < m_AvailableGoals.Count; j++)
		{
			m_AvailableGoals[j].OnStart();
		}
		m_Fallback.OnStart(this);
		m_Fallback.IsActive = false;
	}
}
