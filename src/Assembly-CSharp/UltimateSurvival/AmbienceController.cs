using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000882 RID: 2178
	public class AmbienceController : MonoBehaviour
	{
		// Token: 0x06003840 RID: 14400 RVA: 0x001A290C File Offset: 0x001A0B0C
		private void Update()
		{
			this.m_DayAudioSrc.volume = this.m_DayVolCurve.Evaluate(GameController.NormalizedTime) * this.m_DayVolFactor;
			this.m_NightAudioSrc.volume = this.m_NightVolCurve.Evaluate(GameController.NormalizedTime) * this.m_NightVolFactor;
		}

		// Token: 0x04003294 RID: 12948
		[Header("Setup")]
		[SerializeField]
		private AudioSource m_DayAudioSrc;

		// Token: 0x04003295 RID: 12949
		[SerializeField]
		private AudioSource m_NightAudioSrc;

		// Token: 0x04003296 RID: 12950
		[Header("Ambience Volume")]
		[SerializeField]
		private AnimationCurve m_DayVolCurve;

		// Token: 0x04003297 RID: 12951
		[SerializeField]
		private float m_DayVolFactor = 0.7f;

		// Token: 0x04003298 RID: 12952
		[SerializeField]
		private AnimationCurve m_NightVolCurve;

		// Token: 0x04003299 RID: 12953
		[SerializeField]
		private float m_NightVolFactor = 0.7f;
	}
}
