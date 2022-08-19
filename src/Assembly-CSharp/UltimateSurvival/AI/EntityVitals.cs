using System;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x0200066E RID: 1646
	[Serializable]
	public class EntityVitals
	{
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x0016DDD3 File Offset: 0x0016BFD3
		// (set) Token: 0x06003461 RID: 13409 RVA: 0x0016DDDB File Offset: 0x0016BFDB
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

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x0016DDE4 File Offset: 0x0016BFE4
		// (set) Token: 0x06003463 RID: 13411 RVA: 0x0016DDEC File Offset: 0x0016BFEC
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

		// Token: 0x06003464 RID: 13412 RVA: 0x0016DDF5 File Offset: 0x0016BFF5
		public void Update(AIBrain brain)
		{
			if (!this.m_IsHungry && Time.time - this.m_LastTimeFed > this.m_HungerRegenerationTime)
			{
				this.m_IsHungry = true;
			}
			StateData.OverrideValue("Is Hungry", this.m_IsHungry, brain.WorldState);
		}

		// Token: 0x04002E8E RID: 11918
		[SerializeField]
		private float m_HungerRegenerationTime;

		// Token: 0x04002E8F RID: 11919
		private float m_LastTimeFed;

		// Token: 0x04002E90 RID: 11920
		private bool m_IsHungry;
	}
}
