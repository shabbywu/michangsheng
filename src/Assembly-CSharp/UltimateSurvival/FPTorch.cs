using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008CB RID: 2251
	public class FPTorch : FPObject
	{
		// Token: 0x060039E4 RID: 14820 RVA: 0x0002A136 File Offset: 0x00028336
		public override void On_Draw(SavableItem correspondingItem)
		{
			base.On_Draw(correspondingItem);
			base.StopAllCoroutines();
			base.StartCoroutine(this.C_ToggleTorch(true));
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x0002A153 File Offset: 0x00028353
		public override void On_Holster()
		{
			base.On_Holster();
			if (base.gameObject.activeSelf)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.C_ToggleTorch(false));
			}
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x0002A17C File Offset: 0x0002837C
		private void Start()
		{
			this.m_Light.intensity = this.m_LightIntensity;
			this.m_Light.range = this.m_LightRange;
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x0002A1A0 File Offset: 0x000283A0
		private void Update()
		{
			if (!this.m_IsInTransition)
			{
				this.m_Light.intensity = this.m_LightIntensity + Mathf.PerlinNoise(Time.time, Time.time + 3f) * this.m_IntensityNoise;
			}
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x0002A1D8 File Offset: 0x000283D8
		private IEnumerator C_ToggleTorch(bool toggle)
		{
			this.m_IsInTransition = true;
			if (toggle)
			{
				this.m_MainSystem.Play(true);
				GameController.Audio.LerpVolumeOverTime(this.m_AudioSource, this.m_Volume, 1f / this.m_LightDrawDuration);
			}
			else
			{
				this.m_MainSystem.Stop(true);
				GameController.Audio.LerpVolumeOverTime(this.m_AudioSource, 0f, 1f / this.m_LightDrawDuration);
			}
			float endTime = Time.time + this.m_LightDrawDuration;
			while (Time.time < endTime)
			{
				this.m_Light.intensity = Mathf.MoveTowards(this.m_Light.intensity, toggle ? this.m_LightIntensity : 0f, Time.deltaTime * this.m_LightIntensity / this.m_LightDrawDuration);
				this.m_Light.range = Mathf.MoveTowards(this.m_Light.range, toggle ? this.m_LightRange : 0f, Time.deltaTime * this.m_LightIntensity / this.m_LightDrawDuration);
				yield return null;
			}
			this.m_IsInTransition = false;
			yield break;
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x001A7128 File Offset: 0x001A5328
		private void OnValidate()
		{
			if (this.m_Light)
			{
				this.m_Light.intensity = this.m_LightIntensity;
				this.m_Light.range = this.m_LightRange;
			}
			if (this.m_AudioSource)
			{
				this.m_AudioSource.volume = this.m_Volume;
			}
		}

		// Token: 0x04003415 RID: 13333
		[Header("Torch Settings")]
		[SerializeField]
		[Tooltip("The torch light.")]
		private Light m_Light;

		// Token: 0x04003416 RID: 13334
		[SerializeField]
		[Tooltip("The parent of all the torch particle systems.")]
		private ParticleSystem m_MainSystem;

		// Token: 0x04003417 RID: 13335
		[SerializeField]
		[Range(0f, 10f)]
		[Tooltip("The light intensity of the torch.")]
		private float m_LightIntensity = 2f;

		// Token: 0x04003418 RID: 13336
		[SerializeField]
		[Range(0f, 20f)]
		[Tooltip("The light range of the torch.")]
		private float m_LightRange = 10f;

		// Token: 0x04003419 RID: 13337
		[SerializeField]
		[Range(0.1f, 1f)]
		[Tooltip("Aproximately how much time it takes to draw / equip the torch.")]
		private float m_LightDrawDuration = 0.5f;

		// Token: 0x0400341A RID: 13338
		[SerializeField]
		[Clamp(0f, float.PositiveInfinity)]
		[Tooltip("How much the light intensity changes over time.")]
		private float m_IntensityNoise = 1f;

		// Token: 0x0400341B RID: 13339
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x0400341C RID: 13340
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Volume;

		// Token: 0x0400341D RID: 13341
		private bool m_IsInTransition;
	}
}
