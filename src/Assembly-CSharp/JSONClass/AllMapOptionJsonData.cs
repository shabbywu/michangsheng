using System;
using System.Collections.Generic;

namespace JSONClass;

public class AllMapOptionJsonData : IJSONClass
{
	public static Dictionary<int, AllMapOptionJsonData> DataDict = new Dictionary<int, AllMapOptionJsonData>();

	public static List<AllMapOptionJsonData> DataList = new List<AllMapOptionJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public int value2;

	public int value3;

	public int value4;

	public int value5;

	public int value8;

	public int value9;

	public int value10;

	public string EventName;

	public string desc;

	public List<int> value6 = new List<int>();

	public List<int> value7 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AllMapOptionJsonData.list)
		{
			try
			{
				AllMapOptionJsonData allMapOptionJsonData = new AllMapOptionJsonData();
				allMapOptionJsonData.id = item["id"].I;
				allMapOptionJsonData.value1 = item["value1"].I;
				allMapOptionJsonData.value2 = item["value2"].I;
				allMapOptionJsonData.value3 = item["value3"].I;
				allMapOptionJsonData.value4 = item["value4"].I;
				allMapOptionJsonData.value5 = item["value5"].I;
				allMapOptionJsonData.value8 = item["value8"].I;
				allMapOptionJsonData.value9 = item["value9"].I;
				allMapOptionJsonData.value10 = item["value10"].I;
				allMapOptionJsonData.EventName = item["EventName"].Str;
				allMapOptionJsonData.desc = item["desc"].Str;
				allMapOptionJsonData.value6 = item["value6"].ToList();
				allMapOptionJsonData.value7 = item["value7"].ToList();
				if (DataDict.ContainsKey(allMapOptionJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AllMapOptionJsonData.DataDict添加数据时出现重复的键，Key:{allMapOptionJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(allMapOptionJsonData.id, allMapOptionJsonData);
				DataList.Add(allMapOptionJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AllMapOptionJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
