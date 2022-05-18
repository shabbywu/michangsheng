using System;
using System.Collections.Generic;
using JSONClass;

// Token: 0x02000329 RID: 809
public class NPCFuYe
{
	// Token: 0x060017CF RID: 6095 RVA: 0x000CF878 File Offset: 0x000CDA78
	public void NpcLianDan(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
		int i2 = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"]["21"]["level"].I;
		int j = jsonData.instance.NpcLevelShouYiDate[jsonobject["Level"].I.ToString()]["money"].I;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		int num = 0;
		while (j > 0)
		{
			int randomDanYao = this.GetRandomDanYao(i2 + 1);
			int i3 = jsonData.instance.ItemJsonData[randomDanYao.ToString()]["price"].I;
			j -= i3;
			if (dictionary.ContainsKey(randomDanYao))
			{
				Dictionary<int, int> dictionary2 = dictionary;
				int key = randomDanYao;
				dictionary2[key]++;
			}
			else
			{
				dictionary.Add(randomDanYao, 1);
			}
			num += (int)((float)i3 / (float)this.danYaoQualityList[i2] * 1f);
		}
		if (j < 0)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, j);
		}
		foreach (int num2 in dictionary.Keys)
		{
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, num2, dictionary[num2], null, false);
			NpcJieSuanManager.inst.npcNoteBook.NoteLianDan(npcId, num2, i2 + 1, dictionary[num2]);
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcId, 21, num);
		int i4 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["LianDan"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["LianDan"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i4);
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x000CFAE4 File Offset: 0x000CDCE4
	public void NpcCreateZhenQi(int npcId)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"]["10"]["level"].I;
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i2 = jsonData.instance.NpcLevelShouYiDate[jsonobject["Level"].I.ToString()]["money"].I;
		if (i > 0)
		{
			int num = i - 1 + 30002;
			if (i2 >= _ItemJsonData.DataDict[num].price)
			{
				NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, num, 1, null, false);
				NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, -_ItemJsonData.DataDict[num].price);
			}
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, (int)((float)jsonData.instance.NpcLevelShouYiDate[jsonobject["Level"].I.ToString()]["money"].I * 1.5f));
	}

	// Token: 0x060017D1 RID: 6097 RVA: 0x000CFC20 File Offset: 0x000CDE20
	private int GetRandomDanYao(int targetQuality)
	{
		if (targetQuality == 6 && Tools.instance.GetRandomInt(0, 100) > 10)
		{
			targetQuality = 5;
		}
		if (!this.danYaoQualityDictionary.ContainsKey(targetQuality))
		{
			foreach (string text in jsonData.instance.ItemJsonData.Keys)
			{
				if (jsonData.instance.ItemJsonData[text]["type"].I == 5)
				{
					int i = jsonData.instance.ItemJsonData[text]["quality"].I;
					if (this.danYaoQualityDictionary.ContainsKey(i))
					{
						this.danYaoQualityDictionary[i].Add(int.Parse(text));
					}
					else
					{
						this.danYaoQualityDictionary.Add(i, new List<int>
						{
							int.Parse(text)
						});
					}
				}
			}
		}
		return this.danYaoQualityDictionary[targetQuality][NpcJieSuanManager.inst.getRandomInt(0, this.danYaoQualityDictionary[targetQuality].Count - 1)];
	}

	// Token: 0x060017D2 RID: 6098 RVA: 0x000CFD5C File Offset: 0x000CDF5C
	public void NpcLianQi(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
		int i2 = jsonobject["Level"].I;
		int num = NpcJieSuanManager.inst.IsCanChangeEquip(jsonobject);
		if (num == 0)
		{
			if (i2 <= 7)
			{
				num = NpcJieSuanManager.inst.getRandomInt(1, 2);
			}
			else
			{
				num = NpcJieSuanManager.inst.getRandomInt(1, 4);
			}
		}
		NpcJieSuanManager.inst.AddNpcEquip(npcId, num, true);
		int i3 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["LianQi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["LianQi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i3);
	}

	// Token: 0x04001330 RID: 4912
	private Dictionary<int, List<int>> danYaoQualityDictionary = new Dictionary<int, List<int>>();

	// Token: 0x04001331 RID: 4913
	private List<int> danYaoQualityList = new List<int>
	{
		10,
		15,
		30,
		50,
		70,
		100
	};
}
