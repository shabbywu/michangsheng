using System;
using System.Collections.Generic;

namespace JSONClass;

public class LunDaoReduceData : IJSONClass
{
	public static Dictionary<int, LunDaoReduceData> DataDict = new Dictionary<int, LunDaoReduceData>();

	public static List<LunDaoReduceData> DataList = new List<LunDaoReduceData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ShuaiJianXiShu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LunDaoReduceData.list)
		{
			try
			{
				LunDaoReduceData lunDaoReduceData = new LunDaoReduceData();
				lunDaoReduceData.id = item["id"].I;
				lunDaoReduceData.ShuaiJianXiShu = item["ShuaiJianXiShu"].I;
				if (DataDict.ContainsKey(lunDaoReduceData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LunDaoReduceData.DataDict添加数据时出现重复的键，Key:{lunDaoReduceData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lunDaoReduceData.id, lunDaoReduceData);
				DataList.Add(lunDaoReduceData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LunDaoReduceData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
