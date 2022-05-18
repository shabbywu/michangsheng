using System;
using GUIPackage;
using YSGame;

// Token: 0x0200029D RID: 669
public class selectPage : prepareSelect
{
	// Token: 0x06001470 RID: 5232 RVA: 0x00012E8D File Offset: 0x0001108D
	private void Start()
	{
		base.Invoke("resetObj", 0.3f);
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x00012E9F File Offset: 0x0001109F
	public override void nextPage()
	{
		if (this.obj.isNewJiaoYi && !JiaoYiManager.inst.canClick)
		{
			return;
		}
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.addNowPage();
		this.resetObj();
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x00012ED8 File Offset: 0x000110D8
	public override void lastPage()
	{
		if (this.obj.isNewJiaoYi && !JiaoYiManager.inst.canClick)
		{
			return;
		}
		base.lastPage();
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x00012EFA File Offset: 0x000110FA
	public void setMaxPage(int max)
	{
		this.maxPage = max;
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x000B9C00 File Offset: 0x000B7E00
	public override void addNowPage()
	{
		this.obj.nowIndex++;
		this.nowIndex = this.obj.nowIndex;
		if (this.nowIndex >= this.maxPage)
		{
			this.nowIndex = 0;
			this.obj.nowIndex = 0;
		}
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x00012F03 File Offset: 0x00011103
	public virtual void RestePageIndex()
	{
		this.nowIndex = 0;
		this.obj.nowIndex = 0;
		base.setPageTetx();
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x000B9C54 File Offset: 0x000B7E54
	public override void reduceIndex()
	{
		this.obj.nowIndex--;
		this.nowIndex--;
		if (this.nowIndex < 0)
		{
			this.nowIndex = this.maxPage - 1;
			this.obj.nowIndex = this.maxPage - 1;
		}
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x000B9CAC File Offset: 0x000B7EAC
	public override void resetObj()
	{
		base.setPageTetx();
		if (this.obj.ISPlayer)
		{
			this.obj.LoadInventory();
			return;
		}
		ExchangePlan exchengePlan = Singleton.ints.exchengePlan;
		this.obj.MonstarLoadInventory(exchengePlan.MonstarID);
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000FDD RID: 4061
	public Inventory2 obj;
}
