using System;
using Bag;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityEngine.Events;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI.UI;

public sealed class PublishingDataUI : IPublishDataUI
{
	private readonly PlayerExchangeData data;

	public PublishingDataUI(GameObject gameObject, IExchangeData data)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		_go = gameObject;
		_action = new UnityAction(Cancel);
		this.data = data as PlayerExchangeData;
		Init();
	}

	protected override void Init()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		base.Init();
		if (IsErrorData())
		{
			throw new RuntimeException("exchangeData数据异常！");
		}
		NeedItem.SetSlotData(data.NeedItems[0]);
		for (int i = 0; i < data.GiveItems.Count; i++)
		{
			try
			{
				GiveItems[i].SetSlotData(data.GiveItems[i].Clone());
			}
			catch (Exception)
			{
				_go.SetActive(false);
			}
			((Component)GiveItems[i]).gameObject.SetActive(true);
		}
		抽成.SetText(data.CostMoney);
		_go.SetActive(true);
	}

	private void Cancel()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if (IsErrorData())
		{
			throw new RuntimeException("exchangeData数据异常！");
		}
		USelectBox.Show("确定要放弃吗？", (UnityAction)delegate
		{
			foreach (BaseItem giveItem in data.GiveItems)
			{
				PlayerEx.Player.addItem(giveItem.Id, giveItem.Count, giveItem.Seid, ShowText: true);
			}
			PlayerEx.Player.AddMoney(data.CostMoney);
			IExchangeMag.Inst.ExchangeIO.Remove(data);
			Object.Destroy((Object)(object)_go);
		});
	}

	private bool IsErrorData()
	{
		if (data == null)
		{
			return true;
		}
		if (data.NeedItems.Count < 1 || data.GiveItems.Count < 1 || data.GiveItems.Count > giveItemCount)
		{
			return true;
		}
		return false;
	}
}
