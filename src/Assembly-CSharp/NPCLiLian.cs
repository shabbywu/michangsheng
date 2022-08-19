using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000212 RID: 530
public class NPCLiLian
{
	// Token: 0x06001523 RID: 5411 RVA: 0x00087854 File Offset: 0x00085A54
	public NPCLiLian()
	{
		this.yaoShouDropDictionary = new Dictionary<int, Dictionary<int, List<int>>>();
		this.qiYuQuanZhongDictionary = new Dictionary<int, Dictionary<int, int>>();
		this.yaoShouDropDictionary.Add(1, new Dictionary<int, List<int>>());
		this.yaoShouDropDictionary.Add(2, new Dictionary<int, List<int>>());
		this.qiYuQuanZhongDictionary.Add(1, new Dictionary<int, int>());
		this.qiYuQuanZhongDictionary.Add(2, new Dictionary<int, int>());
		this.qiYuQuanZhongDictionary.Add(3, new Dictionary<int, int>());
		this.qiYuQuanZhongDictionary.Add(4, new Dictionary<int, int>());
		this.qiYuQuanZhongDictionary.Add(5, new Dictionary<int, int>());
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x000878F4 File Offset: 0x00085AF4
	public void NPCNingZhouKillYaoShou(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1, true);
		this.NPCGetYaoShouDrop(npcId, 1);
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x00087910 File Offset: 0x00085B10
	public void NPCHaiShangKillYaoShou(int npcId)
	{
		this.NPCGetYaoShouDrop(npcId, 2);
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x0008791C File Offset: 0x00085B1C
	public void NPCDoZhuChengTask(int npcId)
	{
		float n = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].n;
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1, true);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, jsonData.instance.NpcLevelShouYiDate[jsonobject["Level"].I.ToString()]["money"].I);
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x000879C0 File Offset: 0x00085BC0
	public void NPCDoMenPaiTask(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(npcId, jsonData.instance.NpcLevelShouYiDate[jsonobject["Level"].I.ToString()]["money"].I);
		NpcJieSuanManager.inst.npcSetField.NpcAddGongXian(npcId, jsonData.instance.NpcLevelShouYiDate[jsonobject["Level"].I.ToString()]["gongxian"].I);
		if (NpcJieSuanManager.inst.getRandomInt(0, 100) > 50)
		{
			try
			{
				NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 2, true);
				return;
			}
			catch (Exception)
			{
				Debug.LogError(string.Format("错误的Npc行为，行为ID35,npc类型：{0},npcId:{1}", jsonobject["Type"].I, npcId));
				return;
			}
		}
		int i = jsonobject["Type"].I;
		int i2 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["XunLuo"].I;
		if (i2 == 0)
		{
			Debug.LogError(string.Format("错误的Npc行为，行为ID35,npc类型：{0},npcId:{1}", i, npcId));
			return;
		}
		"F" + i2;
		int i3 = jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["XunLuoDian"][NpcJieSuanManager.inst.getRandomInt(0, jsonData.instance.NpcFuBenMapBingDate[i.ToString()]["XunLuoDian"].Count - 1)].I;
		NpcJieSuanManager.inst.npcMap.AddNpcToFuBen(npcId, i2, i3);
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x00087BB0 File Offset: 0x00085DB0
	public void NPCNingZhouYouLi(int npcId)
	{
		NpcJieSuanManager.inst.npcMap.AddNpcToBigMap(npcId, 1, true);
		this.NPCYouLi(npcId);
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x00087BCB File Offset: 0x00085DCB
	public void NpcHaiShangYouLi(int npcId)
	{
		this.NPCYouLi(npcId);
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x00087BD4 File Offset: 0x00085DD4
	private void NPCYouLi(int npcId)
	{
		NpcJieSuanManager.inst.GetNpcData(npcId);
		if (NpcJieSuanManager.inst.getRandomInt(1, 100) > 40)
		{
			return;
		}
		if (this.qiYuQuanZhongDictionary[1].Count < 1)
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcQiYuDate.list)
			{
				for (int i = 0; i < jsonobject["JingJie"].Count; i++)
				{
					this.qiYuQuanZhongDictionary[jsonobject["JingJie"][i].I].Add(jsonobject["id"].I, jsonobject["quanzhong"].I);
				}
			}
		}
		int randomQiYu = this.GetRandomQiYu(NpcJieSuanManager.inst.GetNpcBigLevel(npcId));
		JSONObject jsonobject2 = jsonData.instance.NpcQiYuDate[randomQiYu.ToString()];
		int num = -1;
		int num2 = -1;
		if (jsonobject2["ZhuangTai"].I > 0)
		{
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, jsonobject2["ZhuangTai"].I);
		}
		if (jsonobject2["Item"].Count > 0)
		{
			num = FactoryManager.inst.npcFactory.GetRandomItemByShopType(jsonobject2["Item"][0].I, jsonobject2["Item"][1].I)["id"].I;
			num2 = jsonobject2["Itemnum"].I;
			if (jsonobject2["Itemnum"].I > 0)
			{
				NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, num, num2, null, false);
			}
		}
		if (jsonobject2["XiuWei"].I > 0)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, jsonobject2["XiuWei"].I);
		}
		if (jsonobject2["XueLiang"].I > 0)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, jsonobject2["XueLiang"].I);
		}
		NpcJieSuanManager.inst.npcNoteBook.NoteQiYu(npcId, randomQiYu, num, num2);
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x00087E38 File Offset: 0x00086038
	public int GetRandomQiYu(int bigLevel)
	{
		int num = 0;
		int num2 = 0;
		int result = 0;
		foreach (int key in this.qiYuQuanZhongDictionary[bigLevel].Keys)
		{
			num += this.qiYuQuanZhongDictionary[bigLevel][key];
		}
		int randomInt = NpcJieSuanManager.inst.getRandomInt(1, num);
		foreach (int num3 in this.qiYuQuanZhongDictionary[bigLevel].Keys)
		{
			num2 += this.qiYuQuanZhongDictionary[bigLevel][num3];
			if (num2 >= randomInt)
			{
				result = num3;
				break;
			}
		}
		return result;
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x00087F24 File Offset: 0x00086124
	private void NPCGetYaoShouDrop(int npcId, int yaoShouType)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int num = NpcJieSuanManager.inst.GetNpcBigLevel(npcId);
		if (num >= 5)
		{
			num = 4;
		}
		int i = jsonData.instance.NpcLevelShouYiDate[jsonobject["Level"].I.ToString()]["money"].I;
		if (this.yaoShouDropDictionary[1].Count < 1)
		{
			foreach (JSONObject jsonobject2 in jsonData.instance.NpcYaoShouDrop.list)
			{
				int i2 = jsonobject2["jingjie"].I;
				if (jsonobject2["NingZhou"].I == 1)
				{
					if (this.yaoShouDropDictionary[1].ContainsKey(i2))
					{
						for (int j = 0; j < jsonobject2["chanchu"].Count; j++)
						{
							this.yaoShouDropDictionary[1][i2].Add(jsonobject2["chanchu"][j].I);
						}
					}
					else
					{
						List<int> list = new List<int>();
						for (int k = 0; k < jsonobject2["chanchu"].Count; k++)
						{
							list.Add(jsonobject2["chanchu"][k].I);
						}
						this.yaoShouDropDictionary[1].Add(i2, list);
					}
				}
				if (jsonobject2["HaiShang"].I == 1)
				{
					if (this.yaoShouDropDictionary[2].ContainsKey(i2))
					{
						for (int l = 0; l < jsonobject2["chanchu"].Count; l++)
						{
							this.yaoShouDropDictionary[2][i2].Add(jsonobject2["chanchu"][l].I);
						}
					}
					else
					{
						List<int> list2 = new List<int>();
						for (int m = 0; m < jsonobject2["chanchu"].Count; m++)
						{
							list2.Add(jsonobject2["chanchu"][m].I);
						}
						this.yaoShouDropDictionary[2].Add(i2, list2);
					}
				}
			}
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		while (i > 0)
		{
			int num2 = 0;
			try
			{
				num2 = this.yaoShouDropDictionary[yaoShouType][num][NpcJieSuanManager.inst.getRandomInt(0, this.yaoShouDropDictionary[yaoShouType][num].Count - 1)];
			}
			catch (Exception)
			{
				Debug.LogError(string.Format("出错yaoShouType:{0},npcBigLevl{1}", yaoShouType, num));
			}
			if (dictionary.ContainsKey(num2))
			{
				Dictionary<int, int> dictionary2 = dictionary;
				int key = num2;
				int num3 = dictionary2[key];
				dictionary2[key] = num3 + 1;
			}
			else
			{
				dictionary.Add(num2, 1);
			}
			int i3 = jsonData.instance.ItemJsonData[num2.ToString()]["price"].I;
			i -= i3;
		}
		foreach (int num4 in dictionary.Keys)
		{
			NpcJieSuanManager.inst.AddItemToNpcBackpack(npcId, num4, dictionary[num4], null, false);
		}
	}

	// Token: 0x04000FE2 RID: 4066
	private Dictionary<int, Dictionary<int, List<int>>> yaoShouDropDictionary;

	// Token: 0x04000FE3 RID: 4067
	private Dictionary<int, Dictionary<int, int>> qiYuQuanZhongDictionary;
}
