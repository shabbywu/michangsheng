using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcFuBenMapBingDate : IJSONClass
{
	public static Dictionary<int, NpcFuBenMapBingDate> DataDict = new Dictionary<int, NpcFuBenMapBingDate>();

	public static List<NpcFuBenMapBingDate> DataList = new List<NpcFuBenMapBingDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int CaiJi;

	public int CaiKuang;

	public int XunLuo;

	public int LingHe;

	public List<int> CaiJiDian = new List<int>();

	public List<int> CaiKuangDian = new List<int>();

	public List<int> XunLuoDian = new List<int>();

	public List<int> LingHeDian1 = new List<int>();

	public List<int> LingHeDian2 = new List<int>();

	public List<int> LingHeDian3 = new List<int>();

	public List<int> LingHeDian4 = new List<int>();

	public List<int> LingHeDian5 = new List<int>();

	public List<int> LingHeDian6 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcFuBenMapBingDate.list)
		{
			try
			{
				NpcFuBenMapBingDate npcFuBenMapBingDate = new NpcFuBenMapBingDate();
				npcFuBenMapBingDate.id = item["id"].I;
				npcFuBenMapBingDate.CaiJi = item["CaiJi"].I;
				npcFuBenMapBingDate.CaiKuang = item["CaiKuang"].I;
				npcFuBenMapBingDate.XunLuo = item["XunLuo"].I;
				npcFuBenMapBingDate.LingHe = item["LingHe"].I;
				npcFuBenMapBingDate.CaiJiDian = item["CaiJiDian"].ToList();
				npcFuBenMapBingDate.CaiKuangDian = item["CaiKuangDian"].ToList();
				npcFuBenMapBingDate.XunLuoDian = item["XunLuoDian"].ToList();
				npcFuBenMapBingDate.LingHeDian1 = item["LingHeDian1"].ToList();
				npcFuBenMapBingDate.LingHeDian2 = item["LingHeDian2"].ToList();
				npcFuBenMapBingDate.LingHeDian3 = item["LingHeDian3"].ToList();
				npcFuBenMapBingDate.LingHeDian4 = item["LingHeDian4"].ToList();
				npcFuBenMapBingDate.LingHeDian5 = item["LingHeDian5"].ToList();
				npcFuBenMapBingDate.LingHeDian6 = item["LingHeDian6"].ToList();
				if (DataDict.ContainsKey(npcFuBenMapBingDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcFuBenMapBingDate.DataDict添加数据时出现重复的键，Key:{npcFuBenMapBingDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcFuBenMapBingDate.id, npcFuBenMapBingDate);
				DataList.Add(npcFuBenMapBingDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcFuBenMapBingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
