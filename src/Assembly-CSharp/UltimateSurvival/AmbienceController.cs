using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005C2 RID: 1474
	public class AmbienceController : MonoBehaviour
	{
		// Token: 0x06002FB7 RID: 12215 RVA: 0x001588CC File Offset: 0x00156ACC
		private void Update()
		{
			this.m_DayAudioSrc.volume = this.m_DayVolCurve.Evaluate(GameController.NormalizedTime) * this.m_DayVolFactor;
			this.m_NightAudioSrc.volume = this.m_NightVolCurve.Evaluate(GameController.NormalizedTime) * this.m_NightVolFactor;
		}

		// Token: 0x04002A06 RID: 10758
		[Header("Setup")]
		[SerializeField]
		private AudioSource m_DayAudioSrc;

		// Token: 0x04002A07 RID: 10759
		[SerializeField]
		private AudioSource m_NightAudioSrc;

		// Token: 0x04002A08 RID: 10760
		[Header("Ambience Volume")]
		[SerializeField]
		private AnimationCurve m_DayVolCurve;

		// Token: 0x04002A09 RID: 10761
		[SerializeField]
		private float m_DayVolFactor = 0.7f;

		// Token: 0x04002A0A RID: 10762
		[SerializeField]
		private AnimationCurve m_NightVolCurve;

		// Token: 0x04002A0B RID: 10763
		[SerializeField]
		private float m_NightVolFactor = 0.7f;
	}
}
