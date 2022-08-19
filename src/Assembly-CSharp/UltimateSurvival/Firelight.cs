using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005FE RID: 1534
	[RequireComponent(typeof(Light))]
	public class Firelight : MonoBehaviour
	{
		// Token: 0x0600313F RID: 12607 RVA: 0x0015E6A5 File Offset: 0x0015C8A5
		public void Toggle(bool toggle)
		{
			this.m_TargetState = toggle;
			base.StopAllCoroutines();
			base.StartCoroutine(this.C_Toggle(toggle));
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x0015E6C2 File Offset: 0x0015C8C2
		private void Awake()
		{
			this.m_Light = base.GetComponent<Light>();
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x0015E6D0 File Offset: 0x0015C8D0
		private void Update()
		{
			if (!this.m_IsInTransition && this.m_TargetState)
			{
				this.m_Light.intensity = this.m_LightIntensity + Mathf.PerlinNoise(Time.time, Time.time + 3f) * this.m_IntensityNoise;
			}
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x0015E710 File Offset: 0x0015C910
		private IEnumerator C_Toggle(bool toggle)
		{
			this.m_IsInTransition = true;
			float endTime = Time.time + this.m_ToggleDuration;
			while (Time.time < endTime)
			{
				this.m_Light.intensity = Mathf.MoveTowards(this.m_Light.intensity, toggle ? this.m_LightIntensity : 0f, Time.deltaTime * this.m_LightIntensity / this.m_ToggleDuration);
				this.m_Light.range = Mathf.MoveTowards(this.m_Light.range, toggle ? this.m_LightRange : 0f, Time.deltaTime * this.m_LightIntensity / this.m_ToggleDuration);
				yield return null;
			}
			this.m_IsInTransition = false;
			yield break;
		}

		// Token: 0x04002B5D RID: 11101
		[SerializeField]
		[Range(0.1f, 3f)]
		private float m_ToggleDuration = 0.5f;

		// Token: 0x04002B5E RID: 11102
		[SerializeField]
		[Range(0.1f, 3f)]
		private float m_LightIntensity = 1f;

		// Token: 0x04002B5F RID: 11103
		[SerializeField]
		[Range(0.5f, 3f)]
		private float m_LightRange = 2f;

		// Token: 0x04002B60 RID: 11104
		[SerializeField]
		[Range(0f, 3f)]
		[Tooltip("How much the light intensity changes over time.")]
		private float m_IntensityNoise = 1f;

		// Token: 0x04002B61 RID: 11105
		private Light m_Light;

		// Token: 0x04002B62 RID: 11106
		private bool m_IsInTransition;

		// Token: 0x04002B63 RID: 11107
		private bool m_TargetState;
	}
}
