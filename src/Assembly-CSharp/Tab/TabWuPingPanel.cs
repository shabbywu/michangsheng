using System;
using System.Collections.Generic;
using Bag;
using GUIPackage;
using KBEngine;
using UnityEngine;

namespace Tab;

[Serializable]
public class TabWuPingPanel : ITabPanelBase
{
	private bool _isInit;

	public Dictionary<int, EquipSlot> EquipDict = new Dictionary<int, EquipSlot>();

	private FangAnData FangAnData = Tools.instance.getPlayer().StreamData.FangAnData;

	public TabWuPingPanel(GameObject gameObject)
	{
		HasHp = true;
		_go = gameObject;
		_isInit = false;
	}

	private void Init()
	{
		Transform transform = Get("EquipList").transform;
		EquipSlot equipSlot = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			equipSlot = ((Component)transform.GetChild(i)).GetComponent<EquipSlot>();
			EquipDict.Add(i + 1, equipSlot);
		}
		if (Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231))
		{
			((Component)EquipDict[3]).gameObject.SetActive(true);
			return;
		}
		((Component)EquipDict[3]).gameObject.SetActive(false);
		EquipDict.Remove(3);
	}

	public override void Show()
	{
		if (!_isInit)
		{
			Init();
			_isInit = true;
		}
		SingletonMono<TabUIMag>.Instance.TabBag.OpenBag(BagType.背包);
		LoadEquipData();
		SingletonMono<TabUIMag>.Instance.ShowBaseData();
		_go.SetActive(true);
	}

	public void LoadEquipData()
	{
		Dictionary<int, BaseItem> curEquipDict = FangAnData.GetCurEquipDict();
		foreach (int key in EquipDict.Keys)
		{
			if (curEquipDict.ContainsKey(key))
			{
				EquipDict[key].SetSlotData(curEquipDict[key]);
			}
			else
			{
				EquipDict[key].SetNull();
			}
		}
		ResetEquipSeid();
	}

	public void AddEquip(int index, EquipItem equipItem)
	{
		if (EquipDict[index].IsNull())
		{
			FangAnData.GetCurEquipDict()[index] = equipItem.Clone();
			EquipDict[index].SetSlotData(equipItem.Clone());
			DragMag.Inst.DragSlot.SetNull();
			Tools.instance.getPlayer().removeItem(equipItem.Uid);
		}
		else
		{
			DragMag.Inst.DragSlot.SetSlotData(EquipDict[index].Item.Clone());
			Tools.instance.getPlayer().AddEquip(EquipDict[index].Item.Id, EquipDict[index].Item.Uid, EquipDict[index].Item.Seid);
			FangAnData.GetCurEquipDict()[index] = equipItem;
			EquipDict[index].SetSlotData(equipItem);
			Tools.instance.getPlayer().removeItem(equipItem.Uid);
		}
		ResetEquipSeid();
		SingletonMono<TabUIMag>.Instance.TabBag.UpDateSlotList();
	}

	public void AddEquip(EquipItem equipItem)
	{
		foreach (int key in EquipDict.Keys)
		{
			if (equipItem.CanPutSlotType == EquipDict[key].AcceptType)
			{
				AddEquip(key, equipItem);
				break;
			}
		}
		DragMag.Inst.Clear();
	}

	public void RmoveEquip(EquipSlotType slotType)
	{
		BaseItem item = EquipDict[(int)slotType].Item;
		Tools.instance.getPlayer().AddEquip(item.Id, item.Uid, item.Seid);
		SingletonMono<TabUIMag>.Instance.TabBag.UpDateSlotList();
		EquipDict[(int)slotType].SetNull();
		FangAnData.GetCurEquipDict().Remove((int)slotType);
		ResetEquipSeid();
	}

	public void ExEquip(EquipSlotType slotType1, EquipSlotType slotType2)
	{
		if (EquipDict[(int)slotType2].IsNull())
		{
			EquipDict[(int)slotType2].SetSlotData(EquipDict[(int)slotType1].Item.Clone());
			EquipDict[(int)slotType1].SetNull();
			FangAnData.GetCurEquipDict().Remove((int)slotType1);
		}
		else
		{
			BaseItem slotData = EquipDict[(int)slotType1].Item.Clone();
			BaseItem slotData2 = EquipDict[(int)slotType2].Item.Clone();
			EquipDict[(int)slotType1].SetSlotData(slotData2);
			EquipDict[(int)slotType2].SetSlotData(slotData);
			FangAnData.GetCurEquipDict()[(int)slotType1] = EquipDict[(int)slotType1].Item.Clone();
		}
		FangAnData.GetCurEquipDict()[(int)slotType2] = EquipDict[(int)slotType2].Item.Clone();
		ResetEquipSeid();
		Tools.instance.ResetEquipSeid();
	}

	public void ResetEquipSeid()
	{
		Avatar player = Tools.instance.getPlayer();
		player.EquipSeidFlag = new Dictionary<int, Dictionary<int, int>>();
		Dictionary<int, BaseItem> curEquipDict = FangAnData.GetCurEquipDict();
		foreach (int key in curEquipDict.Keys)
		{
			if (curEquipDict[key].Seid == null && curEquipDict[key].Seid.HasField("ItemSeids"))
			{
				foreach (JSONObject item in curEquipDict[key].Seid["ItemSeids"].list)
				{
					Equips equips = new Equips(curEquipDict[key].Id, 0, 5);
					equips.ItemAddSeid = item;
					equips.Puting(player, player, 2);
				}
			}
			else
			{
				new Equips(curEquipDict[key].Id, 0, 5).Puting(player, player, 2);
			}
		}
		player.nowConfigEquipItem = FangAnData.CurEquipIndex;
		player.equipItemList.values = player.StreamData.FangAnData.CurEquipDictToOldList();
	}
}
