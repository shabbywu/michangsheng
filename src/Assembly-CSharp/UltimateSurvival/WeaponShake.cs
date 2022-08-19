using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005AF RID: 1455
	[Serializable]
	public class WeaponShake
	{
		// Token: 0x06002F5B RID: 12123 RVA: 0x00156EC9 File Offset: 0x001550C9
		public void Shake()
		{
			MonoSingleton<FPCameraController>.Instance.Shake(this.GetShakeInstance());
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x00156EDC File Offset: 0x001550DC
		private ShakeInstance GetShakeInstance()
		{
			return new ShakeInstance(this.m_Magnitude, this.m_Roughness, this.m_FadeInTime, this.m_FadeOutTime)
			{
				PositionInfluence = this.m_PositionInfluence,
				RotationInfluence = this.m_RotationInfluence,
				IsWeapon = true
			};
		}

		// Token: 0x0400298E RID: 10638
		[Header("Shake")]
		[SerializeField]
		private float m_Magnitude = 15f;

		// Token: 0x0400298F RID: 10639
		[SerializeField]
		private float m_Roughness = 3f;

		// Token: 0x04002990 RID: 10640
		[SerializeField]
		private Vector3 m_PositionInfluence = new Vector3(0.01f, 0.01f, 0.01f);

		// Token: 0x04002991 RID: 10641
		[SerializeField]
		private Vector3 m_RotationInfluence = new Vector3(0.8f, 0.5f, 0.5f);

		// Token: 0x04002992 RID: 10642
		[SerializeField]
		private float m_FadeInTime = 0.2f;

		// Token: 0x04002993 RID: 10643
		[SerializeField]
		private float m_FadeOutTime = 0.3f;
	}
}
