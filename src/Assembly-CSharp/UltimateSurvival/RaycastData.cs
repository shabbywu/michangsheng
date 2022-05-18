using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008D4 RID: 2260
	public class RaycastData
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x0002A406 File Offset: 0x00028606
		// (set) Token: 0x06003A28 RID: 14888 RVA: 0x0002A40E File Offset: 0x0002860E
		public bool ObjectIsInteractable { get; private set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x0002A417 File Offset: 0x00028617
		// (set) Token: 0x06003A2A RID: 14890 RVA: 0x0002A41F File Offset: 0x0002861F
		public GameObject GameObject { get; private set; }

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x0002A428 File Offset: 0x00028628
		// (set) Token: 0x06003A2C RID: 14892 RVA: 0x0002A430 File Offset: 0x00028630
		public InteractableObject InteractableObject { get; private set; }

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x0002A439 File Offset: 0x00028639
		// (set) Token: 0x06003A2E RID: 14894 RVA: 0x0002A441 File Offset: 0x00028641
		public RaycastHit HitInfo { get; private set; }

		// Token: 0x06003A2F RID: 14895 RVA: 0x000079B2 File Offset: 0x00005BB2
		public static implicit operator bool(RaycastData raycastData)
		{
			return raycastData != null;
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x001A78F0 File Offset: 0x001A5AF0
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
