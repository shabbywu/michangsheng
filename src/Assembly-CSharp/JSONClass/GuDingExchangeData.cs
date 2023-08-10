using System;
using System.Collections.Generic;

namespace JSONClass;

public class GuDingExchangeData : IJSONClass
{
	public static Dictionary<int, GuDingExchangeData> DataDict = new Dictionary<int, GuDingExchangeData>();

	public static List<GuDingExchangeData> DataList = new List<GuDingExchangeData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ItemID;

	public string fuhao;

	public List<int> YiWuFlag = new List<int>();

	public List<int> NumFlag = new List<int>();

	public List<int> YiWuItem = new List<int>();

	public List<int> NumItem = new List<int>();

	public List<int> EventValue = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.GuDingExchangeData.list)
		{
			try
			{
				GuDingExchangeData guDingExchangeData = new GuDingExchangeData();
				guDingExchangeData.id = item["id"].I;
				guDingExchangeData.ItemID = item["ItemID"].I;
				guDingExchangeData.fuhao = item["fuhao"].Str;
				guDingExchangeData.YiWuFlag = item["YiWuFlag"].ToList();
				guDingExchangeData.NumFlag = item["NumFlag"].ToList();
				guDingExchangeData.YiWuItem = item["YiWuItem"].ToList();
				guDingExchangeData.NumItem = item["NumItem"].ToList();
				guDingExchangeData.EventValue = item["EventValue"].ToList();
				if (DataDict.ContainsKey(guDingExchangeData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典GuDingExchangeData.DataDict添加数据时出现重复的键，Key:{guDingExchangeData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(guDingExchangeData.id, guDingExchangeData);
				DataList.Add(guDingExchangeData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典GuDingExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
