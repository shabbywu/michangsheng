using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000969 RID: 2409
	public class Door : InteractableObject
	{
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x0002C5CB File Offset: 0x0002A7CB
		public bool Open
		{
			get
			{
				return this.m_Open;
			}
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x001B4EB4 File Offset: 0x001B30B4
		public override void OnInteract(PlayerEventHandler player)
		{
			if (Time.time > this.m_NextTimeCanInteract)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.C_DoAnimation(!this.m_Open));
				this.m_NextTimeCanInteract = Time.time + this.m_InteractionThreeshold;
				if (this.m_Open)
				{
					this.m_DoorOpen.PlayAtPosition(ItemSelectionMethod.RandomlyButExcludeLast, base.transform.position, 1f);
					return;
				}
				this.m_DoorClose.PlayAtPosition(ItemSelectionMethod.RandomlyButExcludeLast, base.transform.position, 1f);
			}
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x0002C5D3 File Offset: 0x0002A7D3
		private IEnumerator C_DoAnimation(bool open)
		{
			this.m_Collider.enabled = false;
			this.m_Open = open;
			Quaternion targetRotation = Quaternion.Euler(open ? this.m_OpenRotation : this.m_ClosedRotation);
			while (Quaternion.Angle(targetRotation, this.m_Model.transform.localRotation) > 0.5f)
			{
				this.m_Model.transform.localRotation = Quaternion.Lerp(this.m_Model.transform.localRotation, targetRotation, Time.deltaTime * this.m_AnimationSpeed);
				yield return null;
			}
			this.m_Collider.enabled = true;
			yield break;
		}

		// Token: 0x040037C7 RID: 14279
		[SerializeField]
		private Transform m_Model;

		// Token: 0x040037C8 RID: 14280
		[SerializeField]
		private Collider m_Collider;

		// Token: 0x040037C9 RID: 14281
		[Header("Functionality")]
		[SerializeField]
		private Vector3 m_ClosedRotation;

		// Token: 0x040037CA RID: 14282
		[SerializeField]
		private Vector3 m_OpenRotation;

		// Token: 0x040037CB RID: 14283
		[SerializeField]
		[Range(0.1f, 30f)]
		private float m_AnimationSpeed = 1f;

		// Token: 0x040037CC RID: 14284
		[SerializeField]
		[Range(0.3f, 3f)]
		private float m_InteractionThreeshold = 1f;

		// Token: 0x040037CD RID: 14285
		[Header("Audio")]
		[SerializeField]
		private SoundPlayer m_DoorOpen;

		// Token: 0x040037CE RID: 14286
		[SerializeField]
		private SoundPlayer m_DoorClose;

		// Token: 0x040037CF RID: 14287
		private bool m_Open;

		// Token: 0x040037D0 RID: 14288
		private float m_NextTimeCanInteract;
	}
}
