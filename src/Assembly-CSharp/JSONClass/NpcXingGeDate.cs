using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcXingGeDate : IJSONClass
{
	public static Dictionary<int, NpcXingGeDate> DataDict = new Dictionary<int, NpcXingGeDate>();

	public static List<NpcXingGeDate> DataList = new List<NpcXingGeDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int zhengxie;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcXingGeDate.list)
		{
			try
			{
				NpcXingGeDate npcXingGeDate = new NpcXingGeDate();
				npcXingGeDate.id = item["id"].I;
				npcXingGeDate.zhengxie = item["zhengxie"].I;
				if (DataDict.ContainsKey(npcXingGeDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcXingGeDate.DataDict添加数据时出现重复的键，Key:{npcXingGeDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcXingGeDate.id, npcXingGeDate);
				DataList.Add(npcXingGeDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcXingGeDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
