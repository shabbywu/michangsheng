using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiCommandTips : IJSONClass
{
	public static Dictionary<int, PaiMaiCommandTips> DataDict = new Dictionary<int, PaiMaiCommandTips>();

	public static List<PaiMaiCommandTips> DataList = new List<PaiMaiCommandTips>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string Type;

	public string Text;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiCommandTips.list)
		{
			try
			{
				PaiMaiCommandTips paiMaiCommandTips = new PaiMaiCommandTips();
				paiMaiCommandTips.id = item["id"].I;
				paiMaiCommandTips.Type = item["Type"].Str;
				paiMaiCommandTips.Text = item["Text"].Str;
				if (DataDict.ContainsKey(paiMaiCommandTips.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiCommandTips.DataDict添加数据时出现重复的键，Key:{paiMaiCommandTips.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiCommandTips.id, paiMaiCommandTips);
				DataList.Add(paiMaiCommandTips);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiCommandTips.DataDict添加数据时出现异常，已跳过，请检查配表");
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
