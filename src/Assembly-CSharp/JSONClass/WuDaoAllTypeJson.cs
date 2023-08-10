using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuDaoAllTypeJson : IJSONClass
{
	public static Dictionary<int, WuDaoAllTypeJson> DataDict = new Dictionary<int, WuDaoAllTypeJson>();

	public static List<WuDaoAllTypeJson> DataList = new List<WuDaoAllTypeJson>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string name;

	public string name1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoAllTypeJson.list)
		{
			try
			{
				WuDaoAllTypeJson wuDaoAllTypeJson = new WuDaoAllTypeJson();
				wuDaoAllTypeJson.id = item["id"].I;
				wuDaoAllTypeJson.name = item["name"].Str;
				wuDaoAllTypeJson.name1 = item["name1"].Str;
				if (DataDict.ContainsKey(wuDaoAllTypeJson.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuDaoAllTypeJson.DataDict添加数据时出现重复的键，Key:{wuDaoAllTypeJson.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuDaoAllTypeJson.id, wuDaoAllTypeJson);
				DataList.Add(wuDaoAllTypeJson);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuDaoAllTypeJson.DataDict添加数据时出现异常，已跳过，请检查配表");
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
