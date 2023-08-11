using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcWuDaoChiData : IJSONClass
{
	public static Dictionary<int, NpcWuDaoChiData> DataDict = new Dictionary<int, NpcWuDaoChiData>();

	public static List<NpcWuDaoChiData> DataList = new List<NpcWuDaoChiData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int xiaohao;

	public List<int> wudaochi = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcWuDaoChiData.list)
		{
			try
			{
				NpcWuDaoChiData npcWuDaoChiData = new NpcWuDaoChiData();
				npcWuDaoChiData.id = item["id"].I;
				npcWuDaoChiData.xiaohao = item["xiaohao"].I;
				npcWuDaoChiData.wudaochi = item["wudaochi"].ToList();
				if (DataDict.ContainsKey(npcWuDaoChiData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcWuDaoChiData.DataDict添加数据时出现重复的键，Key:{npcWuDaoChiData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcWuDaoChiData.id, npcWuDaoChiData);
				DataList.Add(npcWuDaoChiData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcWuDaoChiData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
