using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;

// Token: 0x020004FB RID: 1275
public class LangHuaItem : MonoBehaviour
{
	// Token: 0x06002113 RID: 8467 RVA: 0x00115550 File Offset: 0x00113750
	public void Show()
	{
		this.r = base.GetComponent<MeshRenderer>();
		this.order = this.r.sortingOrder;
		this.r.sortingOrder = -1000;
		base.gameObject.SetActive(true);
		base.StartCoroutine("ChangeOrder");
	}

	// Token: 0x06002114 RID: 8468 RVA: 0x0001B419 File Offset: 0x00019619
	private IEnumerator ChangeOrder()
	{
		yield return new WaitForEndOfFrame();
		this.r.sortingOrder = this.order;
		yield break;
	}

	// Token: 0x04001C84 RID: 7300
	public SkeletonAnimation SpineAnim;

	// Token: 0x04001C85 RID: 7301
	private MeshRenderer r;

	// Token: 0x04001C86 RID: 7302
	private int order;
}
