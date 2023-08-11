using System;
using System.Collections.Generic;

namespace JSONClass;

public class LingGanTimeMaxData : IJSONClass
{
	public static Dictionary<int, LingGanTimeMaxData> DataDict = new Dictionary<int, LingGanTimeMaxData>();

	public static List<LingGanTimeMaxData> DataList = new List<LingGanTimeMaxData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int MaxTime;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LingGanTimeMaxData.list)
		{
			try
			{
				LingGanTimeMaxData lingGanTimeMaxData = new LingGanTimeMaxData();
				lingGanTimeMaxData.id = item["id"].I;
				lingGanTimeMaxData.MaxTime = item["MaxTime"].I;
				if (DataDict.ContainsKey(lingGanTimeMaxData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LingGanTimeMaxData.DataDict添加数据时出现重复的键，Key:{lingGanTimeMaxData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lingGanTimeMaxData.id, lingGanTimeMaxData);
				DataList.Add(lingGanTimeMaxData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LingGanTimeMaxData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
