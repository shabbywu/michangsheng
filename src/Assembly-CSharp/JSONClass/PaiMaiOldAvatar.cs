using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiOldAvatar : IJSONClass
{
	public static Dictionary<int, PaiMaiOldAvatar> DataDict = new Dictionary<int, PaiMaiOldAvatar>();

	public static List<PaiMaiOldAvatar> DataList = new List<PaiMaiOldAvatar>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int LingShi;

	public int GaiLv;

	public List<int> PaiMaiID = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiOldAvatar.list)
		{
			try
			{
				PaiMaiOldAvatar paiMaiOldAvatar = new PaiMaiOldAvatar();
				paiMaiOldAvatar.id = item["id"].I;
				paiMaiOldAvatar.LingShi = item["LingShi"].I;
				paiMaiOldAvatar.GaiLv = item["GaiLv"].I;
				paiMaiOldAvatar.PaiMaiID = item["PaiMaiID"].ToList();
				if (DataDict.ContainsKey(paiMaiOldAvatar.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiOldAvatar.DataDict添加数据时出现重复的键，Key:{paiMaiOldAvatar.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiOldAvatar.id, paiMaiOldAvatar);
				DataList.Add(paiMaiOldAvatar);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiOldAvatar.DataDict添加数据时出现异常，已跳过，请检查配表");
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
