using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcStatusDate : IJSONClass
{
	public static Dictionary<int, NpcStatusDate> DataDict = new Dictionary<int, NpcStatusDate>();

	public static List<NpcStatusDate> DataList = new List<NpcStatusDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Time;

	public int LunDao;

	public string ZhuangTaiInfo;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcStatusDate.list)
		{
			try
			{
				NpcStatusDate npcStatusDate = new NpcStatusDate();
				npcStatusDate.id = item["id"].I;
				npcStatusDate.Time = item["Time"].I;
				npcStatusDate.LunDao = item["LunDao"].I;
				npcStatusDate.ZhuangTaiInfo = item["ZhuangTaiInfo"].Str;
				if (DataDict.ContainsKey(npcStatusDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcStatusDate.DataDict添加数据时出现重复的键，Key:{npcStatusDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcStatusDate.id, npcStatusDate);
				DataList.Add(npcStatusDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcStatusDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
