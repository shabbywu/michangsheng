using System;
using YSGame;

// Token: 0x020002EF RID: 751
public class SelectLianDanPage : selectPage
{
	// Token: 0x06001A1A RID: 6682 RVA: 0x000BAAD9 File Offset: 0x000B8CD9
	private void Start()
	{
		base.Invoke("resetObj", 0.3f);
	}

	// Token: 0x06001A1B RID: 6683 RVA: 0x000BAAEB File Offset: 0x000B8CEB
	public override void resetObj()
	{
		base.setPageTetx();
		this.lianDanInventory.LoadCaiLiaoInventory();
	}

	// Token: 0x06001A1C RID: 6684 RVA: 0x000BAB00 File Offset: 0x000B8D00
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

	// Token: 0x06001A1D RID: 6685 RVA: 0x000BAB52 File Offset: 0x000B8D52
	public override void RestePageIndex()
	{
		this.nowIndex = 0;
		this.lianDanInventory.nowIndex = 0;
		base.setPageTetx();
	}

	// Token: 0x06001A1E RID: 6686 RVA: 0x000BAB70 File Offset: 0x000B8D70
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

	// Token: 0x06001A1F RID: 6687 RVA: 0x0006A969 File Offset: 0x00068B69
	public override void nextPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.addNowPage();
		this.resetObj();
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x0006AA16 File Offset: 0x00068C16
	public override void lastPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.reduceIndex();
		this.resetObj();
	}

	// Token: 0x04001525 RID: 5413
	public LianDanInventory lianDanInventory;
}
