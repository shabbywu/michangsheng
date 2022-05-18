using System;
using System.Collections.Generic;
using System.Linq;
using UltimateSurvival.AI.Actions;
using UltimateSurvival.AI.Goals;

namespace UltimateSurvival.AI
{
	// Token: 0x0200097A RID: 2426
	public class Planner
	{
		// Token: 0x06003E12 RID: 15890 RVA: 0x001B670C File Offset: 0x001B490C
		public bool Plan(List<Goal> availableGoals, List<UltimateSurvival.AI.Actions.Action> availableActions, AIBrain brain, out Queue<UltimateSurvival.AI.Actions.Action> plan, out Goal selected)
		{
			bool result = false;
			plan = new Queue<UltimateSurvival.AI.Actions.Action>();
			selected = null;
			List<UltimateSurvival.AI.Actions.Action> list = new List<UltimateSurvival.AI.Actions.Action>();
			list = list.CopyOther(availableActions);
			List<Goal> list2 = new List<Goal>();
			list2 = list2.CopyOther(availableGoals);
			list2 = (from x in list2
			orderby x.Priority descending
			select x).ToList<Goal>();
			for (int i = 0; i < list2.Count; i++)
			{
				Goal goal = list2[i];
				List<UltimateSurvival.AI.Actions.Action> list3 = new List<UltimateSurvival.AI.Actions.Action>();
				this.FindActionThatMatchesState(goal.GoalState, list, list3);
				if (list3.Count > 0)
				{
					for (int j = list3.Count - 1; j >= 0; j--)
					{
						plan.Enqueue(list3[j]);
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

		// Token: 0x06003E13 RID: 15891 RVA: 0x001B67EC File Offset: 0x001B49EC
		private void FindActionThatMatchesState(StateData goalState, List<UltimateSurvival.AI.Actions.Action> actions, List<UltimateSurvival.AI.Actions.Action> related)
		{
			for (int i = 0; i < actions.Count; i++)
			{
				StateData effects = actions[i].Effects;
				StateData preconditions = actions[i].Preconditions;
				foreach (KeyValuePair<string, object> keyValuePair in goalState.m_Dictionary)
				{
					foreach (KeyValuePair<string, object> keyValuePair2 in effects.m_Dictionary)
					{
						bool flag = keyValuePair2.Key == keyValuePair.Key;
						bool flag2 = keyValuePair2.Value.ToString() == keyValuePair.Value.ToString();
						if (flag && flag2)
						{
							related.Add(actions[i]);
							actions.Remove(actions[i]);
							this.FindActionThatMatchesState(preconditions, actions, related);
						}
					}
				}
			}
		}
	}
}
