using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000608 RID: 1544
	public class SleepingBag : InteractableObject
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x0015F277 File Offset: 0x0015D477
		public Vector3 SpawnPosOffset
		{
			get
			{
				return base.transform.position + base.transform.TransformVector(this.m_SpawnPosOffset);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x0015F29A File Offset: 0x0015D49A
		public Vector3 SleepPosition
		{
			get
			{
				return base.transform.position + base.transform.TransformVector(this.m_SleepPosOffset);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x0015F2BD File Offset: 0x0015D4BD
		public Quaternion SleepRotation
		{
			get
			{
				return base.transform.rotation * Quaternion.Euler(this.m_SleepRotOffset);
			}
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x0015F2DA File Offset: 0x0015D4DA
		public override void OnInteract(PlayerEventHandler player)
		{
			if (!player.Sleep.Active && MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Night && player.StartSleeping.Try(this))
			{
				player.Sleep.ForceStart();
			}
		}

		// Token: 0x04002B9C RID: 11164
		[SerializeField]
		[Tooltip("The player spawn position offset, relative to this object.")]
		private Vector3 m_SpawnPosOffset = new Vector3(0f, 0.3f, 0f);

		// Token: 0x04002B9D RID: 11165
		[SerializeField]
		[Tooltip("Player sleep position, relative to this object.")]
		private Vector3 m_SleepPosOffset;

		// Token: 0x04002B9E RID: 11166
		[SerializeField]
		[Tooltip("Player sleep rotation, relative to this object.")]
		private Vector3 m_SleepRotOffset;
	}
}
