using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianQiJieSuanBiao : IJSONClass
{
	public static Dictionary<int, LianQiJieSuanBiao> DataDict = new Dictionary<int, LianQiJieSuanBiao>();

	public static List<LianQiJieSuanBiao> DataList = new List<LianQiJieSuanBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int damage;

	public int exp;

	public int time;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianQiJieSuanBiao.list)
		{
			try
			{
				LianQiJieSuanBiao lianQiJieSuanBiao = new LianQiJieSuanBiao();
				lianQiJieSuanBiao.id = item["id"].I;
				lianQiJieSuanBiao.damage = item["damage"].I;
				lianQiJieSuanBiao.exp = item["exp"].I;
				lianQiJieSuanBiao.time = item["time"].I;
				if (DataDict.ContainsKey(lianQiJieSuanBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianQiJieSuanBiao.DataDict添加数据时出现重复的键，Key:{lianQiJieSuanBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianQiJieSuanBiao.id, lianQiJieSuanBiao);
				DataList.Add(lianQiJieSuanBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianQiJieSuanBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
