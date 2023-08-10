using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcImprotantPanDingData : IJSONClass
{
	public static Dictionary<int, NpcImprotantPanDingData> DataDict = new Dictionary<int, NpcImprotantPanDingData>();

	public static List<NpcImprotantPanDingData> DataList = new List<NpcImprotantPanDingData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int NPC;

	public int XingWei;

	public string fuhao;

	public string StartTime;

	public string EndTime;

	public List<int> EventValue = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcImprotantPanDingData.list)
		{
			try
			{
				NpcImprotantPanDingData npcImprotantPanDingData = new NpcImprotantPanDingData();
				npcImprotantPanDingData.id = item["id"].I;
				npcImprotantPanDingData.NPC = item["NPC"].I;
				npcImprotantPanDingData.XingWei = item["XingWei"].I;
				npcImprotantPanDingData.fuhao = item["fuhao"].Str;
				npcImprotantPanDingData.StartTime = item["StartTime"].Str;
				npcImprotantPanDingData.EndTime = item["EndTime"].Str;
				npcImprotantPanDingData.EventValue = item["EventValue"].ToList();
				if (DataDict.ContainsKey(npcImprotantPanDingData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcImprotantPanDingData.DataDict添加数据时出现重复的键，Key:{npcImprotantPanDingData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcImprotantPanDingData.id, npcImprotantPanDingData);
				DataList.Add(npcImprotantPanDingData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcImprotantPanDingData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
