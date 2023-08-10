using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShiLiShenFenData : IJSONClass
{
	public static Dictionary<int, ShiLiShenFenData> DataDict = new Dictionary<int, ShiLiShenFenData>();

	public static List<ShiLiShenFenData> DataList = new List<ShiLiShenFenData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ShenFen;

	public int ShiLi;

	public string ZongMen;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShiLiShenFenData.list)
		{
			try
			{
				ShiLiShenFenData shiLiShenFenData = new ShiLiShenFenData();
				shiLiShenFenData.id = item["id"].I;
				shiLiShenFenData.ShenFen = item["ShenFen"].I;
				shiLiShenFenData.ShiLi = item["ShiLi"].I;
				shiLiShenFenData.ZongMen = item["ZongMen"].Str;
				shiLiShenFenData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(shiLiShenFenData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ShiLiShenFenData.DataDict添加数据时出现重复的键，Key:{shiLiShenFenData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shiLiShenFenData.id, shiLiShenFenData);
				DataList.Add(shiLiShenFenData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShiLiShenFenData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
