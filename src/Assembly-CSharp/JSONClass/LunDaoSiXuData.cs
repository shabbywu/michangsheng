using System;
using System.Collections.Generic;

namespace JSONClass;

public class LunDaoSiXuData : IJSONClass
{
	public static Dictionary<int, LunDaoSiXuData> DataDict = new Dictionary<int, LunDaoSiXuData>();

	public static List<LunDaoSiXuData> DataList = new List<LunDaoSiXuData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int PinJie;

	public int SiXvXiaoLv;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LunDaoSiXuData.list)
		{
			try
			{
				LunDaoSiXuData lunDaoSiXuData = new LunDaoSiXuData();
				lunDaoSiXuData.id = item["id"].I;
				lunDaoSiXuData.PinJie = item["PinJie"].I;
				lunDaoSiXuData.SiXvXiaoLv = item["SiXvXiaoLv"].I;
				if (DataDict.ContainsKey(lunDaoSiXuData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LunDaoSiXuData.DataDict添加数据时出现重复的键，Key:{lunDaoSiXuData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lunDaoSiXuData.id, lunDaoSiXuData);
				DataList.Add(lunDaoSiXuData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LunDaoSiXuData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
