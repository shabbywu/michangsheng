using System;
using System.Collections.Generic;

namespace JSONClass;

public class AllMapLuDainType : IJSONClass
{
	public static Dictionary<int, AllMapLuDainType> DataDict = new Dictionary<int, AllMapLuDainType>();

	public static List<AllMapLuDainType> DataList = new List<AllMapLuDainType>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int MapType;

	public string LuDianName;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AllMapLuDainType.list)
		{
			try
			{
				AllMapLuDainType allMapLuDainType = new AllMapLuDainType();
				allMapLuDainType.id = item["id"].I;
				allMapLuDainType.MapType = item["MapType"].I;
				allMapLuDainType.LuDianName = item["LuDianName"].Str;
				if (DataDict.ContainsKey(allMapLuDainType.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AllMapLuDainType.DataDict添加数据时出现重复的键，Key:{allMapLuDainType.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(allMapLuDainType.id, allMapLuDainType);
				DataList.Add(allMapLuDainType);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AllMapLuDainType.DataDict添加数据时出现异常，已跳过，请检查配表");
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
