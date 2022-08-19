using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;

// Token: 0x0200037B RID: 891
public class LangHuaItem : MonoBehaviour
{
	// Token: 0x06001DA4 RID: 7588 RVA: 0x000D1594 File Offset: 0x000CF794
	public void Show()
	{
		this.r = base.GetComponent<MeshRenderer>();
		this.order = this.r.sortingOrder;
		this.r.sortingOrder = -1000;
		base.gameObject.SetActive(true);
		base.StartCoroutine("ChangeOrder");
	}

	// Token: 0x06001DA5 RID: 7589 RVA: 0x000D15E6 File Offset: 0x000CF7E6
	private IEnumerator ChangeOrder()
	{
		yield return new WaitForEndOfFrame();
		this.r.sortingOrder = this.order;
		yield break;
	}

	// Token: 0x0400182F RID: 6191
	public SkeletonAnimation SpineAnim;

	// Token: 0x04001830 RID: 6192
	private MeshRenderer r;

	// Token: 0x04001831 RID: 6193
	private int order;
}
