using System;
using System.Collections.Generic;

namespace JSONClass;

public class FuBenInfoJsonData : IJSONClass
{
	public static Dictionary<string, FuBenInfoJsonData> DataDict = new Dictionary<string, FuBenInfoJsonData>();

	public static List<FuBenInfoJsonData> DataList = new List<FuBenInfoJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int TimeY;

	public int TimeM;

	public int TimeD;

	public int CanDie;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.FuBenInfoJsonData.list)
		{
			try
			{
				FuBenInfoJsonData fuBenInfoJsonData = new FuBenInfoJsonData();
				fuBenInfoJsonData.TimeY = item["TimeY"].I;
				fuBenInfoJsonData.TimeM = item["TimeM"].I;
				fuBenInfoJsonData.TimeD = item["TimeD"].I;
				fuBenInfoJsonData.CanDie = item["CanDie"].I;
				fuBenInfoJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(fuBenInfoJsonData.Name))
				{
					PreloadManager.LogException("!!!错误!!!向字典FuBenInfoJsonData.DataDict添加数据时出现重复的键，Key:" + fuBenInfoJsonData.Name + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(fuBenInfoJsonData.Name, fuBenInfoJsonData);
				DataList.Add(fuBenInfoJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典FuBenInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
