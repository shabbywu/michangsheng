using System;
using System.Collections.Generic;

namespace JSONClass;

public class CyNpcAnswerData : IJSONClass
{
	public static Dictionary<int, CyNpcAnswerData> DataDict = new Dictionary<int, CyNpcAnswerData>();

	public static List<CyNpcAnswerData> DataList = new List<CyNpcAnswerData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int NPCActionID;

	public int AnswerType;

	public int IsPangBai;

	public int AnswerAction;

	public string DuiHua;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CyNpcAnswerData.list)
		{
			try
			{
				CyNpcAnswerData cyNpcAnswerData = new CyNpcAnswerData();
				cyNpcAnswerData.id = item["id"].I;
				cyNpcAnswerData.NPCActionID = item["NPCActionID"].I;
				cyNpcAnswerData.AnswerType = item["AnswerType"].I;
				cyNpcAnswerData.IsPangBai = item["IsPangBai"].I;
				cyNpcAnswerData.AnswerAction = item["AnswerAction"].I;
				cyNpcAnswerData.DuiHua = item["DuiHua"].Str;
				if (DataDict.ContainsKey(cyNpcAnswerData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CyNpcAnswerData.DataDict添加数据时出现重复的键，Key:{cyNpcAnswerData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(cyNpcAnswerData.id, cyNpcAnswerData);
				DataList.Add(cyNpcAnswerData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CyNpcAnswerData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
