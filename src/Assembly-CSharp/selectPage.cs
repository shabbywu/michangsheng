using GUIPackage;
using UnityEngine;
using YSGame;

public class selectPage : prepareSelect
{
	public Inventory2 obj;

	private void Start()
	{
		((MonoBehaviour)this).Invoke("resetObj", 0.3f);
	}

	public override void nextPage()
	{
		if (!obj.isNewJiaoYi || JiaoYiManager.inst.canClick)
		{
			MusicMag.instance.PlayEffectMusic(13);
			addNowPage();
			resetObj();
		}
	}

	public override void lastPage()
	{
		if (!obj.isNewJiaoYi || JiaoYiManager.inst.canClick)
		{
			base.lastPage();
		}
	}

	public void setMaxPage(int max)
	{
		maxPage = max;
	}

	public override void addNowPage()
	{
		obj.nowIndex++;
		nowIndex = obj.nowIndex;
		if (nowIndex >= maxPage)
		{
			nowIndex = 0;
			obj.nowIndex = 0;
		}
	}

	public virtual void RestePageIndex()
	{
		nowIndex = 0;
		obj.nowIndex = 0;
		setPageTetx();
	}

	public override void reduceIndex()
	{
		obj.nowIndex--;
		nowIndex--;
		if (nowIndex < 0)
		{
			nowIndex = maxPage - 1;
			obj.nowIndex = maxPage - 1;
		}
	}

	public override void resetObj()
	{
		setPageTetx();
		if (obj.ISPlayer)
		{
			obj.LoadInventory();
			return;
		}
		ExchangePlan exchengePlan = Singleton.ints.exchengePlan;
		obj.MonstarLoadInventory(exchengePlan.MonstarID);
	}

	private void Update()
	{
	}
}
