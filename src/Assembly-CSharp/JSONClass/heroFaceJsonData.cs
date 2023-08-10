using System;
using System.Collections.Generic;

namespace JSONClass;

public class heroFaceJsonData : IJSONClass
{
	public static Dictionary<int, heroFaceJsonData> DataDict = new Dictionary<int, heroFaceJsonData>();

	public static List<heroFaceJsonData> DataList = new List<heroFaceJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int HeroId;

	public List<int> surfaceId = new List<int>();

	public List<int> faceMode = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.heroFaceJsonData.list)
		{
			try
			{
				heroFaceJsonData heroFaceJsonData2 = new heroFaceJsonData();
				heroFaceJsonData2.id = item["id"].I;
				heroFaceJsonData2.HeroId = item["HeroId"].I;
				heroFaceJsonData2.surfaceId = item["surfaceId"].ToList();
				heroFaceJsonData2.faceMode = item["faceMode"].ToList();
				if (DataDict.ContainsKey(heroFaceJsonData2.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典heroFaceJsonData.DataDict添加数据时出现重复的键，Key:{heroFaceJsonData2.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(heroFaceJsonData2.id, heroFaceJsonData2);
				DataList.Add(heroFaceJsonData2);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典heroFaceJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
