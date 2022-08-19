using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000624 RID: 1572
	[Serializable]
	public class LerpControlledBob
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x00162077 File Offset: 0x00160277
		// (set) Token: 0x060031FC RID: 12796 RVA: 0x0016207F File Offset: 0x0016027F
		public float Value { get; private set; }

		// Token: 0x060031FD RID: 12797 RVA: 0x00162088 File Offset: 0x00160288
		public IEnumerator DoBobCycle(float displacement)
		{
			float t = 0f;
			float bobDuration = this.m_MaxBobDuration * this.m_DurationCurve.Evaluate(displacement);
			float bobAmount = this.m_MaxBobAmount * this.m_DisplacementCurve.Evaluate(displacement);
			while (t < bobDuration)
			{
				this.Value = Mathf.Lerp(0f, bobAmount, t / bobDuration);
				t += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}
			t = 0f;
			while (t < bobDuration)
			{
				this.Value = Mathf.Lerp(bobAmount, 0f, t / bobDuration);
				t += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}
			this.Value = 0f;
			yield break;
		}

		// Token: 0x04002C4D RID: 11341
		[SerializeField]
		private float m_MaxBobDuration;

		// Token: 0x04002C4E RID: 11342
		[SerializeField]
		private float m_MaxBobAmount;

		// Token: 0x04002C4F RID: 11343
		[SerializeField]
		private AnimationCurve m_DurationCurve;

		// Token: 0x04002C50 RID: 11344
		[SerializeField]
		private AnimationCurve m_DisplacementCurve;
	}
}
