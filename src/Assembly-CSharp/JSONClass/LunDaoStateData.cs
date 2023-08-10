using System;
using System.Collections.Generic;

namespace JSONClass;

public class LunDaoStateData : IJSONClass
{
	public static Dictionary<int, LunDaoStateData> DataDict = new Dictionary<int, LunDaoStateData>();

	public static List<LunDaoStateData> DataList = new List<LunDaoStateData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Time;

	public int WuDaoExp;

	public int WuDaoZhi;

	public int LingGanXiaoHao;

	public string ZhuangTaiInfo;

	public string MiaoShu;

	public string MiaoShu1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LunDaoStateData.list)
		{
			try
			{
				LunDaoStateData lunDaoStateData = new LunDaoStateData();
				lunDaoStateData.id = item["id"].I;
				lunDaoStateData.Time = item["Time"].I;
				lunDaoStateData.WuDaoExp = item["WuDaoExp"].I;
				lunDaoStateData.WuDaoZhi = item["WuDaoZhi"].I;
				lunDaoStateData.LingGanXiaoHao = item["LingGanXiaoHao"].I;
				lunDaoStateData.ZhuangTaiInfo = item["ZhuangTaiInfo"].Str;
				lunDaoStateData.MiaoShu = item["MiaoShu"].Str;
				lunDaoStateData.MiaoShu1 = item["MiaoShu1"].Str;
				if (DataDict.ContainsKey(lunDaoStateData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LunDaoStateData.DataDict添加数据时出现重复的键，Key:{lunDaoStateData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lunDaoStateData.id, lunDaoStateData);
				DataList.Add(lunDaoStateData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LunDaoStateData.DataDict添加数据时出现异常，已跳过，请检查配表");
				PreloadManager.LogException($"异常信息:\n{arg}");
				PreloadManager.LogException($"数据序列化:\n{item}");
			}
		}
		if (OnInitFinishAction != null)
		{
			OnInitFinishAction();
		}
	}

	private static void OnInitFinish()
	{
	}
}
