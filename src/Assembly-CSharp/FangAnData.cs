using System;
using System.Collections.Generic;
using Bag;
using KBEngine;

// Token: 0x020003AA RID: 938
[Serializable]
public class FangAnData
{
	// Token: 0x06001E82 RID: 7810 RVA: 0x000D68C4 File Offset: 0x000D4AC4
	public void SaveHandle()
	{
		foreach (int key in this.EquipDictionary.Keys)
		{
			foreach (int key2 in this.EquipDictionary[key].Keys)
			{
				this.EquipDictionary[key][key2].HandleSeid();
			}
		}
	}

	// Token: 0x06001E83 RID: 7811 RVA: 0x000D6974 File Offset: 0x000D4B74
	public void LoadHandle()
	{
		foreach (int key in this.EquipDictionary.Keys)
		{
			foreach (int key2 in this.EquipDictionary[key].Keys)
			{
				this.EquipDictionary[key][key2].LoadSeid();
			}
		}
	}

	// Token: 0x06001E84 RID: 7812 RVA: 0x000D6A24 File Offset: 0x000D4C24
	public Dictionary<int, BaseItem> GetCurEquipDict()
	{
		if (!this.EquipDictionary.ContainsKey(this.CurEquipIndex))
		{
			this.EquipDictionary.Add(this.CurEquipIndex, new Dictionary<int, BaseItem>());
		}
		return this.EquipDictionary[this.CurEquipIndex];
	}

	// Token: 0x06001E85 RID: 7813 RVA: 0x000D6A60 File Offset: 0x000D4C60
	public List<ITEM_INFO> CurEquipDictToOldList()
	{
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		Dictionary<int, BaseItem> curEquipDict = this.GetCurEquipDict();
		foreach (int key in curEquipDict.Keys)
		{
			list.Add(new ITEM_INFO
			{
				uuid = curEquipDict[key].Uid,
				itemId = curEquipDict[key].Id,
				itemCount = 1U,
				Seid = curEquipDict[key].Seid
			});
		}
		return list;
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x000D6B0C File Offset: 0x000D4D0C
	public void SwitchFangAn(int index)
	{
		Dictionary<int, BaseItem> curEquipDict = this.GetCurEquipDict();
		foreach (int key in curEquipDict.Keys)
		{
			Tools.instance.getPlayer().AddEquip(curEquipDict[key].Id, curEquipDict[key].Uid, curEquipDict[key].Seid);
		}
		this.CurEquipIndex = index;
		curEquipDict = this.GetCurEquipDict();
		foreach (int key2 in curEquipDict.Keys)
		{
			Tools.instance.getPlayer().removeItem(curEquipDict[key2].Uid);
		}
	}

	// Token: 0x04001904 RID: 6404
	public Dictionary<int, Dictionary<int, BaseItem>> EquipDictionary = new Dictionary<int, Dictionary<int, BaseItem>>();

	// Token: 0x04001905 RID: 6405
	public int CurEquipIndex = 1;
}
