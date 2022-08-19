using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

// Token: 0x02000284 RID: 644
public class NPCUseItem
{
	// Token: 0x06001742 RID: 5954 RVA: 0x0009ED24 File Offset: 0x0009CF24
	public NPCUseItem()
	{
		this.ItemSeidDictionary.Add(4, "exp");
		this.ItemSeidDictionary.Add(5, "shengShi");
		this.ItemSeidDictionary.Add(6, "HP");
		this.ItemSeidDictionary.Add(7, "shouYuan");
		this.ItemSeidDictionary.Add(9, "ziZhi");
		this.ItemSeidDictionary.Add(10, "wuXin");
		this.ItemSeidDictionary.Add(11, "dunSu");
		this.ItemSeidDictionary.Add(25, "wuDaoExp");
		this.ItemSeidDictionary.Add(26, "wuDaoDian");
	}

	// Token: 0x06001743 RID: 5955 RVA: 0x0009EDF8 File Offset: 0x0009CFF8
	public void autoUseItem(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		this.deleteList = new List<JSONObject>();
		this.addList = new List<JSONObject>();
		for (int i = 0; i < jsonobject.Count; i++)
		{
			this.UseItem(npcId, jsonobject[i], true);
		}
		for (int j = 0; j < this.deleteList.Count; j++)
		{
			jsonobject.list.Remove(this.deleteList[j]);
		}
		for (int k = 0; k < this.addList.Count; k++)
		{
			jsonobject.Add(this.addList[k]);
		}
		this.deleteList = null;
		this.addList = null;
	}

	// Token: 0x06001744 RID: 5956 RVA: 0x0009EEC0 File Offset: 0x0009D0C0
	public void UseItem(int npcID, JSONObject item, bool isAuToUse = false)
	{
		if (!isAuToUse)
		{
			this.deleteList = new List<JSONObject>();
			this.addList = new List<JSONObject>();
		}
		if (item["Num"].I <= 0)
		{
			this.deleteList.Add(item);
			return;
		}
		int i = item["ItemID"].I;
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcID.ToString()];
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[i];
		int type = itemJsonData.type;
		int canUse = itemJsonData.CanUse;
		if (type == 0 || type == 1 || type == 2)
		{
			this.UseEquip(npcID, item, jsonobject["equipList"], type);
		}
		else if (type == 5 && itemJsonData.NPCCanUse == 1)
		{
			this.UseDanYao(npcID, item, itemJsonData.seid[0]);
		}
		if (!isAuToUse)
		{
			JSONObject jsonobject2 = jsonData.instance.AvatarBackpackJsonData[npcID.ToString()]["Backpack"];
			for (int j = 0; j < this.deleteList.Count; j++)
			{
				jsonobject2.list.Remove(this.deleteList[j]);
			}
			for (int k = 0; k < this.addList.Count; k++)
			{
				jsonobject2.Add(this.addList[k]);
			}
			this.deleteList = null;
			this.addList = null;
		}
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x0009F024 File Offset: 0x0009D224
	public bool isCanChangEquip(JSONObject npcPianHao, JSONObject oldSeid, JSONObject NewSeid)
	{
		if (oldSeid["quality"].I == NewSeid["quality"].I)
		{
			if (NewSeid["QPingZhi"].I >= oldSeid["QPingZhi"].I)
			{
				for (int i = 0; i < NewSeid["shuXingIdList"].list.Count; i++)
				{
					if (npcPianHao.ToList().Contains(NewSeid["shuXingIdList"][i].I))
					{
						return true;
					}
				}
			}
			return false;
		}
		if (NewSeid["quality"].I > oldSeid["quality"].I)
		{
			List<int> list = NewSeid["shuXingIdList"].ToList();
			npcPianHao.ToList();
			for (int j = 0; j < NewSeid["shuXingIdList"].list.Count; j++)
			{
				int item = list[j];
				if (npcPianHao.ToList().Contains(item))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x0009F130 File Offset: 0x0009D330
	public int GetDanYaoCanUseNum(int npcId, int itemId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = _ItemJsonData.DataDict[itemId].CanUse;
		if (jsonobject["wuDaoSkillList"].ToList().Contains(2131))
		{
			num *= 2;
		}
		if (jsonobject.HasField("useItem") && jsonobject["useItem"].HasField(itemId.ToString()))
		{
			int i = jsonobject["useItem"][itemId.ToString()].I;
			num -= i;
		}
		int num2 = _ItemJsonData.DataDict[itemId].seid[0];
		if (num2 == 4)
		{
			if (jsonobject.TryGetField("Level").I >= 15)
			{
				num = 0;
			}
			else
			{
				int i2 = jsonData.instance.ItemsSeidJsonData[num2][itemId.ToString()]["value1"].I;
				int i3 = jsonobject.TryGetField("exp").I;
				int num3 = jsonobject.TryGetField("NextExp").I - i3;
				if (num3 <= 0)
				{
					num = 0;
				}
				else
				{
					num = Mathf.Min(num, num3 / i2 + 1);
				}
			}
		}
		return num;
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x0009F268 File Offset: 0x0009D468
	private void UseDanYao(int npcID, JSONObject item, int seid)
	{
		int i = item["ItemID"].I;
		int i2 = item["Num"].I;
		int num = i2;
		int danYaoCanUseNum = this.GetDanYaoCanUseNum(npcID, i);
		if (danYaoCanUseNum <= 0)
		{
			return;
		}
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcID.ToString()];
		if (!ModVar.closeNpcNaiYao)
		{
			if (jsonobject["useItem"].HasField(i.ToString()))
			{
				num = danYaoCanUseNum;
				if (num > i2)
				{
					num = i2;
				}
				jsonobject["useItem"].SetField(i.ToString(), jsonobject["useItem"][i.ToString()].I + num);
			}
			else
			{
				num = danYaoCanUseNum;
				if (num > i2)
				{
					num = i2;
				}
				jsonobject["useItem"].SetField(i.ToString(), num);
			}
		}
		int num2 = i2 - num;
		if (num2 == 0)
		{
			this.deleteList.Add(item);
		}
		else
		{
			item.SetField("Num", num2);
		}
		int num3 = jsonData.instance.ItemsSeidJsonData[seid][i.ToString()]["value1"].I * num;
		switch (seid)
		{
		case 4:
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcID, num3);
			break;
		case 5:
			NpcJieSuanManager.inst.npcSetField.AddNpcShenShi(npcID, num3);
			break;
		case 6:
			NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcID, num3);
			break;
		case 7:
			NpcJieSuanManager.inst.npcSetField.AddNpcShouYuan(npcID, num3);
			break;
		case 8:
			break;
		case 9:
			NpcJieSuanManager.inst.npcSetField.AddNpcZhiZi(npcID, num3);
			break;
		case 10:
			NpcJieSuanManager.inst.npcSetField.AddNpcWuXing(npcID, num3);
			break;
		case 11:
			NpcJieSuanManager.inst.npcSetField.AddNpcDunSu(npcID, num3);
			break;
		default:
			if (seid != 25)
			{
				if (seid == 26)
				{
					NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoDian(npcID, num3);
				}
			}
			else
			{
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcID, jsonData.instance.ItemsSeidJsonData[seid][i.ToString()]["value1"].I, jsonData.instance.ItemsSeidJsonData[seid][i.ToString()]["value2"].I * num);
			}
			break;
		}
		if (seid == 25)
		{
			seid = 12;
		}
		else if (seid == 26)
		{
			seid = 13;
		}
		NpcJieSuanManager.inst.npcNoteBook.NoteUseDanYao(npcID, seid, i, num);
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x0009F508 File Offset: 0x0009D708
	private void UseEquip(int npcID, JSONObject item, JSONObject equipList, int itemType)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcID);
		string text = "";
		string text2 = "";
		int i = item["ItemID"].I;
		switch (itemType)
		{
		case 0:
			text = "equipWeapon";
			break;
		case 1:
			text = "equipClothing";
			break;
		case 2:
			text = "equipRing";
			break;
		}
		switch (itemType)
		{
		case 0:
			text2 = "Weapon1";
			break;
		case 1:
			text2 = "Clothing";
			break;
		case 2:
			text2 = "Ring";
			break;
		}
		if (text == "equipWeapon" && NpcJieSuanManager.inst.GetChangeEquipWeapon(npcData) == 4)
		{
			text2 = "Weapon2";
		}
		if (i >= 18001 && i <= 18010)
		{
			JSONObject jsonobject = equipList[text2];
			JSONObject jsonobject2 = item["Seid"];
			if (jsonobject2 == null)
			{
				return;
			}
			if (jsonobject != null && jsonobject.HasField("SeidDesc"))
			{
				JSONObject jsonobject3 = new JSONObject();
				for (int j = 0; j < jsonData.instance.NPCLeiXingDate.Count; j++)
				{
					if (jsonData.instance.NPCLeiXingDate[j]["Type"].I == npcData["Type"].I && jsonData.instance.NPCLeiXingDate[j]["LiuPai"].I == npcData["LiuPai"].I && jsonData.instance.NPCLeiXingDate[j]["Level"].I == npcData["Level"].I)
					{
						jsonobject3 = new JSONObject(jsonData.instance.NPCLeiXingDate[j].ToString(), -2, false, false);
						break;
					}
				}
				JSONObject npcPianHao = jsonobject3[text];
				if (!this.isCanChangEquip(npcPianHao, jsonobject, jsonobject2))
				{
					return;
				}
				jsonData.instance.AvatarJsonData[npcID.ToString()]["equipList"].SetField(text2, jsonobject2);
			}
			else
			{
				jsonData.instance.AvatarJsonData[npcID.ToString()]["equipList"].SetField(text2, jsonobject2);
			}
			this.deleteList.Add(item);
			if (jsonobject != null && jsonobject.HasField("Money"))
			{
				NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcID, jsonobject["Money"].I);
			}
		}
	}

	// Token: 0x040011FE RID: 4606
	private Dictionary<int, string> ItemSeidDictionary = new Dictionary<int, string>();

	// Token: 0x040011FF RID: 4607
	private List<JSONObject> deleteList = new List<JSONObject>();

	// Token: 0x04001200 RID: 4608
	private List<JSONObject> addList = new List<JSONObject>();
}
