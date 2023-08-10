using System.Collections.Generic;
using JSONClass;

public class NpcTianJiGe
{
	private List<int> itemTypeList = new List<int>();

	private List<int> itemQualityList = new List<int>();

	public void TianJiGePaoShang(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1);
	}

	public void TianJiGeJinHuo(int npcId)
	{
		NpcJinHuoData npcJinHuoData = NpcJinHuoData.DataDict[1];
		if (itemTypeList.Count < 1)
		{
			for (int i = 0; i < npcJinHuoData.Type.Count; i++)
			{
				for (int j = 0; j < npcJinHuoData.quanzhong[i]; j++)
				{
					itemTypeList.Add(npcJinHuoData.Type[i]);
					itemQualityList.Add(npcJinHuoData.quality[i]);
				}
			}
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		int num = 0;
		int num2 = 0;
		List<int> list = new List<int>();
		for (int k = 1; k <= 30; k++)
		{
			num2 = NpcJieSuanManager.inst.getRandomInt(0, itemTypeList.Count - 1);
			num = FactoryManager.inst.npcFactory.GetRandomItemByShopType(itemTypeList[num2], itemQualityList[num2])["id"].I;
			if (jsonData.instance.ItemJsonData[num.ToString()]["maxNum"].I == 1)
			{
				list.Add(num);
			}
			else if (dictionary.ContainsKey(num))
			{
				dictionary[num]++;
			}
			else
			{
				dictionary.Add(num, 1);
			}
		}
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()].SetField("Backpack", JSONObject.arr);
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()].SetField("money", 200000);
		foreach (int key in dictionary.Keys)
		{
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, key, dictionary[key]);
		}
		foreach (int item in list)
		{
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, item, 1);
		}
	}
}
