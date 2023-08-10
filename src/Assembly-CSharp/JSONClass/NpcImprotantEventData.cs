using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcImprotantEventData : IJSONClass
{
	public static Dictionary<int, NpcImprotantEventData> DataDict = new Dictionary<int, NpcImprotantEventData>();

	public static List<NpcImprotantEventData> DataList = new List<NpcImprotantEventData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ImportantNPC;

	public string Time;

	public string fuhao;

	public string ShiJianInfo;

	public List<int> EventLv = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcImprotantEventData.list)
		{
			try
			{
				NpcImprotantEventData npcImprotantEventData = new NpcImprotantEventData();
				npcImprotantEventData.id = item["id"].I;
				npcImprotantEventData.ImportantNPC = item["ImportantNPC"].I;
				npcImprotantEventData.Time = item["Time"].Str;
				npcImprotantEventData.fuhao = item["fuhao"].Str;
				npcImprotantEventData.ShiJianInfo = item["ShiJianInfo"].Str;
				npcImprotantEventData.EventLv = item["EventLv"].ToList();
				if (DataDict.ContainsKey(npcImprotantEventData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcImprotantEventData.DataDict添加数据时出现重复的键，Key:{npcImprotantEventData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcImprotantEventData.id, npcImprotantEventData);
				DataList.Add(npcImprotantEventData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcImprotantEventData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
