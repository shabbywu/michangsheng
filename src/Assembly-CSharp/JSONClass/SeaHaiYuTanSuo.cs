using System;
using System.Collections.Generic;

namespace JSONClass;

public class SeaHaiYuTanSuo : IJSONClass
{
	public static Dictionary<int, SeaHaiYuTanSuo> DataDict = new Dictionary<int, SeaHaiYuTanSuo>();

	public static List<SeaHaiYuTanSuo> DataList = new List<SeaHaiYuTanSuo>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int IsTanSuo;

	public int Value;

	public int ZuoBiao;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SeaHaiYuTanSuo.list)
		{
			try
			{
				SeaHaiYuTanSuo seaHaiYuTanSuo = new SeaHaiYuTanSuo();
				seaHaiYuTanSuo.id = item["id"].I;
				seaHaiYuTanSuo.IsTanSuo = item["IsTanSuo"].I;
				seaHaiYuTanSuo.Value = item["Value"].I;
				seaHaiYuTanSuo.ZuoBiao = item["ZuoBiao"].I;
				if (DataDict.ContainsKey(seaHaiYuTanSuo.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SeaHaiYuTanSuo.DataDict添加数据时出现重复的键，Key:{seaHaiYuTanSuo.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(seaHaiYuTanSuo.id, seaHaiYuTanSuo);
				DataList.Add(seaHaiYuTanSuo);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SeaHaiYuTanSuo.DataDict添加数据时出现异常，已跳过，请检查配表");
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
