using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000869 RID: 2153
	public static class CameraShakePresets
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x000287AE File Offset: 0x000269AE
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

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x001A0F2C File Offset: 0x0019F12C
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

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x001A0F84 File Offset: 0x0019F184
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

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060037CB RID: 14283 RVA: 0x001A0FDC File Offset: 0x0019F1DC
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

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x000287E9 File Offset: 0x000269E9
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

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060037CD RID: 14285 RVA: 0x001A1038 File Offset: 0x0019F238
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

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x00028829 File Offset: 0x00026A29
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
