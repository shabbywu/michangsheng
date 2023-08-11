using System;
using System.Collections.Generic;

namespace JSONClass;

public class FightAIData : IJSONClass
{
	public static Dictionary<int, FightAIData> DataDict = new Dictionary<int, FightAIData>();

	public static List<FightAIData> DataList = new List<FightAIData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> ShunXu = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.FightAIData.list)
		{
			try
			{
				FightAIData fightAIData = new FightAIData();
				fightAIData.id = item["id"].I;
				fightAIData.ShunXu = item["ShunXu"].ToList();
				if (DataDict.ContainsKey(fightAIData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典FightAIData.DataDict添加数据时出现重复的键，Key:{fightAIData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(fightAIData.id, fightAIData);
				DataList.Add(fightAIData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典FightAIData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
