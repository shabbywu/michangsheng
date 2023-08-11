using System;
using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData312 : IJSONClass
{
	public static int SEIDID = 312;

	public static Dictionary<int, BuffSeidJsonData312> DataDict = new Dictionary<int, BuffSeidJsonData312>();

	public static List<BuffSeidJsonData312> DataList = new List<BuffSeidJsonData312>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public string panduan;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[312].list)
		{
			try
			{
				BuffSeidJsonData312 buffSeidJsonData = new BuffSeidJsonData312();
				buffSeidJsonData.id = item["id"].I;
				buffSeidJsonData.value1 = item["value1"].I;
				buffSeidJsonData.panduan = item["panduan"].Str;
				if (DataDict.ContainsKey(buffSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BuffSeidJsonData312.DataDict添加数据时出现重复的键，Key:{buffSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				DataList.Add(buffSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData312.DataDict添加数据时出现异常，已跳过，请检查配表");
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