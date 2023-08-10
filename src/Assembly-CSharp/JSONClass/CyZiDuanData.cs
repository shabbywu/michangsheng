using System;
using System.Collections.Generic;

namespace JSONClass;

public class CyZiDuanData : IJSONClass
{
	public static Dictionary<int, CyZiDuanData> DataDict = new Dictionary<int, CyZiDuanData>();

	public static List<CyZiDuanData> DataList = new List<CyZiDuanData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CyZiDuanData.list)
		{
			try
			{
				CyZiDuanData cyZiDuanData = new CyZiDuanData();
				cyZiDuanData.id = item["id"].I;
				cyZiDuanData.name = item["name"].Str;
				if (DataDict.ContainsKey(cyZiDuanData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CyZiDuanData.DataDict添加数据时出现重复的键，Key:{cyZiDuanData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(cyZiDuanData.id, cyZiDuanData);
				DataList.Add(cyZiDuanData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CyZiDuanData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
