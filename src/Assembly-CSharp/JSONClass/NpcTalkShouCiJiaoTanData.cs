using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcTalkShouCiJiaoTanData : IJSONClass
{
	public static Dictionary<int, NpcTalkShouCiJiaoTanData> DataDict = new Dictionary<int, NpcTalkShouCiJiaoTanData>();

	public static List<NpcTalkShouCiJiaoTanData> DataList = new List<NpcTalkShouCiJiaoTanData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int JingJie;

	public int ShengWang;

	public int XingGe;

	public int HaoGanDu;

	public string FirstTalk;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcTalkShouCiJiaoTanData.list)
		{
			try
			{
				NpcTalkShouCiJiaoTanData npcTalkShouCiJiaoTanData = new NpcTalkShouCiJiaoTanData();
				npcTalkShouCiJiaoTanData.id = item["id"].I;
				npcTalkShouCiJiaoTanData.JingJie = item["JingJie"].I;
				npcTalkShouCiJiaoTanData.ShengWang = item["ShengWang"].I;
				npcTalkShouCiJiaoTanData.XingGe = item["XingGe"].I;
				npcTalkShouCiJiaoTanData.HaoGanDu = item["HaoGanDu"].I;
				npcTalkShouCiJiaoTanData.FirstTalk = item["FirstTalk"].Str;
				if (DataDict.ContainsKey(npcTalkShouCiJiaoTanData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcTalkShouCiJiaoTanData.DataDict添加数据时出现重复的键，Key:{npcTalkShouCiJiaoTanData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcTalkShouCiJiaoTanData.id, npcTalkShouCiJiaoTanData);
				DataList.Add(npcTalkShouCiJiaoTanData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcTalkShouCiJiaoTanData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
