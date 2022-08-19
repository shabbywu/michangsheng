using System;
using System.Collections.Generic;
using JSONClass;

// Token: 0x02000227 RID: 551
public class NpcTianJiGe
{
	// Token: 0x060015DE RID: 5598 RVA: 0x00090158 File Offset: 0x0008E358
	public void TianJiGePaoShang(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1, true);
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x00092FFC File Offset: 0x000911FC
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

	// Token: 0x04001044 RID: 4164
	private List<int> itemTypeList = new List<int>();

	// Token: 0x04001045 RID: 4165
	private List<int> itemQualityList = new List<int>();
}
