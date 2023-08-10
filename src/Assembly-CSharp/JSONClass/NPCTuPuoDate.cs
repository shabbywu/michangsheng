using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCTuPuoDate : IJSONClass
{
	public static Dictionary<int, NPCTuPuoDate> DataDict = new Dictionary<int, NPCTuPuoDate>();

	public static List<NPCTuPuoDate> DataList = new List<NPCTuPuoDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int jilv;

	public int mubiaojilv;

	public int shengyushouyuan;

	public int ZiZhiJiaCheng;

	public int LingShiPanDuan;

	public int sunshi;

	public int FailAddLv;

	public List<int> JinDanFen = new List<int>();

	public List<int> ShouJiItem = new List<int>();

	public List<int> TuPoItem = new List<int>();

	public List<int> TiShengJiLv = new List<int>();

	public List<int> ShangXian = new List<int>();

	public List<int> TiShengJinDan = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCTuPuoDate.list)
		{
			try
			{
				NPCTuPuoDate nPCTuPuoDate = new NPCTuPuoDate();
				nPCTuPuoDate.id = item["id"].I;
				nPCTuPuoDate.jilv = item["jilv"].I;
				nPCTuPuoDate.mubiaojilv = item["mubiaojilv"].I;
				nPCTuPuoDate.shengyushouyuan = item["shengyushouyuan"].I;
				nPCTuPuoDate.ZiZhiJiaCheng = item["ZiZhiJiaCheng"].I;
				nPCTuPuoDate.LingShiPanDuan = item["LingShiPanDuan"].I;
				nPCTuPuoDate.sunshi = item["sunshi"].I;
				nPCTuPuoDate.FailAddLv = item["FailAddLv"].I;
				nPCTuPuoDate.JinDanFen = item["JinDanFen"].ToList();
				nPCTuPuoDate.ShouJiItem = item["ShouJiItem"].ToList();
				nPCTuPuoDate.TuPoItem = item["TuPoItem"].ToList();
				nPCTuPuoDate.TiShengJiLv = item["TiShengJiLv"].ToList();
				nPCTuPuoDate.ShangXian = item["ShangXian"].ToList();
				nPCTuPuoDate.TiShengJinDan = item["TiShengJinDan"].ToList();
				if (DataDict.ContainsKey(nPCTuPuoDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCTuPuoDate.DataDict添加数据时出现重复的键，Key:{nPCTuPuoDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCTuPuoDate.id, nPCTuPuoDate);
				DataList.Add(nPCTuPuoDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCTuPuoDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
