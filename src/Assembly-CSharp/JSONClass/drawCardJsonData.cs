using System;
using System.Collections.Generic;

namespace JSONClass;

public class drawCardJsonData : IJSONClass
{
	public static Dictionary<int, drawCardJsonData> DataDict = new Dictionary<int, drawCardJsonData>();

	public static List<drawCardJsonData> DataList = new List<drawCardJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int probability;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.drawCardJsonData.list)
		{
			try
			{
				drawCardJsonData drawCardJsonData2 = new drawCardJsonData();
				drawCardJsonData2.id = item["id"].I;
				drawCardJsonData2.probability = item["probability"].I;
				if (DataDict.ContainsKey(drawCardJsonData2.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典drawCardJsonData.DataDict添加数据时出现重复的键，Key:{drawCardJsonData2.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(drawCardJsonData2.id, drawCardJsonData2);
				DataList.Add(drawCardJsonData2);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典drawCardJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
