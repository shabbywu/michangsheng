using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSONClass;

public class TianJiDaBiReward : IJSONClass
{
	public static Dictionary<int, TianJiDaBiReward> DataDict = new Dictionary<int, TianJiDaBiReward>();

	public static List<TianJiDaBiReward> DataList = new List<TianJiDaBiReward>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> Reward1 = new List<int>();

	public List<int> Reward2 = new List<int>();

	public List<int> Reward3 = new List<int>();

	public List<int> Reward4 = new List<int>();

	public List<int> Reward5 = new List<int>();

	public List<int> Reward6 = new List<int>();

	public List<int> Reward7 = new List<int>();

	public List<int> Reward8 = new List<int>();

	public List<int> Reward9 = new List<int>();

	public List<int> Reward10 = new List<int>();

	public Dictionary<int, List<int>> RewardDict;

	private static bool rewardInited;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TianJiDaBiReward.list)
		{
			try
			{
				TianJiDaBiReward tianJiDaBiReward = new TianJiDaBiReward();
				tianJiDaBiReward.id = item["id"].I;
				tianJiDaBiReward.Reward1 = item["Reward1"].ToList();
				tianJiDaBiReward.Reward2 = item["Reward2"].ToList();
				tianJiDaBiReward.Reward3 = item["Reward3"].ToList();
				tianJiDaBiReward.Reward4 = item["Reward4"].ToList();
				tianJiDaBiReward.Reward5 = item["Reward5"].ToList();
				tianJiDaBiReward.Reward6 = item["Reward6"].ToList();
				tianJiDaBiReward.Reward7 = item["Reward7"].ToList();
				tianJiDaBiReward.Reward8 = item["Reward8"].ToList();
				tianJiDaBiReward.Reward9 = item["Reward9"].ToList();
				tianJiDaBiReward.Reward10 = item["Reward10"].ToList();
				if (DataDict.ContainsKey(tianJiDaBiReward.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TianJiDaBiReward.DataDict添加数据时出现重复的键，Key:{tianJiDaBiReward.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(tianJiDaBiReward.id, tianJiDaBiReward);
				DataList.Add(tianJiDaBiReward);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TianJiDaBiReward.DataDict添加数据时出现异常，已跳过，请检查配表");
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

	private static void InitReward()
	{
		if (rewardInited)
		{
			return;
		}
		rewardInited = true;
		foreach (TianJiDaBiReward data in DataList)
		{
			data.RewardDict = new Dictionary<int, List<int>>();
			data.RewardDict[1] = data.Reward1;
			data.RewardDict[2] = data.Reward2;
			data.RewardDict[3] = data.Reward3;
			data.RewardDict[4] = data.Reward4;
			data.RewardDict[5] = data.Reward5;
			data.RewardDict[6] = data.Reward6;
			data.RewardDict[7] = data.Reward7;
			data.RewardDict[8] = data.Reward8;
			data.RewardDict[9] = data.Reward9;
			data.RewardDict[10] = data.Reward10;
		}
	}

	public static List<int> GetReward(int liuPai, int rank)
	{
		InitReward();
		TianJiDaBiReward tianJiDaBiReward = null;
		if (DataDict.ContainsKey(liuPai))
		{
			tianJiDaBiReward = DataDict[liuPai];
		}
		else if (DataDict.ContainsKey(999))
		{
			tianJiDaBiReward = DataDict[999];
		}
		else
		{
			Debug.LogError((object)"天机大比奖励无法获取到保底流派999");
			new List<int>();
		}
		if (tianJiDaBiReward.RewardDict.ContainsKey(rank))
		{
			return tianJiDaBiReward.RewardDict[rank];
		}
		Debug.LogError((object)$"获取天机大比奖励时使用了未计划的排名{rank}，返回了空列表");
		return new List<int>();
	}
}
