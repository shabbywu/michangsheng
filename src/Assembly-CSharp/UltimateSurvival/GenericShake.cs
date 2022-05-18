using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200086C RID: 2156
	[Serializable]
	public class GenericShake
	{
		// Token: 0x060037DC RID: 14300 RVA: 0x00028942 File Offset: 0x00026B42
		public void Shake(float scale)
		{
			MonoSingleton<FPCameraController>.Instance.Shake(this.GetShakeInstance(scale));
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x001A140C File Offset: 0x0019F60C
		private ShakeInstance GetShakeInstance(float scale)
		{
			ShakeInstance shakeInstance = new ShakeInstance(this.m_Magnitude, this.m_Roughness, this.m_FadeInTime, this.m_FadeOutTime);
			shakeInstance.PositionInfluence = this.m_PositionInfluence;
			shakeInstance.RotationInfluence = this.m_RotationInfluence;
			shakeInstance.ScaleMagnitude *= Mathf.Max(scale, this.m_MinMagnitudeScale);
			return shakeInstance;
		}

		// Token: 0x04003215 RID: 12821
		[Header("Shake")]
		[SerializeField]
		private float m_Magnitude = 15f;

		// Token: 0x04003216 RID: 12822
		[SerializeField]
		private float m_MinMagnitudeScale = 0.5f;

		// Token: 0x04003217 RID: 12823
		[SerializeField]
		private float m_Roughness = 3f;

		// Token: 0x04003218 RID: 12824
		[SerializeField]
		private Vector3 m_PositionInfluence = new Vector3(0.01f, 0.01f, 0.01f);

		// Token: 0x04003219 RID: 12825
		[SerializeField]
		private Vector3 m_RotationInfluence = new Vector3(0.8f, 0.5f, 0.5f);

		// Token: 0x0400321A RID: 12826
		[SerializeField]
		private float m_FadeInTime = 0.2f;

		// Token: 0x0400321B RID: 12827
		[SerializeField]
		private float m_FadeOutTime = 0.3f;
	}
}
