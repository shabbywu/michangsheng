using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData209 : IJSONClass
{
	public static int SEIDID = 209;

	public static Dictionary<int, BuffSeidJsonData209> DataDict = new Dictionary<int, BuffSeidJsonData209>();

	public static List<BuffSeidJsonData209> DataList = new List<BuffSeidJsonData209>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[209].list)
		{
			try
			{
				BuffSeidJsonData209 buffSeidJsonData = new BuffSeidJsonData209();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData209.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData209.DataDict添加数据时出现异常，已跳过，请检查配表");
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
