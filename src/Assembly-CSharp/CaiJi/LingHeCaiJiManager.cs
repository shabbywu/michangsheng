using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace CaiJi;

public static class LingHeCaiJiManager
{
	public static bool IsOnTiaoZhan;

	public static LingHeTiaoZhanArg LingHeTiaoZhanArg;

	public static void TryOpenCaiJi(int mapIndex)
	{
		if (UINPCJiaoHu.Inst.NPCIDList.Count > 0)
		{
			LingHeTiaoZhanArg = new LingHeTiaoZhanArg();
			LingHeCaiJi lingHeCaiJi = LingHeCaiJi.DataDict[mapIndex];
			LingHeTiaoZhanArg.LingMaiLv = lingHeCaiJi.ShouYiLv;
			LingHeTiaoZhanArg.NPCID = UINPCJiaoHu.Inst.NPCIDList[0];
			IsOnTiaoZhan = true;
		}
		else
		{
			ResManager.inst.LoadPrefab("LingHeCaiJiPanel").Inst();
			LingHeCaiJiUIMag.inst.OpenCaiJi(mapIndex);
		}
	}

	public static LingHeCaiJiResult DoCaiJi(int mapIndex, int costTime)
	{
		LingHeCaiJiResult lingHeCaiJiResult = new LingHeCaiJiResult();
		lingHeCaiJiResult.RealCostTime = costTime;
		LingHeCaiJi lingHeCaiJi = LingHeCaiJi.DataDict[mapIndex];
		LingMaiPinJie lingMaiPinJie = LingMaiPinJie.DataDict[lingHeCaiJi.ShouYiLv];
		Avatar player = PlayerEx.Player;
		JSONObject lingHeCaiJi2 = player.LingHeCaiJi;
		bool flag = false;
		if (lingHeCaiJi2.HasField("LastTiaoZhan") && DateTime.Parse(lingHeCaiJi2["LastTiaoZhan"].str).AddYears(1) >= player.worldTimeMag.getNowTime())
		{
			flag = true;
		}
		if (!flag)
		{
			float num = Mathf.Pow(0.95f, (float)costTime) * 100f;
			if (player.RandomSeedNext() % 100 > (int)num)
			{
				lingHeCaiJiResult.RealCostTime = player.RandomSeedNext() % costTime + 1;
				lingHeCaiJiResult.HasTiaoZhan = true;
			}
		}
		int num2 = 0;
		for (int i = 0; i < lingHeCaiJiResult.RealCostTime; i++)
		{
			int num3 = 10 - player.RandomSeedNext() % 20;
			int num4 = lingMaiPinJie.ShouYiLv * (100 + num3) / 100;
			num2 += num4;
		}
		if (player.shengShi >= lingHeCaiJi.ShengShiLimit)
		{
			lingHeCaiJiResult.LingHeCount = num2 * lingHeCaiJi.LingHe / 10000;
			lingHeCaiJiResult.LingShiCount = num2 * (100 - lingHeCaiJi.LingHe) / 100;
		}
		else
		{
			lingHeCaiJiResult.LingHeCount = 0;
			lingHeCaiJiResult.LingShiCount = num2 / 2;
		}
		return lingHeCaiJiResult;
	}
}
