using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuDaoZhiData : IJSONClass
{
	public static Dictionary<int, WuDaoZhiData> DataDict = new Dictionary<int, WuDaoZhiData>();

	public static List<WuDaoZhiData> DataList = new List<WuDaoZhiData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int LevelUpExp;

	public int LevelUpNum;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoZhiData.list)
		{
			try
			{
				WuDaoZhiData wuDaoZhiData = new WuDaoZhiData();
				wuDaoZhiData.id = item["id"].I;
				wuDaoZhiData.LevelUpExp = item["LevelUpExp"].I;
				wuDaoZhiData.LevelUpNum = item["LevelUpNum"].I;
				if (DataDict.ContainsKey(wuDaoZhiData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuDaoZhiData.DataDict添加数据时出现重复的键，Key:{wuDaoZhiData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuDaoZhiData.id, wuDaoZhiData);
				DataList.Add(wuDaoZhiData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuDaoZhiData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
