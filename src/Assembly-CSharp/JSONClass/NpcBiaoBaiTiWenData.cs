using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcBiaoBaiTiWenData : IJSONClass
{
	public static Dictionary<int, NpcBiaoBaiTiWenData> DataDict = new Dictionary<int, NpcBiaoBaiTiWenData>();

	public static List<NpcBiaoBaiTiWenData> DataList = new List<NpcBiaoBaiTiWenData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int TiWen;

	public int XingGe;

	public int BiaoQian;

	public int optionDesc1;

	public int optionDesc2;

	public int optionDesc3;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcBiaoBaiTiWenData.list)
		{
			try
			{
				NpcBiaoBaiTiWenData npcBiaoBaiTiWenData = new NpcBiaoBaiTiWenData();
				npcBiaoBaiTiWenData.id = item["id"].I;
				npcBiaoBaiTiWenData.TiWen = item["TiWen"].I;
				npcBiaoBaiTiWenData.XingGe = item["XingGe"].I;
				npcBiaoBaiTiWenData.BiaoQian = item["BiaoQian"].I;
				npcBiaoBaiTiWenData.optionDesc1 = item["optionDesc1"].I;
				npcBiaoBaiTiWenData.optionDesc2 = item["optionDesc2"].I;
				npcBiaoBaiTiWenData.optionDesc3 = item["optionDesc3"].I;
				if (DataDict.ContainsKey(npcBiaoBaiTiWenData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcBiaoBaiTiWenData.DataDict添加数据时出现重复的键，Key:{npcBiaoBaiTiWenData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcBiaoBaiTiWenData.id, npcBiaoBaiTiWenData);
				DataList.Add(npcBiaoBaiTiWenData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcBiaoBaiTiWenData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
