using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200090B RID: 2315
	[Serializable]
	public class LerpControlledBob
	{
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x0002ADFC File Offset: 0x00028FFC
		// (set) Token: 0x06003B30 RID: 15152 RVA: 0x0002AE04 File Offset: 0x00029004
		public float Value { get; private set; }

		// Token: 0x06003B31 RID: 15153 RVA: 0x0002AE0D File Offset: 0x0002900D
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

		// Token: 0x04003574 RID: 13684
		[SerializeField]
		private float m_MaxBobDuration;

		// Token: 0x04003575 RID: 13685
		[SerializeField]
		private float m_MaxBobAmount;

		// Token: 0x04003576 RID: 13686
		[SerializeField]
		private AnimationCurve m_DurationCurve;

		// Token: 0x04003577 RID: 13687
		[SerializeField]
		private AnimationCurve m_DisplacementCurve;
	}
}
