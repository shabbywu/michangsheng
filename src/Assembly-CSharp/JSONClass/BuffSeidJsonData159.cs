using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData159 : IJSONClass
{
	public static int SEIDID = 159;

	public static Dictionary<int, BuffSeidJsonData159> DataDict = new Dictionary<int, BuffSeidJsonData159>();

	public static List<BuffSeidJsonData159> DataList = new List<BuffSeidJsonData159>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int target;

	public int value1;

	public int value2;

	public string panduan;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[159].list)
		{
			try
			{
				BuffSeidJsonData159 buffSeidJsonData = new BuffSeidJsonData159();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.target = item["target"].I;
				buffSeidJsonData.value1 = item["value1"].I;
				buffSeidJsonData.value2 = item["value2"].I;
				buffSeidJsonData.panduan = item["panduan"].Str;
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData159.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData159.DataDict添加数据时出现异常，已跳过，请检查配表");
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
