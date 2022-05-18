using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000876 RID: 2166
	public class EntityVitals : GenericVitals
	{
		// Token: 0x0600380B RID: 14347 RVA: 0x001A21E0 File Offset: 0x001A03E0
		private void Awake()
		{
			base.Entity.ChangeHealth.SetTryer(new Attempt<HealthEventData>.GenericTryerDelegate(this.Try_ChangeHealth));
			base.Entity.Land.AddListener(new Action<float>(this.On_Landed));
			base.Entity.Health.AddChangeListener(new Action(this.OnChanged_Health));
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x001A2244 File Offset: 0x001A0444
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

		// Token: 0x0600380D RID: 14349 RVA: 0x001A22F4 File Offset: 0x001A04F4
		private void On_Landed(float landSpeed)
		{
			if (landSpeed >= this.m_MinFallSpeed)
			{
				base.Entity.ChangeHealth.Try(new HealthEventData(-100f * (landSpeed / this.m_MaxFallSpeed), null, default(Vector3), default(Vector3), 0f));
				this.m_FallDamageAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 1f);
			}
		}

		// Token: 0x0400325F RID: 12895
		[Header("Fall Damage")]
		[SerializeField]
		[Range(1f, 15f)]
		[Tooltip("At which landing speed, the entity will start taking damage.")]
		private float m_MinFallSpeed = 4f;

		// Token: 0x04003260 RID: 12896
		[SerializeField]
		[Range(10f, 50f)]
		[Tooltip("At which landing speed, the entity will die, if it has no defense.")]
		private float m_MaxFallSpeed = 15f;

		// Token: 0x04003261 RID: 12897
		[Header("Audio")]
		[SerializeField]
		[Tooltip("The sounds that will be played when this entity receives damage.")]
		private SoundPlayer m_HurtAudio;

		// Token: 0x04003262 RID: 12898
		[SerializeField]
		private float m_TimeBetweenScreams = 1f;

		// Token: 0x04003263 RID: 12899
		[SerializeField]
		private SoundPlayer m_FallDamageAudio;

		// Token: 0x04003264 RID: 12900
		[Header("Animation")]
		[SerializeField]
		private Animator m_Animator;

		// Token: 0x04003265 RID: 12901
		[SerializeField]
		private float m_GetHitMax = 30f;

		// Token: 0x04003266 RID: 12902
		private float m_NextTimeCanScream;
	}
}
