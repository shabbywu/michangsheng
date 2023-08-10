using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData131 : IJSONClass
{
	public static int SEIDID = 131;

	public static Dictionary<int, BuffSeidJsonData131> DataDict = new Dictionary<int, BuffSeidJsonData131>();

	public static List<BuffSeidJsonData131> DataList = new List<BuffSeidJsonData131>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public List<int> value2 = new List<int>();

	public List<int> value3 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[131].list)
		{
			try
			{
				BuffSeidJsonData131 buffSeidJsonData = new BuffSeidJsonData131();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.value1 = item["value1"].I;
				buffSeidJsonData.value2 = item["value2"].ToList();
				buffSeidJsonData.value3 = item["value3"].ToList();
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData131.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData131.DataDict添加数据时出现异常，已跳过，请检查配表");
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
