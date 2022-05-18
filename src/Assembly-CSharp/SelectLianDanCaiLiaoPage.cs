using System;
using UnityEngine;

// Token: 0x02000449 RID: 1097
public class SelectLianDanCaiLiaoPage : MonoBehaviour, IESCClose
{
	// Token: 0x06001D35 RID: 7477 RVA: 0x0001856F File Offset: 0x0001676F
	private void Awake()
	{
		SelectLianDanCaiLiaoPage.Inst = this;
	}

	// Token: 0x06001D36 RID: 7478 RVA: 0x00018577 File Offset: 0x00016777
	public void openCaiLiaoPackge(int type)
	{
		this.isInSelectPage = true;
		LianDanSystemManager.inst.inventory.selectType = type;
		this.pageManager.resetObj();
		base.gameObject.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x000185B2 File Offset: 0x000167B2
	public void CloseCaiLiaoPackge()
	{
		base.gameObject.SetActive(false);
		this.isInSelectPage = false;
		this.curSelectIndex = -1;
		base.Invoke("ResetClick", 0.25f);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x000185E9 File Offset: 0x000167E9
	public void clickMask()
	{
		base.gameObject.SetActive(false);
		base.Invoke("ResetClick", 0.25f);
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x00018607 File Offset: 0x00016807
	private void ResetClick()
	{
		LianDanSystemManager.inst.lianDanPageManager.CanClick = true;
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x00018619 File Offset: 0x00016819
	public void setCurSelectIndex(int index)
	{
		this.curSelectIndex = index;
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x00018622 File Offset: 0x00016822
	public int getCurSelectIndex()
	{
		return this.curSelectIndex;
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x0001862A File Offset: 0x0001682A
	public bool TryEscClose()
	{
		this.clickMask();
		return true;
	}

	// Token: 0x04001927 RID: 6439
	public static SelectLianDanCaiLiaoPage Inst;

	// Token: 0x04001928 RID: 6440
	[SerializeField]
	private SelectLianDanPage pageManager;

	// Token: 0x04001929 RID: 6441
	public bool isInSelectPage;

	// Token: 0x0400192A RID: 6442
	private int curSelectIndex;
}
