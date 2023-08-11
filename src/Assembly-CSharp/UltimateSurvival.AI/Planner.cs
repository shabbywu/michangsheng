using System.Collections.Generic;
using System.Linq;
using UltimateSurvival.AI.Actions;
using UltimateSurvival.AI.Goals;

namespace UltimateSurvival.AI;

public class Planner
{
	public bool Plan(List<Goal> availableGoals, List<Action> availableActions, AIBrain brain, out Queue<Action> plan, out Goal selected)
	{
		bool result = false;
		plan = new Queue<Action>();
		selected = null;
		List<Action> list = new List<Action>();
		list = list.CopyOther(availableActions);
		List<Goal> list2 = new List<Goal>();
		list2 = list2.CopyOther(availableGoals);
		list2 = list2.OrderByDescending((Goal x) => x.Priority).ToList();
		for (int i = 0; i < list2.Count; i++)
		{
			Goal goal = list2[i];
			List<Action> list3 = new List<Action>();
			FindActionThatMatchesState(goal.GoalState, list, list3);
			if (list3.Count > 0)
			{
				for (int num = list3.Count - 1; num >= 0; num--)
				{
					plan.Enqueue(list3[num]);
				}
				if (plan.Peek().CanActivate(brain))
				{
					selected = goal;
					result = true;
					break;
				}
			}
		}
		return result;
	}

	private void FindActionThatMatchesState(StateData goalState, List<Action> actions, List<Action> related)
	{
		for (int i = 0; i < actions.Count; i++)
		{
			StateData effects = actions[i].Effects;
			StateData preconditions = actions[i].Preconditions;
			foreach (KeyValuePair<string, object> item in goalState.m_Dictionary)
			{
				foreach (KeyValuePair<string, object> item2 in effects.m_Dictionary)
				{
					bool num = item2.Key == item.Key;
					bool flag = item2.Value.ToString() == item.Value.ToString();
					if (num && flag)
					{
						related.Add(actions[i]);
						actions.Remove(actions[i]);
						FindActionThatMatchesState(preconditions, actions, related);
					}
				}
			}
		}
	}
}
