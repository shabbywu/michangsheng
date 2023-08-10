using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiChuJia : IJSONClass
{
	public static Dictionary<int, PaiMaiChuJia> DataDict = new Dictionary<int, PaiMaiChuJia>();

	public static List<PaiMaiChuJia> DataList = new List<PaiMaiChuJia>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ZhanBi;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiChuJia.list)
		{
			try
			{
				PaiMaiChuJia paiMaiChuJia = new PaiMaiChuJia();
				paiMaiChuJia.id = item["id"].I;
				paiMaiChuJia.ZhanBi = item["ZhanBi"].I;
				if (DataDict.ContainsKey(paiMaiChuJia.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiChuJia.DataDict添加数据时出现重复的键，Key:{paiMaiChuJia.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiChuJia.id, paiMaiChuJia);
				DataList.Add(paiMaiChuJia);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiChuJia.DataDict添加数据时出现异常，已跳过，请检查配表");
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
