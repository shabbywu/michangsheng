using System;
using GUIPackage;
using YSGame;

// Token: 0x020001A1 RID: 417
public class selectPage : prepareSelect
{
	// Token: 0x060011C9 RID: 4553 RVA: 0x0006BAA1 File Offset: 0x00069CA1
	private void Start()
	{
		base.Invoke("resetObj", 0.3f);
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x0006BAB3 File Offset: 0x00069CB3
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

	// Token: 0x060011CB RID: 4555 RVA: 0x0006BAEC File Offset: 0x00069CEC
	public override void lastPage()
	{
		if (this.obj.isNewJiaoYi && !JiaoYiManager.inst.canClick)
		{
			return;
		}
		base.lastPage();
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x0006BB0E File Offset: 0x00069D0E
	public void setMaxPage(int max)
	{
		this.maxPage = max;
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x0006BB18 File Offset: 0x00069D18
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

	// Token: 0x060011CE RID: 4558 RVA: 0x0006BB6A File Offset: 0x00069D6A
	public virtual void RestePageIndex()
	{
		this.nowIndex = 0;
		this.obj.nowIndex = 0;
		base.setPageTetx();
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x0006BB88 File Offset: 0x00069D88
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

	// Token: 0x060011D0 RID: 4560 RVA: 0x0006BBE0 File Offset: 0x00069DE0
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

	// Token: 0x060011D1 RID: 4561 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000CB9 RID: 3257
	public Inventory2 obj;
}
