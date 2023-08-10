using System;
using System.Collections.Generic;

namespace JSONClass;

public class TipsExchangeData : IJSONClass
{
	public static Dictionary<int, TipsExchangeData> DataDict = new Dictionary<int, TipsExchangeData>();

	public static List<TipsExchangeData> DataList = new List<TipsExchangeData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string TiShi;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TipsExchangeData.list)
		{
			try
			{
				TipsExchangeData tipsExchangeData = new TipsExchangeData();
				tipsExchangeData.id = item["id"].I;
				tipsExchangeData.TiShi = item["TiShi"].Str;
				if (DataDict.ContainsKey(tipsExchangeData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TipsExchangeData.DataDict添加数据时出现重复的键，Key:{tipsExchangeData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(tipsExchangeData.id, tipsExchangeData);
				DataList.Add(tipsExchangeData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TipsExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
