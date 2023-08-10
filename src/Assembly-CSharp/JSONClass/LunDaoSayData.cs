using System;
using System.Collections.Generic;

namespace JSONClass;

public class LunDaoSayData : IJSONClass
{
	public static Dictionary<int, LunDaoSayData> DataDict = new Dictionary<int, LunDaoSayData>();

	public static List<LunDaoSayData> DataList = new List<LunDaoSayData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int WuDaoId;

	public string Desc1;

	public string Desc2;

	public string Desc3;

	public string Desc4;

	public string Desc5;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LunDaoSayData.list)
		{
			try
			{
				LunDaoSayData lunDaoSayData = new LunDaoSayData();
				lunDaoSayData.id = item["id"].I;
				lunDaoSayData.WuDaoId = item["WuDaoId"].I;
				lunDaoSayData.Desc1 = item["Desc1"].Str;
				lunDaoSayData.Desc2 = item["Desc2"].Str;
				lunDaoSayData.Desc3 = item["Desc3"].Str;
				lunDaoSayData.Desc4 = item["Desc4"].Str;
				lunDaoSayData.Desc5 = item["Desc5"].Str;
				if (DataDict.ContainsKey(lunDaoSayData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LunDaoSayData.DataDict添加数据时出现重复的键，Key:{lunDaoSayData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lunDaoSayData.id, lunDaoSayData);
				DataList.Add(lunDaoSayData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LunDaoSayData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
