using System;
using UnityEngine;

namespace UltimateSurvival.AI.Goals
{
	// Token: 0x02000673 RID: 1651
	public class Goal : ScriptableObject
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x0016E16F File Offset: 0x0016C36F
		public float Priority
		{
			get
			{
				return this.m_Priority;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x0016E177 File Offset: 0x0016C377
		public StateData GoalState
		{
			get
			{
				return this.m_GoalState;
			}
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnStart()
		{
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void RecalculatePriority(AIBrain brain)
		{
		}

		// Token: 0x04002E92 RID: 11922
		[SerializeField]
		[ShowOnly]
		protected float m_Priority;

		// Token: 0x04002E93 RID: 11923
		[SerializeField]
		protected Vector2 m_PriorityRange;

		// Token: 0x04002E94 RID: 11924
		private StateData m_GoalState = new StateData();
	}
}
