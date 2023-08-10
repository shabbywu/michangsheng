using Bag;
using UnityEngine;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI.UI;

public sealed class PublishDataUI : IPublishDataUI
{
	public GameObject DisablePublish;

	public PublishDataUI(GameObject gameObject)
	{
		_go = gameObject;
		Init();
	}

	protected override void Init()
	{
		base.Init();
		DisablePublish = Get("禁止发布");
	}

	public override void Clear()
	{
		NeedItem.SetNull();
		foreach (BaseSlot giveItem in GiveItems)
		{
			giveItem.SetNull();
		}
		抽成.SetText("0");
		((Component)((Component)Btn).transform.parent).gameObject.SetActive(false);
		DisablePublish.SetActive(true);
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		if (IExchangeUIMag.Inst == null)
		{
			Debug.LogError((object)"IExchangeUIMag.Inst is null In PublishDataUI UpdateUI");
			return;
		}
		if ((Object)(object)Btn == (Object)null || (Object)(object)DisablePublish == (Object)null)
		{
			Debug.LogError((object)"Btn or DisablePublish is null In PublishDataUI UpdateUI");
			return;
		}
		if (IExchangeUIMag.Inst.PublishCtr.CheckCanClickPublish())
		{
			((Component)((Component)Btn).transform.parent).gameObject.SetActive(true);
			DisablePublish.SetActive(false);
		}
		else
		{
			((Component)((Component)Btn).transform.parent).gameObject.SetActive(false);
			DisablePublish.SetActive(true);
		}
		if ((Object)(object)NeedItem == (Object)null || NeedItem.IsNull())
		{
			DrawMoney = 0;
		}
		else
		{
			DrawMoney = NeedItem.Item.BasePrice * 2 / 100;
		}
		抽成.SetText(DrawMoney.ToString());
	}
}
