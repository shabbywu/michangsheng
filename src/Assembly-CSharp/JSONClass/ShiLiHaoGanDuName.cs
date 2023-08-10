using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShiLiHaoGanDuName : IJSONClass
{
	public static Dictionary<int, ShiLiHaoGanDuName> DataDict = new Dictionary<int, ShiLiHaoGanDuName>();

	public static List<ShiLiHaoGanDuName> DataList = new List<ShiLiHaoGanDuName>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string ChinaText;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShiLiHaoGanDuName.list)
		{
			try
			{
				ShiLiHaoGanDuName shiLiHaoGanDuName = new ShiLiHaoGanDuName();
				shiLiHaoGanDuName.id = item["id"].I;
				shiLiHaoGanDuName.ChinaText = item["ChinaText"].Str;
				if (DataDict.ContainsKey(shiLiHaoGanDuName.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ShiLiHaoGanDuName.DataDict添加数据时出现重复的键，Key:{shiLiHaoGanDuName.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shiLiHaoGanDuName.id, shiLiHaoGanDuName);
				DataList.Add(shiLiHaoGanDuName);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShiLiHaoGanDuName.DataDict添加数据时出现异常，已跳过，请检查配表");
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
