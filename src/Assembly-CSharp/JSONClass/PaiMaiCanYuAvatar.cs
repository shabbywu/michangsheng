using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiCanYuAvatar : IJSONClass
{
	public static Dictionary<int, PaiMaiCanYuAvatar> DataDict = new Dictionary<int, PaiMaiCanYuAvatar>();

	public static List<PaiMaiCanYuAvatar> DataList = new List<PaiMaiCanYuAvatar>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int PaiMaiID;

	public int AvatrNum;

	public int CompereID;

	public int Jie;

	public string StarTime;

	public string EndTime;

	public List<int> FuYou = new List<int>();

	public List<int> AvatrID = new List<int>();

	public List<int> JinJie = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiCanYuAvatar.list)
		{
			try
			{
				PaiMaiCanYuAvatar paiMaiCanYuAvatar = new PaiMaiCanYuAvatar();
				paiMaiCanYuAvatar.id = item["id"].I;
				paiMaiCanYuAvatar.PaiMaiID = item["PaiMaiID"].I;
				paiMaiCanYuAvatar.AvatrNum = item["AvatrNum"].I;
				paiMaiCanYuAvatar.CompereID = item["CompereID"].I;
				paiMaiCanYuAvatar.Jie = item["Jie"].I;
				paiMaiCanYuAvatar.StarTime = item["StarTime"].Str;
				paiMaiCanYuAvatar.EndTime = item["EndTime"].Str;
				paiMaiCanYuAvatar.FuYou = item["FuYou"].ToList();
				paiMaiCanYuAvatar.AvatrID = item["AvatrID"].ToList();
				paiMaiCanYuAvatar.JinJie = item["JinJie"].ToList();
				if (DataDict.ContainsKey(paiMaiCanYuAvatar.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiCanYuAvatar.DataDict添加数据时出现重复的键，Key:{paiMaiCanYuAvatar.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiCanYuAvatar.id, paiMaiCanYuAvatar);
				DataList.Add(paiMaiCanYuAvatar);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiCanYuAvatar.DataDict添加数据时出现异常，已跳过，请检查配表");
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
