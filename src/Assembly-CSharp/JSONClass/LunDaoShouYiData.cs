using System;
using System.Collections.Generic;

namespace JSONClass;

public class LunDaoShouYiData : IJSONClass
{
	public static Dictionary<int, LunDaoShouYiData> DataDict = new Dictionary<int, LunDaoShouYiData>();

	public static List<LunDaoShouYiData> DataList = new List<LunDaoShouYiData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int WuDaoExp;

	public int WuDaoZhi;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LunDaoShouYiData.list)
		{
			try
			{
				LunDaoShouYiData lunDaoShouYiData = new LunDaoShouYiData();
				lunDaoShouYiData.id = item["id"].I;
				lunDaoShouYiData.WuDaoExp = item["WuDaoExp"].I;
				lunDaoShouYiData.WuDaoZhi = item["WuDaoZhi"].I;
				if (DataDict.ContainsKey(lunDaoShouYiData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LunDaoShouYiData.DataDict添加数据时出现重复的键，Key:{lunDaoShouYiData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lunDaoShouYiData.id, lunDaoShouYiData);
				DataList.Add(lunDaoShouYiData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LunDaoShouYiData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
