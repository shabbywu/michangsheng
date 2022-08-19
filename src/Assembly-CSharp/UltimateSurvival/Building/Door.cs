using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000664 RID: 1636
	public class Door : InteractableObject
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x0016C1E6 File Offset: 0x0016A3E6
		public bool Open
		{
			get
			{
				return this.m_Open;
			}
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x0016C1F0 File Offset: 0x0016A3F0
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

		// Token: 0x06003403 RID: 13315 RVA: 0x0016C279 File Offset: 0x0016A479
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

		// Token: 0x04002E3E RID: 11838
		[SerializeField]
		private Transform m_Model;

		// Token: 0x04002E3F RID: 11839
		[SerializeField]
		private Collider m_Collider;

		// Token: 0x04002E40 RID: 11840
		[Header("Functionality")]
		[SerializeField]
		private Vector3 m_ClosedRotation;

		// Token: 0x04002E41 RID: 11841
		[SerializeField]
		private Vector3 m_OpenRotation;

		// Token: 0x04002E42 RID: 11842
		[SerializeField]
		[Range(0.1f, 30f)]
		private float m_AnimationSpeed = 1f;

		// Token: 0x04002E43 RID: 11843
		[SerializeField]
		[Range(0.3f, 3f)]
		private float m_InteractionThreeshold = 1f;

		// Token: 0x04002E44 RID: 11844
		[Header("Audio")]
		[SerializeField]
		private SoundPlayer m_DoorOpen;

		// Token: 0x04002E45 RID: 11845
		[SerializeField]
		private SoundPlayer m_DoorClose;

		// Token: 0x04002E46 RID: 11846
		private bool m_Open;

		// Token: 0x04002E47 RID: 11847
		private float m_NextTimeCanInteract;
	}
}
