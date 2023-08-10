using System;
using System.Collections.Generic;

namespace JSONClass;

public class MapRandomJsonData : IJSONClass
{
	public static Dictionary<int, MapRandomJsonData> DataDict = new Dictionary<int, MapRandomJsonData>();

	public static List<MapRandomJsonData> DataList = new List<MapRandomJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int EventType;

	public int EventList;

	public int EventData;

	public int MosterID;

	public int EventCastTime;

	public int percent;

	public int once;

	public string EventName;

	public string Icon;

	public string StartTime;

	public string EndTime;

	public string fuhao;

	public List<int> EventLv = new List<int>();

	public List<int> EventValue = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.MapRandomJsonData.list)
		{
			try
			{
				MapRandomJsonData mapRandomJsonData = new MapRandomJsonData();
				mapRandomJsonData.id = item["id"].I;
				mapRandomJsonData.EventType = item["EventType"].I;
				mapRandomJsonData.EventList = item["EventList"].I;
				mapRandomJsonData.EventData = item["EventData"].I;
				mapRandomJsonData.MosterID = item["MosterID"].I;
				mapRandomJsonData.EventCastTime = item["EventCastTime"].I;
				mapRandomJsonData.percent = item["percent"].I;
				mapRandomJsonData.once = item["once"].I;
				mapRandomJsonData.EventName = item["EventName"].Str;
				mapRandomJsonData.Icon = item["Icon"].Str;
				mapRandomJsonData.StartTime = item["StartTime"].Str;
				mapRandomJsonData.EndTime = item["EndTime"].Str;
				mapRandomJsonData.fuhao = item["fuhao"].Str;
				mapRandomJsonData.EventLv = item["EventLv"].ToList();
				mapRandomJsonData.EventValue = item["EventValue"].ToList();
				if (DataDict.ContainsKey(mapRandomJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典MapRandomJsonData.DataDict添加数据时出现重复的键，Key:{mapRandomJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(mapRandomJsonData.id, mapRandomJsonData);
				DataList.Add(mapRandomJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典MapRandomJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
