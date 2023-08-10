using System;
using System.Collections.Generic;

namespace JSONClass;

public class LingGanMaxData : IJSONClass
{
	public static Dictionary<int, LingGanMaxData> DataDict = new Dictionary<int, LingGanMaxData>();

	public static List<LingGanMaxData> DataList = new List<LingGanMaxData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int lingGanShangXian;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LingGanMaxData.list)
		{
			try
			{
				LingGanMaxData lingGanMaxData = new LingGanMaxData();
				lingGanMaxData.id = item["id"].I;
				lingGanMaxData.lingGanShangXian = item["lingGanShangXian"].I;
				if (DataDict.ContainsKey(lingGanMaxData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LingGanMaxData.DataDict添加数据时出现重复的键，Key:{lingGanMaxData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lingGanMaxData.id, lingGanMaxData);
				DataList.Add(lingGanMaxData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LingGanMaxData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
