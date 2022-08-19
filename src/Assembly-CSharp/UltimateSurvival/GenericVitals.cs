using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005BA RID: 1466
	public class GenericVitals : EntityBehaviour
	{
		// Token: 0x06002F91 RID: 12177 RVA: 0x001580F8 File Offset: 0x001562F8
		private void Start()
		{
			base.Entity.ChangeHealth.SetTryer(new Attempt<HealthEventData>.GenericTryerDelegate(this.Try_ChangeHealth));
			this.m_MaxHealth *= 5f;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0015812C File Offset: 0x0015632C
		protected virtual void Update()
		{
			if (this.m_HealthRegeneration.CanRegenerate && base.Entity.Health.Get() < 100f && base.Entity.Health.Get() > 0f)
			{
				HealthEventData arg = new HealthEventData(this.m_HealthRegeneration.RegenDelta, null, default(Vector3), default(Vector3), 0f);
				base.Entity.ChangeHealth.Try(arg);
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x001581B0 File Offset: 0x001563B0
		protected virtual bool Try_ChangeHealth(HealthEventData healthEventData)
		{
			if (base.Entity.Health.Get() == 0f)
			{
				return false;
			}
			if (healthEventData.Delta > 0f && base.Entity.Health.Get() == 100f)
			{
				return false;
			}
			float num = healthEventData.Delta;
			if (num < 0f)
			{
				num *= 1f - this.m_Resistance;
			}
			float value = Mathf.Clamp(base.Entity.Health.Get() + num, 0f, 100f);
			base.Entity.Health.Set(value);
			if (num < 0f)
			{
				this.m_HealthRegeneration.Pause();
			}
			return true;
		}

		// Token: 0x040029E1 RID: 10721
		[Header("Health & Damage")]
		[SerializeField]
		[Tooltip("The health to start with.")]
		private float m_MaxHealth = 100f;

		// Token: 0x040029E2 RID: 10722
		[SerializeField]
		private StatRegenData m_HealthRegeneration;

		// Token: 0x040029E3 RID: 10723
		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("0 -> the damage received will not be decreased, \n1 -> the damage will be reduced to 0 (GOD mode).")]
		private float m_Resistance = 0.1f;

		// Token: 0x040029E4 RID: 10724
		[Header("Audio")]
		[SerializeField]
		protected AudioSource m_AudioSource;

		// Token: 0x040029E5 RID: 10725
		protected float m_HealthDelta;

		// Token: 0x040029E6 RID: 10726
		private float m_NextRegenTime;
	}
}
