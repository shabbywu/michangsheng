using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005AD RID: 1453
	public static class CameraShakePresets
	{
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x0015691B File Offset: 0x00154B1B
		public static ShakeInstance Bump
		{
			get
			{
				return new ShakeInstance(2.5f, 4f, 0.1f, 0.75f)
				{
					PositionInfluence = Vector3.one * 0.15f,
					RotationInfluence = Vector3.one
				};
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x00156958 File Offset: 0x00154B58
		public static ShakeInstance Explosion
		{
			get
			{
				return new ShakeInstance(5f, 10f, 0f, 1.5f)
				{
					PositionInfluence = Vector3.one * 0.25f,
					RotationInfluence = new Vector3(4f, 1f, 1f)
				};
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x001569B0 File Offset: 0x00154BB0
		public static ShakeInstance Earthquake
		{
			get
			{
				return new ShakeInstance(0.6f, 3.5f, 2f, 10f)
				{
					PositionInfluence = Vector3.one * 0.25f,
					RotationInfluence = new Vector3(1f, 1f, 4f)
				};
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x00156A08 File Offset: 0x00154C08
		public static ShakeInstance BadTrip
		{
			get
			{
				return new ShakeInstance(10f, 0.15f, 5f, 10f)
				{
					PositionInfluence = new Vector3(0f, 0f, 0.15f),
					RotationInfluence = new Vector3(2f, 1f, 4f)
				};
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x00156A62 File Offset: 0x00154C62
		public static ShakeInstance HandheldCamera
		{
			get
			{
				return new ShakeInstance(1f, 0.25f, 5f, 10f)
				{
					PositionInfluence = Vector3.zero,
					RotationInfluence = new Vector3(1f, 0.5f, 0.5f)
				};
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x00156AA4 File Offset: 0x00154CA4
		public static ShakeInstance Vibration
		{
			get
			{
				return new ShakeInstance(0.4f, 20f, 2f, 2f)
				{
					PositionInfluence = new Vector3(0f, 0.15f, 0f),
					RotationInfluence = new Vector3(1.25f, 0f, 4f)
				};
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x00156AFE File Offset: 0x00154CFE
		public static ShakeInstance RoughDriving
		{
			get
			{
				return new ShakeInstance(1f, 2f, 1f, 1f)
				{
					PositionInfluence = Vector3.zero,
					RotationInfluence = Vector3.one
				};
			}
		}
	}
}
