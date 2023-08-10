using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiMiaoShuBiao : IJSONClass
{
	public static Dictionary<int, PaiMaiMiaoShuBiao> DataDict = new Dictionary<int, PaiMaiMiaoShuBiao>();

	public static List<PaiMaiMiaoShuBiao> DataList = new List<PaiMaiMiaoShuBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public int Type2;

	public string Text;

	public string Text2;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiMiaoShuBiao.list)
		{
			try
			{
				PaiMaiMiaoShuBiao paiMaiMiaoShuBiao = new PaiMaiMiaoShuBiao();
				paiMaiMiaoShuBiao.id = item["id"].I;
				paiMaiMiaoShuBiao.Type = item["Type"].I;
				paiMaiMiaoShuBiao.Type2 = item["Type2"].I;
				paiMaiMiaoShuBiao.Text = item["Text"].Str;
				paiMaiMiaoShuBiao.Text2 = item["Text2"].Str;
				if (DataDict.ContainsKey(paiMaiMiaoShuBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiMiaoShuBiao.DataDict添加数据时出现重复的键，Key:{paiMaiMiaoShuBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiMiaoShuBiao.id, paiMaiMiaoShuBiao);
				DataList.Add(paiMaiMiaoShuBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiMiaoShuBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
