using UnityEngine;
using YSGame;

public class SelectCaiLiaoPage : selectPage
{
	public CaiLiaoInventory caiLiaoInventory;

	private void Start()
	{
		((MonoBehaviour)this).Invoke("resetObj", 0.3f);
	}

	public override void resetObj()
	{
		setPageTetx();
		caiLiaoInventory.LoadCaiLiaoInventory();
	}

	public override void addNowPage()
	{
		caiLiaoInventory.nowIndex++;
		nowIndex = caiLiaoInventory.nowIndex;
		if (nowIndex >= maxPage)
		{
			nowIndex = 0;
			caiLiaoInventory.nowIndex = 0;
		}
	}

	public override void RestePageIndex()
	{
		nowIndex = 0;
		caiLiaoInventory.nowIndex = 0;
		setPageTetx();
	}

	public override void reduceIndex()
	{
		caiLiaoInventory.nowIndex--;
		nowIndex--;
		if (nowIndex < 0)
		{
			nowIndex = maxPage - 1;
			caiLiaoInventory.nowIndex = maxPage - 1;
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
