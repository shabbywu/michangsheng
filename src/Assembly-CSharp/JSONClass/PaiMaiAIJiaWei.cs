using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiAIJiaWei : IJSONClass
{
	public static Dictionary<int, PaiMaiAIJiaWei> DataDict = new Dictionary<int, PaiMaiAIJiaWei>();

	public static List<PaiMaiAIJiaWei> DataList = new List<PaiMaiAIJiaWei>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Jiawei;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiAIJiaWei.list)
		{
			try
			{
				PaiMaiAIJiaWei paiMaiAIJiaWei = new PaiMaiAIJiaWei();
				paiMaiAIJiaWei.id = item["id"].I;
				paiMaiAIJiaWei.Jiawei = item["Jiawei"].I;
				if (DataDict.ContainsKey(paiMaiAIJiaWei.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiAIJiaWei.DataDict添加数据时出现重复的键，Key:{paiMaiAIJiaWei.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiAIJiaWei.id, paiMaiAIJiaWei);
				DataList.Add(paiMaiAIJiaWei);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiAIJiaWei.DataDict添加数据时出现异常，已跳过，请检查配表");
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
