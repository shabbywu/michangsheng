using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiPanDing : IJSONClass
{
	public static Dictionary<int, PaiMaiPanDing> DataDict = new Dictionary<int, PaiMaiPanDing>();

	public static List<PaiMaiPanDing> DataList = new List<PaiMaiPanDing>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int JiaWei;

	public int ShiZaiBiDe;

	public int YueYueYuShi;

	public int LueGanXingQu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiPanDing.list)
		{
			try
			{
				PaiMaiPanDing paiMaiPanDing = new PaiMaiPanDing();
				paiMaiPanDing.id = item["id"].I;
				paiMaiPanDing.JiaWei = item["JiaWei"].I;
				paiMaiPanDing.ShiZaiBiDe = item["ShiZaiBiDe"].I;
				paiMaiPanDing.YueYueYuShi = item["YueYueYuShi"].I;
				paiMaiPanDing.LueGanXingQu = item["LueGanXingQu"].I;
				if (DataDict.ContainsKey(paiMaiPanDing.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiPanDing.DataDict添加数据时出现重复的键，Key:{paiMaiPanDing.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiPanDing.id, paiMaiPanDing);
				DataList.Add(paiMaiPanDing);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiPanDing.DataDict添加数据时出现异常，已跳过，请检查配表");
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
