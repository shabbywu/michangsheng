using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiZhuChiBiao : IJSONClass
{
	public static Dictionary<int, PaiMaiZhuChiBiao> DataDict = new Dictionary<int, PaiMaiZhuChiBiao>();

	public static List<PaiMaiZhuChiBiao> DataList = new List<PaiMaiZhuChiBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public string Text;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiZhuChiBiao.list)
		{
			try
			{
				PaiMaiZhuChiBiao paiMaiZhuChiBiao = new PaiMaiZhuChiBiao();
				paiMaiZhuChiBiao.id = item["id"].I;
				paiMaiZhuChiBiao.Type = item["Type"].I;
				paiMaiZhuChiBiao.Text = item["Text"].Str;
				if (DataDict.ContainsKey(paiMaiZhuChiBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiZhuChiBiao.DataDict添加数据时出现重复的键，Key:{paiMaiZhuChiBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiZhuChiBiao.id, paiMaiZhuChiBiao);
				DataList.Add(paiMaiZhuChiBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiZhuChiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
