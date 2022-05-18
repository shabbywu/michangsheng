using System;
using System.Collections.Generic;
using Bag;
using KBEngine;

// Token: 0x02000535 RID: 1333
[Serializable]
public class FangAnData
{
	// Token: 0x06002205 RID: 8709 RVA: 0x00119FD0 File Offset: 0x001181D0
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

	// Token: 0x06002206 RID: 8710 RVA: 0x0011A080 File Offset: 0x00118280
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

	// Token: 0x06002207 RID: 8711 RVA: 0x0001BE84 File Offset: 0x0001A084
	public Dictionary<int, BaseItem> GetCurEquipDict()
	{
		if (!this.EquipDictionary.ContainsKey(this.CurEquipIndex))
		{
			this.EquipDictionary.Add(this.CurEquipIndex, new Dictionary<int, BaseItem>());
		}
		return this.EquipDictionary[this.CurEquipIndex];
	}

	// Token: 0x06002208 RID: 8712 RVA: 0x0011A130 File Offset: 0x00118330
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

	// Token: 0x06002209 RID: 8713 RVA: 0x0011A1DC File Offset: 0x001183DC
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

	// Token: 0x04001D78 RID: 7544
	public Dictionary<int, Dictionary<int, BaseItem>> EquipDictionary = new Dictionary<int, Dictionary<int, BaseItem>>();

	// Token: 0x04001D79 RID: 7545
	public int CurEquipIndex = 1;
}
