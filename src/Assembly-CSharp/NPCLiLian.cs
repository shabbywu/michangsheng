using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCLiLian
{
	private Dictionary<int, Dictionary<int, List<int>>> yaoShouDropDictionary;

	private Dictionary<int, Dictionary<int, int>> qiYuQuanZhongDictionary;

	public NPCLiLian()
	{
		yaoShouDropDictionary = new Dictionary<int, Dictionary<int, List<int>>>();
		qiYuQuanZhongDictionary = new Dictionary<int, Dictionary<int, int>>();
		yaoShouDropDictionary.Add(1, new Dictionary<int, List<int>>());
		yaoShouDropDictionary.Add(2, new Dictionary<int, List<int>>());
		qiYuQuanZhongDictionary.Add(1, new Dictionary<int, int>());
		qiYuQuanZhongDictionary.Add(2, new Dictionary<int, int>());
		qiYuQuanZhongDictionary.Add(3, new Dictionary<int, int>());
		qiYuQuanZhongDictionary.Add(4, new Dictionary<int, int>());
		qiYuQuanZhongDictionary.Add(5, new Dictionary<int, int>());
	}

	public void NPCNingZhouKillYaoShou(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1);
		NPCGetYaoShouDrop(npcId, 1);
	}

	public void NPCHaiShangKillYaoShou(int npcId)
	{
		NPCGetYaoShouDrop(npcId, 2);
	}

	public void NPCDoZhuChengTask(int npcId)
	{
		_ = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].n;
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, jsonData.instance.NpcLevelShouYiDate[jSONObject["Level"].I.ToString()]["money"].I);
	}

	public void NPCDoMenPaiTask(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, jsonData.instance.NpcLevelShouYiDate[jSONObject["Level"].I.ToString()]["money"].I);
		NpcJieSuanManager.inst.npcSetField.NpcAddGongXian(npcId, jsonData.instance.NpcLevelShouYiDate[jSONObject["Level"].I.ToString()]["gongxian"].I);
		if (NpcJieSuanManager.inst.getRandomInt(0, 100) > 50)
		{
			try
			{
				NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 2);
				return;
			}
			catch (Exception)
			{
				Debug.LogError((object)string.Format("错误的Npc行为，行为ID35,npc类型：{0},npcId:{1}", jSONObject["Type"].I, npcId));
				return;
			}
		}
		int i = jSONObject["Type"].I;
		int i2 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["XunLuo"].I;
		if (i2 == 0)
		{
			Debug.LogError((object)$"错误的Npc行为，行为ID35,npc类型：{i},npcId:{npcId}");
			return;
		}
		_ = "F" + i2;
		int i3 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["XunLuoDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["XunLuoDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, i2, i3);
	}

	public void NPCNingZhouYouLi(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1);
		NPCYouLi(npcId);
	}

	public void NpcHaiShangYouLi(int npcId)
	{
		NPCYouLi(npcId);
	}

	private void NPCYouLi(int npcId)
	{
		NpcJieSuanManager.inst.GetNpcData(npcId);
		if (NpcJieSuanManager.inst.getRandomInt(1, 100) > 40)
		{
			return;
		}
		if (qiYuQuanZhongDictionary[1].Count < 1)
		{
			foreach (JSONObject item in jsonData.instance.NpcQiYuDate.list)
			{
				for (int i = 0; i < item["JingJie"].Count; i++)
				{
					qiYuQuanZhongDictionary[item["JingJie"][i].I].Add(item["id"].I, item["quanzhong"].I);
				}
			}
		}
		int randomQiYu = GetRandomQiYu(NpcJieSuanManager.inst.GetNpcBigLevel(npcId));
		JSONObject jSONObject = jsonData.instance.NpcQiYuDate[randomQiYu.ToString()];
		int num = -1;
		int num2 = -1;
		if (jSONObject["ZhuangTai"].I > 0)
		{
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, jSONObject["ZhuangTai"].I);
		}
		if (jSONObject["Item"].Count > 0)
		{
			num = FactoryManager.inst.npcFactory.GetRandomItemByShopType(jSONObject["Item"][0].I, jSONObject["Item"][1].I)["id"].I;
			num2 = jSONObject["Itemnum"].I;
			if (jSONObject["Itemnum"].I > 0)
			{
				NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, num, num2);
			}
		}
		if (jSONObject["XiuWei"].I > 0)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, jSONObject["XiuWei"].I);
		}
		if (jSONObject["XueLiang"].I > 0)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, jSONObject["XueLiang"].I);
		}
		NpcJieSuanManager.inst.npcNoteBook.NoteQiYu(npcId, randomQiYu, num, num2);
	}

	public int GetRandomQiYu(int bigLevel)
	{
		int num = 0;
		int num2 = 0;
		int result = 0;
		foreach (int key in qiYuQuanZhongDictionary[bigLevel].Keys)
		{
			num += qiYuQuanZhongDictionary[bigLevel][key];
		}
		int randomInt = NpcJieSuanManager.inst.getRandomInt(1, num);
		foreach (int key2 in qiYuQuanZhongDictionary[bigLevel].Keys)
		{
			num2 += qiYuQuanZhongDictionary[bigLevel][key2];
			if (num2 >= randomInt)
			{
				result = key2;
				break;
			}
		}
		return result;
	}

	private void NPCGetYaoShouDrop(int npcId, int yaoShouType)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = NpcJieSuanManager.inst.GetNpcBigLevel(npcId);
		if (num >= 5)
		{
			num = 4;
		}
		int num2 = jsonData.instance.NpcLevelShouYiDate[jSONObject["Level"].I.ToString()]["money"].I;
		if (yaoShouDropDictionary[1].Count < 1)
		{
			foreach (JSONObject item in jsonData.instance.NpcYaoShouDrop.list)
			{
				int i = item["jingjie"].I;
				if (item["NingZhou"].I == 1)
				{
					if (yaoShouDropDictionary[1].ContainsKey(i))
					{
						for (int j = 0; j < item["chanchu"].Count; j++)
						{
							yaoShouDropDictionary[1][i].Add(item["chanchu"][j].I);
						}
					}
					else
					{
						List<int> list = new List<int>();
						for (int k = 0; k < item["chanchu"].Count; k++)
						{
							list.Add(item["chanchu"][k].I);
						}
						yaoShouDropDictionary[1].Add(i, list);
					}
				}
				if (item["HaiShang"].I != 1)
				{
					continue;
				}
				if (yaoShouDropDictionary[2].ContainsKey(i))
				{
					for (int l = 0; l < item["chanchu"].Count; l++)
					{
						yaoShouDropDictionary[2][i].Add(item["chanchu"][l].I);
					}
					continue;
				}
				List<int> list2 = new List<int>();
				for (int m = 0; m < item["chanchu"].Count; m++)
				{
					list2.Add(item["chanchu"][m].I);
				}
				yaoShouDropDictionary[2].Add(i, list2);
			}
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		while (num2 > 0)
		{
			int key = 0;
			try
			{
				key = yaoShouDropDictionary[yaoShouType][num][NpcJieSuanManager.inst.getRandomInt(0, yaoShouDropDictionary[yaoShouType][num].Count - 1)];
			}
			catch (Exception)
			{
				Debug.LogError((object)$"出错yaoShouType:{yaoShouType},npcBigLevl{num}");
			}
			if (dictionary.ContainsKey(key))
			{
				dictionary[key]++;
			}
			else
			{
				dictionary.Add(key, 1);
			}
			int i2 = jsonData.instance.ItemJsonData[key.ToString()]["price"].I;
			num2 -= i2;
		}
		foreach (int key2 in dictionary.Keys)
		{
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, key2, dictionary[key2]);
		}
	}
}
