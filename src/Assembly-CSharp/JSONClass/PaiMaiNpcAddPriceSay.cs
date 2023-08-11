using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiNpcAddPriceSay : IJSONClass
{
	public static Dictionary<int, PaiMaiNpcAddPriceSay> DataDict = new Dictionary<int, PaiMaiNpcAddPriceSay>();

	public static List<PaiMaiNpcAddPriceSay> DataList = new List<PaiMaiNpcAddPriceSay>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public string ChuJiaDuiHua;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiNpcAddPriceSay.list)
		{
			try
			{
				PaiMaiNpcAddPriceSay paiMaiNpcAddPriceSay = new PaiMaiNpcAddPriceSay();
				paiMaiNpcAddPriceSay.id = item["id"].I;
				paiMaiNpcAddPriceSay.Type = item["Type"].I;
				paiMaiNpcAddPriceSay.ChuJiaDuiHua = item["ChuJiaDuiHua"].Str;
				if (DataDict.ContainsKey(paiMaiNpcAddPriceSay.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiNpcAddPriceSay.DataDict添加数据时出现重复的键，Key:{paiMaiNpcAddPriceSay.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiNpcAddPriceSay.id, paiMaiNpcAddPriceSay);
				DataList.Add(paiMaiNpcAddPriceSay);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiNpcAddPriceSay.DataDict添加数据时出现异常，已跳过，请检查配表");
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
