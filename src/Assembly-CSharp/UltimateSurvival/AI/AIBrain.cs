using System;
using System.Collections.Generic;
using System.Linq;
using UltimateSurvival.AI.Actions;
using UltimateSurvival.AI.Goals;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x02000973 RID: 2419
	[RequireComponent(typeof(AISettings))]
	public class AIBrain : AIBehaviour
	{
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x0002C889 File Offset: 0x0002AA89
		public List<UltimateSurvival.AI.Actions.Action> AvailableActions
		{
			get
			{
				return this.m_AvailableActions;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x0002C891 File Offset: 0x0002AA91
		public UltimateSurvival.AI.Actions.Action Fallback
		{
			get
			{
				return this.m_Fallback;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06003DD4 RID: 15828 RVA: 0x0002C899 File Offset: 0x0002AA99
		public Queue<UltimateSurvival.AI.Actions.Action> ActionQueue
		{
			get
			{
				return this.m_ActionQueue;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x0002C8A1 File Offset: 0x0002AAA1
		public Goal CurrentGoal
		{
			get
			{
				return this.m_CurrentGoal;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06003DD6 RID: 15830 RVA: 0x0002C8A9 File Offset: 0x0002AAA9
		public StateData WorldState
		{
			get
			{
				return this.m_WorldState;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x0002C8B1 File Offset: 0x0002AAB1
		public AISettings Settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x001B5C8C File Offset: 0x001B3E8C
		private void Start()
		{
			this.m_Fallback = Object.Instantiate<UltimateSurvival.AI.Actions.Action>(this.m_Fallback);
			for (int i = 0; i < this.m_AvailableActions.Count; i++)
			{
				this.m_AvailableActions[i] = Object.Instantiate<UltimateSurvival.AI.Actions.Action>(this.m_AvailableActions[i]);
			}
			for (int j = 0; j < this.m_AvailableGoals.Count; j++)
			{
				this.m_AvailableGoals[j] = Object.Instantiate<Goal>(this.m_AvailableGoals[j]);
			}
			this.m_Planner = new Planner();
			this.m_Settings = base.GetComponent<AISettings>();
			this.CreateNewWorldState();
			this.InitializeData();
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x001B5D34 File Offset: 0x001B3F34
		private void Update()
		{
			bool flag = this.IsReplanNeededBecauseOfGoals();
			bool flag2 = this.m_ActionQueue == null || this.m_ActionQueue.Count == 0;
			if (flag2 || flag)
			{
				this.Replan();
			}
			if (!flag2)
			{
				UltimateSurvival.AI.Actions.Action action = this.m_ActionQueue.Peek();
				bool flag3 = Time.time - this.m_LastPlanTime > this.m_MinPlanInterval && action.IsInterruptable;
				if (this.m_ActionQueue.Count > 1 && flag3)
				{
					UltimateSurvival.AI.Actions.Action action2 = this.m_ActionQueue.ToArray()[1];
					bool flag4 = action2.Priority > action.Priority;
					bool flag5 = action2.CanActivate(this);
					if (flag4 && flag5)
					{
						this.m_LastPlanTime = Time.time;
						this.m_ActionQueue.Dequeue();
						action = this.m_ActionQueue.Peek();
					}
				}
				if (action.IsActive)
				{
					if (!action.StillValid(this))
					{
						action.IsActive = false;
						this.m_ActionQueue.Clear();
						return;
					}
					action.OnUpdate(this);
					if (action.IsDone(this))
					{
						action.OnCompletion(this);
						action.IsActive = false;
						this.m_ActionQueue.Dequeue();
						return;
					}
				}
				else if (!action.IsActive)
				{
					if (!action.CanActivate(this))
					{
						this.m_ActionQueue.Clear();
						return;
					}
					action.Activate(this);
					action.IsActive = true;
				}
			}
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x001B5E74 File Offset: 0x001B4074
		private bool IsReplanNeededBecauseOfGoals()
		{
			if (this.m_AvailableGoals == null || this.m_AvailableGoals.Count == 0)
			{
				return false;
			}
			if (this.m_ActionQueue == null || this.m_ActionQueue.Count == 0)
			{
				return false;
			}
			bool result = false;
			if (Time.time - this.m_LastGoalPriorityCheckTime > this.m_MinGoalPriorityCheckInterval)
			{
				this.m_LastGoalPriorityCheckTime = Time.time;
				for (int i = 0; i < this.m_AvailableGoals.Count; i++)
				{
					this.m_AvailableGoals[i].RecalculatePriority(this);
				}
				this.m_AvailableGoals = (from x in this.m_AvailableGoals
				orderby x.Priority descending
				select x).ToList<Goal>();
				if (this.m_AvailableGoals[0] != this.CurrentGoal)
				{
					this.m_ActionQueue.Peek().OnDeactivation(this);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x001B5F60 File Offset: 0x001B4160
		private void Replan()
		{
			for (int i = 0; i < this.m_AvailableActions.Count; i++)
			{
				this.m_AvailableActions[i].IsActive = false;
			}
			if (!this.m_Planner.Plan(this.m_AvailableGoals, this.m_AvailableActions, this, out this.m_ActionQueue, out this.m_CurrentGoal))
			{
				this.FallBack();
				return;
			}
			this.m_Fallback.OnDeactivation(this);
			this.m_Fallback.IsActive = false;
			this.m_LastPlanTime = Time.time;
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x001B5FE8 File Offset: 0x001B41E8
		private void FallBack()
		{
			if (this.m_ActionQueue == null || this.m_ActionQueue.Peek() != this.m_Fallback)
			{
				this.m_ActionQueue = new Queue<UltimateSurvival.AI.Actions.Action>();
				this.m_ActionQueue.Enqueue(this.m_Fallback);
				this.m_Fallback.Activate(this);
				this.m_Fallback.IsActive = true;
			}
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x001B604C File Offset: 0x001B424C
		private void CreateNewWorldState()
		{
			this.m_WorldState = new StateData();
			this.m_WorldState.Add("Is Player Dead", false);
			this.m_WorldState.Add("Can Attack Player", false);
			this.m_WorldState.Add("Player in sight", false);
			this.m_WorldState.Add("Next To Food", false);
			this.m_WorldState.Add("Is Hungry", false);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x001B60D4 File Offset: 0x001B42D4
		private void InitializeData()
		{
			if (this.m_AvailableActions.Count == 0)
			{
				Debug.LogError("No actions set for " + base.gameObject.name + " entity", this);
				return;
			}
			if (this.m_AvailableGoals.Count == 0)
			{
				Debug.LogError("No goals set for " + base.gameObject.name + " entity", this);
				return;
			}
			for (int i = 0; i < this.m_AvailableActions.Count; i++)
			{
				this.m_AvailableActions[i].OnStart(this);
				this.m_AvailableActions[i].IsActive = false;
			}
			for (int j = 0; j < this.m_AvailableGoals.Count; j++)
			{
				this.m_AvailableGoals[j].OnStart();
			}
			this.m_Fallback.OnStart(this);
			this.m_Fallback.IsActive = false;
		}

		// Token: 0x040037FE RID: 14334
		[SerializeField]
		private List<UltimateSurvival.AI.Actions.Action> m_AvailableActions;

		// Token: 0x040037FF RID: 14335
		[SerializeField]
		private List<Goal> m_AvailableGoals;

		// Token: 0x04003800 RID: 14336
		[SerializeField]
		private UltimateSurvival.AI.Actions.Action m_Fallback;

		// Token: 0x04003801 RID: 14337
		[SerializeField]
		private float m_MinPlanInterval;

		// Token: 0x04003802 RID: 14338
		[SerializeField]
		private float m_MinGoalPriorityCheckInterval;

		// Token: 0x04003803 RID: 14339
		private Queue<UltimateSurvival.AI.Actions.Action> m_ActionQueue;

		// Token: 0x04003804 RID: 14340
		private Planner m_Planner;

		// Token: 0x04003805 RID: 14341
		private Goal m_CurrentGoal;

		// Token: 0x04003806 RID: 14342
		private StateData m_WorldState;

		// Token: 0x04003807 RID: 14343
		private AISettings m_Settings;

		// Token: 0x04003808 RID: 14344
		private float m_LastPlanTime;

		// Token: 0x04003809 RID: 14345
		private float m_LastGoalPriorityCheckTime;
	}
}
