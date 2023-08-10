using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcPaiMaiData : IJSONClass
{
	public static Dictionary<int, NpcPaiMaiData> DataDict = new Dictionary<int, NpcPaiMaiData>();

	public static List<NpcPaiMaiData> DataList = new List<NpcPaiMaiData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int PaiMaiID;

	public int ItemNum;

	public List<int> Type = new List<int>();

	public List<int> quality = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcPaiMaiData.list)
		{
			try
			{
				NpcPaiMaiData npcPaiMaiData = new NpcPaiMaiData();
				npcPaiMaiData.id = item["id"].I;
				npcPaiMaiData.PaiMaiID = item["PaiMaiID"].I;
				npcPaiMaiData.ItemNum = item["ItemNum"].I;
				npcPaiMaiData.Type = item["Type"].ToList();
				npcPaiMaiData.quality = item["quality"].ToList();
				if (DataDict.ContainsKey(npcPaiMaiData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcPaiMaiData.DataDict添加数据时出现重复的键，Key:{npcPaiMaiData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcPaiMaiData.id, npcPaiMaiData);
				DataList.Add(npcPaiMaiData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcPaiMaiData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
