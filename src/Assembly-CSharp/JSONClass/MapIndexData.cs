using System;
using System.Collections.Generic;

namespace JSONClass;

public class MapIndexData : IJSONClass
{
	public static Dictionary<int, MapIndexData> DataDict = new Dictionary<int, MapIndexData>();

	public static List<MapIndexData> DataList = new List<MapIndexData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int mapIndex;

	public string StrValue;

	public string name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.MapIndexData.list)
		{
			try
			{
				MapIndexData mapIndexData = new MapIndexData();
				mapIndexData.id = item["id"].I;
				mapIndexData.mapIndex = item["mapIndex"].I;
				mapIndexData.StrValue = item["StrValue"].Str;
				mapIndexData.name = item["name"].Str;
				if (DataDict.ContainsKey(mapIndexData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典MapIndexData.DataDict添加数据时出现重复的键，Key:{mapIndexData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(mapIndexData.id, mapIndexData);
				DataList.Add(mapIndexData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典MapIndexData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
