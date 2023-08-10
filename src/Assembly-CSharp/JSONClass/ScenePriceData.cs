using System;
using System.Collections.Generic;

namespace JSONClass;

public class ScenePriceData : IJSONClass
{
	public static Dictionary<int, ScenePriceData> DataDict = new Dictionary<int, ScenePriceData>();

	public static List<ScenePriceData> DataList = new List<ScenePriceData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> ItemFlag = new List<int>();

	public List<int> percent = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ScenePriceData.list)
		{
			try
			{
				ScenePriceData scenePriceData = new ScenePriceData();
				scenePriceData.id = item["id"].I;
				scenePriceData.ItemFlag = item["ItemFlag"].ToList();
				scenePriceData.percent = item["percent"].ToList();
				if (DataDict.ContainsKey(scenePriceData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ScenePriceData.DataDict添加数据时出现重复的键，Key:{scenePriceData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(scenePriceData.id, scenePriceData);
				DataList.Add(scenePriceData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ScenePriceData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
