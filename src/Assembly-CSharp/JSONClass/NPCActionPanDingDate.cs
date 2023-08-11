using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCActionPanDingDate : IJSONClass
{
	public static Dictionary<int, NPCActionPanDingDate> DataDict = new Dictionary<int, NPCActionPanDingDate>();

	public static List<NPCActionPanDingDate> DataList = new List<NPCActionPanDingDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ChangeTo;

	public int PingJing;

	public int LingShi;

	public int BeiBao;

	public int PaiMaiTime;

	public int PaiMaiType;

	public List<int> JingJie = new List<int>();

	public List<int> YueFen = new List<int>();

	public List<int> LingHeDianWei = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCActionPanDingDate.list)
		{
			try
			{
				NPCActionPanDingDate nPCActionPanDingDate = new NPCActionPanDingDate();
				nPCActionPanDingDate.id = item["id"].I;
				nPCActionPanDingDate.ChangeTo = item["ChangeTo"].I;
				nPCActionPanDingDate.PingJing = item["PingJing"].I;
				nPCActionPanDingDate.LingShi = item["LingShi"].I;
				nPCActionPanDingDate.BeiBao = item["BeiBao"].I;
				nPCActionPanDingDate.PaiMaiTime = item["PaiMaiTime"].I;
				nPCActionPanDingDate.PaiMaiType = item["PaiMaiType"].I;
				nPCActionPanDingDate.JingJie = item["JingJie"].ToList();
				nPCActionPanDingDate.YueFen = item["YueFen"].ToList();
				nPCActionPanDingDate.LingHeDianWei = item["LingHeDianWei"].ToList();
				if (DataDict.ContainsKey(nPCActionPanDingDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCActionPanDingDate.DataDict添加数据时出现重复的键，Key:{nPCActionPanDingDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCActionPanDingDate.id, nPCActionPanDingDate);
				DataList.Add(nPCActionPanDingDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCActionPanDingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
