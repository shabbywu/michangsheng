using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiDuiHuaAI : IJSONClass
{
	public static Dictionary<int, PaiMaiDuiHuaAI> DataDict = new Dictionary<int, PaiMaiDuiHuaAI>();

	public static List<PaiMaiDuiHuaAI> DataList = new List<PaiMaiDuiHuaAI>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int CeLue;

	public int JieGuo;

	public int MuBiao;

	public string DuiHua;

	public string HuiFu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiDuiHuaAI.list)
		{
			try
			{
				PaiMaiDuiHuaAI paiMaiDuiHuaAI = new PaiMaiDuiHuaAI();
				paiMaiDuiHuaAI.id = item["id"].I;
				paiMaiDuiHuaAI.CeLue = item["CeLue"].I;
				paiMaiDuiHuaAI.JieGuo = item["JieGuo"].I;
				paiMaiDuiHuaAI.MuBiao = item["MuBiao"].I;
				paiMaiDuiHuaAI.DuiHua = item["DuiHua"].Str;
				paiMaiDuiHuaAI.HuiFu = item["HuiFu"].Str;
				if (DataDict.ContainsKey(paiMaiDuiHuaAI.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiDuiHuaAI.DataDict添加数据时出现重复的键，Key:{paiMaiDuiHuaAI.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiDuiHuaAI.id, paiMaiDuiHuaAI);
				DataList.Add(paiMaiDuiHuaAI);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiDuiHuaAI.DataDict添加数据时出现异常，已跳过，请检查配表");
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
