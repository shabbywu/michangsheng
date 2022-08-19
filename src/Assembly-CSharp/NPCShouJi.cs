using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class NPCShouJi
{
	// Token: 0x06001591 RID: 5521 RVA: 0x0008F67A File Offset: 0x0008D87A
	public NPCShouJi()
	{
		this.CaiJiMapList.Add("ThreeSence");
		this.CaiJiMapList.Add("FuBen");
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x0008F6B0 File Offset: 0x0008D8B0
	public void NpcCaiYao(int npcID)
	{
		int i = jsonData.instance.AvatarJsonData[npcID.ToString()]["Type"].I;
		int randomInt = NpcJieSuanManager.inst.getRandomInt(0, this.CaiJiMapList.Count - 1);
		string str = jsonData.instance.NPCActionDate["3"][this.CaiJiMapList[randomInt]].str;
		JSONObject jsonobject = new JSONObject();
		try
		{
			List<int> list = new List<int>();
			int num;
			if (str == "ThreeSence")
			{
				if (jsonData.instance.NpcThreeMapBingDate[i.ToString()]["CaiJi"].Count > 0)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			else if (jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiJi"].I > 0)
			{
				num = 2;
			}
			else
			{
				num = 1;
			}
			if (num == 1)
			{
				jsonobject = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["CaiJi"];
				int i2 = jsonobject[NpcJieSuanManager.inst.getRandomInt(0, jsonobject.Count - 1)].I;
				NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
				for (int j = 0; j < jsonData.instance.CaiYaoDiaoLuo.Count; j++)
				{
					if (jsonData.instance.CaiYaoDiaoLuo[j]["ThreeSence"].I == i2 && jsonData.instance.CaiYaoDiaoLuo[j]["type"].I == 1)
					{
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
			}
			else
			{
				int i4 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiJi"].I;
				string b = "F" + i4;
				int i5 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiJiDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiJiDian"].Count - 1)].I;
				NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcID, i4, i5);
				for (int l = 0; l < jsonData.instance.CaiYaoDiaoLuo.Count; l++)
				{
					if (jsonData.instance.CaiYaoDiaoLuo[l]["FuBen"].str == b && jsonData.instance.CaiYaoDiaoLuo[l]["type"].I == 1)
					{
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
			}
			foreach (JSONObject obj in this.GetRandomCaiLiao(npcID, list))
			{
				jsonData.instance.AvatarBackpackJsonData[npcID.ToString()]["Backpack"].Add(obj);
			}
		}
		catch (Exception)
		{
			Debug.LogError(string.Format("npc出错类型:{0}", i));
		}
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x0008FAD0 File Offset: 0x0008DCD0
	public void NpcCaiKuang(int npcID)
	{
		int i = jsonData.instance.AvatarJsonData[npcID.ToString()]["Type"].I;
		int randomInt = NpcJieSuanManager.inst.getRandomInt(0, this.CaiJiMapList.Count - 1);
		string str = jsonData.instance.NPCActionDate["4"][this.CaiJiMapList[randomInt]].str;
		JSONObject jsonobject = new JSONObject();
		try
		{
			List<int> list = new List<int>();
			int num;
			if (str == "ThreeSence")
			{
				if (jsonData.instance.NpcThreeMapBingDate[i.ToString()]["CaiKuang"].Count > 0)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			else if (jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiKuang"].I > 0)
			{
				num = 2;
			}
			else
			{
				num = 1;
			}
			if (num == 1)
			{
				jsonobject = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["CaiKuang"];
				if (jsonobject.list.Count == 0)
				{
					Debug.LogError(string.Format("NPC采矿异常，jsonData.instance.NpcThreeMapBingDate[{0}][\"CaiKuang\"]为空", i));
					return;
				}
				int randomInt2 = NpcJieSuanManager.inst.getRandomInt(0, jsonobject.Count - 1);
				int i2 = jsonobject[randomInt2].I;
				NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
				for (int j = 0; j < jsonData.instance.CaiYaoDiaoLuo.Count; j++)
				{
					if (jsonData.instance.CaiYaoDiaoLuo[j]["ThreeSence"].I == i2 && jsonData.instance.CaiYaoDiaoLuo[j]["type"].I == 2)
					{
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
			}
			else
			{
				int i4 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiKuang"].I;
				string b = "F" + i4;
				int i5 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiKuangDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["CaiKuangDian"].Count - 1)].I;
				NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcID, i4, i5);
				for (int l = 0; l < jsonData.instance.CaiYaoDiaoLuo.Count; l++)
				{
					if (jsonData.instance.CaiYaoDiaoLuo[l]["FuBen"].str == b && jsonData.instance.CaiYaoDiaoLuo[l]["type"].I == 2)
					{
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
			}
			foreach (JSONObject obj in this.GetRandomCaiLiao(npcID, list))
			{
				jsonData.instance.AvatarBackpackJsonData[npcID.ToString()]["Backpack"].Add(obj);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x0008FF08 File Offset: 0x0008E108
	public void NpcBuyMiJi(int npcID)
	{
		int i = jsonData.instance.AvatarJsonData[npcID.ToString()]["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["MiJi"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["MiJi"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x0008FFAC File Offset: 0x0008E1AC
	public void NpcBuyDanYao(int npcId)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["YaoDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["YaoDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcId, i2);
		int npcBigLevel = NpcJieSuanManager.inst.GetNpcBigLevel(npcId);
		for (int j = 0; j < 2; j++)
		{
			JSONObject randomItemByType = FactoryManager.inst.npcFactory.GetRandomItemByType(5, npcBigLevel);
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, randomItemByType["id"].I, 1, null, false);
		}
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x00090098 File Offset: 0x0008E298
	public void NpcBuyFaBao(int npcID)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcID.ToString()];
		int i = jsonobject["Type"].I;
		int i2 = jsonData.instance.NpcThreeMapBingDate[i.ToString()]["FaBao"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcThreeMapBingDate[i.ToString()]["FaBao"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToThreeScene(npcID, i2);
		int num = NpcJieSuanManager.inst.IsCanChangeEquip(jsonobject);
		if (num == 0)
		{
			return;
		}
		NpcJieSuanManager.inst.AddNpcEquip(npcID, num, false);
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x00090158 File Offset: 0x0008E358
	public void NpcShouJiTuPoItem(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1, true);
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x0009016C File Offset: 0x0008E36C
	public void NextNpcShouJiTuPoItem(int npcId)
	{
		if (!NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2))
		{
			return;
		}
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I;
		JSONObject jsonobject = jsonData.instance.NPCTuPuoDate[i.ToString()]["ShouJiItem"];
		if (jsonobject.Count < 1)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		try
		{
			num = jsonobject[NpcJieSuanManager.inst.getRandomInt(0, jsonobject.Count - 1)].I;
			num2 = jsonData.instance.ItemJsonData[num.ToString()]["price"].I;
		}
		catch (Exception)
		{
			Debug.LogError(string.Format("收集突破物品ID出错ID:{0}", num));
			return;
		}
		NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, num, 1, null, false);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, -num2);
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x00090274 File Offset: 0x0008E474
	public void NpcSpeedDeath(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1, true);
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x00090298 File Offset: 0x0008E498
	public void NpcToLingHe1(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			using (List<int>.Enumerator enumerator = NpcFuBenMapBingDate.DataDict[i].LingHeDian1.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(num2))
					{
						num = num2;
						break;
					}
				}
				goto IL_D0;
			}
		}
		num = NpcFuBenMapBingDate.DataDict[i].LingHeDian1[0];
		IL_D0:
		if (num == 0)
		{
			Debug.LogError("NPC采集灵核1出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x000903D8 File Offset: 0x0008E5D8
	public void NpcToLingHe2(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			using (List<int>.Enumerator enumerator = NpcFuBenMapBingDate.DataDict[i].LingHeDian2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(num2))
					{
						num = num2;
						break;
					}
				}
				goto IL_D0;
			}
		}
		num = NpcFuBenMapBingDate.DataDict[i].LingHeDian2[0];
		IL_D0:
		if (num == 0)
		{
			Debug.LogError("NPC采集灵核2出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x00090518 File Offset: 0x0008E718
	public void NpcToLingHe3(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			using (List<int>.Enumerator enumerator = NpcFuBenMapBingDate.DataDict[i].LingHeDian3.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(num2))
					{
						num = num2;
						break;
					}
				}
				goto IL_D0;
			}
		}
		num = NpcFuBenMapBingDate.DataDict[i].LingHeDian3[0];
		IL_D0:
		if (num == 0)
		{
			Debug.LogError("NPC采集灵核3出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x00090658 File Offset: 0x0008E858
	public void NpcToLingHe4(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			using (List<int>.Enumerator enumerator = NpcFuBenMapBingDate.DataDict[i].LingHeDian4.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(num2))
					{
						num = num2;
						break;
					}
				}
				goto IL_D0;
			}
		}
		num = NpcFuBenMapBingDate.DataDict[i].LingHeDian4[0];
		IL_D0:
		if (num == 0)
		{
			Debug.LogError("NPC采集灵核4出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x00090798 File Offset: 0x0008E998
	public void NpcToLingHe5(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			using (List<int>.Enumerator enumerator = NpcFuBenMapBingDate.DataDict[i].LingHeDian5.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(num2))
					{
						num = num2;
						break;
					}
				}
				goto IL_D0;
			}
		}
		num = NpcFuBenMapBingDate.DataDict[i].LingHeDian5[0];
		IL_D0:
		if (num == 0)
		{
			Debug.LogError("NPC采集灵核5出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000908D8 File Offset: 0x0008EAD8
	public void NpcToLingHe6(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Type"].I;
		int lingHe = NpcFuBenMapBingDate.DataDict[i].LingHe;
		string key = "F" + lingHe;
		int num = 0;
		if (NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(key))
		{
			using (List<int>.Enumerator enumerator = NpcFuBenMapBingDate.DataDict[i].LingHeDian6.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key].ContainsKey(num2))
					{
						num = num2;
						break;
					}
				}
				goto IL_D0;
			}
		}
		num = NpcFuBenMapBingDate.DataDict[i].LingHeDian6[0];
		IL_D0:
		if (num == 0)
		{
			Debug.LogError("NPC采集灵核6出错，副本没有空位置");
			return;
		}
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, lingHe, num);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, NpcLevelShouYiDate.DataDict[npcData["Level"].I].money);
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x00090A18 File Offset: 0x0008EC18
	public List<JSONObject> GetRandomCaiLiao(int npcID, List<int> itemList)
	{
		int i = jsonData.instance.NpcLevelShouYiDate[jsonData.instance.AvatarJsonData[npcID.ToString()]["Level"].I.ToString()]["money"].I;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		while (i > 0)
		{
			int num = itemList[NpcJieSuanManager.inst.getRandomInt(0, itemList.Count - 1)];
			if (dictionary.ContainsKey(num))
			{
				Dictionary<int, int> dictionary2 = dictionary;
				int key = num;
				int num2 = dictionary2[key];
				dictionary2[key] = num2 + 1;
			}
			else
			{
				dictionary.Add(num, 1);
			}
			i -= jsonData.instance.ItemJsonData[num.ToString()]["price"].I;
		}
		List<JSONObject> list = new List<JSONObject>();
		foreach (int num3 in dictionary.Keys)
		{
			JSONObject item = jsonData.instance.setAvatarBackpack(Tools.getUUID(), num3, 1, dictionary[num3], 100, 1, Tools.CreateItemSeid(num3), 0);
			list.Add(item);
		}
		return list;
	}

	// Token: 0x04001011 RID: 4113
	private List<string> CaiJiMapList = new List<string>();
}
