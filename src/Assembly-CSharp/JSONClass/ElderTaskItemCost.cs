using System;
using System.Collections.Generic;

namespace JSONClass;

public class ElderTaskItemCost : IJSONClass
{
	public static Dictionary<int, ElderTaskItemCost> DataDict = new Dictionary<int, ElderTaskItemCost>();

	public static List<ElderTaskItemCost> DataList = new List<ElderTaskItemCost>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int quality;

	public int time;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ElderTaskItemCost.list)
		{
			try
			{
				ElderTaskItemCost elderTaskItemCost = new ElderTaskItemCost();
				elderTaskItemCost.quality = item["quality"].I;
				elderTaskItemCost.time = item["time"].I;
				if (DataDict.ContainsKey(elderTaskItemCost.quality))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ElderTaskItemCost.DataDict添加数据时出现重复的键，Key:{elderTaskItemCost.quality}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(elderTaskItemCost.quality, elderTaskItemCost);
				DataList.Add(elderTaskItemCost);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ElderTaskItemCost.DataDict添加数据时出现异常，已跳过，请检查配表");
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
