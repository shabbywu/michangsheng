using System;
using System.Collections.Generic;
using Bag;
using KBEngine;

[Serializable]
public class FangAnData
{
	public Dictionary<int, Dictionary<int, BaseItem>> EquipDictionary = new Dictionary<int, Dictionary<int, BaseItem>>();

	public int CurEquipIndex = 1;

	public void SaveHandle()
	{
		foreach (int key in EquipDictionary.Keys)
		{
			foreach (int key2 in EquipDictionary[key].Keys)
			{
				EquipDictionary[key][key2].HandleSeid();
			}
		}
	}

	public void LoadHandle()
	{
		foreach (int key in EquipDictionary.Keys)
		{
			foreach (int key2 in EquipDictionary[key].Keys)
			{
				EquipDictionary[key][key2].LoadSeid();
			}
		}
	}

	public Dictionary<int, BaseItem> GetCurEquipDict()
	{
		if (!EquipDictionary.ContainsKey(CurEquipIndex))
		{
			EquipDictionary.Add(CurEquipIndex, new Dictionary<int, BaseItem>());
		}
		return EquipDictionary[CurEquipIndex];
	}

	public List<ITEM_INFO> CurEquipDictToOldList()
	{
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		Dictionary<int, BaseItem> curEquipDict = GetCurEquipDict();
		foreach (int key in curEquipDict.Keys)
		{
			ITEM_INFO iTEM_INFO = new ITEM_INFO();
			iTEM_INFO.uuid = curEquipDict[key].Uid;
			iTEM_INFO.itemId = curEquipDict[key].Id;
			iTEM_INFO.itemCount = 1u;
			iTEM_INFO.Seid = curEquipDict[key].Seid;
			list.Add(iTEM_INFO);
		}
		return list;
	}

	public void SwitchFangAn(int index)
	{
		Dictionary<int, BaseItem> curEquipDict = GetCurEquipDict();
		foreach (int key in curEquipDict.Keys)
		{
			Tools.instance.getPlayer().AddEquip(curEquipDict[key].Id, curEquipDict[key].Uid, curEquipDict[key].Seid);
		}
		CurEquipIndex = index;
		curEquipDict = GetCurEquipDict();
		foreach (int key2 in curEquipDict.Keys)
		{
			Tools.instance.getPlayer().removeItem(curEquipDict[key2].Uid);
		}
	}
}
