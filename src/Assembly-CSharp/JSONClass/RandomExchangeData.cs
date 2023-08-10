using System;
using System.Collections.Generic;

namespace JSONClass;

public class RandomExchangeData : IJSONClass
{
	public static Dictionary<int, RandomExchangeData> DataDict = new Dictionary<int, RandomExchangeData>();

	public static List<RandomExchangeData> DataList = new List<RandomExchangeData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ItemID;

	public int percent;

	public List<int> YiWuFlag = new List<int>();

	public List<int> NumFlag = new List<int>();

	public List<int> YiWuItem = new List<int>();

	public List<int> NumItem = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.RandomExchangeData.list)
		{
			try
			{
				RandomExchangeData randomExchangeData = new RandomExchangeData();
				randomExchangeData.id = item["id"].I;
				randomExchangeData.ItemID = item["ItemID"].I;
				randomExchangeData.percent = item["percent"].I;
				randomExchangeData.YiWuFlag = item["YiWuFlag"].ToList();
				randomExchangeData.NumFlag = item["NumFlag"].ToList();
				randomExchangeData.YiWuItem = item["YiWuItem"].ToList();
				randomExchangeData.NumItem = item["NumItem"].ToList();
				if (DataDict.ContainsKey(randomExchangeData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典RandomExchangeData.DataDict添加数据时出现重复的键，Key:{randomExchangeData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(randomExchangeData.id, randomExchangeData);
				DataList.Add(randomExchangeData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典RandomExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
