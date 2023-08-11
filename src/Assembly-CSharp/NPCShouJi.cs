using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

public class NPCShouJi
{
	private List<string> CaiJiMapList = new List<string>();

	public NPCShouJi()
	{
		CaiJiMapList.Add("ThreeSence");
		CaiJiMapList.Add("FuBen");
	}

	public void NpcCaiYao(int npcID)
	{
		int i = jsonData.instance.AvatarJsonData[npcID.ToString()]["Type"].I;
		int randomInt = NpcJieSuanManager.inst.getRandomInt(0, CaiJiMapList.Count - 1);
		string str = jsonData.instance.NPCActionDate["3"][CaiJiMapList[randomInt]].str;
		JSONObject jSONObject = new JSONObject();
		int num = 0;
		try
		{
			List<int> list = new List<int>();
			num = ((str == "ThreeSence") ? ((jsonData.instance.NpcThreeMapBingDate[i.ToString()]["CaiJi"].Count > 0) ? 1 : 2) : ((jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiJi"].I <= 0) ? 1 : 2));
			if (num == 1)
			{
				jSONObject = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["CaiJi"];
				int i2 = jSONObject[NpcJieSuanManager.inst.getRandomInt(0, jSONObject.Count - 1)].I;
				NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
				for (int j = 0; j < jsonData.instance.CaiYaoDiaoLuo.Count; j++)
				{
					if (jsonData.instance.CaiYaoDiaoLuo[j]["ThreeSence"].I != i2 || jsonData.instance.CaiYaoDiaoLuo[j]["type"].I != 1)
					{
						continue;
					}
					for (int k = 1; k <= 8; k++)
					{
						int i3 = jsonData.instance.CaiYaoDiaoLuo[j]["value" + k].I;
						if (i3 > 0)
						{
							list.Add(i3);
						}
					}
				}
			}
			else
			{
				int i4 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiJi"].I;
				string text = "F" + i4;
				int i5 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiJiDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiJiDian"].Count - 1)].I;
				NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcID, i4, i5);
				for (int l = 0; l < jsonData.instance.CaiYaoDiaoLuo.Count; l++)
				{
					if (!(jsonData.instance.CaiYaoDiaoLuo[l]["FuBen"].str == text) || jsonData.instance.CaiYaoDiaoLuo[l]["type"].I != 1)
					{
						continue;
					}
					for (int m = 1; m <= 8; m++)
					{
						int i6 = jsonData.instance.CaiYaoDiaoLuo[l]["value" + m].I;
						if (i6 > 0)
						{
							list.Add(i6);
						}
					}
				}
			}
			foreach (JSONObject item in GetRandomCaiLiao(npcID, list))
			{
				jsonData.instance.AvatarBackpackJsonData[npcID.ToString()]["Backpack"].Add(item);
			}
		}
		catch (Exception)
		{
			Debug.LogError((object)$"npc出错类型:{i}");
		}
	}

	public void NpcCaiKuang(int npcID)
	{
		int i = jsonData.instance.AvatarJsonData[npcID.ToString()]["Type"].I;
		int randomInt = NpcJieSuanManager.inst.getRandomInt(0, CaiJiMapList.Count - 1);
		string str = jsonData.instance.NPCActionDate["4"][CaiJiMapList[randomInt]].str;
		JSONObject jSONObject = new JSONObject();
		int num = 0;
		try
		{
			List<int> list = new List<int>();
			num = ((str == "ThreeSence") ? ((jsonData.instance.NpcThreeMapBingDate[i.ToString()]["CaiKuang"].Count > 0) ? 1 : 2) : ((jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiKuang"].I <= 0) ? 1 : 2));
			if (num == 1)
			{
				jSONObject = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["CaiKuang"];
				if (jSONObject.list.Count == 0)
				{
					Debug.LogError((object)$"NPC采矿异常，jsonData.instance.NpcThreeMapBingDate[{i}][\"CaiKuang\"]为空");
					return;
				}
				int randomInt2 = NpcJieSuanManager.inst.getRandomInt(0, jSONObject.Count - 1);
				int i2 = jSONObject[randomInt2].I;
				NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
				for (int j = 0; j < jsonData.instance.CaiYaoDiaoLuo.Count; j++)
				{
					if (jsonData.instance.CaiYaoDiaoLuo[j]["ThreeSence"].I != i2 || jsonData.instance.CaiYaoDiaoLuo[j]["type"].I != 2)
					{
						continue;
					}
					for (int k = 1; k <= 8; k++)
					{
						int i3 = jsonData.instance.CaiYaoDiaoLuo[j]["value" + k].I;
						if (i3 > 0)
						{
							list.Add(i3);
						}
					}
				}
			}
			else
			{
				int i4 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiKuang"].I;
				string text = "F" + i4;
				int i5 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiKuangDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiKuangDian"].Count - 1)].I;
				NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcID, i4, i5);
				for (int l = 0; l < jsonData.instance.CaiYaoDiaoLuo.Count; l++)
				{
					if (!(jsonData.instance.CaiYaoDiaoLuo[l]["FuBen"].str == text) || jsonData.instance.CaiYaoDiaoLuo[l]["type"].I != 2)
					{
						continue;
					}
					for (int m = 1; m <= 8; m++)
					{
						int i6 = jsonData.instance.CaiYaoDiaoLuo[l]["value" + m].I;
						if (i6 > 0)
						{
							list.Add(i6);
						}
					}
				}
			}
			foreach (JSONObject item in GetRandomCaiLiao(npcID, list))
			{
				jsonData.instance.AvatarBackpackJsonData[npcID.ToString()]["Backpack"].Add(item);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public void NpcBuyMiJi(int npcID)
	{
		int i = jsonData.instance.AvatarJsonData[npcID.ToString()]["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["MiJi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["MiJi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
	}

	public void NpcBuyDanYao(int npcId)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["YaoDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["YaoDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		int npcBigLevel = NpcJieSuanManager.inst.GetNpcBigLevel(npcId);
		for (int j = 0; j < 2; j++)
		{
			JSONObject randomItemByType = FactoryManager.inst.npcFactory.GetRandomItemByType(5, npcBigLevel);
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, randomItemByType["id"].I, 1);
		}
	}

	public void NpcBuyFaBao(int npcID)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcID.ToString()];
		int i = jSONObject["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["FaBao"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["FaBao"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
		int num = NpcJieSuanManager.inst.IsCanChangeEquip(jSONObject);
		if (num != 0)
		{
			NpcJieSuanManager.inst.AddNpcEquip(npcID, num);
		}
	}

	public void NpcShouJiTuPoItem(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1);
	}

	public void NextNpcShouJiTuPoItem(int npcId)
	{
		if (!NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2))
		{
			return;
		}
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I;
		JSONObject jSONObject = jsonData.instance.NPCTuPuoDate[i.ToString()]["ShouJiItem"];
		if (jSONObject.Count >= 1)
		{
			int num = 0;
			int num2 = 0;
			try
			{
				num = jSONObject[NpcJieSuanManager.inst.getRandomInt(0, jSONObject.Count - 1)].I;
				num2 = jsonData.instance.ItemJsonData[num.ToString()]["price"].I;
			}
			catch (Exception)
			{
				Debug.LogError((object)$"收集突破物品ID出错ID:{num}");
				return;
			}
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, num, 1);
			NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, -num2);
		}
	}

	public void NpcSpeedDeath(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1);
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
	}

	public void NpcToLingHe1(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			foreach (int item in NpcFuBenMapBingDate.DataDict[i].LingHeDian1)
			{
				if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(item))
				{
					num = item;
					break;
				}
			}
		}
		else
		{
			num = NpcFuBenMapBingDate.DataDict[i].LingHeDian1[0];
		}
		if (num == 0)
		{
			Debug.LogError((object)"NPC采集灵核1出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	public void NpcToLingHe2(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			foreach (int item in NpcFuBenMapBingDate.DataDict[i].LingHeDian2)
			{
				if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(item))
				{
					num = item;
					break;
				}
			}
		}
		else
		{
			num = NpcFuBenMapBingDate.DataDict[i].LingHeDian2[0];
		}
		if (num == 0)
		{
			Debug.LogError((object)"NPC采集灵核2出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	public void NpcToLingHe3(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			foreach (int item in NpcFuBenMapBingDate.DataDict[i].LingHeDian3)
			{
				if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(item))
				{
					num = item;
					break;
				}
			}
		}
		else
		{
			num = NpcFuBenMapBingDate.DataDict[i].LingHeDian3[0];
		}
		if (num == 0)
		{
			Debug.LogError((object)"NPC采集灵核3出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	public void NpcToLingHe4(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			foreach (int item in NpcFuBenMapBingDate.DataDict[i].LingHeDian4)
			{
				if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(item))
				{
					num = item;
					break;
				}
			}
		}
		else
		{
			num = NpcFuBenMapBingDate.DataDict[i].LingHeDian4[0];
		}
		if (num == 0)
		{
			Debug.LogError((object)"NPC采集灵核4出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	public void NpcToLingHe5(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			foreach (int item in NpcFuBenMapBingDate.DataDict[i].LingHeDian5)
			{
				if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(item))
				{
					num = item;
					break;
				}
			}
		}
		else
		{
			num = NpcFuBenMapBingDate.DataDict[i].LingHeDian5[0];
		}
		if (num == 0)
		{
			Debug.LogError((object)"NPC采集灵核5出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	public void NpcToLingHe6(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			foreach (int item in NpcFuBenMapBingDate.DataDict[i].LingHeDian6)
			{
				if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(item))
				{
					num = item;
					break;
				}
			}
		}
		else
		{
			num = NpcFuBenMapBingDate.DataDict[i].LingHeDian6[0];
		}
		if (num == 0)
		{
			Debug.LogError((object)"NPC采集灵核6出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	public List<JSONObject> GetRandomCaiLiao(int npcID, List<int> itemList)
	{
		int num = jsonData.instance.NpcLevelShouYiDate[jsonData.instance.AvatarJsonData[npcID.ToString()]["Level"].I.ToString()]["money"].I;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		while (num > 0)
		{
			int key = itemList[NpcJieSuanManager.inst.getRandomInt(0, itemList.Count - 1)];
			if (dictionary.ContainsKey(key))
			{
				dictionary[key]++;
			}
			else
			{
				dictionary.Add(key, 1);
			}
			num -= jsonData.instance.ItemJsonData[key.ToString()]["price"].I;
		}
		List<JSONObject> list = new List<JSONObject>();
		foreach (int key2 in dictionary.Keys)
		{
			JSONObject item = jsonData.instance.setAvatarBackpack(Tools.getUUID(), key2, 1, dictionary[key2], 100, 1, Tools.CreateItemSeid(key2));
			list.Add(item);
		}
		return list;
	}
}
