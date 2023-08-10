using Bag;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.ExchangeMeeting.Logic;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Base;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI.UI;

public sealed class SysExchangeDataUI : IExchangeDataUI
{
	public ExchangeSlotC SubSlot;

	private SysExchangeData data;

	private FpBtn submitBtn;

	private GameObject disable;

	private int i;

	public SysExchangeDataUI(GameObject gameObject, IExchangeData data)
	{
		_go = gameObject;
		_go.SetActive(true);
		_data = data;
		Init();
	}

	protected override void Init()
	{
		i = 1;
		data = _data as SysExchangeData;
		InitGiveItem();
		InitNeedTag();
		InitNeedItem();
		InitSubmitItem();
	}

	private void InitGiveItem()
	{
		if (data == null || data.GiveItems.Count < 1 || data.GiveItems[0] == null)
		{
			Debug.LogError((object)"初始化系统交易会数据失败，已自动隐藏");
			_go.SetActive(false);
		}
		else
		{
			Get<BaseSlot>("GiveList/1").SetSlotData(data.GiveItems[0].Clone());
		}
	}

	private void InitNeedItem()
	{
		if (IsNeedError())
		{
			Debug.LogError((object)"需求物品数量异常，已自动隐藏");
			_go.SetActive(false);
			return;
		}
		foreach (BaseItem needItem in data.NeedItems)
		{
			Get("NeedList/" + i).SetActive(true);
			Get<Text>("NeedList/" + i).SetText($"{needItem.GetName()}x{needItem.Count}");
			i++;
		}
		if (i < 4)
		{
			Get($"NeedList/{i - 1}/或者").SetActive(false);
		}
	}

	private void InitNeedTag()
	{
		if (IsNeedError())
		{
			Debug.LogError((object)"需求物品数量异常，已自动隐藏");
			_go.SetActive(false);
			return;
		}
		foreach (int key in data.NeedTags.Keys)
		{
			if (!ItemFlagData.DataDict.ContainsKey(key))
			{
				Debug.LogError((object)("不存在物品标签id：" + key + "，已跳过"));
				continue;
			}
			Get("NeedList/" + i).SetActive(true);
			Get<Text>("NeedList/" + i).SetText($"{ItemFlagData.DataDict[key].name}x{data.NeedTags[key]}");
			i++;
		}
	}

	private void InitSubmitItem()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		submitBtn = Get<FpBtn>("提交/提交按钮");
		submitBtn.mouseUpEvent.AddListener(new UnityAction(Submit));
		disable = Get("提交/不可点击");
		SubSlot = Get<ExchangeSlotC>("提交/1");
		SubSlot.SetNull();
		SubSlot.Init(this);
	}

	public void UpdateCanSubmit()
	{
		if (SubSlot.IsNull() || !IsNeedItem(SubSlot.Item))
		{
			disable.SetActive(true);
			((Component)submitBtn).gameObject.SetActive(false);
		}
		else
		{
			disable.SetActive(false);
			((Component)submitBtn).gameObject.SetActive(true);
		}
	}

	private void Submit()
	{
		if (IsNeedItem(SubSlot.Item))
		{
			PlayerEx.Player.removeItem(SubSlot.Item.Uid, SubSlot.Item.Count);
			foreach (BaseItem giveItem in data.GiveItems)
			{
				PlayerEx.Player.addItem(giveItem.Id, giveItem.Count, Tools.CreateItemSeid(giveItem.Id), ShowText: true);
			}
			if (data.IsGuDing && GuDingExchangeData.DataDict.ContainsKey(data.Id))
			{
				IExchangeMag.Inst.ExchangeIO.SaveGuDingId(data.Id);
			}
			IExchangeMag.Inst.ExchangeIO.Remove(_data);
			IExchangeUIMag.Inst.SubmitBag.CreateTempList();
			Object.Destroy((Object)(object)_go);
		}
		else
		{
			UIPopTip.Inst.Pop("提交物品异常，请反馈");
			Debug.LogError((object)"提交物品异常");
		}
	}

	public bool IsNeedItem(BaseItem baseItem)
	{
		if (baseItem == null || !_ItemJsonData.DataDict.ContainsKey(baseItem.Id))
		{
			Debug.LogError((object)"物品为空或id异常");
			return false;
		}
		if (data.NeedTags.Count > 0)
		{
			foreach (int item in _ItemJsonData.DataDict[baseItem.Id].ItemFlag)
			{
				if (data.NeedTags.ContainsKey(item) && data.NeedTags[item] <= baseItem.Count)
				{
					return true;
				}
			}
		}
		if (data.NeedItems.Count > 0)
		{
			foreach (BaseItem needItem in data.NeedItems)
			{
				if (baseItem.Id == needItem.Id)
				{
					if (baseItem.Count >= needItem.Count)
					{
						return true;
					}
					return false;
				}
			}
		}
		return false;
	}

	public int GetPutNum(BaseItem baseItem)
	{
		if (data.NeedTags.Count > 0)
		{
			foreach (int item in _ItemJsonData.DataDict[baseItem.Id].ItemFlag)
			{
				if (data.NeedTags.ContainsKey(item) && data.NeedTags[item] <= baseItem.Count)
				{
					return data.NeedTags[item];
				}
			}
		}
		if (data.NeedItems.Count > 0)
		{
			foreach (BaseItem needItem in data.NeedItems)
			{
				if (baseItem.Id == needItem.Id && baseItem.Count >= needItem.Count)
				{
					return needItem.Count;
				}
			}
		}
		Debug.LogError((object)"获取放入数目异常");
		return -1;
	}

	private bool IsNeedError()
	{
		if ((data.NeedTags.Count < 1 && data.NeedItems.Count < 1) || data.NeedTags.Count + data.NeedItems.Count > 3)
		{
			Debug.LogError((object)"需求物品数量异常，已自动隐藏");
			_go.SetActive(false);
			return true;
		}
		return false;
	}
}
