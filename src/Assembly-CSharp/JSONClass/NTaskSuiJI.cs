using System;
using System.Collections.Generic;

namespace JSONClass;

public class NTaskSuiJI : IJSONClass
{
	public static Dictionary<int, NTaskSuiJI> DataDict = new Dictionary<int, NTaskSuiJI>();

	public static List<NTaskSuiJI> DataList = new List<NTaskSuiJI>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Value;

	public int jiaZhi;

	public int huobi;

	public string Str;

	public string StrValue;

	public string name;

	public List<int> type = new List<int>();

	public List<int> shuxing = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NTaskSuiJI.list)
		{
			try
			{
				NTaskSuiJI nTaskSuiJI = new NTaskSuiJI();
				nTaskSuiJI.id = item["id"].I;
				nTaskSuiJI.Value = item["Value"].I;
				nTaskSuiJI.jiaZhi = item["jiaZhi"].I;
				nTaskSuiJI.huobi = item["huobi"].I;
				nTaskSuiJI.Str = item["Str"].Str;
				nTaskSuiJI.StrValue = item["StrValue"].Str;
				nTaskSuiJI.name = item["name"].Str;
				nTaskSuiJI.type = item["type"].ToList();
				nTaskSuiJI.shuxing = item["shuxing"].ToList();
				if (DataDict.ContainsKey(nTaskSuiJI.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NTaskSuiJI.DataDict添加数据时出现重复的键，Key:{nTaskSuiJI.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nTaskSuiJI.id, nTaskSuiJI);
				DataList.Add(nTaskSuiJI);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NTaskSuiJI.DataDict添加数据时出现异常，已跳过，请检查配表");
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
