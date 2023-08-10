using System.Collections.Generic;
using KBEngine;
using UnityEngine;

public static class BiaoBaiManager
{
	private static bool isinited;

	private static List<TiKuData> TiKuDatas = new List<TiKuData>();

	private static List<TiWenData> TiWenDatas = new List<TiWenData>();

	private static UINPCData npc;

	public static BiaoBaiScore BiaoBaiScore;

	private static int[] options;

	private static int[] optionResults;

	private static void Init()
	{
		if (isinited)
		{
			return;
		}
		isinited = true;
		foreach (JSONObject item in jsonData.instance.NpcBiaoBaiTiKuData.list)
		{
			TiKuData tiKuData = new TiKuData();
			tiKuData.id = item["id"].I;
			tiKuData.Type = item["Type"].I;
			tiKuData.TiWen = item["TiWen"].Str;
			tiKuData.optionDesc[0] = item["optionDesc1"].Str;
			tiKuData.optionDesc[1] = item["optionDesc2"].Str;
			tiKuData.optionDesc[2] = item["optionDesc3"].Str;
			TiKuDatas.Add(tiKuData);
		}
		foreach (JSONObject item2 in jsonData.instance.NpcBiaoBaiTiWenData.list)
		{
			TiWenData tiWenData = new TiWenData();
			tiWenData.id = item2["id"].I;
			tiWenData.TiWen = item2["TiWen"].I;
			tiWenData.XingGe = item2["XingGe"].I;
			tiWenData.BiaoQian = item2["BiaoQian"].I;
			tiWenData.optionDesc[0] = item2["optionDesc1"].I;
			tiWenData.optionDesc[1] = item2["optionDesc2"].I;
			tiWenData.optionDesc[2] = item2["optionDesc3"].I;
			TiWenDatas.Add(tiWenData);
		}
	}

	public static void InitBiaoBai()
	{
		Init();
		options = new int[3];
		optionResults = new int[3];
		BiaoBaiScore = new BiaoBaiScore();
		CalcBiaoBaiScore();
	}

	public static void CalcBiaoBaiScore()
	{
		npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		Avatar player = PlayerEx.Player;
		BiaoBaiScore.FavorScore = npc.Favor;
		bool flag = ((!npc.IsNingZhouNPC) ? (PlayerEx.GetSeaShengWangLevel() > 3) : (PlayerEx.GetNingZhouShengWangLevel() > 3));
		if (flag != npc.ZhengXie)
		{
			BiaoBaiScore.ZhengXieScore = -10;
		}
		int num = npc.BigLevel - player.getLevelType();
		if (num > 0)
		{
			BiaoBaiScore.LevelScore = -num * 10;
		}
		else if (num < 0)
		{
			BiaoBaiScore.LevelScore = -num * 5;
		}
		int num2 = Mathf.Abs(npc.Age - (int)player.age);
		num2 /= 100;
		BiaoBaiScore.AgeScore = -num2;
		int num3 = GlobalValue.Get(170, "BiaoBaiManager.CalcBiaoBaiScore 获取道侣数量");
		BiaoBaiScore.DaoLvScore = -num3 * 5;
		if (player.DongFuData.Count == 0)
		{
			BiaoBaiScore.DongFuScore = -10;
		}
		BiaoBaiScore.DaTiScore = 0;
		int i;
		for (i = 0; i < 3; i++)
		{
			foreach (TiWenData item in TiWenDatas.FindAll((TiWenData t) => t.TiWen == options[i]))
			{
				if (item.XingGe > 0)
				{
					if (item.XingGe == npc.XingGe)
					{
						BiaoBaiScore.DaTiScore += item.optionDesc[optionResults[i] - 1];
						break;
					}
				}
				else if (item.BiaoQian > 0 && item.BiaoQian == npc.Tag)
				{
					BiaoBaiScore.DaTiScore += item.optionDesc[optionResults[i] - 1];
					break;
				}
			}
		}
		BiaoBaiScore.OtherTotalScore = 0;
		BiaoBaiScore.TotalScore = 0;
		BiaoBaiScore.OtherTotalScore += BiaoBaiScore.FavorScore;
		BiaoBaiScore.OtherTotalScore += BiaoBaiScore.ZhengXieScore;
		BiaoBaiScore.OtherTotalScore += BiaoBaiScore.LevelScore;
		BiaoBaiScore.OtherTotalScore += BiaoBaiScore.AgeScore;
		BiaoBaiScore.OtherTotalScore += BiaoBaiScore.DaoLvScore;
		BiaoBaiScore.OtherTotalScore += BiaoBaiScore.DongFuScore;
		BiaoBaiScore.TotalScore += BiaoBaiScore.OtherTotalScore;
		BiaoBaiScore.TotalScore += BiaoBaiScore.DaTiScore;
		BiaoBaiScore.Player18 = PlayerEx.Player.age >= 18;
		BiaoBaiScore.NPC18 = npc.Age >= 18;
		if (!BiaoBaiScore.Player18 || !BiaoBaiScore.NPC18)
		{
			BiaoBaiScore.TotalScore = 0;
		}
	}

	public static void GetRandomTiKu(int type, out TiKuData ti)
	{
		List<TiKuData> list = TiKuDatas.FindAll((TiKuData t) => t.Type == type);
		ti = list[Random.Range(0, list.Count)];
		options[type - 1] = ti.id;
	}

	public static void SetPlayerOptionResult(int type, int xuanze)
	{
		optionResults[type - 1] = xuanze;
	}
}
