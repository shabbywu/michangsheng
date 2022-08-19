using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F5 RID: 1525
	public class FPTorch : FPObject
	{
		// Token: 0x06003100 RID: 12544 RVA: 0x0015DBEC File Offset: 0x0015BDEC
		public override void On_Draw(SavableItem correspondingItem)
		{
			base.On_Draw(correspondingItem);
			base.StopAllCoroutines();
			base.StartCoroutine(this.C_ToggleTorch(true));
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x0015DC09 File Offset: 0x0015BE09
		public override void On_Holster()
		{
			base.On_Holster();
			if (base.gameObject.activeSelf)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.C_ToggleTorch(false));
			}
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x0015DC32 File Offset: 0x0015BE32
		private void Start()
		{
			this.m_Light.intensity = this.m_LightIntensity;
			this.m_Light.range = this.m_LightRange;
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x0015DC56 File Offset: 0x0015BE56
		private void Update()
		{
			if (!this.m_IsInTransition)
			{
				this.m_Light.intensity = this.m_LightIntensity + Mathf.PerlinNoise(Time.time, Time.time + 3f) * this.m_IntensityNoise;
			}
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x0015DC8E File Offset: 0x0015BE8E
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

		// Token: 0x06003105 RID: 12549 RVA: 0x0015DCA4 File Offset: 0x0015BEA4
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

		// Token: 0x04002B37 RID: 11063
		[Header("Torch Settings")]
		[SerializeField]
		[Tooltip("The torch light.")]
		private Light m_Light;

		// Token: 0x04002B38 RID: 11064
		[SerializeField]
		[Tooltip("The parent of all the torch particle systems.")]
		private ParticleSystem m_MainSystem;

		// Token: 0x04002B39 RID: 11065
		[SerializeField]
		[Range(0f, 10f)]
		[Tooltip("The light intensity of the torch.")]
		private float m_LightIntensity = 2f;

		// Token: 0x04002B3A RID: 11066
		[SerializeField]
		[Range(0f, 20f)]
		[Tooltip("The light range of the torch.")]
		private float m_LightRange = 10f;

		// Token: 0x04002B3B RID: 11067
		[SerializeField]
		[Range(0.1f, 1f)]
		[Tooltip("Aproximately how much time it takes to draw / equip the torch.")]
		private float m_LightDrawDuration = 0.5f;

		// Token: 0x04002B3C RID: 11068
		[SerializeField]
		[Clamp(0f, float.PositiveInfinity)]
		[Tooltip("How much the light intensity changes over time.")]
		private float m_IntensityNoise = 1f;

		// Token: 0x04002B3D RID: 11069
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002B3E RID: 11070
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Volume;

		// Token: 0x04002B3F RID: 11071
		private bool m_IsInTransition;
	}
}
