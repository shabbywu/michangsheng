using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200087E RID: 2174
	public class PlayerVitals : EntityVitals
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x00028D48 File Offset: 0x00026F48
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

		// Token: 0x0600382F RID: 14383 RVA: 0x001A25E8 File Offset: 0x001A07E8
		protected override void Update()
		{
			this.m_ThirstDepletion.Update(this.Player.Thirst, this.Player.ChangeHealth);
			this.m_HungerDepletion.Update(this.Player.Hunger, this.Player.ChangeHealth);
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x00028D82 File Offset: 0x00026F82
		protected override bool Try_ChangeHealth(HealthEventData healthEventData)
		{
			healthEventData.Delta *= 1f - (float)this.Player.Defense.Get() / 100f;
			return base.Try_ChangeHealth(healthEventData);
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x001A2638 File Offset: 0x001A0838
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

		// Token: 0x06003832 RID: 14386 RVA: 0x001A26A4 File Offset: 0x001A08A4
		private void ModifyStamina(float delta)
		{
			float num = this.Player.Stamina.Get() + delta;
			num = Mathf.Clamp(num, 0f, 100f);
			this.Player.Stamina.Set(num);
		}

		// Token: 0x0400327F RID: 12927
		[Header("Stamina")]
		[SerializeField]
		[Clamp(0f, 300f)]
		private float m_StaminaDepletionRate = 30f;

		// Token: 0x04003280 RID: 12928
		[SerializeField]
		private StatRegenData m_StaminaRegeneration;

		// Token: 0x04003281 RID: 12929
		[SerializeField]
		private SoundPlayer m_BreathingHeavyAudio;

		// Token: 0x04003282 RID: 12930
		[SerializeField]
		private float m_BreathingHeavyDuration = 11f;

		// Token: 0x04003283 RID: 12931
		[SerializeField]
		[Clamp(0f, 100f)]
		private float m_JumpStaminaTake = 15f;

		// Token: 0x04003284 RID: 12932
		[Header("Thirst")]
		[SerializeField]
		private StatDepleter m_ThirstDepletion;

		// Token: 0x04003285 RID: 12933
		[Header("Hunger")]
		[SerializeField]
		private StatDepleter m_HungerDepletion;

		// Token: 0x04003286 RID: 12934
		[Header("Sleeping")]
		[SerializeField]
		private bool m_SleepRestoresHealth = true;

		// Token: 0x04003287 RID: 12935
		private PlayerEventHandler m_Player;

		// Token: 0x04003288 RID: 12936
		private float m_LastHeavyBreathTime;
	}
}
