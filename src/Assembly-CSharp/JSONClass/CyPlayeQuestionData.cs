using System;
using System.Collections.Generic;

namespace JSONClass;

public class CyPlayeQuestionData : IJSONClass
{
	public static Dictionary<int, CyPlayeQuestionData> DataDict = new Dictionary<int, CyPlayeQuestionData>();

	public static List<CyPlayeQuestionData> DataList = new List<CyPlayeQuestionData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int SendAction;

	public string WenTi;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CyPlayeQuestionData.list)
		{
			try
			{
				CyPlayeQuestionData cyPlayeQuestionData = new CyPlayeQuestionData();
				cyPlayeQuestionData.id = item["id"].I;
				cyPlayeQuestionData.SendAction = item["SendAction"].I;
				cyPlayeQuestionData.WenTi = item["WenTi"].Str;
				if (DataDict.ContainsKey(cyPlayeQuestionData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CyPlayeQuestionData.DataDict添加数据时出现重复的键，Key:{cyPlayeQuestionData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(cyPlayeQuestionData.id, cyPlayeQuestionData);
				DataList.Add(cyPlayeQuestionData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CyPlayeQuestionData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
