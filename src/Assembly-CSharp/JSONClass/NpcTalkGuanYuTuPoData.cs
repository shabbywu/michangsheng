using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcTalkGuanYuTuPoData : IJSONClass
{
	public static Dictionary<int, NpcTalkGuanYuTuPoData> DataDict = new Dictionary<int, NpcTalkGuanYuTuPoData>();

	public static List<NpcTalkGuanYuTuPoData> DataList = new List<NpcTalkGuanYuTuPoData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int WanJiaJingJie;

	public int JingJie;

	public int XingGe;

	public string TuPoTalk;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcTalkGuanYuTuPoData.list)
		{
			try
			{
				NpcTalkGuanYuTuPoData npcTalkGuanYuTuPoData = new NpcTalkGuanYuTuPoData();
				npcTalkGuanYuTuPoData.id = item["id"].I;
				npcTalkGuanYuTuPoData.WanJiaJingJie = item["WanJiaJingJie"].I;
				npcTalkGuanYuTuPoData.JingJie = item["JingJie"].I;
				npcTalkGuanYuTuPoData.XingGe = item["XingGe"].I;
				npcTalkGuanYuTuPoData.TuPoTalk = item["TuPoTalk"].Str;
				if (DataDict.ContainsKey(npcTalkGuanYuTuPoData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcTalkGuanYuTuPoData.DataDict添加数据时出现重复的键，Key:{npcTalkGuanYuTuPoData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcTalkGuanYuTuPoData.id, npcTalkGuanYuTuPoData);
				DataList.Add(npcTalkGuanYuTuPoData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcTalkGuanYuTuPoData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
