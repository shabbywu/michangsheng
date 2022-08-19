using System;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class SelectLianDanCaiLiaoPage : MonoBehaviour, IESCClose
{
	// Token: 0x06001A11 RID: 6673 RVA: 0x000BAA15 File Offset: 0x000B8C15
	private void Awake()
	{
		SelectLianDanCaiLiaoPage.Inst = this;
	}

	// Token: 0x06001A12 RID: 6674 RVA: 0x000BAA1D File Offset: 0x000B8C1D
	public void openCaiLiaoPackge(int type)
	{
		this.isInSelectPage = true;
		LianDanSystemManager.inst.inventory.selectType = type;
		this.pageManager.resetObj();
		base.gameObject.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001A13 RID: 6675 RVA: 0x000BAA58 File Offset: 0x000B8C58
	public void CloseCaiLiaoPackge()
	{
		base.gameObject.SetActive(false);
		this.isInSelectPage = false;
		this.curSelectIndex = -1;
		base.Invoke("ResetClick", 0.25f);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x000BAA8F File Offset: 0x000B8C8F
	public void clickMask()
	{
		base.gameObject.SetActive(false);
		base.Invoke("ResetClick", 0.25f);
	}

	// Token: 0x06001A15 RID: 6677 RVA: 0x000BAAAD File Offset: 0x000B8CAD
	private void ResetClick()
	{
		LianDanSystemManager.inst.lianDanPageManager.CanClick = true;
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x000BAABF File Offset: 0x000B8CBF
	public void setCurSelectIndex(int index)
	{
		this.curSelectIndex = index;
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x000BAAC8 File Offset: 0x000B8CC8
	public int getCurSelectIndex()
	{
		return this.curSelectIndex;
	}

	// Token: 0x06001A18 RID: 6680 RVA: 0x000BAAD0 File Offset: 0x000B8CD0
	public bool TryEscClose()
	{
		this.clickMask();
		return true;
	}

	// Token: 0x04001521 RID: 5409
	public static SelectLianDanCaiLiaoPage Inst;

	// Token: 0x04001522 RID: 5410
	[SerializeField]
	private SelectLianDanPage pageManager;

	// Token: 0x04001523 RID: 5411
	public bool isInSelectPage;

	// Token: 0x04001524 RID: 5412
	private int curSelectIndex;
}
