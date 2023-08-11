using System;
using System.Collections.Generic;

namespace JSONClass;

public class FavorabilityInfoJsonData : IJSONClass
{
	public static Dictionary<int, FavorabilityInfoJsonData> DataDict = new Dictionary<int, FavorabilityInfoJsonData>();

	public static List<FavorabilityInfoJsonData> DataList = new List<FavorabilityInfoJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int AvatarID;

	public int JinDu;

	public int ItemID;

	public int HaoGanDu;

	public int Time;

	public int AvatarLevel;

	public string yes;

	public string no;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.FavorabilityInfoJsonData.list)
		{
			try
			{
				FavorabilityInfoJsonData favorabilityInfoJsonData = new FavorabilityInfoJsonData();
				favorabilityInfoJsonData.id = item["id"].I;
				favorabilityInfoJsonData.AvatarID = item["AvatarID"].I;
				favorabilityInfoJsonData.JinDu = item["JinDu"].I;
				favorabilityInfoJsonData.ItemID = item["ItemID"].I;
				favorabilityInfoJsonData.HaoGanDu = item["HaoGanDu"].I;
				favorabilityInfoJsonData.Time = item["Time"].I;
				favorabilityInfoJsonData.AvatarLevel = item["AvatarLevel"].I;
				favorabilityInfoJsonData.yes = item["yes"].Str;
				favorabilityInfoJsonData.no = item["no"].Str;
				if (DataDict.ContainsKey(favorabilityInfoJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典FavorabilityInfoJsonData.DataDict添加数据时出现重复的键，Key:{favorabilityInfoJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(favorabilityInfoJsonData.id, favorabilityInfoJsonData);
				DataList.Add(favorabilityInfoJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典FavorabilityInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
