using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200087D RID: 2173
	[Serializable]
	public class StatDepleter
	{
		// Token: 0x0600382C RID: 14380 RVA: 0x001A2584 File Offset: 0x001A0784
		public void Update(Value<float> statValue, Attempt<HealthEventData> healthChanger)
		{
			if (statValue.Is(0f) && Time.time - this.m_LastDamageTime > this.m_DamagePeriod)
			{
				this.m_LastDamageTime = Time.time;
				healthChanger.Try(new HealthEventData(-0.01f, null, default(Vector3), default(Vector3), 0f));
			}
		}

		// Token: 0x0400327B RID: 12923
		[SerializeField]
		[Clamp(0f, 100f)]
		private float m_DepletionRate = 1f;

		// Token: 0x0400327C RID: 12924
		[SerializeField]
		[Clamp(0f, 100f)]
		[Tooltip("Damage applied when this stat reaches 0.")]
		private float m_Damage;

		// Token: 0x0400327D RID: 12925
		[SerializeField]
		[Clamp(0f, 100f)]
		[Tooltip("How frequent damage will be applied.")]
		private float m_DamagePeriod = 3f;

		// Token: 0x0400327E RID: 12926
		private float m_LastDamageTime;
	}
}
