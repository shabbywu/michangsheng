using System;
using System.Collections.Generic;
using JSONClass;

// Token: 0x02000342 RID: 834
public class NPCTeShu
{
	// Token: 0x0600187F RID: 6271 RVA: 0x000DA458 File Offset: 0x000D8658
	public void NpcToDongShiGuShiFang(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DongShiGuFangShi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DongShiGuFangShi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x000DA500 File Offset: 0x000D8700
	public void NpcToTianXingShiFang(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["TianXingChengFangShi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["TianXingChengFangShi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}

	// Token: 0x06001881 RID: 6273 RVA: 0x000DA5A8 File Offset: 0x000D87A8
	public void NpcToHaiShangShiFang(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["HaiShangFangShi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["HaiShangFangShi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}

	// Token: 0x06001882 RID: 6274 RVA: 0x000DA650 File Offset: 0x000D8850
	public void NpcToDongShiGuPaiMai(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DongShiGuPaiMai"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DongShiGuPaiMai"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		this.AddNpcToPaiMaiList(i2, npcId);
	}

	// Token: 0x06001883 RID: 6275 RVA: 0x000DA6F0 File Offset: 0x000D88F0
	public void NpcToHaiShangPaiMai(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["HaiShangPaiMai"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["HaiShangPaiMai"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		this.AddNpcToPaiMaiList(i2, npcId);
	}

	// Token: 0x06001884 RID: 6276 RVA: 0x000DA790 File Offset: 0x000D8990
	public void NpcToNanYaChengPaiMai(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["NanYaChengPaiMai"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["NanYaChengPaiMai"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		this.AddNpcToPaiMaiList(i2, npcId);
	}

	// Token: 0x06001885 RID: 6277 RVA: 0x000DA830 File Offset: 0x000D8A30
	public void NpcToTianJiGePaiMai(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["TianJiGePaiMai"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["TianJiGePaiMai"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		this.AddNpcToPaiMaiList(i2, npcId);
	}

	// Token: 0x06001886 RID: 6278 RVA: 0x000DA8D0 File Offset: 0x000D8AD0
	public void NpcToSuiXingFaZhan(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
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

	// Token: 0x06001887 RID: 6279 RVA: 0x000DA9D4 File Offset: 0x000D8BD4
	public void NextNpcShiFang(int npcId)
	{
		NpcJieSuanManager.inst.getNpcData(npcId);
		new Dictionary<int, List<JSONObject>>();
		int num = NpcJieSuanManager.inst.GetNpcBeiBaoAllItemSum(npcId);
		if (num < 15)
		{
			return;
		}
		List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"].list;
		int index = NpcJieSuanManager.inst.getNpcBigLevel(npcId) - 1;
		NPCTuPuoDate npctuPuoDate = NPCTuPuoDate.DataList[index];
		List<int> list2 = new List<int>();
		foreach (int item in npctuPuoDate.ShouJiItem)
		{
			list2.Add(item);
		}
		using (List<int>.Enumerator enumerator = npctuPuoDate.TuPoItem.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int item2 = enumerator.Current;
				list2.Add(item2);
			}
			goto IL_199;
		}
		IL_E7:
		int randomInt = NpcJieSuanManager.inst.getRandomInt(0, list.Count - 1);
		if (!list2.Contains(list[randomInt]["ItemID"].I))
		{
			int i = list[randomInt]["Num"].I;
			int i2 = jsonData.instance.ItemJsonData[list[randomInt]["ItemID"].I.ToString()]["price"].I;
			list.Remove(list[randomInt]);
			NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, i2 * i);
			num -= i;
		}
		IL_199:
		if (num <= 15)
		{
			return;
		}
		goto IL_E7;
	}

	// Token: 0x06001888 RID: 6280 RVA: 0x000DABA0 File Offset: 0x000D8DA0
	public void NextNpcPaiMai()
	{
		foreach (int key in NpcJieSuanManager.inst.PaiMaiNpcDictionary.Keys)
		{
			int i = jsonData.instance.NpcPaiMaiData[key.ToString()]["ItemNum"].I;
			int num = 0;
			while (num < NpcJieSuanManager.inst.PaiMaiNpcDictionary[key].Count && num < i)
			{
				int npcId = NpcJieSuanManager.inst.PaiMaiNpcDictionary[key][num];
				if (jsonData.instance.AvatarJsonData.HasField(npcId.ToString()))
				{
					int randomInt = NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcPaiMaiData[key.ToString()]["Type"].Count - 1);
					int i2 = jsonData.instance.NpcPaiMaiData[key.ToString()]["Type"][randomInt].I;
					int i3 = jsonData.instance.NpcPaiMaiData[key.ToString()]["quality"][randomInt].I;
					JSONObject randomItemByShopType = FactoryManager.inst.npcFactory.GetRandomItemByShopType(i2, i3);
					int i4 = randomItemByShopType["id"].I;
					int i5 = randomItemByShopType["price"].I;
					NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, i4, 1, null, false);
					NpcJieSuanManager.inst.npcNoteBook.NotePaiMai(npcId, i4, "");
					NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, -i5);
				}
				num++;
			}
		}
	}

	// Token: 0x06001889 RID: 6281 RVA: 0x000DADA4 File Offset: 0x000D8FA4
	public void AddNpcToPaiMaiList(int sceneId, int npcId)
	{
		if (NpcJieSuanManager.inst.PaiMaiNpcDictionary.ContainsKey(sceneId))
		{
			NpcJieSuanManager.inst.PaiMaiNpcDictionary[sceneId].Add(npcId);
			return;
		}
		NpcJieSuanManager.inst.PaiMaiNpcDictionary.Add(sceneId, new List<int>
		{
			npcId
		});
	}

	// Token: 0x0600188A RID: 6282 RVA: 0x000DADF8 File Offset: 0x000D8FF8
	public void NpcToGuangChang(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GuangChang"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		this.NpcAddDoSomething(npcId);
	}

	// Token: 0x0600188B RID: 6283 RVA: 0x000DAE98 File Offset: 0x000D9098
	public void NpcZhangLaoToDaDian(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		this.NpcAddDoSomething(npcId);
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x000DAE98 File Offset: 0x000D9098
	public void NpcZhangMenToDaDian(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		this.NpcAddDoSomething(npcId);
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x000DAF38 File Offset: 0x000D9138
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

	// Token: 0x0600188E RID: 6286 RVA: 0x000DAFC0 File Offset: 0x000D91C0
	public void NpcSuiXingDaoJinHuo(int npcId)
	{
		NpcJinHuoData npcJinHuoData = NpcJinHuoData.DataDict[2];
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
		for (int k = 1; k <= 30; k++)
		{
			int randomInt = NpcJieSuanManager.inst.getRandomInt(0, this.itemTypeList.Count - 1);
			int i2 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(this.itemTypeList[randomInt], this.itemQualityList[randomInt])["id"].I;
			if (dictionary.ContainsKey(i2))
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
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x000DB190 File Offset: 0x000D9390
	public void NpcToGangKou(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GangKou"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["GangKou"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}

	// Token: 0x06001890 RID: 6288 RVA: 0x00015461 File Offset: 0x00013661
	public void NpcToJieSha(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1, false);
		NpcJieSuanManager.inst.JieShaNpcList.Add(npcId);
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x000DB228 File Offset: 0x000D9428
	public void NextJieSha()
	{
		if (NpcJieSuanManager.inst.JieShaNpcList.Count < 1)
		{
			return;
		}
		if (NpcJieSuanManager.inst.allBigMapNpcList.Count < 1)
		{
			return;
		}
		foreach (int num in NpcJieSuanManager.inst.JieShaNpcList)
		{
			if (!NpcJieSuanManager.inst.IsDeath(num) && NpcJieSuanManager.inst.getRandomInt(0, 1000) <= 15)
			{
				JSONObject npcData = NpcJieSuanManager.inst.getNpcData(num);
				for (int i = NpcJieSuanManager.inst.allBigMapNpcList.Count - 1; i >= 0; i--)
				{
					int num2 = NpcJieSuanManager.inst.allBigMapNpcList[i];
					if (NpcJieSuanManager.inst.IsDeath(num2))
					{
						NpcJieSuanManager.inst.allBigMapNpcList.RemoveAt(i);
					}
					else
					{
						JSONObject npcData2 = NpcJieSuanManager.inst.getNpcData(num2);
						if (NpcJieSuanManager.inst.getNpcBigLevel(num) == NpcJieSuanManager.inst.getNpcBigLevel(num2))
						{
							if (NpcJieSuanManager.inst.getRandomInt(0, 100) > 50)
							{
								if (NpcJieSuanManager.inst.getRandomInt(0, 1000) > jsonData.instance.NpcLevelShouYiDate[npcData2["Level"].I.ToString()]["siwangjilv"].I || npcData2["isImportant"].b)
								{
									NpcJieSuanManager.inst.npcNoteBook.NoteJieShaFail1(num, jsonData.instance.AvatarRandomJsonData[num2.ToString()]["Name"].str);
									NpcJieSuanManager.inst.npcNoteBook.NoteFanShaFail1(num2, jsonData.instance.AvatarRandomJsonData[num.ToString()]["Name"].str);
								}
								else
								{
									NpcJieSuanManager.inst.npcNoteBook.NoteJieShaSuccess(num, jsonData.instance.AvatarRandomJsonData[num2.ToString()]["Name"].str);
									if (Tools.instance.getPlayer().emailDateMag.IsFriend(num2))
									{
										int randomInt = NpcJieSuanManager.inst.getRandomInt(1, 3);
										int duiBaiId = NpcJieSuanManager.inst.GetDuiBaiId(jsonData.instance.AvatarJsonData[num2.ToString()]["XingGe"].I, 999);
										Tools.instance.getPlayer().emailDateMag.SendToPlayerNoPd(num2, duiBaiId, randomInt, jsonData.instance.AvatarRandomJsonData[num.ToString()]["Name"].Str, NpcJieSuanManager.inst.JieSuanTime);
									}
									NpcJieSuanManager.inst.npcDeath.SetNpcDeath(5, num2, num, false);
									NpcJieSuanManager.inst.npcSetField.AddNpcMoney(num, jsonData.instance.NpcLevelShouYiDate[npcData["Level"].I.ToString()]["money"].I * 10);
								}
							}
							else if (NpcJieSuanManager.inst.getRandomInt(0, 1000) > jsonData.instance.NpcLevelShouYiDate[npcData["Level"].I.ToString()]["siwangjilv"].I || npcData["isImportant"].b)
							{
								NpcJieSuanManager.inst.npcNoteBook.NoteJieShaFail2(num, jsonData.instance.AvatarRandomJsonData[num2.ToString()]["Name"].str);
								NpcJieSuanManager.inst.npcNoteBook.NoteFanShaFail2(num2, jsonData.instance.AvatarRandomJsonData[num.ToString()]["Name"].str);
							}
							else
							{
								NpcJieSuanManager.inst.npcNoteBook.NoteFanShaSuccess(num2, jsonData.instance.AvatarRandomJsonData[num.ToString()]["Name"].str);
								NpcJieSuanManager.inst.npcDeath.SetNpcDeath(11, num, num2, false);
								NpcJieSuanManager.inst.npcSetField.AddNpcMoney(num2, jsonData.instance.NpcLevelShouYiDate[npcData2["Level"].I.ToString()]["money"].I * 10);
							}
							NpcJieSuanManager.inst.allBigMapNpcList.RemoveAt(i);
							break;
						}
					}
				}
			}
		}
		NpcJieSuanManager.inst.JieShaNpcList = new List<int>();
		NpcJieSuanManager.inst.allBigMapNpcList = new List<int>();
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x000DB718 File Offset: 0x000D9918
	public void NpcFriendToDongFu(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = jsonobject["xiuLianSpeed"].I * 2;
		if (jsonobject.HasField("JinDanData"))
		{
			float num2 = jsonobject["JinDanData"]["JinDanAddSpeed"].f / 100f;
			num += (int)(num2 * (float)num);
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, num);
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, 101);
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x000DB718 File Offset: 0x000D9918
	public void NpcDaoLuToDongFu(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = jsonobject["xiuLianSpeed"].I * 2;
		if (jsonobject.HasField("JinDanData"))
		{
			float num2 = jsonobject["JinDanData"]["JinDanAddSpeed"].f / 100f;
			num += (int)(num2 * (float)num);
		}
		NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, num);
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, 101);
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x000DB7B8 File Offset: 0x000D99B8
	public void NpcToSuiXingShop(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["DaDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
	}

	// Token: 0x0400139A RID: 5018
	private List<int> itemTypeList = new List<int>();

	// Token: 0x0400139B RID: 5019
	private List<int> itemQualityList = new List<int>();
}
