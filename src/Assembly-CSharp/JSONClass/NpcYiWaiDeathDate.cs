using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcYiWaiDeathDate : IJSONClass
{
	public static Dictionary<int, NpcYiWaiDeathDate> DataDict = new Dictionary<int, NpcYiWaiDeathDate>();

	public static List<NpcYiWaiDeathDate> DataList = new List<NpcYiWaiDeathDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int HaoGanDu;

	public int SiWangJiLv;

	public int SiWangLeiXing;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcYiWaiDeathDate.list)
		{
			try
			{
				NpcYiWaiDeathDate npcYiWaiDeathDate = new NpcYiWaiDeathDate();
				npcYiWaiDeathDate.id = item["id"].I;
				npcYiWaiDeathDate.HaoGanDu = item["HaoGanDu"].I;
				npcYiWaiDeathDate.SiWangJiLv = item["SiWangJiLv"].I;
				npcYiWaiDeathDate.SiWangLeiXing = item["SiWangLeiXing"].I;
				if (DataDict.ContainsKey(npcYiWaiDeathDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcYiWaiDeathDate.DataDict添加数据时出现重复的键，Key:{npcYiWaiDeathDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcYiWaiDeathDate.id, npcYiWaiDeathDate);
				DataList.Add(npcYiWaiDeathDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcYiWaiDeathDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
