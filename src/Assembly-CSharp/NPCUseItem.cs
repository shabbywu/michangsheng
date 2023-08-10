using System.Collections.Generic;
using JSONClass;
using UnityEngine;

public class NPCUseItem
{
	private Dictionary<int, string> ItemSeidDictionary = new Dictionary<int, string>();

	private List<JSONObject> deleteList = new List<JSONObject>();

	private List<JSONObject> addList = new List<JSONObject>();

	public NPCUseItem()
	{
		ItemSeidDictionary.Add(4, "exp");
		ItemSeidDictionary.Add(5, "shengShi");
		ItemSeidDictionary.Add(6, "HP");
		ItemSeidDictionary.Add(7, "shouYuan");
		ItemSeidDictionary.Add(9, "ziZhi");
		ItemSeidDictionary.Add(10, "wuXin");
		ItemSeidDictionary.Add(11, "dunSu");
		ItemSeidDictionary.Add(25, "wuDaoExp");
		ItemSeidDictionary.Add(26, "wuDaoDian");
	}

	public void autoUseItem(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		deleteList = new List<JSONObject>();
		addList = new List<JSONObject>();
		for (int i = 0; i < jSONObject.Count; i++)
		{
			UseItem(npcId, jSONObject[i], isAuToUse: true);
		}
		for (int j = 0; j < deleteList.Count; j++)
		{
			jSONObject.list.Remove(deleteList[j]);
		}
		for (int k = 0; k < addList.Count; k++)
		{
			jSONObject.Add(addList[k]);
		}
		deleteList = null;
		addList = null;
	}

	public void UseItem(int npcID, JSONObject item, bool isAuToUse = false)
	{
		if (!isAuToUse)
		{
			deleteList = new List<JSONObject>();
			addList = new List<JSONObject>();
		}
		if (item["Num"].I <= 0)
		{
			deleteList.Add(item);
			return;
		}
		int i = item["ItemID"].I;
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcID.ToString()];
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[i];
		int type = itemJsonData.type;
		_ = itemJsonData.CanUse;
		switch (type)
		{
		case 0:
		case 1:
		case 2:
			UseEquip(npcID, item, jSONObject["equipList"], type);
			break;
		case 5:
			if (itemJsonData.NPCCanUse == 1)
			{
				UseDanYao(npcID, item, itemJsonData.seid[0]);
			}
			break;
		}
		if (!isAuToUse)
		{
			JSONObject jSONObject2 = jsonData.instance.AvatarBackpackJsonData[npcID.ToString()]["Backpack"];
			for (int j = 0; j < deleteList.Count; j++)
			{
				jSONObject2.list.Remove(deleteList[j]);
			}
			for (int k = 0; k < addList.Count; k++)
			{
				jSONObject2.Add(addList[k]);
			}
			deleteList = null;
			addList = null;
		}
	}

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

	public int GetDanYaoCanUseNum(int npcId, int itemId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = _ItemJsonData.DataDict[itemId].CanUse;
		if (jSONObject["wuDaoSkillList"].ToList().Contains(2131))
		{
			num *= 2;
		}
		if (jSONObject.HasField("useItem") && jSONObject["useItem"].HasField(itemId.ToString()))
		{
			int i = jSONObject["useItem"][itemId.ToString()].I;
			num -= i;
		}
		int num2 = _ItemJsonData.DataDict[itemId].seid[0];
		if (num2 == 4)
		{
			if (jSONObject.TryGetField("Level").I >= 15)
			{
				num = 0;
			}
			else
			{
				int i2 = jsonData.instance.ItemsSeidJsonData[num2][itemId.ToString()]["value1"].I;
				int i3 = jSONObject.TryGetField("exp").I;
				int num3 = jSONObject.TryGetField("NextExp").I - i3;
				num = ((num3 > 0) ? Mathf.Min(num, num3 / i2 + 1) : 0);
			}
		}
		return num;
	}

	private void UseDanYao(int npcID, JSONObject item, int seid)
	{
		int i = item["ItemID"].I;
		int i2 = item["Num"].I;
		int num = i2;
		int danYaoCanUseNum = GetDanYaoCanUseNum(npcID, i);
		if (danYaoCanUseNum <= 0)
		{
			return;
		}
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcID.ToString()];
		if (!ModVar.closeNpcNaiYao)
		{
			if (jSONObject["useItem"].HasField(i.ToString()))
			{
				num = danYaoCanUseNum;
				if (num > i2)
				{
					num = i2;
				}
				jSONObject["useItem"].SetField(i.ToString(), jSONObject["useItem"][i.ToString()].I + num);
			}
			else
			{
				num = danYaoCanUseNum;
				if (num > i2)
				{
					num = i2;
				}
				jSONObject["useItem"].SetField(i.ToString(), num);
			}
		}
		int num2 = i2 - num;
		if (num2 == 0)
		{
			deleteList.Add(item);
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
		case 9:
			NpcJieSuanManager.inst.npcSetField.AddNpcZhiZi(npcID, num3);
			break;
		case 10:
			NpcJieSuanManager.inst.npcSetField.AddNpcWuXing(npcID, num3);
			break;
		case 11:
			NpcJieSuanManager.inst.npcSetField.AddNpcDunSu(npcID, num3);
			break;
		case 25:
			NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcID, jsonData.instance.ItemsSeidJsonData[seid][i.ToString()]["value1"].I, jsonData.instance.ItemsSeidJsonData[seid][i.ToString()]["value2"].I * num);
			break;
		case 26:
			NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoDian(npcID, num3);
			break;
		}
		switch (seid)
		{
		case 25:
			seid = 12;
			break;
		case 26:
			seid = 13;
			break;
		}
		NpcJieSuanManager.inst.npcNoteBook.NoteUseDanYao(npcID, seid, i, num);
	}

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
		if (i < 18001 || i > 18010)
		{
			return;
		}
		JSONObject jSONObject = equipList[text2];
		JSONObject jSONObject2 = item["Seid"];
		if (jSONObject2 == null)
		{
			return;
		}
		if (jSONObject != null && jSONObject.HasField("SeidDesc"))
		{
			JSONObject jSONObject3 = new JSONObject();
			for (int j = 0; j < jsonData.instance.NPCLeiXingDate.Count; j++)
			{
				if (jsonData.instance.NPCLeiXingDate[j]["Type"].I == npcData["Type"].I && jsonData.instance.NPCLeiXingDate[j]["LiuPai"].I == npcData["LiuPai"].I && jsonData.instance.NPCLeiXingDate[j]["Level"].I == npcData["Level"].I)
				{
					jSONObject3 = new JSONObject(jsonData.instance.NPCLeiXingDate[j].ToString());
					break;
				}
			}
			JSONObject npcPianHao = jSONObject3[text];
			if (!isCanChangEquip(npcPianHao, jSONObject, jSONObject2))
			{
				return;
			}
			jsonData.instance.AvatarJsonData[npcID.ToString()]["equipList"].SetField(text2, jSONObject2);
		}
		else
		{
			jsonData.instance.AvatarJsonData[npcID.ToString()]["equipList"].SetField(text2, jSONObject2);
		}
		deleteList.Add(item);
		if (jSONObject != null && jSONObject.HasField("Money"))
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcID, jSONObject["Money"].I);
		}
	}
}
