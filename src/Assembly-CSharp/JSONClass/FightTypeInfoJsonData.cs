using System;
using System.Collections.Generic;

namespace JSONClass;

public class FightTypeInfoJsonData : IJSONClass
{
	public static Dictionary<int, FightTypeInfoJsonData> DataDict = new Dictionary<int, FightTypeInfoJsonData>();

	public static List<FightTypeInfoJsonData> DataList = new List<FightTypeInfoJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type1;

	public int Type2;

	public int Type9;

	public int Type3;

	public int Type4;

	public int Type5;

	public int Type6;

	public int Type7;

	public int Type8;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.FightTypeInfoJsonData.list)
		{
			try
			{
				FightTypeInfoJsonData fightTypeInfoJsonData = new FightTypeInfoJsonData();
				fightTypeInfoJsonData.id = item["id"].I;
				fightTypeInfoJsonData.Type1 = item["Type1"].I;
				fightTypeInfoJsonData.Type2 = item["Type2"].I;
				fightTypeInfoJsonData.Type9 = item["Type9"].I;
				fightTypeInfoJsonData.Type3 = item["Type3"].I;
				fightTypeInfoJsonData.Type4 = item["Type4"].I;
				fightTypeInfoJsonData.Type5 = item["Type5"].I;
				fightTypeInfoJsonData.Type6 = item["Type6"].I;
				fightTypeInfoJsonData.Type7 = item["Type7"].I;
				fightTypeInfoJsonData.Type8 = item["Type8"].I;
				fightTypeInfoJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(fightTypeInfoJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典FightTypeInfoJsonData.DataDict添加数据时出现重复的键，Key:{fightTypeInfoJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(fightTypeInfoJsonData.id, fightTypeInfoJsonData);
				DataList.Add(fightTypeInfoJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典FightTypeInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
