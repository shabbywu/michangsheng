using System.Collections.Generic;
using Bag;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Base;
using script.ExchangeMeeting.UI.Factory;
using script.ExchangeMeeting.UI.Interface;
using script.ExchangeMeeting.UI.UI;

namespace script.ExchangeMeeting.UI.Ctr;

public class PublishCtr : IPublishCtr
{
	public PublishCtr(GameObject gameObject)
	{
		UI = new PublishUI(gameObject);
		Factory = new PlayerExchangeFactory();
		Init();
	}

	private void Init()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		UI.PublishDataUI.SetClickAction(new UnityAction(Publish));
		UnityEvent mouseUpEvent = UI.BackBtn.mouseUpEvent;
		IExchangeUIMag inst = IExchangeUIMag.Inst;
		mouseUpEvent.AddListener(new UnityAction(inst.OpenExchange));
		CreatePlayerList();
	}

	public override void Publish()
	{
		if (!CheckCanPublish())
		{
			return;
		}
		List<BaseItem> list = new List<BaseItem>();
		PlayerEx.Player.AddMoney(-UI.PublishDataUI.DrawMoney);
		foreach (BaseSlot giveItem in UI.PublishDataUI.GiveItems)
		{
			if (!giveItem.IsNull())
			{
				list.Add(giveItem.Item.Clone());
				PlayerEx.Player.removeItem(giveItem.Item.Uid, giveItem.Item.Count);
			}
		}
		List<BaseItem> list2 = new List<BaseItem>();
		list2.Add(UI.PublishDataUI.NeedItem.Item);
		IExchangeMag.Inst.ExchangeIO.CreatePlayerExchange(list2, list);
		UpdatePlayerList();
		UI.PublishDataUI.Clear();
		IExchangeUIMag.Inst.SubmitBag.CreateTempList();
	}

	private bool CheckCanPublish()
	{
		int num = 0;
		int num2 = 0;
		Dictionary<int, _ItemJsonData> dataDict = _ItemJsonData.DataDict;
		foreach (BaseSlot giveItem in UI.PublishDataUI.GiveItems)
		{
			if (!giveItem.IsNull() && dataDict.ContainsKey(giveItem.Item.Id))
			{
				int num3 = giveItem.Item.GetPrice() * giveItem.Item.Count;
				if (dataDict[giveItem.Item.Id].ItemFlag.Contains(52))
				{
					num3 = num3 * 13 / 10;
				}
				if (dataDict[giveItem.Item.Id].ItemFlag.Contains(53))
				{
					num3 = num3 * 13 / 10;
				}
				num += num3;
			}
		}
		if (!UI.PublishDataUI.NeedItem.IsNull() && dataDict.ContainsKey(UI.PublishDataUI.NeedItem.Item.Id))
		{
			int num4 = UI.PublishDataUI.NeedItem.Item.GetPrice();
			if (dataDict[UI.PublishDataUI.NeedItem.Item.Id].ItemFlag.Contains(52))
			{
				num4 = num4 * 13 / 10;
			}
			if (dataDict[UI.PublishDataUI.NeedItem.Item.Id].ItemFlag.Contains(53))
			{
				num4 = num4 * 13 / 10;
			}
			num2 = num4;
		}
		if (num2 <= 0 || num <= 0)
		{
			return false;
		}
		int needSay = GetNeedSay(num2, num);
		if (needSay <= 3)
		{
			IExchangeUIMag.Inst.Say(needSay);
			return false;
		}
		IExchangeUIMag.Inst.Say(needSay);
		return true;
	}

	private int GetNeedSay(int need, int give)
	{
		if (need * 95 / 100 > give)
		{
			return 1;
		}
		if (UI.PublishDataUI.DrawMoney == 0 || UI.PublishDataUI.DrawMoney > (int)PlayerEx.Player.money)
		{
			return 2;
		}
		foreach (BaseSlot giveItem in UI.PublishDataUI.GiveItems)
		{
			if (!giveItem.IsNull() && UI.PublishDataUI.NeedItem.Item.Id == giveItem.Item.Id)
			{
				return 3;
			}
		}
		int id = UI.PublishDataUI.NeedItem.Item.Id;
		if (_ItemJsonData.DataDict[id].ItemFlag.Contains(53))
		{
			return 11;
		}
		return 12;
	}

	public override void CreatePlayerList()
	{
		List<IExchangeData> playerList = IExchangeMag.Inst.ExchangeIO.GetPlayerList();
		Tools.ClearChild(UI.ExchangeParent);
		foreach (IExchangeData item in playerList)
		{
			Factory.Create(UI.ExchangePrefab, UI.ExchangeParent, item);
		}
	}

	public override void UpdatePlayerList()
	{
		CreatePlayerList();
	}

	public override void PutNeedItem(BaseItem baseItem)
	{
		if (baseItem == null)
		{
			Debug.LogError((object)"PutNeedItem,物品数据异常 baseItem=null");
			UIPopTip.Inst.Pop("物品数据异常");
		}
		else if (!_ItemJsonData.DataDict.ContainsKey(baseItem.Id))
		{
			Debug.LogError((object)$"PutNeedItem,物品数据异常 baseItem.Id={baseItem.Id}");
			UIPopTip.Inst.Pop("物品数据异常");
		}
		else
		{
			UI.PublishDataUI.NeedItem.SetSlotData(baseItem.Clone());
			UI.PublishDataUI.UpdateUI();
		}
	}

	public override void BackNeedItem()
	{
		UI.PublishDataUI.NeedItem.SetNull();
		UI.PublishDataUI.UpdateUI();
	}

	public override void PutGiveItem(BaseSlot slot)
	{
		if ((Object)(object)slot == (Object)null || slot.IsNull())
		{
			Debug.LogError((object)"数据异常 slot=null 或者 slot.IsNull");
		}
		if ((Object)(object)IExchangeUIMag.Inst.PlayerBag.ToSlot == (Object)null)
		{
			Debug.LogError((object)"数据异常 IExchangeUIMag.Inst.PlayerBag.ToSlot=null");
			return;
		}
		BaseSlot toSlot = IExchangeUIMag.Inst.PlayerBag.ToSlot;
		ExchangeBag bag = IExchangeUIMag.Inst.PlayerBag;
		if (slot.Item.IsEqual(toSlot.Item) && slot.Item.MaxNum > 1)
		{
			UnityAction<int> val = delegate(int num)
			{
				toSlot.Item.Count += num;
				toSlot.UpdateUI();
				bag.Hide();
				bag.RemoveTempItem(slot.Item.Uid, num);
				UI.PublishDataUI.UpdateUI();
			};
			if (slot.Item.Count == 1)
			{
				val.Invoke(1);
			}
			else
			{
				USelectNum.Show(slot.Item.GetName() + " x{num}", 1, slot.Item.Count, val);
			}
			return;
		}
		UnityAction<int> val2 = delegate(int num)
		{
			if (!toSlot.IsNull())
			{
				bag.AddTempItem(toSlot.Item.Clone(), toSlot.Item.Count);
			}
			BaseItem baseItem = slot.Item.Clone();
			baseItem.Count = num;
			toSlot.SetSlotData(baseItem);
			bag.Hide();
			bag.RemoveTempItem(slot.Item.Uid, num);
			UI.PublishDataUI.UpdateUI();
		};
		if (slot.Item.Count == 1)
		{
			val2.Invoke(1);
		}
		else
		{
			USelectNum.Show(slot.Item.GetName() + " x{num}", 1, slot.Item.Count, val2);
		}
	}

	public override void BackGiveItem(BaseSlot slot)
	{
		if ((Object)(object)slot == (Object)null || slot.IsNull())
		{
			Debug.LogError((object)"数据异常 slot=null 或者 slot.IsNull");
		}
		ExchangeBag bag = IExchangeUIMag.Inst.PlayerBag;
		UnityAction<int> val = delegate(int num)
		{
			bag.AddTempItem(slot.Item.Clone(), num);
			if (slot.Item.Count <= num)
			{
				slot.SetNull();
			}
			else
			{
				slot.Item.Count -= num;
				slot.UpdateUI();
			}
			UI.PublishDataUI.UpdateUI();
		};
		if (slot.Item.Count == 1)
		{
			val.Invoke(1);
		}
		else
		{
			USelectNum.Show(slot.Item.GetName() + " x{num}", 1, slot.Item.Count, val);
		}
	}

	public override bool CheckCanClickPublish()
	{
		if (UI == null)
		{
			Debug.LogError((object)"CheckCanPublish UI=null");
			return false;
		}
		if (UI.PublishDataUI == null)
		{
			Debug.LogError((object)"CheckCanPublish UI.PublishDataUI=null");
			return false;
		}
		if ((Object)(object)UI.PublishDataUI.NeedItem == (Object)null)
		{
			Debug.LogError((object)"CheckCanPublish UI.PublishDataUI.NeedItem=null");
			return false;
		}
		if (UI.PublishDataUI.NeedItem.IsNull())
		{
			return false;
		}
		if (UI.PublishDataUI.GiveItems == null)
		{
			Debug.LogError((object)"CheckCanPublish UI.PublishDataUI.GiveItems=null");
			return false;
		}
		foreach (BaseSlot giveItem in UI.PublishDataUI.GiveItems)
		{
			if (!giveItem.IsNull())
			{
				return true;
			}
		}
		return false;
	}
}
