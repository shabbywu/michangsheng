using System;
using System.Collections.Generic;

namespace JSONClass;

public class SceneNameJsonData : IJSONClass
{
	public static Dictionary<string, SceneNameJsonData> DataDict = new Dictionary<string, SceneNameJsonData>();

	public static List<SceneNameJsonData> DataList = new List<SceneNameJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int MapType;

	public int MoneyType;

	public int HighlightID;

	public string id;

	public string EventName;

	public string MapName;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SceneNameJsonData.list)
		{
			try
			{
				SceneNameJsonData sceneNameJsonData = new SceneNameJsonData();
				sceneNameJsonData.MapType = item["MapType"].I;
				sceneNameJsonData.MoneyType = item["MoneyType"].I;
				sceneNameJsonData.HighlightID = item["HighlightID"].I;
				sceneNameJsonData.id = item["id"].Str;
				sceneNameJsonData.EventName = item["EventName"].Str;
				sceneNameJsonData.MapName = item["MapName"].Str;
				if (DataDict.ContainsKey(sceneNameJsonData.id))
				{
					PreloadManager.LogException("!!!错误!!!向字典SceneNameJsonData.DataDict添加数据时出现重复的键，Key:" + sceneNameJsonData.id + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(sceneNameJsonData.id, sceneNameJsonData);
				DataList.Add(sceneNameJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SceneNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
