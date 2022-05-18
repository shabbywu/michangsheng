using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008DC RID: 2268
	public class Lamp : InteractableObject
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06003A4C RID: 14924 RVA: 0x0002A5AC File Offset: 0x000287AC
		public bool State
		{
			get
			{
				return this.m_Light && this.m_Light.enabled;
			}
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x001A7D80 File Offset: 0x001A5F80
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

		// Token: 0x06003A4E RID: 14926 RVA: 0x0002A5C8 File Offset: 0x000287C8
		private void Start()
		{
			if (this.m_Mode == Lamp.Mode.Both || this.m_Mode == Lamp.Mode.TimeOfDay_Dependent)
			{
				MonoSingleton<TimeOfDay>.Instance.State.AddChangeListener(new Action(this.OnChanged_TimeOfDay_State));
			}
			this.OnChanged_TimeOfDay_State();
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x001A7DD8 File Offset: 0x001A5FD8
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

		// Token: 0x06003A50 RID: 14928 RVA: 0x0002A5FD File Offset: 0x000287FD
		private void OnDestroy()
		{
			if ((this.m_Mode == Lamp.Mode.Both || this.m_Mode == Lamp.Mode.TimeOfDay_Dependent) && MonoSingleton<TimeOfDay>.Instance != null)
			{
				MonoSingleton<TimeOfDay>.Instance.State.RemoveChangeListener(new Action(this.OnChanged_TimeOfDay_State));
			}
		}

		// Token: 0x0400345F RID: 13407
		[SerializeField]
		private Lamp.Mode m_Mode;

		// Token: 0x04003460 RID: 13408
		[SerializeField]
		private Light m_Light;

		// Token: 0x04003461 RID: 13409
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04003462 RID: 13410
		[SerializeField]
		private SoundPlayer m_ToggleAudio;

		// Token: 0x020008DD RID: 2269
		public enum Mode
		{
			// Token: 0x04003464 RID: 13412
			TimeOfDay_Dependent,
			// Token: 0x04003465 RID: 13413
			Manual = 5,
			// Token: 0x04003466 RID: 13414
			Both = 10
		}
	}
}
