using System.Collections.Generic;
using JSONClass;

public class NPCTeShu
{
	private List<int> itemTypeList = new List<int>();

	private List<int> itemQualityList = new List<int>();

	public void NpcToDongShiGuShiFang(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DongShiGuFangShi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DongShiGuFangShi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}

	public void NpcToTianXingShiFang(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["TianXingChengFangShi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["TianXingChengFangShi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}

	public void NpcToHaiShangShiFang(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["HaiShangFangShi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["HaiShangFangShi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}

	public void NpcToDongShiGuPaiMai(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DongShiGuPaiMai"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DongShiGuPaiMai"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		AddNpcToPaiMaiList(i2, npcId);
	}

	public void NpcToHaiShangPaiMai(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["HaiShangPaiMai"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["HaiShangPaiMai"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		AddNpcToPaiMaiList(i2, npcId);
	}

	public void NpcToNanYaChengPaiMai(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["NanYaChengPaiMai"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["NanYaChengPaiMai"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		AddNpcToPaiMaiList(i2, npcId);
	}

	public void NpcToTianJiGePaiMai(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["TianJiGePaiMai"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["TianJiGePaiMai"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		AddNpcToPaiMaiList(i2, npcId);
	}

	public void NpcToSuiXingFaZhan(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		int num = npcData["xiuLianSpeed"].I * 2;
		if (npcData.HasField("JinDanData"))
		{
			float num2 = npcData["JinDanData"]["JinDanAddSpeed"].f / 100f;
			num += (int)(num2 * (float)num);
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, num);
	}

	public void NextNpcShiFang(int npcId)
	{
		NpcJieSuanManager.inst.GetNpcData(npcId);
		new Dictionary<int, List<JSONObject>>();
		int num = NpcJieSuanManager.inst.GetNpcBeiBaoAllItemSum(npcId);
		if (num < 15)
		{
			return;
		}
		List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"].list;
		int num2 = 0;
		int index = NpcJieSuanManager.inst.GetNpcBigLevel(npcId) - 1;
		NPCTuPuoDate nPCTuPuoDate = NPCTuPuoDate.DataList[index];
		List<int> list2 = new List<int>();
		foreach (int item in nPCTuPuoDate.ShouJiItem)
		{
			list2.Add(item);
		}
		foreach (int item2 in nPCTuPuoDate.TuPoItem)
		{
			list2.Add(item2);
		}
		while (num > 15)
		{
			num2 = NpcJieSuanManager.inst.getRandomInt(0, list.Count - 1);
			if (!list2.Contains(list[num2]["ItemID"].I))
			{
				int i = list[num2]["Num"].I;
				int i2 = jsonData.instance.ItemJsonData[list[num2]["ItemID"].I.ToString()]["price"].I;
				list.Remove(list[num2]);
				NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, i2 * i);
				num -= i;
			}
		}
	}

	public void NextNpcPaiMai()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		foreach (int key in NpcJieSuanManager.inst.PaiMaiNpcDictionary.Keys)
		{
			num = jsonData.instance.NpcPaiMaiData[key.ToString()]["ItemNum"].I;
			for (int i = 0; i < NpcJieSuanManager.inst.PaiMaiNpcDictionary[key].Count && i < num; i++)
			{
				num7 = NpcJieSuanManager.inst.PaiMaiNpcDictionary[key][i];
				if (jsonData.instance.AvatarJsonData.HasField(num7.ToString()))
				{
					num3 = NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcPaiMaiData[key.ToString()]["Type"].Count - 1);
					num4 = jsonData.instance.NpcPaiMaiData[key.ToString()]["Type"][num3].I;
					num5 = jsonData.instance.NpcPaiMaiData[key.ToString()]["quality"][num3].I;
					JSONObject randomItemByShopType = FactoryManager.inst.npcFactory.GetRandomItemByShopType(num4, num5);
					num2 = randomItemByShopType["id"].I;
					num6 = randomItemByShopType["price"].I;
					NpcJieSuanManager.inst.AddItemToNpcBackpack(num7, num2, 1);
					NpcJieSuanManager.inst.npcNoteBook.NotePaiMai(num7, num2);
					NpcJieSuanManager.inst.npcSetField.AddNpcMoney(num7, -num6);
				}
			}
		}
	}

	public void AddNpcToPaiMaiList(int sceneId, int npcId)
	{
		if (NpcJieSuanManager.inst.PaiMaiNpcDictionary.ContainsKey(sceneId))
		{
			NpcJieSuanManager.inst.PaiMaiNpcDictionary[sceneId].Add(npcId);
			return;
		}
		NpcJieSuanManager.inst.PaiMaiNpcDictionary.Add(sceneId, new List<int> { npcId });
	}

	public void NpcToGuangChang(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		NpcAddDoSomething(npcId);
	}

	public void NpcZhangLaoToDaDian(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		NpcAddDoSomething(npcId);
	}

	public void NpcZhangMenToDaDian(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		NpcAddDoSomething(npcId);
	}

	public void NpcAddDoSomething(int npcId)
	{
		if (NpcJieSuanManager.inst.getRandomInt(0, 1) == 0)
		{
			NpcJieSuanManager.inst.npcXiuLian.NpcBiGuan(npcId);
			return;
		}
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I;
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, jsonData.instance.NpcLevelShouYiDate[i.ToString()]["money"].I);
	}

	public void NpcSuiXingDaoJinHuo(int npcId)
	{
		NpcJinHuoData npcJinHuoData = NpcJinHuoData.DataDict[2];
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
		for (int k = 1; k <= 30; k++)
		{
			num2 = NpcJieSuanManager.inst.getRandomInt(0, itemTypeList.Count - 1);
			num = FactoryManager.inst.npcFactory.GetRandomItemByShopType(itemTypeList[num2], itemQualityList[num2])["id"].I;
			if (dictionary.ContainsKey(num))
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
	}

	public void NpcToGangKou(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GangKou"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GangKou"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}

	public void NpcToJieSha(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1, isCanJieSha: false);
		NpcJieSuanManager.inst.JieShaNpcList.Add(npcId);
	}

	public void NextJieSha()
	{
		if (NpcJieSuanManager.inst.JieShaNpcList.Count < 1 || NpcJieSuanManager.inst.allBigMapNpcList.Count < 1)
		{
			return;
		}
		foreach (int jieShaNpc in NpcJieSuanManager.inst.JieShaNpcList)
		{
			if (NpcJieSuanManager.inst.IsDeath(jieShaNpc) || NpcJieSuanManager.inst.getRandomInt(0, 1000) > 15)
			{
				continue;
			}
			JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(jieShaNpc);
			for (int num = NpcJieSuanManager.inst.allBigMapNpcList.Count - 1; num >= 0; num--)
			{
				int num2 = NpcJieSuanManager.inst.allBigMapNpcList[num];
				if (NpcJieSuanManager.inst.IsDeath(num2))
				{
					NpcJieSuanManager.inst.allBigMapNpcList.RemoveAt(num);
				}
				else
				{
					JSONObject npcData2 = NpcJieSuanManager.inst.GetNpcData(num2);
					if (NpcJieSuanManager.inst.GetNpcBigLevel(jieShaNpc) == NpcJieSuanManager.inst.GetNpcBigLevel(num2))
					{
						if (NpcJieSuanManager.inst.getRandomInt(0, 100) > 50)
						{
							if (NpcJieSuanManager.inst.getRandomInt(0, 1000) > jsonData.instance.NpcLevelShouYiDate[npcData2["Level"].I.ToString()]["siwangjilv"].I || npcData2["isImportant"].b)
							{
								NpcJieSuanManager.inst.npcNoteBook.NoteJieShaFail1(jieShaNpc, jsonData.instance.AvatarRandomJsonData[num2.ToString()]["Name"].str);
								NpcJieSuanManager.inst.npcNoteBook.NoteFanShaFail1(num2, jsonData.instance.AvatarRandomJsonData[jieShaNpc.ToString()]["Name"].str);
							}
							else
							{
								NpcJieSuanManager.inst.npcNoteBook.NoteJieShaSuccess(jieShaNpc, jsonData.instance.AvatarRandomJsonData[num2.ToString()]["Name"].str);
								if (Tools.instance.getPlayer().emailDateMag.IsFriend(num2))
								{
									int randomInt = NpcJieSuanManager.inst.getRandomInt(1, 3);
									int duiBaiId = NpcJieSuanManager.inst.GetDuiBaiId(jsonData.instance.AvatarJsonData[num2.ToString()]["XingGe"].I, 999);
									Tools.instance.getPlayer().emailDateMag.SendToPlayerNoPd(num2, duiBaiId, randomInt, jsonData.instance.AvatarRandomJsonData[jieShaNpc.ToString()]["Name"].Str, NpcJieSuanManager.inst.JieSuanTime);
								}
								NpcJieSuanManager.inst.npcDeath.SetNpcDeath(5, num2, jieShaNpc);
								NpcJieSuanManager.inst.npcSetField.AddNpcMoney(jieShaNpc, jsonData.instance.NpcLevelShouYiDate[npcData["Level"].I.ToString()]["money"].I * 10);
							}
						}
						else if (NpcJieSuanManager.inst.getRandomInt(0, 1000) > jsonData.instance.NpcLevelShouYiDate[npcData["Level"].I.ToString()]["siwangjilv"].I || npcData["isImportant"].b)
						{
							NpcJieSuanManager.inst.npcNoteBook.NoteJieShaFail2(jieShaNpc, jsonData.instance.AvatarRandomJsonData[num2.ToString()]["Name"].str);
							NpcJieSuanManager.inst.npcNoteBook.NoteFanShaFail2(num2, jsonData.instance.AvatarRandomJsonData[jieShaNpc.ToString()]["Name"].str);
						}
						else
						{
							NpcJieSuanManager.inst.npcNoteBook.NoteFanShaSuccess(num2, jsonData.instance.AvatarRandomJsonData[jieShaNpc.ToString()]["Name"].str);
							NpcJieSuanManager.inst.npcDeath.SetNpcDeath(11, jieShaNpc, num2);
							NpcJieSuanManager.inst.npcSetField.AddNpcMoney(num2, jsonData.instance.NpcLevelShouYiDate[npcData2["Level"].I.ToString()]["money"].I * 10);
						}
						NpcJieSuanManager.inst.allBigMapNpcList.RemoveAt(num);
						break;
					}
				}
			}
		}
		NpcJieSuanManager.inst.JieShaNpcList = new List<int>();
		NpcJieSuanManager.inst.allBigMapNpcList = new List<int>();
	}

	public void NpcFriendToDongFu(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = jSONObject["xiuLianSpeed"].I * 2;
		if (jSONObject.HasField("JinDanData"))
		{
			float num2 = jSONObject["JinDanData"]["JinDanAddSpeed"].f / 100f;
			num += (int)(num2 * (float)num);
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, num);
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, 101);
	}

	public void NpcDaoLuToDongFu(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = jSONObject["xiuLianSpeed"].I * 2;
		if (jSONObject.HasField("JinDanData"))
		{
			float num2 = jSONObject["JinDanData"]["JinDanAddSpeed"].f / 100f;
			num += (int)(num2 * (float)num);
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, num);
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, 101);
	}

	public void NpcToSuiXingShop(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}
}
