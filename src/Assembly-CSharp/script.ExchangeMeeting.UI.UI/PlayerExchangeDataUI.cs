using Bag;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI.UI;

public sealed class PlayerExchangeDataUI : IExchangeDataUI
{
	private PlayerExchangeData data;

	public PlayerExchangeDataUI(GameObject gameObject, IExchangeData data)
	{
		_go = gameObject;
		_go.SetActive(true);
		_data = data;
		Init();
	}

	protected override void Init()
	{
		data = _data as PlayerExchangeData;
		InitGiveItem();
		InitNeedItem();
	}

	private void InitGiveItem()
	{
		int num = 1;
		foreach (BaseItem giveItem in data.GiveItems)
		{
			if (giveItem == null || !_ItemJsonData.DataDict.ContainsKey(giveItem.Id))
			{
				Debug.LogError((object)"PlayerExchangeDataUI.InitGiveItem物品数据异常已跳过");
				continue;
			}
			Get<Text>("GiveList/" + num).SetText($"{giveItem.GetName()}x{giveItem.Count}");
			Get("GiveList/" + num).SetActive(true);
			num++;
		}
		if (num < 5)
		{
			Get($"GiveList/{num - 1}/加上").SetActive(false);
		}
	}

	private void InitNeedItem()
	{
		Get<BaseSlot>("NeedList/1").SetSlotData(data.NeedItems[0].Clone());
	}
}
