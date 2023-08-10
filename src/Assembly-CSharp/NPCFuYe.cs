using System.Collections.Generic;
using JSONClass;

public class NPCFuYe
{
	private Dictionary<int, List<int>> danYaoQualityDictionary = new Dictionary<int, List<int>>();

	private List<int> danYaoQualityList = new List<int> { 10, 15, 30, 50, 70, 100 };

	public void NpcLianDan(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
		int i2 = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"]["21"]["level"].I;
		int num = jsonData.instance.NpcLevelShouYiDate[jSONObject["Level"].I.ToString()]["money"].I;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		int num2 = 0;
		while (num > 0)
		{
			int randomDanYao = GetRandomDanYao(i2 + 1);
			int i3 = jsonData.instance.ItemJsonData[randomDanYao.ToString()]["price"].I;
			num -= i3;
			if (dictionary.ContainsKey(randomDanYao))
			{
				dictionary[randomDanYao]++;
			}
			else
			{
				dictionary.Add(randomDanYao, 1);
			}
			num2 += (int)((float)i3 / (float)danYaoQualityList[i2] * 1f);
		}
		if (num < 0)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, num);
		}
		foreach (int key in dictionary.Keys)
		{
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, key, dictionary[key]);
			NpcJieSuanManager.inst.npcNoteBook.NoteLianDan(npcId, key, i2 + 1, dictionary[key]);
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcId, 21, num2);
		int i4 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["LianDan"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["LianDan"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i4);
	}

	public void NpcCreateZhenQi(int npcId)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"]["10"]["level"].I;
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i2 = jsonData.instance.NpcLevelShouYiDate[jSONObject["Level"].I.ToString()]["money"].I;
		if (i > 0)
		{
			int num = i - 1 + 30002;
			if (i2 >= _ItemJsonData.DataDict[num].price)
			{
				NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, num, 1);
				NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, -_ItemJsonData.DataDict[num].price);
			}
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, (int)((float)jsonData.instance.NpcLevelShouYiDate[jSONObject["Level"].I.ToString()]["money"].I * 1.5f));
	}

	private int GetRandomDanYao(int targetQuality)
	{
		if (targetQuality == 6 && Tools.instance.GetRandomInt(0, 100) > 10)
		{
			targetQuality = 5;
		}
		if (!danYaoQualityDictionary.ContainsKey(targetQuality))
		{
			foreach (string key in jsonData.instance.ItemJsonData.Keys)
			{
				if (jsonData.instance.ItemJsonData[key]["type"].I == 5 && jsonData.instance.ItemJsonData[key]["ShopType"].I != 99)
				{
					int i = jsonData.instance.ItemJsonData[key]["quality"].I;
					if (danYaoQualityDictionary.ContainsKey(i))
					{
						danYaoQualityDictionary[i].Add(int.Parse(key));
						continue;
					}
					danYaoQualityDictionary.Add(i, new List<int> { int.Parse(key) });
				}
			}
		}
		return danYaoQualityDictionary[targetQuality][NpcJieSuanManager.inst.getRandomInt(0, danYaoQualityDictionary[targetQuality].Count - 1)];
	}

	public void NpcLianQi(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
		int i2 = jSONObject["Level"].I;
		int num = NpcJieSuanManager.inst.IsCanChangeEquip(jSONObject);
		if (num == 0)
		{
			num = ((i2 > 7) ? NpcJieSuanManager.inst.getRandomInt(1, 4) : NpcJieSuanManager.inst.getRandomInt(1, 2));
		}
		NpcJieSuanManager.inst.AddNpcEquip(npcId, num, isLianQi: true);
		int i3 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["LianQi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["LianQi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i3);
	}
}
