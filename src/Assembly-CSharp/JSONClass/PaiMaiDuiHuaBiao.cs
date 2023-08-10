using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiDuiHuaBiao : IJSONClass
{
	public static Dictionary<int, PaiMaiDuiHuaBiao> DataDict = new Dictionary<int, PaiMaiDuiHuaBiao>();

	public static List<PaiMaiDuiHuaBiao> DataList = new List<PaiMaiDuiHuaBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string Text;

	public List<int> huanhua = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiDuiHuaBiao.list)
		{
			try
			{
				PaiMaiDuiHuaBiao paiMaiDuiHuaBiao = new PaiMaiDuiHuaBiao();
				paiMaiDuiHuaBiao.id = item["id"].I;
				paiMaiDuiHuaBiao.Text = item["Text"].Str;
				paiMaiDuiHuaBiao.huanhua = item["huanhua"].ToList();
				if (DataDict.ContainsKey(paiMaiDuiHuaBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiDuiHuaBiao.DataDict添加数据时出现重复的键，Key:{paiMaiDuiHuaBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiDuiHuaBiao.id, paiMaiDuiHuaBiao);
				DataList.Add(paiMaiDuiHuaBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiDuiHuaBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
