using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuXianBiGuanJsonData : IJSONClass
{
	public static Dictionary<int, WuXianBiGuanJsonData> DataDict = new Dictionary<int, WuXianBiGuanJsonData>();

	public static List<WuXianBiGuanJsonData> DataList = new List<WuXianBiGuanJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string SceneName;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuXianBiGuanJsonData.list)
		{
			try
			{
				WuXianBiGuanJsonData wuXianBiGuanJsonData = new WuXianBiGuanJsonData();
				wuXianBiGuanJsonData.id = item["id"].I;
				wuXianBiGuanJsonData.SceneName = item["SceneName"].Str;
				if (DataDict.ContainsKey(wuXianBiGuanJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuXianBiGuanJsonData.DataDict添加数据时出现重复的键，Key:{wuXianBiGuanJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuXianBiGuanJsonData.id, wuXianBiGuanJsonData);
				DataList.Add(wuXianBiGuanJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuXianBiGuanJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
