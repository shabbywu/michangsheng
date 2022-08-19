using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005FA RID: 1530
	public class RaycastData
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x0015E358 File Offset: 0x0015C558
		// (set) Token: 0x0600312B RID: 12587 RVA: 0x0015E360 File Offset: 0x0015C560
		public bool ObjectIsInteractable { get; private set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x0015E369 File Offset: 0x0015C569
		// (set) Token: 0x0600312D RID: 12589 RVA: 0x0015E371 File Offset: 0x0015C571
		public GameObject GameObject { get; private set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x0015E37A File Offset: 0x0015C57A
		// (set) Token: 0x0600312F RID: 12591 RVA: 0x0015E382 File Offset: 0x0015C582
		public InteractableObject InteractableObject { get; private set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x0015E38B File Offset: 0x0015C58B
		// (set) Token: 0x06003131 RID: 12593 RVA: 0x0015E393 File Offset: 0x0015C593
		public RaycastHit HitInfo { get; private set; }

		// Token: 0x06003132 RID: 12594 RVA: 0x00014667 File Offset: 0x00012867
		public static implicit operator bool(RaycastData raycastData)
		{
			return raycastData != null;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x0015E39C File Offset: 0x0015C59C
		public RaycastData(RaycastHit hitInfo)
		{
			this.GameObject = hitInfo.collider.gameObject;
			this.InteractableObject = hitInfo.collider.GetComponent<InteractableObject>();
			if (this.InteractableObject && !this.InteractableObject.enabled)
			{
				this.InteractableObject = null;
			}
			this.ObjectIsInteractable = (this.InteractableObject != null);
			this.HitInfo = hitInfo;
		}
	}
}
