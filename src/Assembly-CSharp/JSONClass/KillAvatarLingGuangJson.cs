using System;
using System.Collections.Generic;

namespace JSONClass;

public class KillAvatarLingGuangJson : IJSONClass
{
	public static Dictionary<int, KillAvatarLingGuangJson> DataDict = new Dictionary<int, KillAvatarLingGuangJson>();

	public static List<KillAvatarLingGuangJson> DataList = new List<KillAvatarLingGuangJson>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int lingguangid;

	public List<int> avatar = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.KillAvatarLingGuangJson.list)
		{
			try
			{
				KillAvatarLingGuangJson killAvatarLingGuangJson = new KillAvatarLingGuangJson();
				killAvatarLingGuangJson.id = item["id"].I;
				killAvatarLingGuangJson.lingguangid = item["lingguangid"].I;
				killAvatarLingGuangJson.avatar = item["avatar"].ToList();
				if (DataDict.ContainsKey(killAvatarLingGuangJson.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典KillAvatarLingGuangJson.DataDict添加数据时出现重复的键，Key:{killAvatarLingGuangJson.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(killAvatarLingGuangJson.id, killAvatarLingGuangJson);
				DataList.Add(killAvatarLingGuangJson);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典KillAvatarLingGuangJson.DataDict添加数据时出现异常，已跳过，请检查配表");
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
