using System;
using YSGame;

// Token: 0x0200030A RID: 778
public class SelectCaiLiaoPage : selectPage
{
	// Token: 0x06001B1F RID: 6943 RVA: 0x000BAAD9 File Offset: 0x000B8CD9
	private void Start()
	{
		base.Invoke("resetObj", 0.3f);
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x000C194F File Offset: 0x000BFB4F
	public override void resetObj()
	{
		base.setPageTetx();
		this.caiLiaoInventory.LoadCaiLiaoInventory();
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x000C1964 File Offset: 0x000BFB64
	public override void addNowPage()
	{
		this.caiLiaoInventory.nowIndex++;
		this.nowIndex = this.caiLiaoInventory.nowIndex;
		if (this.nowIndex >= this.maxPage)
		{
			this.nowIndex = 0;
			this.caiLiaoInventory.nowIndex = 0;
		}
	}

	// Token: 0x06001B22 RID: 6946 RVA: 0x000C19B6 File Offset: 0x000BFBB6
	public override void RestePageIndex()
	{
		this.nowIndex = 0;
		this.caiLiaoInventory.nowIndex = 0;
		base.setPageTetx();
	}

	// Token: 0x06001B23 RID: 6947 RVA: 0x000C19D4 File Offset: 0x000BFBD4
	public override void reduceIndex()
	{
		this.caiLiaoInventory.nowIndex--;
		this.nowIndex--;
		if (this.nowIndex < 0)
		{
			this.nowIndex = this.maxPage - 1;
			this.caiLiaoInventory.nowIndex = this.maxPage - 1;
		}
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x0006A969 File Offset: 0x00068B69
	public override void nextPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.addNowPage();
		this.resetObj();
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x0006AA16 File Offset: 0x00068C16
	public override void lastPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.reduceIndex();
		this.resetObj();
	}

	// Token: 0x040015A8 RID: 5544
	public CaiLiaoInventory caiLiaoInventory;
}
