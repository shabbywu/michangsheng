using System;
using System.Collections.Generic;

namespace JSONClass;

public class CheckInJsonData : IJSONClass
{
	public static Dictionary<int, CheckInJsonData> DataDict = new Dictionary<int, CheckInJsonData>();

	public static List<CheckInJsonData> DataList = new List<CheckInJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int checkinType;

	public int checkinId;

	public int checkincount;

	public int day;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CheckInJsonData.list)
		{
			try
			{
				CheckInJsonData checkInJsonData = new CheckInJsonData();
				checkInJsonData.id = item["id"].I;
				checkInJsonData.checkinType = item["checkinType"].I;
				checkInJsonData.checkinId = item["checkinId"].I;
				checkInJsonData.checkincount = item["checkincount"].I;
				checkInJsonData.day = item["day"].I;
				if (DataDict.ContainsKey(checkInJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CheckInJsonData.DataDict添加数据时出现重复的键，Key:{checkInJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(checkInJsonData.id, checkInJsonData);
				DataList.Add(checkInJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CheckInJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
