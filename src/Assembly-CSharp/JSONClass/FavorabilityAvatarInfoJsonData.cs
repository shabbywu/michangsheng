using System;
using System.Collections.Generic;

namespace JSONClass;

public class FavorabilityAvatarInfoJsonData : IJSONClass
{
	public static Dictionary<int, FavorabilityAvatarInfoJsonData> DataDict = new Dictionary<int, FavorabilityAvatarInfoJsonData>();

	public static List<FavorabilityAvatarInfoJsonData> DataList = new List<FavorabilityAvatarInfoJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> AvatarID = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.FavorabilityAvatarInfoJsonData.list)
		{
			try
			{
				FavorabilityAvatarInfoJsonData favorabilityAvatarInfoJsonData = new FavorabilityAvatarInfoJsonData();
				favorabilityAvatarInfoJsonData.id = item["id"].I;
				favorabilityAvatarInfoJsonData.AvatarID = item["AvatarID"].ToList();
				if (DataDict.ContainsKey(favorabilityAvatarInfoJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典FavorabilityAvatarInfoJsonData.DataDict添加数据时出现重复的键，Key:{favorabilityAvatarInfoJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(favorabilityAvatarInfoJsonData.id, favorabilityAvatarInfoJsonData);
				DataList.Add(favorabilityAvatarInfoJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典FavorabilityAvatarInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
