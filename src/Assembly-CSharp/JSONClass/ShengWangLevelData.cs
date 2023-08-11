using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShengWangLevelData : IJSONClass
{
	public static Dictionary<int, ShengWangLevelData> DataDict = new Dictionary<int, ShengWangLevelData>();

	public static List<ShengWangLevelData> DataList = new List<ShengWangLevelData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string ShengWang;

	public List<int> ShengWangQuJian = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShengWangLevelData.list)
		{
			try
			{
				ShengWangLevelData shengWangLevelData = new ShengWangLevelData();
				shengWangLevelData.id = item["id"].I;
				shengWangLevelData.ShengWang = item["ShengWang"].Str;
				shengWangLevelData.ShengWangQuJian = item["ShengWangQuJian"].ToList();
				if (DataDict.ContainsKey(shengWangLevelData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ShengWangLevelData.DataDict添加数据时出现重复的键，Key:{shengWangLevelData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shengWangLevelData.id, shengWangLevelData);
				DataList.Add(shengWangLevelData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShengWangLevelData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
