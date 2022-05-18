using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000878 RID: 2168
	public class GenericVitals : EntityBehaviour
	{
		// Token: 0x06003815 RID: 14357 RVA: 0x00028C25 File Offset: 0x00026E25
		private void Start()
		{
			base.Entity.ChangeHealth.SetTryer(new Attempt<HealthEventData>.GenericTryerDelegate(this.Try_ChangeHealth));
			this.m_MaxHealth *= 5f;
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x001A2360 File Offset: 0x001A0560
		protected virtual void Update()
		{
			if (this.m_HealthRegeneration.CanRegenerate && base.Entity.Health.Get() < 100f && base.Entity.Health.Get() > 0f)
			{
				HealthEventData arg = new HealthEventData(this.m_HealthRegeneration.RegenDelta, null, default(Vector3), default(Vector3), 0f);
				base.Entity.ChangeHealth.Try(arg);
			}
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x001A23E4 File Offset: 0x001A05E4
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

		// Token: 0x0400326B RID: 12907
		[Header("Health & Damage")]
		[SerializeField]
		[Tooltip("The health to start with.")]
		private float m_MaxHealth = 100f;

		// Token: 0x0400326C RID: 12908
		[SerializeField]
		private StatRegenData m_HealthRegeneration;

		// Token: 0x0400326D RID: 12909
		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("0 -> the damage received will not be decreased, \n1 -> the damage will be reduced to 0 (GOD mode).")]
		private float m_Resistance = 0.1f;

		// Token: 0x0400326E RID: 12910
		[Header("Audio")]
		[SerializeField]
		protected AudioSource m_AudioSource;

		// Token: 0x0400326F RID: 12911
		protected float m_HealthDelta;

		// Token: 0x04003270 RID: 12912
		private float m_NextRegenTime;
	}
}
