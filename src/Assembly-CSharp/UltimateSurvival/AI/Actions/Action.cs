using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000983 RID: 2435
	[Serializable]
	public class Action : ScriptableObject
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x0002CCF2 File Offset: 0x0002AEF2
		// (set) Token: 0x06003E32 RID: 15922 RVA: 0x0002CCFA File Offset: 0x0002AEFA
		public bool IsActive
		{
			get
			{
				return this.m_IsActive;
			}
			set
			{
				this.m_IsActive = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x0002CD03 File Offset: 0x0002AF03
		public int Priority
		{
			get
			{
				return this.m_Priority;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x0002CD0B File Offset: 0x0002AF0B
		public bool IsInterruptable
		{
			get
			{
				return this.m_IsInterruptable;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x0002CD13 File Offset: 0x0002AF13
		public ET.ActionRepeatType RepeatType
		{
			get
			{
				return this.m_RepeatType;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x0002CD1B File Offset: 0x0002AF1B
		public StateData Preconditions
		{
			get
			{
				return this.m_Preconditions;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x0002CD23 File Offset: 0x0002AF23
		public StateData Effects
		{
			get
			{
				return this.m_Effects;
			}
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnStart(AIBrain agent)
		{
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnUpdate(AIBrain agent)
		{
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x0002CD2B File Offset: 0x0002AF2B
		public virtual void OnCompletion(AIBrain agent)
		{
			if (this.m_RepeatType != ET.ActionRepeatType.Repetitive)
			{
				StateData.OverrideValues(this.m_Effects, agent.WorldState);
			}
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x0000A093 File Offset: 0x00008293
		public virtual bool CanActivate(AIBrain brain)
		{
			return true;
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x0000A093 File Offset: 0x00008293
		public virtual bool StillValid(AIBrain brain)
		{
			return true;
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void Activate(AIBrain brain)
		{
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnDeactivation(AIBrain brain)
		{
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x00004050 File Offset: 0x00002250
		public virtual bool IsDone(AIBrain brain)
		{
			return false;
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void ResetValues()
		{
		}

		// Token: 0x0400383B RID: 14395
		protected int m_Priority;

		// Token: 0x0400383C RID: 14396
		protected bool m_IsInterruptable;

		// Token: 0x0400383D RID: 14397
		protected ET.ActionRepeatType m_RepeatType;

		// Token: 0x0400383E RID: 14398
		private bool m_IsActive;

		// Token: 0x0400383F RID: 14399
		private StateData m_Preconditions = new StateData();

		// Token: 0x04003840 RID: 14400
		private StateData m_Effects = new StateData();
	}
}
