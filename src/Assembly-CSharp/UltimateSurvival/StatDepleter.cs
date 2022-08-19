using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005BF RID: 1471
	[Serializable]
	public class StatDepleter
	{
		// Token: 0x06002FA8 RID: 12200 RVA: 0x00158424 File Offset: 0x00156624
		public void Update(Value<float> statValue, Attempt<HealthEventData> healthChanger)
		{
			if (statValue.Is(0f) && Time.time - this.m_LastDamageTime > this.m_DamagePeriod)
			{
				this.m_LastDamageTime = Time.time;
				healthChanger.Try(new HealthEventData(-0.01f, null, default(Vector3), default(Vector3), 0f));
			}
		}

		// Token: 0x040029F1 RID: 10737
		[SerializeField]
		[Clamp(0f, 100f)]
		private float m_DepletionRate = 1f;

		// Token: 0x040029F2 RID: 10738
		[SerializeField]
		[Clamp(0f, 100f)]
		[Tooltip("Damage applied when this stat reaches 0.")]
		private float m_Damage;

		// Token: 0x040029F3 RID: 10739
		[SerializeField]
		[Clamp(0f, 100f)]
		[Tooltip("How frequent damage will be applied.")]
		private float m_DamagePeriod = 3f;

		// Token: 0x040029F4 RID: 10740
		private float m_LastDamageTime;
	}
}
