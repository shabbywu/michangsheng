using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005C0 RID: 1472
	public class PlayerVitals : EntityVitals
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x001584A4 File Offset: 0x001566A4
		public PlayerEventHandler Player
		{
			get
			{
				if (!this.m_Player)
				{
					this.m_Player = base.GetComponent<PlayerEventHandler>();
				}
				if (!this.m_Player)
				{
					this.m_Player = base.GetComponentInParent<PlayerEventHandler>();
				}
				return this.m_Player;
			}
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x001584E0 File Offset: 0x001566E0
		protected override void Update()
		{
			this.m_ThirstDepletion.Update(this.Player.Thirst, this.Player.ChangeHealth);
			this.m_HungerDepletion.Update(this.Player.Hunger, this.Player.ChangeHealth);
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x0015852F File Offset: 0x0015672F
		protected override bool Try_ChangeHealth(HealthEventData healthEventData)
		{
			healthEventData.Delta *= 1f - (float)this.Player.Defense.Get() / 100f;
			return base.Try_ChangeHealth(healthEventData);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x00158564 File Offset: 0x00156764
		private void Start()
		{
			this.Player.Run.AddStartTryer(delegate
			{
				this.m_StaminaRegeneration.Pause();
				return this.Player.Stamina.Get() > 0f;
			});
			this.Player.Jump.AddStartListener(delegate
			{
				this.ModifyStamina(-this.m_JumpStaminaTake);
			});
			if (this.m_SleepRestoresHealth)
			{
				this.Player.Sleep.AddStopListener(delegate
				{
					this.Player.ChangeHealth.Try(new HealthEventData(100f, null, default(Vector3), default(Vector3), 0f));
				});
			}
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x001585D0 File Offset: 0x001567D0
		private void ModifyStamina(float delta)
		{
			float num = this.Player.Stamina.Get() + delta;
			num = Mathf.Clamp(num, 0f, 100f);
			this.Player.Stamina.Set(num);
		}

		// Token: 0x040029F5 RID: 10741
		[Header("Stamina")]
		[SerializeField]
		[Clamp(0f, 300f)]
		private float m_StaminaDepletionRate = 30f;

		// Token: 0x040029F6 RID: 10742
		[SerializeField]
		private StatRegenData m_StaminaRegeneration;

		// Token: 0x040029F7 RID: 10743
		[SerializeField]
		private SoundPlayer m_BreathingHeavyAudio;

		// Token: 0x040029F8 RID: 10744
		[SerializeField]
		private float m_BreathingHeavyDuration = 11f;

		// Token: 0x040029F9 RID: 10745
		[SerializeField]
		[Clamp(0f, 100f)]
		private float m_JumpStaminaTake = 15f;

		// Token: 0x040029FA RID: 10746
		[Header("Thirst")]
		[SerializeField]
		private StatDepleter m_ThirstDepletion;

		// Token: 0x040029FB RID: 10747
		[Header("Hunger")]
		[SerializeField]
		private StatDepleter m_HungerDepletion;

		// Token: 0x040029FC RID: 10748
		[Header("Sleeping")]
		[SerializeField]
		private bool m_SleepRestoresHealth = true;

		// Token: 0x040029FD RID: 10749
		private PlayerEventHandler m_Player;

		// Token: 0x040029FE RID: 10750
		private float m_LastHeavyBreathTime;
	}
}
