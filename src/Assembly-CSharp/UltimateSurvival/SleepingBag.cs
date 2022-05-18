using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008E7 RID: 2279
	public class SleepingBag : InteractableObject
	{
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06003A7D RID: 14973 RVA: 0x0002A7E9 File Offset: 0x000289E9
		public Vector3 SpawnPosOffset
		{
			get
			{
				return base.transform.position + base.transform.TransformVector(this.m_SpawnPosOffset);
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x0002A80C File Offset: 0x00028A0C
		public Vector3 SleepPosition
		{
			get
			{
				return base.transform.position + base.transform.TransformVector(this.m_SleepPosOffset);
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x0002A82F File Offset: 0x00028A2F
		public Quaternion SleepRotation
		{
			get
			{
				return base.transform.rotation * Quaternion.Euler(this.m_SleepRotOffset);
			}
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x0002A84C File Offset: 0x00028A4C
		public override void OnInteract(PlayerEventHandler player)
		{
			if (!player.Sleep.Active && MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Night && player.StartSleeping.Try(this))
			{
				player.Sleep.ForceStart();
			}
		}

		// Token: 0x040034A1 RID: 13473
		[SerializeField]
		[Tooltip("The player spawn position offset, relative to this object.")]
		private Vector3 m_SpawnPosOffset = new Vector3(0f, 0.3f, 0f);

		// Token: 0x040034A2 RID: 13474
		[SerializeField]
		[Tooltip("Player sleep position, relative to this object.")]
		private Vector3 m_SleepPosOffset;

		// Token: 0x040034A3 RID: 13475
		[SerializeField]
		[Tooltip("Player sleep rotation, relative to this object.")]
		private Vector3 m_SleepRotOffset;
	}
}
