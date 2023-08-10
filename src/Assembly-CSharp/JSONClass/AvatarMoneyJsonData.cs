using System;
using System.Collections.Generic;

namespace JSONClass;

public class AvatarMoneyJsonData : IJSONClass
{
	public static Dictionary<int, AvatarMoneyJsonData> DataDict = new Dictionary<int, AvatarMoneyJsonData>();

	public static List<AvatarMoneyJsonData> DataList = new List<AvatarMoneyJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int level;

	public int Min;

	public int Max;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AvatarMoneyJsonData.list)
		{
			try
			{
				AvatarMoneyJsonData avatarMoneyJsonData = new AvatarMoneyJsonData();
				avatarMoneyJsonData.id = item["id"].I;
				avatarMoneyJsonData.level = item["level"].I;
				avatarMoneyJsonData.Min = item["Min"].I;
				avatarMoneyJsonData.Max = item["Max"].I;
				if (DataDict.ContainsKey(avatarMoneyJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AvatarMoneyJsonData.DataDict添加数据时出现重复的键，Key:{avatarMoneyJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(avatarMoneyJsonData.id, avatarMoneyJsonData);
				DataList.Add(avatarMoneyJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AvatarMoneyJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
