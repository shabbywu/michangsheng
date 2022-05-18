using System;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x02000979 RID: 2425
	[Serializable]
	public class EntityVitals
	{
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06003E0C RID: 15884 RVA: 0x0002CB1F File Offset: 0x0002AD1F
		// (set) Token: 0x06003E0D RID: 15885 RVA: 0x0002CB27 File Offset: 0x0002AD27
		public bool IsHungry
		{
			get
			{
				return this.m_IsHungry;
			}
			set
			{
				this.m_IsHungry = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06003E0E RID: 15886 RVA: 0x0002CB30 File Offset: 0x0002AD30
		// (set) Token: 0x06003E0F RID: 15887 RVA: 0x0002CB38 File Offset: 0x0002AD38
		public float LastTimeFed
		{
			get
			{
				return this.m_LastTimeFed;
			}
			set
			{
				this.m_LastTimeFed = value;
			}
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x0002CB41 File Offset: 0x0002AD41
		public void Update(AIBrain brain)
		{
			if (!this.m_IsHungry && Time.time - this.m_LastTimeFed > this.m_HungerRegenerationTime)
			{
				this.m_IsHungry = true;
			}
			StateData.OverrideValue("Is Hungry", this.m_IsHungry, brain.WorldState);
		}

		// Token: 0x04003829 RID: 14377
		[SerializeField]
		private float m_HungerRegenerationTime;

		// Token: 0x0400382A RID: 14378
		private float m_LastTimeFed;

		// Token: 0x0400382B RID: 14379
		private bool m_IsHungry;
	}
}
