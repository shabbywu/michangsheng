using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData113 : IJSONClass
{
	public static int SEIDID = 113;

	public static Dictionary<int, BuffSeidJsonData113> DataDict = new Dictionary<int, BuffSeidJsonData113>();

	public static List<BuffSeidJsonData113> DataList = new List<BuffSeidJsonData113>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> value1 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[113].list)
		{
			try
			{
				BuffSeidJsonData113 buffSeidJsonData = new BuffSeidJsonData113();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.value1 = item["value1"].ToList();
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData113.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData113.DataDict添加数据时出现异常，已跳过，请检查配表");
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
