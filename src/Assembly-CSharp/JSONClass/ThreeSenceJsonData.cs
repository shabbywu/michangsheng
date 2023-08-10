using System;
using System.Collections.Generic;

namespace JSONClass;

public class ThreeSenceJsonData : IJSONClass
{
	public static Dictionary<int, ThreeSenceJsonData> DataDict = new Dictionary<int, ThreeSenceJsonData>();

	public static List<ThreeSenceJsonData> DataList = new List<ThreeSenceJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int SceneID;

	public int AvatarID;

	public int TalkID;

	public int circulation;

	public int transaction;

	public int transaction2;

	public int transaction3;

	public int qiecuoLv;

	public string StarTime;

	public string EndTime;

	public string FirstSay;

	public List<int> Level = new List<int>();

	public List<int> SaticValue = new List<int>();

	public List<int> SaticValueX = new List<int>();

	public List<int> TaskIconValue = new List<int>();

	public List<int> TaskIconValueX = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ThreeSenceJsonData.list)
		{
			try
			{
				ThreeSenceJsonData threeSenceJsonData = new ThreeSenceJsonData();
				threeSenceJsonData.id = item["id"].I;
				threeSenceJsonData.SceneID = item["SceneID"].I;
				threeSenceJsonData.AvatarID = item["AvatarID"].I;
				threeSenceJsonData.TalkID = item["TalkID"].I;
				threeSenceJsonData.circulation = item["circulation"].I;
				threeSenceJsonData.transaction = item["transaction"].I;
				threeSenceJsonData.transaction2 = item["transaction2"].I;
				threeSenceJsonData.transaction3 = item["transaction3"].I;
				threeSenceJsonData.qiecuoLv = item["qiecuoLv"].I;
				threeSenceJsonData.StarTime = item["StarTime"].Str;
				threeSenceJsonData.EndTime = item["EndTime"].Str;
				threeSenceJsonData.FirstSay = item["FirstSay"].Str;
				threeSenceJsonData.Level = item["Level"].ToList();
				threeSenceJsonData.SaticValue = item["SaticValue"].ToList();
				threeSenceJsonData.SaticValueX = item["SaticValueX"].ToList();
				threeSenceJsonData.TaskIconValue = item["TaskIconValue"].ToList();
				threeSenceJsonData.TaskIconValueX = item["TaskIconValueX"].ToList();
				if (DataDict.ContainsKey(threeSenceJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ThreeSenceJsonData.DataDict添加数据时出现重复的键，Key:{threeSenceJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(threeSenceJsonData.id, threeSenceJsonData);
				DataList.Add(threeSenceJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ThreeSenceJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
