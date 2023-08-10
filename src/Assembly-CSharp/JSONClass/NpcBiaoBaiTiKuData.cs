using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcBiaoBaiTiKuData : IJSONClass
{
	public static Dictionary<int, NpcBiaoBaiTiKuData> DataDict = new Dictionary<int, NpcBiaoBaiTiKuData>();

	public static List<NpcBiaoBaiTiKuData> DataList = new List<NpcBiaoBaiTiKuData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public string TiWen;

	public string optionDesc1;

	public string optionDesc2;

	public string optionDesc3;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcBiaoBaiTiKuData.list)
		{
			try
			{
				NpcBiaoBaiTiKuData npcBiaoBaiTiKuData = new NpcBiaoBaiTiKuData();
				npcBiaoBaiTiKuData.id = item["id"].I;
				npcBiaoBaiTiKuData.Type = item["Type"].I;
				npcBiaoBaiTiKuData.TiWen = item["TiWen"].Str;
				npcBiaoBaiTiKuData.optionDesc1 = item["optionDesc1"].Str;
				npcBiaoBaiTiKuData.optionDesc2 = item["optionDesc2"].Str;
				npcBiaoBaiTiKuData.optionDesc3 = item["optionDesc3"].Str;
				if (DataDict.ContainsKey(npcBiaoBaiTiKuData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcBiaoBaiTiKuData.DataDict添加数据时出现重复的键，Key:{npcBiaoBaiTiKuData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcBiaoBaiTiKuData.id, npcBiaoBaiTiKuData);
				DataList.Add(npcBiaoBaiTiKuData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcBiaoBaiTiKuData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
