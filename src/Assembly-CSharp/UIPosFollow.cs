using System;
using UnityEngine;

// Token: 0x020004E3 RID: 1251
[RequireComponent(typeof(RectTransform))]
public class UIPosFollow : MonoBehaviour
{
	// Token: 0x0600209D RID: 8349 RVA: 0x0001AD25 File Offset: 0x00018F25
	private void Awake()
	{
		this.Me = (RectTransform)base.transform;
		this.Obj = base.transform.GetChild(0).gameObject;
	}

	// Token: 0x0600209E RID: 8350 RVA: 0x00113998 File Offset: 0x00111B98
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

	// Token: 0x04001C1B RID: 7195
	public RectTransform Target;

	// Token: 0x04001C1C RID: 7196
	public bool FollowActive;

	// Token: 0x04001C1D RID: 7197
	private RectTransform Me;

	// Token: 0x04001C1E RID: 7198
	private GameObject Obj;
}
