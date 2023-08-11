using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData70 : IJSONClass
{
	public static int SEIDID = 70;

	public static Dictionary<int, BuffSeidJsonData70> DataDict = new Dictionary<int, BuffSeidJsonData70>();

	public static List<BuffSeidJsonData70> DataList = new List<BuffSeidJsonData70>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value2;

	public List<int> value1 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[70].list)
		{
			try
			{
				BuffSeidJsonData70 buffSeidJsonData = new BuffSeidJsonData70();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.value2 = item["value2"].I;
				buffSeidJsonData.value1 = item["value1"].ToList();
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData70.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData70.DataDict添加数据时出现异常，已跳过，请检查配表");
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
