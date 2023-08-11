using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShangJinPingFenData : IJSONClass
{
	public static Dictionary<int, ShangJinPingFenData> DataDict = new Dictionary<int, ShangJinPingFenData>();

	public static List<ShangJinPingFenData> DataList = new List<ShangJinPingFenData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int PingFen;

	public int EWaiPingFen;

	public int ShaShouLv;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShangJinPingFenData.list)
		{
			try
			{
				ShangJinPingFenData shangJinPingFenData = new ShangJinPingFenData();
				shangJinPingFenData.id = item["id"].I;
				shangJinPingFenData.PingFen = item["PingFen"].I;
				shangJinPingFenData.EWaiPingFen = item["EWaiPingFen"].I;
				shangJinPingFenData.ShaShouLv = item["ShaShouLv"].I;
				if (DataDict.ContainsKey(shangJinPingFenData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ShangJinPingFenData.DataDict添加数据时出现重复的键，Key:{shangJinPingFenData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shangJinPingFenData.id, shangJinPingFenData);
				DataList.Add(shangJinPingFenData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShangJinPingFenData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
