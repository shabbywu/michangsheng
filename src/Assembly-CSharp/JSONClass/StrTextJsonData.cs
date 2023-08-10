using System;
using System.Collections.Generic;

namespace JSONClass;

public class StrTextJsonData : IJSONClass
{
	public static Dictionary<string, StrTextJsonData> DataDict = new Dictionary<string, StrTextJsonData>();

	public static List<StrTextJsonData> DataList = new List<StrTextJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public string StrID;

	public string ChinaText;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.StrTextJsonData.list)
		{
			try
			{
				StrTextJsonData strTextJsonData = new StrTextJsonData();
				strTextJsonData.StrID = item["StrID"].Str;
				strTextJsonData.ChinaText = item["ChinaText"].Str;
				if (DataDict.ContainsKey(strTextJsonData.StrID))
				{
					PreloadManager.LogException("!!!错误!!!向字典StrTextJsonData.DataDict添加数据时出现重复的键，Key:" + strTextJsonData.StrID + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(strTextJsonData.StrID, strTextJsonData);
				DataList.Add(strTextJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典StrTextJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
