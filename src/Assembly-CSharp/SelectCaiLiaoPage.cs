using System;
using YSGame;

// Token: 0x02000467 RID: 1127
public class SelectCaiLiaoPage : selectPage
{
	// Token: 0x06001E45 RID: 7749 RVA: 0x00012E8D File Offset: 0x0001108D
	private void Start()
	{
		base.Invoke("resetObj", 0.3f);
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x0001920F File Offset: 0x0001740F
	public override void resetObj()
	{
		base.setPageTetx();
		this.caiLiaoInventory.LoadCaiLiaoInventory();
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x00107114 File Offset: 0x00105314
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

	// Token: 0x06001E48 RID: 7752 RVA: 0x00019222 File Offset: 0x00017422
	public override void RestePageIndex()
	{
		this.nowIndex = 0;
		this.caiLiaoInventory.nowIndex = 0;
		base.setPageTetx();
	}

	// Token: 0x06001E49 RID: 7753 RVA: 0x00107168 File Offset: 0x00105368
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

	// Token: 0x06001E4A RID: 7754 RVA: 0x00012BD3 File Offset: 0x00010DD3
	public override void nextPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.addNowPage();
		this.resetObj();
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x00012C26 File Offset: 0x00010E26
	public override void lastPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.reduceIndex();
		this.resetObj();
	}

	// Token: 0x040019B5 RID: 6581
	public CaiLiaoInventory caiLiaoInventory;
}
