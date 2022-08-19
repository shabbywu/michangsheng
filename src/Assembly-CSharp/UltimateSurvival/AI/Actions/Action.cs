using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000676 RID: 1654
	[Serializable]
	public class Action : ScriptableObject
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x0016E204 File Offset: 0x0016C404
		// (set) Token: 0x06003482 RID: 13442 RVA: 0x0016E20C File Offset: 0x0016C40C
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

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x0016E215 File Offset: 0x0016C415
		public int Priority
		{
			get
			{
				return this.m_Priority;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x0016E21D File Offset: 0x0016C41D
		public bool IsInterruptable
		{
			get
			{
				return this.m_IsInterruptable;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x0016E225 File Offset: 0x0016C425
		public ET.ActionRepeatType RepeatType
		{
			get
			{
				return this.m_RepeatType;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x0016E22D File Offset: 0x0016C42D
		public StateData Preconditions
		{
			get
			{
				return this.m_Preconditions;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x0016E235 File Offset: 0x0016C435
		public StateData Effects
		{
			get
			{
				return this.m_Effects;
			}
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnStart(AIBrain agent)
		{
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnUpdate(AIBrain agent)
		{
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x0016E23D File Offset: 0x0016C43D
		public virtual void OnCompletion(AIBrain agent)
		{
			if (this.m_RepeatType != ET.ActionRepeatType.Repetitive)
			{
				StateData.OverrideValues(this.m_Effects, agent.WorldState);
			}
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x00024C5F File Offset: 0x00022E5F
		public virtual bool CanActivate(AIBrain brain)
		{
			return true;
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x00024C5F File Offset: 0x00022E5F
		public virtual bool StillValid(AIBrain brain)
		{
			return true;
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void Activate(AIBrain brain)
		{
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnDeactivation(AIBrain brain)
		{
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual bool IsDone(AIBrain brain)
		{
			return false;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void ResetValues()
		{
		}

		// Token: 0x04002E95 RID: 11925
		protected int m_Priority;

		// Token: 0x04002E96 RID: 11926
		protected bool m_IsInterruptable;

		// Token: 0x04002E97 RID: 11927
		protected ET.ActionRepeatType m_RepeatType;

		// Token: 0x04002E98 RID: 11928
		private bool m_IsActive;

		// Token: 0x04002E99 RID: 11929
		private StateData m_Preconditions = new StateData();

		// Token: 0x04002E9A RID: 11930
		private StateData m_Effects = new StateData();
	}
}
