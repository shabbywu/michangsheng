using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
	// Token: 0x06000469 RID: 1129 RVA: 0x0006E610 File Offset: 0x0006C810
	public GameObject Attach(GameObject prefab)
	{
		if (this.mPrefab != prefab)
		{
			this.mPrefab = prefab;
			if (this.mChild != null)
			{
				Object.Destroy(this.mChild);
			}
			if (this.mPrefab != null)
			{
				Transform transform = base.transform;
				this.mChild = Object.Instantiate<GameObject>(this.mPrefab, transform.position, transform.rotation);
				Transform transform2 = this.mChild.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return this.mChild;
	}

	// Token: 0x04000293 RID: 659
	public InvBaseItem.Slot slot;

	// Token: 0x04000294 RID: 660
	private GameObject mPrefab;

	// Token: 0x04000295 RID: 661
	private GameObject mChild;
}
