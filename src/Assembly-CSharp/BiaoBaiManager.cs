﻿using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x02000218 RID: 536
public static class BiaoBaiManager
{
	// Token: 0x060015A1 RID: 5537 RVA: 0x00090B64 File Offset: 0x0008ED64
	private static void Init()
	{
		if (!BiaoBaiManager.isinited)
		{
			BiaoBaiManager.isinited = true;
			foreach (JSONObject jsonobject in jsonData.instance.NpcBiaoBaiTiKuData.list)
			{
				TiKuData tiKuData = new TiKuData();
				tiKuData.id = jsonobject["id"].I;
				tiKuData.Type = jsonobject["Type"].I;
				tiKuData.TiWen = jsonobject["TiWen"].Str;
				tiKuData.optionDesc[0] = jsonobject["optionDesc1"].Str;
				tiKuData.optionDesc[1] = jsonobject["optionDesc2"].Str;
				tiKuData.optionDesc[2] = jsonobject["optionDesc3"].Str;
				BiaoBaiManager.TiKuDatas.Add(tiKuData);
			}
			foreach (JSONObject jsonobject2 in jsonData.instance.NpcBiaoBaiTiWenData.list)
			{
				TiWenData tiWenData = new TiWenData();
				tiWenData.id = jsonobject2["id"].I;
				tiWenData.TiWen = jsonobject2["TiWen"].I;
				tiWenData.XingGe = jsonobject2["XingGe"].I;
				tiWenData.BiaoQian = jsonobject2["BiaoQian"].I;
				tiWenData.optionDesc[0] = jsonobject2["optionDesc1"].I;
				tiWenData.optionDesc[1] = jsonobject2["optionDesc2"].I;
				tiWenData.optionDesc[2] = jsonobject2["optionDesc3"].I;
				BiaoBaiManager.TiWenDatas.Add(tiWenData);
			}
		}
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x00090D70 File Offset: 0x0008EF70
	public static void InitBiaoBai()
	{
		BiaoBaiManager.Init();
		BiaoBaiManager.options = new int[3];
		BiaoBaiManager.optionResults = new int[3];
		BiaoBaiManager.BiaoBaiScore = new BiaoBaiScore();
		BiaoBaiManager.CalcBiaoBaiScore();
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x00090D9C File Offset: 0x0008EF9C
	public static void CalcBiaoBaiScore()
	{
		BiaoBaiManager.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		Avatar player = PlayerEx.Player;
		BiaoBaiManager.BiaoBaiScore.FavorScore = BiaoBaiManager.npc.Favor;
		bool flag;
		if (BiaoBaiManager.npc.IsNingZhouNPC)
		{
			flag = (PlayerEx.GetNingZhouShengWangLevel() > 3);
		}
		else
		{
			flag = (PlayerEx.GetSeaShengWangLevel() > 3);
		}
		if (flag != BiaoBaiManager.npc.ZhengXie)
		{
			BiaoBaiManager.BiaoBaiScore.ZhengXieScore = -10;
		}
		int num = BiaoBaiManager.npc.BigLevel - player.getLevelType();
		if (num > 0)
		{
			BiaoBaiManager.BiaoBaiScore.LevelScore = -num * 10;
		}
		else if (num < 0)
		{
			BiaoBaiManager.BiaoBaiScore.LevelScore = -num * 5;
		}
		int num2 = Mathf.Abs(BiaoBaiManager.npc.Age - (int)player.age);
		num2 /= 100;
		BiaoBaiManager.BiaoBaiScore.AgeScore = -num2;
		int num3 = GlobalValue.Get(170, "BiaoBaiManager.CalcBiaoBaiScore 获取道侣数量");
		BiaoBaiManager.BiaoBaiScore.DaoLvScore = -num3 * 5;
		if (player.DongFuData.Count == 0)
		{
			BiaoBaiManager.BiaoBaiScore.DongFuScore = -10;
		}
		BiaoBaiManager.BiaoBaiScore.DaTiScore = 0;
		int i;
		int j;
		for (i = 0; i < 3; i = j + 1)
		{
			foreach (TiWenData tiWenData in BiaoBaiManager.TiWenDatas.FindAll((TiWenData t) => t.TiWen == BiaoBaiManager.options[i]))
			{
				if (tiWenData.XingGe > 0)
				{
					if (tiWenData.XingGe == BiaoBaiManager.npc.XingGe)
					{
						BiaoBaiManager.BiaoBaiScore.DaTiScore += tiWenData.optionDesc[BiaoBaiManager.optionResults[i] - 1];
						break;
					}
				}
				else if (tiWenData.BiaoQian > 0 && tiWenData.BiaoQian == BiaoBaiManager.npc.Tag)
				{
					BiaoBaiManager.BiaoBaiScore.DaTiScore += tiWenData.optionDesc[BiaoBaiManager.optionResults[i] - 1];
					break;
				}
			}
			j = i;
		}
		BiaoBaiManager.BiaoBaiScore.OtherTotalScore = 0;
		BiaoBaiManager.BiaoBaiScore.TotalScore = 0;
		BiaoBaiManager.BiaoBaiScore.OtherTotalScore += BiaoBaiManager.BiaoBaiScore.FavorScore;
		BiaoBaiManager.BiaoBaiScore.OtherTotalScore += BiaoBaiManager.BiaoBaiScore.ZhengXieScore;
		BiaoBaiManager.BiaoBaiScore.OtherTotalScore += BiaoBaiManager.BiaoBaiScore.LevelScore;
		BiaoBaiManager.BiaoBaiScore.OtherTotalScore += BiaoBaiManager.BiaoBaiScore.AgeScore;
		BiaoBaiManager.BiaoBaiScore.OtherTotalScore += BiaoBaiManager.BiaoBaiScore.DaoLvScore;
		BiaoBaiManager.BiaoBaiScore.OtherTotalScore += BiaoBaiManager.BiaoBaiScore.DongFuScore;
		BiaoBaiManager.BiaoBaiScore.TotalScore += BiaoBaiManager.BiaoBaiScore.OtherTotalScore;
		BiaoBaiManager.BiaoBaiScore.TotalScore += BiaoBaiManager.BiaoBaiScore.DaTiScore;
		BiaoBaiManager.BiaoBaiScore.Player18 = (PlayerEx.Player.age >= 18U);
		BiaoBaiManager.BiaoBaiScore.NPC18 = (BiaoBaiManager.npc.Age >= 18);
		if (!BiaoBaiManager.BiaoBaiScore.Player18 || !BiaoBaiManager.BiaoBaiScore.NPC18)
		{
			BiaoBaiManager.BiaoBaiScore.TotalScore = 0;
		}
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x00091114 File Offset: 0x0008F314
	public static void GetRandomTiKu(int type, out TiKuData ti)
	{
		List<TiKuData> list = BiaoBaiManager.TiKuDatas.FindAll((TiKuData t) => t.Type == type);
		ti = list[Random.Range(0, list.Count)];
		BiaoBaiManager.options[type - 1] = ti.id;
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x0009116E File Offset: 0x0008F36E
	public static void SetPlayerOptionResult(int type, int xuanze)
	{
		BiaoBaiManager.optionResults[type - 1] = xuanze;
	}

	// Token: 0x04001012 RID: 4114
	private static bool isinited;

	// Token: 0x04001013 RID: 4115
	private static List<TiKuData> TiKuDatas = new List<TiKuData>();

	// Token: 0x04001014 RID: 4116
	private static List<TiWenData> TiWenDatas = new List<TiWenData>();

	// Token: 0x04001015 RID: 4117
	private static UINPCData npc;

	// Token: 0x04001016 RID: 4118
	public static BiaoBaiScore BiaoBaiScore;

	// Token: 0x04001017 RID: 4119
	private static int[] options;

	// Token: 0x04001018 RID: 4120
	private static int[] optionResults;
}
