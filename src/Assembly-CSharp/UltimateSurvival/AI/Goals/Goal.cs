using System;
using UnityEngine;

namespace UltimateSurvival.AI.Goals
{
	// Token: 0x02000980 RID: 2432
	public class Goal : ScriptableObject
	{
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06003E26 RID: 15910 RVA: 0x0002CC5D File Offset: 0x0002AE5D
		public float Priority
		{
			get
			{
				return this.m_Priority;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x0002CC65 File Offset: 0x0002AE65
		public StateData GoalState
		{
			get
			{
				return this.m_GoalState;
			}
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnStart()
		{
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void RecalculatePriority(AIBrain brain)
		{
		}

		// Token: 0x04003838 RID: 14392
		[SerializeField]
		[ShowOnly]
		protected float m_Priority;

		// Token: 0x04003839 RID: 14393
		[SerializeField]
		protected Vector2 m_PriorityRange;

		// Token: 0x0400383A RID: 14394
		private StateData m_GoalState = new StateData();
	}
}
