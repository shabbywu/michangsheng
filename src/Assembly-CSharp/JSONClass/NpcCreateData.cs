using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcCreateData : IJSONClass
{
	public static Dictionary<int, NpcCreateData> DataDict = new Dictionary<int, NpcCreateData>();

	public static List<NpcCreateData> DataList = new List<NpcCreateData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int NumA;

	public int NumB;

	public List<int> EventValue = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcCreateData.list)
		{
			try
			{
				NpcCreateData npcCreateData = new NpcCreateData();
				npcCreateData.id = item["id"].I;
				npcCreateData.NumA = item["NumA"].I;
				npcCreateData.NumB = item["NumB"].I;
				npcCreateData.EventValue = item["EventValue"].ToList();
				if (DataDict.ContainsKey(npcCreateData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcCreateData.DataDict添加数据时出现重复的键，Key:{npcCreateData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcCreateData.id, npcCreateData);
				DataList.Add(npcCreateData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcCreateData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
