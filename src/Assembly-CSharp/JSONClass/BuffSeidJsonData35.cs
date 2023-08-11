using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData35 : IJSONClass
{
	public static int SEIDID = 35;

	public static Dictionary<int, BuffSeidJsonData35> DataDict = new Dictionary<int, BuffSeidJsonData35>();

	public static List<BuffSeidJsonData35> DataList = new List<BuffSeidJsonData35>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public int value2;

	public int value3;

	public int value4;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[35].list)
		{
			try
			{
				BuffSeidJsonData35 buffSeidJsonData = new BuffSeidJsonData35();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.value1 = item["value1"].I;
				buffSeidJsonData.value2 = item["value2"].I;
				buffSeidJsonData.value3 = item["value3"].I;
				buffSeidJsonData.value4 = item["value4"].I;
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData35.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData35.DataDict添加数据时出现异常，已跳过，请检查配表");
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