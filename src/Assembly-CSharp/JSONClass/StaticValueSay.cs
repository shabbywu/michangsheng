using System;
using System.Collections.Generic;

namespace JSONClass;

public class StaticValueSay : IJSONClass
{
	public static Dictionary<int, StaticValueSay> DataDict = new Dictionary<int, StaticValueSay>();

	public static List<StaticValueSay> DataList = new List<StaticValueSay>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int StaticID;

	public int staticValue;

	public string ChinaText;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.StaticValueSay.list)
		{
			try
			{
				StaticValueSay staticValueSay = new StaticValueSay();
				staticValueSay.id = item["id"].I;
				staticValueSay.StaticID = item["StaticID"].I;
				staticValueSay.staticValue = item["staticValue"].I;
				staticValueSay.ChinaText = item["ChinaText"].Str;
				if (DataDict.ContainsKey(staticValueSay.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典StaticValueSay.DataDict添加数据时出现重复的键，Key:{staticValueSay.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(staticValueSay.id, staticValueSay);
				DataList.Add(staticValueSay);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典StaticValueSay.DataDict添加数据时出现异常，已跳过，请检查配表");
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
