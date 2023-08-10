using System;
using System.Collections.Generic;

namespace JSONClass;

public class StaticLVToLevelJsonData : IJSONClass
{
	public static Dictionary<int, StaticLVToLevelJsonData> DataDict = new Dictionary<int, StaticLVToLevelJsonData>();

	public static List<StaticLVToLevelJsonData> DataList = new List<StaticLVToLevelJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Max1;

	public int Max2;

	public int Max3;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.StaticLVToLevelJsonData.list)
		{
			try
			{
				StaticLVToLevelJsonData staticLVToLevelJsonData = new StaticLVToLevelJsonData();
				staticLVToLevelJsonData.id = item["id"].I;
				staticLVToLevelJsonData.Max1 = item["Max1"].I;
				staticLVToLevelJsonData.Max2 = item["Max2"].I;
				staticLVToLevelJsonData.Max3 = item["Max3"].I;
				staticLVToLevelJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(staticLVToLevelJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典StaticLVToLevelJsonData.DataDict添加数据时出现重复的键，Key:{staticLVToLevelJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(staticLVToLevelJsonData.id, staticLVToLevelJsonData);
				DataList.Add(staticLVToLevelJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典StaticLVToLevelJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
