using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcHaiShangCreateData : IJSONClass
{
	public static Dictionary<int, NpcHaiShangCreateData> DataDict = new Dictionary<int, NpcHaiShangCreateData>();

	public static List<NpcHaiShangCreateData> DataList = new List<NpcHaiShangCreateData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int LiuPai;

	public int level;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcHaiShangCreateData.list)
		{
			try
			{
				NpcHaiShangCreateData npcHaiShangCreateData = new NpcHaiShangCreateData();
				npcHaiShangCreateData.id = item["id"].I;
				npcHaiShangCreateData.LiuPai = item["LiuPai"].I;
				npcHaiShangCreateData.level = item["level"].I;
				if (DataDict.ContainsKey(npcHaiShangCreateData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcHaiShangCreateData.DataDict添加数据时出现重复的键，Key:{npcHaiShangCreateData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcHaiShangCreateData.id, npcHaiShangCreateData);
				DataList.Add(npcHaiShangCreateData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcHaiShangCreateData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
