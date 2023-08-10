using System;
using System.Collections.Generic;

namespace JSONClass;

public class DisableExchangeData : IJSONClass
{
	public static Dictionary<int, DisableExchangeData> DataDict = new Dictionary<int, DisableExchangeData>();

	public static List<DisableExchangeData> DataList = new List<DisableExchangeData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DisableExchangeData.list)
		{
			try
			{
				DisableExchangeData disableExchangeData = new DisableExchangeData();
				disableExchangeData.id = item["id"].I;
				if (DataDict.ContainsKey(disableExchangeData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DisableExchangeData.DataDict添加数据时出现重复的键，Key:{disableExchangeData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(disableExchangeData.id, disableExchangeData);
				DataList.Add(disableExchangeData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DisableExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
