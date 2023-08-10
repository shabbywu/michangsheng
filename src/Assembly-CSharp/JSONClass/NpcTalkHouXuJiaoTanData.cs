using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcTalkHouXuJiaoTanData : IJSONClass
{
	public static Dictionary<int, NpcTalkHouXuJiaoTanData> DataDict = new Dictionary<int, NpcTalkHouXuJiaoTanData>();

	public static List<NpcTalkHouXuJiaoTanData> DataList = new List<NpcTalkHouXuJiaoTanData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int JingJie;

	public int XingGe;

	public int HaoGanDu;

	public string OtherTalk;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcTalkHouXuJiaoTanData.list)
		{
			try
			{
				NpcTalkHouXuJiaoTanData npcTalkHouXuJiaoTanData = new NpcTalkHouXuJiaoTanData();
				npcTalkHouXuJiaoTanData.id = item["id"].I;
				npcTalkHouXuJiaoTanData.JingJie = item["JingJie"].I;
				npcTalkHouXuJiaoTanData.XingGe = item["XingGe"].I;
				npcTalkHouXuJiaoTanData.HaoGanDu = item["HaoGanDu"].I;
				npcTalkHouXuJiaoTanData.OtherTalk = item["OtherTalk"].Str;
				if (DataDict.ContainsKey(npcTalkHouXuJiaoTanData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcTalkHouXuJiaoTanData.DataDict添加数据时出现重复的键，Key:{npcTalkHouXuJiaoTanData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcTalkHouXuJiaoTanData.id, npcTalkHouXuJiaoTanData);
				DataList.Add(npcTalkHouXuJiaoTanData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcTalkHouXuJiaoTanData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
