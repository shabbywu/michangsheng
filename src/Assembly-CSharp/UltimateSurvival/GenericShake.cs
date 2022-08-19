using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005B0 RID: 1456
	[Serializable]
	public class GenericShake
	{
		// Token: 0x06002F5E RID: 12126 RVA: 0x00156F8F File Offset: 0x0015518F
		public void Shake(float scale)
		{
			MonoSingleton<FPCameraController>.Instance.Shake(this.GetShakeInstance(scale));
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x00156FA4 File Offset: 0x001551A4
		private ShakeInstance GetShakeInstance(float scale)
		{
			ShakeInstance shakeInstance = new ShakeInstance(this.m_Magnitude, this.m_Roughness, this.m_FadeInTime, this.m_FadeOutTime);
			shakeInstance.PositionInfluence = this.m_PositionInfluence;
			shakeInstance.RotationInfluence = this.m_RotationInfluence;
			shakeInstance.ScaleMagnitude *= Mathf.Max(scale, this.m_MinMagnitudeScale);
			return shakeInstance;
		}

		// Token: 0x04002994 RID: 10644
		[Header("Shake")]
		[SerializeField]
		private float m_Magnitude = 15f;

		// Token: 0x04002995 RID: 10645
		[SerializeField]
		private float m_MinMagnitudeScale = 0.5f;

		// Token: 0x04002996 RID: 10646
		[SerializeField]
		private float m_Roughness = 3f;

		// Token: 0x04002997 RID: 10647
		[SerializeField]
		private Vector3 m_PositionInfluence = new Vector3(0.01f, 0.01f, 0.01f);

		// Token: 0x04002998 RID: 10648
		[SerializeField]
		private Vector3 m_RotationInfluence = new Vector3(0.8f, 0.5f, 0.5f);

		// Token: 0x04002999 RID: 10649
		[SerializeField]
		private float m_FadeInTime = 0.2f;

		// Token: 0x0400299A RID: 10650
		[SerializeField]
		private float m_FadeOutTime = 0.3f;
	}
}
