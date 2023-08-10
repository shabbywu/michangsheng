using UnityEngine;
using YSGame;

public class SelectLianDanPage : selectPage
{
	public LianDanInventory lianDanInventory;

	private void Start()
	{
		((MonoBehaviour)this).Invoke("resetObj", 0.3f);
	}

	public override void resetObj()
	{
		setPageTetx();
		lianDanInventory.LoadCaiLiaoInventory();
	}

	public override void addNowPage()
	{
		lianDanInventory.nowIndex++;
		nowIndex = lianDanInventory.nowIndex;
		if (nowIndex >= maxPage)
		{
			nowIndex = 0;
			lianDanInventory.nowIndex = 0;
		}
	}

	public override void RestePageIndex()
	{
		nowIndex = 0;
		lianDanInventory.nowIndex = 0;
		setPageTetx();
	}

	public override void reduceIndex()
	{
		lianDanInventory.nowIndex--;
		nowIndex--;
		if (nowIndex < 0)
		{
			nowIndex = maxPage - 1;
			lianDanInventory.nowIndex = maxPage - 1;
		}
	}

	public override void nextPage()
	{
		MusicMag.instance.PlayEffectMusic(13);
		addNowPage();
		resetObj();
	}

	public override void lastPage()
	{
		MusicMag.instance.PlayEffectMusic(13);
		reduceIndex();
		resetObj();
	}
}
