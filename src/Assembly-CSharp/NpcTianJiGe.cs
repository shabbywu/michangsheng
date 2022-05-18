using System;
using System.Collections.Generic;
using JSONClass;

// Token: 0x02000343 RID: 835
public class NpcTianJiGe
{
	// Token: 0x06001896 RID: 6294 RVA: 0x00015250 File Offset: 0x00013450
	public void TianJiGePaoShang(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1, true);
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x000DB850 File Offset: 0x000D9A50
	public void TianJiGeJinHuo(int npcId)
	{
		NpcJinHuoData npcJinHuoData = NpcJinHuoData.DataDict[1];
		if (this.itemTypeList.Count < 1)
		{
			for (int i = 0; i < npcJinHuoData.Type.Count; i++)
			{
				for (int j = 0; j < npcJinHuoData.quanzhong[i]; j++)
				{
					this.itemTypeList.Add(npcJinHuoData.Type[i]);
					this.itemQualityList.Add(npcJinHuoData.quality[i]);
				}
			}
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		List<int> list = new List<int>();
		for (int k = 1; k <= 30; k++)
		{
			int randomInt = NpcJieSuanManager.inst.getRandomInt(0, this.itemTypeList.Count - 1);
			int i2 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(this.itemTypeList[randomInt], this.itemQualityList[randomInt])["id"].I;
			if (jsonData.instance.ItemJsonData[i2.ToString()]["maxNum"].I == 1)
			{
				list.Add(i2);
			}
			else if (dictionary.ContainsKey(i2))
			{
				Dictionary<int, int> dictionary2 = dictionary;
				int key = i2;
				int num = dictionary2[key];
				dictionary2[key] = num + 1;
			}
			else
			{
				dictionary.Add(i2, 1);
			}
		}
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()].SetField("Backpack", JSONObject.arr);
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()].SetField("money", 200000);
		foreach (int num2 in dictionary.Keys)
		{
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, num2, dictionary[num2], null, false);
		}
		foreach (int itemID in list)
		{
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, itemID, 1, null, false);
		}
	}

	// Token: 0x0400139C RID: 5020
	private List<int> itemTypeList = new List<int>();

	// Token: 0x0400139D RID: 5021
	private List<int> itemQualityList = new List<int>();
}
