using System;
using System.Collections.Generic;

namespace JSONClass;

public class AllMapCaiJiAddItemBiao : IJSONClass
{
	public static Dictionary<int, AllMapCaiJiAddItemBiao> DataDict = new Dictionary<int, AllMapCaiJiAddItemBiao>();

	public static List<AllMapCaiJiAddItemBiao> DataList = new List<AllMapCaiJiAddItemBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int ID;

	public int percent;

	public int time;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AllMapCaiJiAddItemBiao.list)
		{
			try
			{
				AllMapCaiJiAddItemBiao allMapCaiJiAddItemBiao = new AllMapCaiJiAddItemBiao();
				allMapCaiJiAddItemBiao.ID = item["ID"].I;
				allMapCaiJiAddItemBiao.percent = item["percent"].I;
				allMapCaiJiAddItemBiao.time = item["time"].I;
				if (DataDict.ContainsKey(allMapCaiJiAddItemBiao.ID))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AllMapCaiJiAddItemBiao.DataDict添加数据时出现重复的键，Key:{allMapCaiJiAddItemBiao.ID}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(allMapCaiJiAddItemBiao.ID, allMapCaiJiAddItemBiao);
				DataList.Add(allMapCaiJiAddItemBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AllMapCaiJiAddItemBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
