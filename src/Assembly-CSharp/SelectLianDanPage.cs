using System;
using YSGame;

// Token: 0x0200044A RID: 1098
public class SelectLianDanPage : selectPage
{
	// Token: 0x06001D3E RID: 7486 RVA: 0x00012E8D File Offset: 0x0001108D
	private void Start()
	{
		base.Invoke("resetObj", 0.3f);
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x00018633 File Offset: 0x00016833
	public override void resetObj()
	{
		base.setPageTetx();
		this.lianDanInventory.LoadCaiLiaoInventory();
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x001010E4 File Offset: 0x000FF2E4
	public override void addNowPage()
	{
		this.lianDanInventory.nowIndex++;
		this.nowIndex = this.lianDanInventory.nowIndex;
		if (this.nowIndex >= this.maxPage)
		{
			this.nowIndex = 0;
			this.lianDanInventory.nowIndex = 0;
		}
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x00018646 File Offset: 0x00016846
	public override void RestePageIndex()
	{
		this.nowIndex = 0;
		this.lianDanInventory.nowIndex = 0;
		base.setPageTetx();
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x00101138 File Offset: 0x000FF338
	public override void reduceIndex()
	{
		this.lianDanInventory.nowIndex--;
		this.nowIndex--;
		if (this.nowIndex < 0)
		{
			this.nowIndex = this.maxPage - 1;
			this.lianDanInventory.nowIndex = this.maxPage - 1;
		}
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x00012BD3 File Offset: 0x00010DD3
	public override void nextPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.addNowPage();
		this.resetObj();
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x00012C26 File Offset: 0x00010E26
	public override void lastPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.reduceIndex();
		this.resetObj();
	}

	// Token: 0x0400192B RID: 6443
	public LianDanInventory lianDanInventory;
}
