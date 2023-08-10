using System;
using System.Collections.Generic;

namespace JSONClass;

public class CyShiLiNameData : IJSONClass
{
	public static Dictionary<int, CyShiLiNameData> DataDict = new Dictionary<int, CyShiLiNameData>();

	public static List<CyShiLiNameData> DataList = new List<CyShiLiNameData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CyShiLiNameData.list)
		{
			try
			{
				CyShiLiNameData cyShiLiNameData = new CyShiLiNameData();
				cyShiLiNameData.id = item["id"].I;
				cyShiLiNameData.name = item["name"].Str;
				if (DataDict.ContainsKey(cyShiLiNameData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CyShiLiNameData.DataDict添加数据时出现重复的键，Key:{cyShiLiNameData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(cyShiLiNameData.id, cyShiLiNameData);
				DataList.Add(cyShiLiNameData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CyShiLiNameData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
