using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008D8 RID: 2264
	[RequireComponent(typeof(Light))]
	public class Firelight : MonoBehaviour
	{
		// Token: 0x06003A3C RID: 14908 RVA: 0x0002A4B5 File Offset: 0x000286B5
		public void Toggle(bool toggle)
		{
			this.m_TargetState = toggle;
			base.StopAllCoroutines();
			base.StartCoroutine(this.C_Toggle(toggle));
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x0002A4D2 File Offset: 0x000286D2
		private void Awake()
		{
			this.m_Light = base.GetComponent<Light>();
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x0002A4E0 File Offset: 0x000286E0
		private void Update()
		{
			if (!this.m_IsInTransition && this.m_TargetState)
			{
				this.m_Light.intensity = this.m_LightIntensity + Mathf.PerlinNoise(Time.time, Time.time + 3f) * this.m_IntensityNoise;
			}
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x0002A520 File Offset: 0x00028720
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

		// Token: 0x0400344A RID: 13386
		[SerializeField]
		[Range(0.1f, 3f)]
		private float m_ToggleDuration = 0.5f;

		// Token: 0x0400344B RID: 13387
		[SerializeField]
		[Range(0.1f, 3f)]
		private float m_LightIntensity = 1f;

		// Token: 0x0400344C RID: 13388
		[SerializeField]
		[Range(0.5f, 3f)]
		private float m_LightRange = 2f;

		// Token: 0x0400344D RID: 13389
		[SerializeField]
		[Range(0f, 3f)]
		[Tooltip("How much the light intensity changes over time.")]
		private float m_IntensityNoise = 1f;

		// Token: 0x0400344E RID: 13390
		private Light m_Light;

		// Token: 0x0400344F RID: 13391
		private bool m_IsInTransition;

		// Token: 0x04003450 RID: 13392
		private bool m_TargetState;
	}
}
