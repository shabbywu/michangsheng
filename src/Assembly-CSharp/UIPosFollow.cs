using System;
using UnityEngine;

// Token: 0x02000366 RID: 870
[RequireComponent(typeof(RectTransform))]
public class UIPosFollow : MonoBehaviour
{
	// Token: 0x06001D33 RID: 7475 RVA: 0x000CF08C File Offset: 0x000CD28C
	private void Awake()
	{
		this.Me = (RectTransform)base.transform;
		this.Obj = base.transform.GetChild(0).gameObject;
	}

	// Token: 0x06001D34 RID: 7476 RVA: 0x000CF0B8 File Offset: 0x000CD2B8
	private void LateUpdate()
	{
		if (this.Target != null)
		{
			this.Me.anchoredPosition = this.Target.anchoredPosition;
			if (this.FollowActive && this.Obj.activeInHierarchy != this.Target.gameObject.activeInHierarchy)
			{
				this.Obj.SetActive(this.Target.gameObject.activeInHierarchy);
			}
		}
	}

	// Token: 0x040017C9 RID: 6089
	public RectTransform Target;

	// Token: 0x040017CA RID: 6090
	public bool FollowActive;

	// Token: 0x040017CB RID: 6091
	private RectTransform Me;

	// Token: 0x040017CC RID: 6092
	private GameObject Obj;
}
