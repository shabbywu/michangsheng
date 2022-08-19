using System;
using System.Collections.Generic;
using System.Linq;
using UltimateSurvival.AI.Actions;
using UltimateSurvival.AI.Goals;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x02000669 RID: 1641
	[RequireComponent(typeof(AISettings))]
	public class AIBrain : AIBehaviour
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x0016D0D0 File Offset: 0x0016B2D0
		public List<UltimateSurvival.AI.Actions.Action> AvailableActions
		{
			get
			{
				return this.m_AvailableActions;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x0016D0D8 File Offset: 0x0016B2D8
		public UltimateSurvival.AI.Actions.Action Fallback
		{
			get
			{
				return this.m_Fallback;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x0016D0E0 File Offset: 0x0016B2E0
		public Queue<UltimateSurvival.AI.Actions.Action> ActionQueue
		{
			get
			{
				return this.m_ActionQueue;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x0016D0E8 File Offset: 0x0016B2E8
		public Goal CurrentGoal
		{
			get
			{
				return this.m_CurrentGoal;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x0016D0F0 File Offset: 0x0016B2F0
		public StateData WorldState
		{
			get
			{
				return this.m_WorldState;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x0016D0F8 File Offset: 0x0016B2F8
		public AISettings Settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x0016D100 File Offset: 0x0016B300
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

		// Token: 0x06003430 RID: 13360 RVA: 0x0016D1A8 File Offset: 0x0016B3A8
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

		// Token: 0x06003431 RID: 13361 RVA: 0x0016D2E8 File Offset: 0x0016B4E8
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

		// Token: 0x06003432 RID: 13362 RVA: 0x0016D3D4 File Offset: 0x0016B5D4
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

		// Token: 0x06003433 RID: 13363 RVA: 0x0016D45C File Offset: 0x0016B65C
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

		// Token: 0x06003434 RID: 13364 RVA: 0x0016D4C0 File Offset: 0x0016B6C0
		private void CreateNewWorldState()
		{
			this.m_WorldState = new StateData();
			this.m_WorldState.Add("Is Player Dead", false);
			this.m_WorldState.Add("Can Attack Player", false);
			this.m_WorldState.Add("Player in sight", false);
			this.m_WorldState.Add("Next To Food", false);
			this.m_WorldState.Add("Is Hungry", false);
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x0016D548 File Offset: 0x0016B748
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

		// Token: 0x04002E65 RID: 11877
		[SerializeField]
		private List<UltimateSurvival.AI.Actions.Action> m_AvailableActions;

		// Token: 0x04002E66 RID: 11878
		[SerializeField]
		private List<Goal> m_AvailableGoals;

		// Token: 0x04002E67 RID: 11879
		[SerializeField]
		private UltimateSurvival.AI.Actions.Action m_Fallback;

		// Token: 0x04002E68 RID: 11880
		[SerializeField]
		private float m_MinPlanInterval;

		// Token: 0x04002E69 RID: 11881
		[SerializeField]
		private float m_MinGoalPriorityCheckInterval;

		// Token: 0x04002E6A RID: 11882
		private Queue<UltimateSurvival.AI.Actions.Action> m_ActionQueue;

		// Token: 0x04002E6B RID: 11883
		private Planner m_Planner;

		// Token: 0x04002E6C RID: 11884
		private Goal m_CurrentGoal;

		// Token: 0x04002E6D RID: 11885
		private StateData m_WorldState;

		// Token: 0x04002E6E RID: 11886
		private AISettings m_Settings;

		// Token: 0x04002E6F RID: 11887
		private float m_LastPlanTime;

		// Token: 0x04002E70 RID: 11888
		private float m_LastGoalPriorityCheckTime;
	}
}
