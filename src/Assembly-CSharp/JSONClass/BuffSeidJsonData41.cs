using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData41 : IJSONClass
{
	public static int SEIDID = 41;

	public static Dictionary<int, BuffSeidJsonData41> DataDict = new Dictionary<int, BuffSeidJsonData41>();

	public static List<BuffSeidJsonData41> DataList = new List<BuffSeidJsonData41>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public int value2;

	public int value3;

	public List<int> value4 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[41].list)
		{
			try
			{
				BuffSeidJsonData41 buffSeidJsonData = new BuffSeidJsonData41();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.value1 = item["value1"].I;
				buffSeidJsonData.value2 = item["value2"].I;
				buffSeidJsonData.value3 = item["value3"].I;
				buffSeidJsonData.value4 = item["value4"].ToList();
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData41.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData41.DataDict添加数据时出现异常，已跳过，请检查配表");
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
