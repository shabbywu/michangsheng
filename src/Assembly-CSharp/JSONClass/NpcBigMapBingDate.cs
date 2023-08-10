using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcBigMapBingDate : IJSONClass
{
	public static Dictionary<int, NpcBigMapBingDate> DataDict = new Dictionary<int, NpcBigMapBingDate>();

	public static List<NpcBigMapBingDate> DataList = new List<NpcBigMapBingDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int MapType;

	public int NPCType;

	public int MapD;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcBigMapBingDate.list)
		{
			try
			{
				NpcBigMapBingDate npcBigMapBingDate = new NpcBigMapBingDate();
				npcBigMapBingDate.id = item["id"].I;
				npcBigMapBingDate.MapType = item["MapType"].I;
				npcBigMapBingDate.NPCType = item["NPCType"].I;
				npcBigMapBingDate.MapD = item["MapD"].I;
				if (DataDict.ContainsKey(npcBigMapBingDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcBigMapBingDate.DataDict添加数据时出现重复的键，Key:{npcBigMapBingDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcBigMapBingDate.id, npcBigMapBingDate);
				DataList.Add(npcBigMapBingDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcBigMapBingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
