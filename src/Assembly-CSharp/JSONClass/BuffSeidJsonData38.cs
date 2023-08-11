using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData38 : IJSONClass
{
	public static int SEIDID = 38;

	public static Dictionary<int, BuffSeidJsonData38> DataDict = new Dictionary<int, BuffSeidJsonData38>();

	public static List<BuffSeidJsonData38> DataList = new List<BuffSeidJsonData38>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public int value2;

	public int value3;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[38].list)
		{
			try
			{
				BuffSeidJsonData38 buffSeidJsonData = new BuffSeidJsonData38();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.value1 = item["value1"].I;
				buffSeidJsonData.value2 = item["value2"].I;
				buffSeidJsonData.value3 = item["value3"].I;
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData38.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData38.DataDict添加数据时出现异常，已跳过，请检查配表");
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
