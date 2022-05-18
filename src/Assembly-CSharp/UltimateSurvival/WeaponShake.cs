using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200086B RID: 2155
	[Serializable]
	public class WeaponShake
	{
		// Token: 0x060037D9 RID: 14297 RVA: 0x000288F1 File Offset: 0x00026AF1
		public void Shake()
		{
			MonoSingleton<FPCameraController>.Instance.Shake(this.GetShakeInstance());
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x00028904 File Offset: 0x00026B04
		private ShakeInstance GetShakeInstance()
		{
			return new ShakeInstance(this.m_Magnitude, this.m_Roughness, this.m_FadeInTime, this.m_FadeOutTime)
			{
				PositionInfluence = this.m_PositionInfluence,
				RotationInfluence = this.m_RotationInfluence,
				IsWeapon = true
			};
		}

		// Token: 0x0400320F RID: 12815
		[Header("Shake")]
		[SerializeField]
		private float m_Magnitude = 15f;

		// Token: 0x04003210 RID: 12816
		[SerializeField]
		private float m_Roughness = 3f;

		// Token: 0x04003211 RID: 12817
		[SerializeField]
		private Vector3 m_PositionInfluence = new Vector3(0.01f, 0.01f, 0.01f);

		// Token: 0x04003212 RID: 12818
		[SerializeField]
		private Vector3 m_RotationInfluence = new Vector3(0.8f, 0.5f, 0.5f);

		// Token: 0x04003213 RID: 12819
		[SerializeField]
		private float m_FadeInTime = 0.2f;

		// Token: 0x04003214 RID: 12820
		[SerializeField]
		private float m_FadeOutTime = 0.3f;
	}
}
