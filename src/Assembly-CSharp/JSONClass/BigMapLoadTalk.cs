using System;
using System.Collections.Generic;

namespace JSONClass;

public class BigMapLoadTalk : IJSONClass
{
	public static Dictionary<int, BigMapLoadTalk> DataDict = new Dictionary<int, BigMapLoadTalk>();

	public static List<BigMapLoadTalk> DataList = new List<BigMapLoadTalk>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Talk;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BigMapLoadTalk.list)
		{
			try
			{
				BigMapLoadTalk bigMapLoadTalk = new BigMapLoadTalk();
				bigMapLoadTalk.id = item["id"].I;
				bigMapLoadTalk.Talk = item["Talk"].I;
				if (DataDict.ContainsKey(bigMapLoadTalk.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BigMapLoadTalk.DataDict添加数据时出现重复的键，Key:{bigMapLoadTalk.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(bigMapLoadTalk.id, bigMapLoadTalk);
				DataList.Add(bigMapLoadTalk);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BigMapLoadTalk.DataDict添加数据时出现异常，已跳过，请检查配表");
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
