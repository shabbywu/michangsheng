using System;
using Bag;
using UnityEngine;

// Token: 0x02000468 RID: 1128
public class SelectCaiLiaoPageManager : MonoBehaviour, IESCClose
{
	// Token: 0x06001E4D RID: 7757 RVA: 0x0001923D File Offset: 0x0001743D
	public void init()
	{
		this.bag.Init(1, true);
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x0001924C File Offset: 0x0001744C
	public void OpenBag()
	{
		this.bag.UpdateItem(false);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001E4F RID: 7759 RVA: 0x00019265 File Offset: 0x00017465
	public void CloseBag()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x00019272 File Offset: 0x00017472
	public void setCurClickCaiLiaoItem(int index)
	{
		this.curClickCaiLiaoItem = index;
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x0001927B File Offset: 0x0001747B
	public int getCurClickCaiLiaoItem()
	{
		return this.curClickCaiLiaoItem;
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x00019283 File Offset: 0x00017483
	public bool TryEscClose()
	{
		this.CloseBag();
		return true;
	}

	// Token: 0x040019B6 RID: 6582
	public LianQiBag bag;

	// Token: 0x040019B7 RID: 6583
	private int curClickCaiLiaoItem;
}
