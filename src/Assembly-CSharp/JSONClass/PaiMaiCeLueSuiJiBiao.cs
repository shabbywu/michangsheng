using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiCeLueSuiJiBiao : IJSONClass
{
	public static Dictionary<int, PaiMaiCeLueSuiJiBiao> DataDict = new Dictionary<int, PaiMaiCeLueSuiJiBiao>();

	public static List<PaiMaiCeLueSuiJiBiao> DataList = new List<PaiMaiCeLueSuiJiBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int itemType;

	public int itemQuality;

	public List<int> Lv1 = new List<int>();

	public List<int> Lv2 = new List<int>();

	public List<int> Lv3 = new List<int>();

	public List<int> Lv4 = new List<int>();

	public List<int> Lv5 = new List<int>();

	public List<int> Lv6 = new List<int>();

	public List<int> Lv7 = new List<int>();

	public List<int> Lv8 = new List<int>();

	public List<int> Lv9 = new List<int>();

	public List<int> Lv10 = new List<int>();

	public List<int> Lv11 = new List<int>();

	public List<int> Lv12 = new List<int>();

	public List<int> Lv13 = new List<int>();

	public List<int> Lv14 = new List<int>();

	public List<int> Lv15 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiCeLueSuiJiBiao.list)
		{
			try
			{
				PaiMaiCeLueSuiJiBiao paiMaiCeLueSuiJiBiao = new PaiMaiCeLueSuiJiBiao();
				paiMaiCeLueSuiJiBiao.id = item["id"].I;
				paiMaiCeLueSuiJiBiao.itemType = item["itemType"].I;
				paiMaiCeLueSuiJiBiao.itemQuality = item["itemQuality"].I;
				paiMaiCeLueSuiJiBiao.Lv1 = item["Lv1"].ToList();
				paiMaiCeLueSuiJiBiao.Lv2 = item["Lv2"].ToList();
				paiMaiCeLueSuiJiBiao.Lv3 = item["Lv3"].ToList();
				paiMaiCeLueSuiJiBiao.Lv4 = item["Lv4"].ToList();
				paiMaiCeLueSuiJiBiao.Lv5 = item["Lv5"].ToList();
				paiMaiCeLueSuiJiBiao.Lv6 = item["Lv6"].ToList();
				paiMaiCeLueSuiJiBiao.Lv7 = item["Lv7"].ToList();
				paiMaiCeLueSuiJiBiao.Lv8 = item["Lv8"].ToList();
				paiMaiCeLueSuiJiBiao.Lv9 = item["Lv9"].ToList();
				paiMaiCeLueSuiJiBiao.Lv10 = item["Lv10"].ToList();
				paiMaiCeLueSuiJiBiao.Lv11 = item["Lv11"].ToList();
				paiMaiCeLueSuiJiBiao.Lv12 = item["Lv12"].ToList();
				paiMaiCeLueSuiJiBiao.Lv13 = item["Lv13"].ToList();
				paiMaiCeLueSuiJiBiao.Lv14 = item["Lv14"].ToList();
				paiMaiCeLueSuiJiBiao.Lv15 = item["Lv15"].ToList();
				if (DataDict.ContainsKey(paiMaiCeLueSuiJiBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiCeLueSuiJiBiao.DataDict添加数据时出现重复的键，Key:{paiMaiCeLueSuiJiBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiCeLueSuiJiBiao.id, paiMaiCeLueSuiJiBiao);
				DataList.Add(paiMaiCeLueSuiJiBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiCeLueSuiJiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
