using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiChuJiaAI : IJSONClass
{
	public static Dictionary<int, PaiMaiChuJiaAI> DataDict = new Dictionary<int, PaiMaiChuJiaAI>();

	public static List<PaiMaiChuJiaAI> DataList = new List<PaiMaiChuJiaAI>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> Type = new List<int>();

	public List<int> YingXiang = new List<int>();

	public List<int> GaiLv = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiChuJiaAI.list)
		{
			try
			{
				PaiMaiChuJiaAI paiMaiChuJiaAI = new PaiMaiChuJiaAI();
				paiMaiChuJiaAI.id = item["id"].I;
				paiMaiChuJiaAI.Type = item["Type"].ToList();
				paiMaiChuJiaAI.YingXiang = item["YingXiang"].ToList();
				paiMaiChuJiaAI.GaiLv = item["GaiLv"].ToList();
				if (DataDict.ContainsKey(paiMaiChuJiaAI.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiChuJiaAI.DataDict添加数据时出现重复的键，Key:{paiMaiChuJiaAI.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiChuJiaAI.id, paiMaiChuJiaAI);
				DataList.Add(paiMaiChuJiaAI);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiChuJiaAI.DataDict添加数据时出现异常，已跳过，请检查配表");
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
