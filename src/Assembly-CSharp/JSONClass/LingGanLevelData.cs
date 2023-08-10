using System;
using System.Collections.Generic;

namespace JSONClass;

public class LingGanLevelData : IJSONClass
{
	public static Dictionary<int, LingGanLevelData> DataDict = new Dictionary<int, LingGanLevelData>();

	public static List<LingGanLevelData> DataList = new List<LingGanLevelData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int lingGanQuJian;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LingGanLevelData.list)
		{
			try
			{
				LingGanLevelData lingGanLevelData = new LingGanLevelData();
				lingGanLevelData.id = item["id"].I;
				lingGanLevelData.lingGanQuJian = item["lingGanQuJian"].I;
				if (DataDict.ContainsKey(lingGanLevelData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LingGanLevelData.DataDict添加数据时出现重复的键，Key:{lingGanLevelData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lingGanLevelData.id, lingGanLevelData);
				DataList.Add(lingGanLevelData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LingGanLevelData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
