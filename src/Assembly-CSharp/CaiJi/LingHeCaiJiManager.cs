using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace CaiJi
{
	// Token: 0x02000AA0 RID: 2720
	public static class LingHeCaiJiManager
	{
		// Token: 0x060045BF RID: 17855 RVA: 0x001DCFFC File Offset: 0x001DB1FC
		public static void TryOpenCaiJi(int mapIndex)
		{
			if (UINPCJiaoHu.Inst.NPCIDList.Count > 0)
			{
				LingHeCaiJiManager.LingHeTiaoZhanArg = new LingHeTiaoZhanArg();
				LingHeCaiJi lingHeCaiJi = LingHeCaiJi.DataDict[mapIndex];
				LingHeCaiJiManager.LingHeTiaoZhanArg.LingMaiLv = lingHeCaiJi.ShouYiLv;
				LingHeCaiJiManager.LingHeTiaoZhanArg.NPCID = UINPCJiaoHu.Inst.NPCIDList[0];
				LingHeCaiJiManager.IsOnTiaoZhan = true;
				return;
			}
			ResManager.inst.LoadPrefab("LingHeCaiJiPanel").Inst(null);
			LingHeCaiJiUIMag.inst.OpenCaiJi(mapIndex);
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x001DD084 File Offset: 0x001DB284
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

		// Token: 0x04003DE7 RID: 15847
		public static bool IsOnTiaoZhan;

		// Token: 0x04003DE8 RID: 15848
		public static LingHeTiaoZhanArg LingHeTiaoZhanArg;
	}
}
