using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData133 : IJSONClass
{
	public static int SEIDID = 133;

	public static Dictionary<int, BuffSeidJsonData133> DataDict = new Dictionary<int, BuffSeidJsonData133>();

	public static List<BuffSeidJsonData133> DataList = new List<BuffSeidJsonData133>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int target;

	public int value1;

	public int value3;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[133].list)
		{
			try
			{
				BuffSeidJsonData133 buffSeidJsonData = new BuffSeidJsonData133();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.target = item["target"].I;
				buffSeidJsonData.value1 = item["value1"].I;
				buffSeidJsonData.value3 = item["value3"].I;
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData133.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData133.DataDict添加数据时出现异常，已跳过，请检查配表");
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
