using System;
using System.Collections.Generic;

namespace JSONClass;

public class RunawayJsonData : IJSONClass
{
	public static Dictionary<int, RunawayJsonData> DataDict = new Dictionary<int, RunawayJsonData>();

	public static List<RunawayJsonData> DataList = new List<RunawayJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int RunCha;

	public int RunTime;

	public int RunDistance;

	public string Text;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.RunawayJsonData.list)
		{
			try
			{
				RunawayJsonData runawayJsonData = new RunawayJsonData();
				runawayJsonData.id = item["id"].I;
				runawayJsonData.RunCha = item["RunCha"].I;
				runawayJsonData.RunTime = item["RunTime"].I;
				runawayJsonData.RunDistance = item["RunDistance"].I;
				runawayJsonData.Text = item["Text"].Str;
				if (DataDict.ContainsKey(runawayJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典RunawayJsonData.DataDict添加数据时出现重复的键，Key:{runawayJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(runawayJsonData.id, runawayJsonData);
				DataList.Add(runawayJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典RunawayJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
