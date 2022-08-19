using System;
using Bag;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class SelectCaiLiaoPageManager : MonoBehaviour, IESCClose
{
	// Token: 0x06001B27 RID: 6951 RVA: 0x000C1A2C File Offset: 0x000BFC2C
	public void init()
	{
		this.bag.Init(1, true);
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x000C1A3B File Offset: 0x000BFC3B
	public void OpenBag()
	{
		this.bag.UpdateItem(false);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x000C1A54 File Offset: 0x000BFC54
	public void CloseBag()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x000C1A61 File Offset: 0x000BFC61
	public void setCurClickCaiLiaoItem(int index)
	{
		this.curClickCaiLiaoItem = index;
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x000C1A6A File Offset: 0x000BFC6A
	public int getCurClickCaiLiaoItem()
	{
		return this.curClickCaiLiaoItem;
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x000C1A72 File Offset: 0x000BFC72
	public bool TryEscClose()
	{
		this.CloseBag();
		return true;
	}

	// Token: 0x040015A9 RID: 5545
	public LianQiBag bag;

	// Token: 0x040015AA RID: 5546
	private int curClickCaiLiaoItem;
}
