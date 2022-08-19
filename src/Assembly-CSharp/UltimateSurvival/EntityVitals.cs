using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005B8 RID: 1464
	public class EntityVitals : GenericVitals
	{
		// Token: 0x06002F87 RID: 12167 RVA: 0x00157ED4 File Offset: 0x001560D4
		private void Awake()
		{
			base.Entity.ChangeHealth.SetTryer(new Attempt<HealthEventData>.GenericTryerDelegate(this.Try_ChangeHealth));
			base.Entity.Land.AddListener(new Action<float>(this.On_Landed));
			base.Entity.Health.AddChangeListener(new Action(this.OnChanged_Health));
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x00157F38 File Offset: 0x00156138
		private void OnChanged_Health()
		{
			float num = base.Entity.Health.Get() - base.Entity.Health.GetLastValue();
			if (num < 0f)
			{
				if (this.m_Animator != null)
				{
					this.m_Animator.SetFloat("Get Hit Amount", Mathf.Abs(num / this.m_GetHitMax));
					this.m_Animator.SetTrigger("Get Hit");
				}
				if (num < 0f && Time.time > this.m_NextTimeCanScream)
				{
					this.m_HurtAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, this.m_AudioSource, 1f);
					this.m_NextTimeCanScream = Time.time + this.m_TimeBetweenScreams;
				}
			}
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x00157FE8 File Offset: 0x001561E8
		private void On_Landed(float landSpeed)
		{
			if (landSpeed >= this.m_MinFallSpeed)
			{
				base.Entity.ChangeHealth.Try(new HealthEventData(-100f * (landSpeed / this.m_MaxFallSpeed), null, default(Vector3), default(Vector3), 0f));
				this.m_FallDamageAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 1f);
			}
		}

		// Token: 0x040029D5 RID: 10709
		[Header("Fall Damage")]
		[SerializeField]
		[Range(1f, 15f)]
		[Tooltip("At which landing speed, the entity will start taking damage.")]
		private float m_MinFallSpeed = 4f;

		// Token: 0x040029D6 RID: 10710
		[SerializeField]
		[Range(10f, 50f)]
		[Tooltip("At which landing speed, the entity will die, if it has no defense.")]
		private float m_MaxFallSpeed = 15f;

		// Token: 0x040029D7 RID: 10711
		[Header("Audio")]
		[SerializeField]
		[Tooltip("The sounds that will be played when this entity receives damage.")]
		private SoundPlayer m_HurtAudio;

		// Token: 0x040029D8 RID: 10712
		[SerializeField]
		private float m_TimeBetweenScreams = 1f;

		// Token: 0x040029D9 RID: 10713
		[SerializeField]
		private SoundPlayer m_FallDamageAudio;

		// Token: 0x040029DA RID: 10714
		[Header("Animation")]
		[SerializeField]
		private Animator m_Animator;

		// Token: 0x040029DB RID: 10715
		[SerializeField]
		private float m_GetHitMax = 30f;

		// Token: 0x040029DC RID: 10716
		private float m_NextTimeCanScream;
	}
}
