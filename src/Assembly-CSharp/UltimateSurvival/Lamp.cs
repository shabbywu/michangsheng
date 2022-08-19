using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000600 RID: 1536
	public class Lamp : InteractableObject
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x0015E871 File Offset: 0x0015CA71
		public bool State
		{
			get
			{
				return this.m_Light && this.m_Light.enabled;
			}
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x0015E890 File Offset: 0x0015CA90
		public override void OnInteract(PlayerEventHandler player)
		{
			if (this.m_Light != null)
			{
				this.m_Light.enabled = !this.m_Light.enabled;
				this.m_ToggleAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, this.m_AudioSource, 1f);
				return;
			}
			Debug.LogError("No Light component assigned to this Lamp!", this);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x0015E8E7 File Offset: 0x0015CAE7
		private void Start()
		{
			if (this.m_Mode == Lamp.Mode.Both || this.m_Mode == Lamp.Mode.TimeOfDay_Dependent)
			{
				MonoSingleton<TimeOfDay>.Instance.State.AddChangeListener(new Action(this.OnChanged_TimeOfDay_State));
			}
			this.OnChanged_TimeOfDay_State();
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x0015E91C File Offset: 0x0015CB1C
		private void OnChanged_TimeOfDay_State()
		{
			if (this.m_Light != null)
			{
				this.m_Light.enabled = MonoSingleton<TimeOfDay>.Instance.State.Is(ET.TimeOfDay.Night);
				this.m_ToggleAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, this.m_AudioSource, 1f);
				return;
			}
			Debug.LogError("No Light component assigned to this Lamp!", this);
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x0015E975 File Offset: 0x0015CB75
		private void OnDestroy()
		{
			if ((this.m_Mode == Lamp.Mode.Both || this.m_Mode == Lamp.Mode.TimeOfDay_Dependent) && MonoSingleton<TimeOfDay>.Instance != null)
			{
				MonoSingleton<TimeOfDay>.Instance.State.RemoveChangeListener(new Action(this.OnChanged_TimeOfDay_State));
			}
		}

		// Token: 0x04002B6A RID: 11114
		[SerializeField]
		private Lamp.Mode m_Mode;

		// Token: 0x04002B6B RID: 11115
		[SerializeField]
		private Light m_Light;

		// Token: 0x04002B6C RID: 11116
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002B6D RID: 11117
		[SerializeField]
		private SoundPlayer m_ToggleAudio;

		// Token: 0x020014C6 RID: 5318
		public enum Mode
		{
			// Token: 0x04006D38 RID: 27960
			TimeOfDay_Dependent,
			// Token: 0x04006D39 RID: 27961
			Manual = 5,
			// Token: 0x04006D3A RID: 27962
			Both = 10
		}
	}
}
