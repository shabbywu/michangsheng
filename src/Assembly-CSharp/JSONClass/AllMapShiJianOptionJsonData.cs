using System;
using System.Collections.Generic;

namespace JSONClass;

public class AllMapShiJianOptionJsonData : IJSONClass
{
	public static Dictionary<int, AllMapShiJianOptionJsonData> DataDict = new Dictionary<int, AllMapShiJianOptionJsonData>();

	public static List<AllMapShiJianOptionJsonData> DataList = new List<AllMapShiJianOptionJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int option1;

	public int option2;

	public int option3;

	public int optionID;

	public string EventName;

	public string desc;

	public string optionDesc1;

	public string optionDesc2;

	public string optionDesc3;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AllMapShiJianOptionJsonData.list)
		{
			try
			{
				AllMapShiJianOptionJsonData allMapShiJianOptionJsonData = new AllMapShiJianOptionJsonData();
				allMapShiJianOptionJsonData.id = item["id"].I;
				allMapShiJianOptionJsonData.option1 = item["option1"].I;
				allMapShiJianOptionJsonData.option2 = item["option2"].I;
				allMapShiJianOptionJsonData.option3 = item["option3"].I;
				allMapShiJianOptionJsonData.optionID = item["optionID"].I;
				allMapShiJianOptionJsonData.EventName = item["EventName"].Str;
				allMapShiJianOptionJsonData.desc = item["desc"].Str;
				allMapShiJianOptionJsonData.optionDesc1 = item["optionDesc1"].Str;
				allMapShiJianOptionJsonData.optionDesc2 = item["optionDesc2"].Str;
				allMapShiJianOptionJsonData.optionDesc3 = item["optionDesc3"].Str;
				if (DataDict.ContainsKey(allMapShiJianOptionJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AllMapShiJianOptionJsonData.DataDict添加数据时出现重复的键，Key:{allMapShiJianOptionJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(allMapShiJianOptionJsonData.id, allMapShiJianOptionJsonData);
				DataList.Add(allMapShiJianOptionJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AllMapShiJianOptionJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
