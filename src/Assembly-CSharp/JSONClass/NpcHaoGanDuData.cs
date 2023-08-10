using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcHaoGanDuData : IJSONClass
{
	public static Dictionary<int, NpcHaoGanDuData> DataDict = new Dictionary<int, NpcHaoGanDuData>();

	public static List<NpcHaoGanDuData> DataList = new List<NpcHaoGanDuData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int XiShu;

	public string HaoGanDu;

	public List<int> QuJian = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcHaoGanDuData.list)
		{
			try
			{
				NpcHaoGanDuData npcHaoGanDuData = new NpcHaoGanDuData();
				npcHaoGanDuData.id = item["id"].I;
				npcHaoGanDuData.XiShu = item["XiShu"].I;
				npcHaoGanDuData.HaoGanDu = item["HaoGanDu"].Str;
				npcHaoGanDuData.QuJian = item["QuJian"].ToList();
				if (DataDict.ContainsKey(npcHaoGanDuData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcHaoGanDuData.DataDict添加数据时出现重复的键，Key:{npcHaoGanDuData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcHaoGanDuData.id, npcHaoGanDuData);
				DataList.Add(npcHaoGanDuData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcHaoGanDuData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
