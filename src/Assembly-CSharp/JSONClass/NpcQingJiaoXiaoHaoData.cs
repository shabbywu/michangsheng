using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcQingJiaoXiaoHaoData : IJSONClass
{
	public static Dictionary<int, NpcQingJiaoXiaoHaoData> DataDict = new Dictionary<int, NpcQingJiaoXiaoHaoData>();

	public static List<NpcQingJiaoXiaoHaoData> DataList = new List<NpcQingJiaoXiaoHaoData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public int quality;

	public int typePinJie;

	public int QingFen;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcQingJiaoXiaoHaoData.list)
		{
			try
			{
				NpcQingJiaoXiaoHaoData npcQingJiaoXiaoHaoData = new NpcQingJiaoXiaoHaoData();
				npcQingJiaoXiaoHaoData.id = item["id"].I;
				npcQingJiaoXiaoHaoData.Type = item["Type"].I;
				npcQingJiaoXiaoHaoData.quality = item["quality"].I;
				npcQingJiaoXiaoHaoData.typePinJie = item["typePinJie"].I;
				npcQingJiaoXiaoHaoData.QingFen = item["QingFen"].I;
				if (DataDict.ContainsKey(npcQingJiaoXiaoHaoData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcQingJiaoXiaoHaoData.DataDict添加数据时出现重复的键，Key:{npcQingJiaoXiaoHaoData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcQingJiaoXiaoHaoData.id, npcQingJiaoXiaoHaoData);
				DataList.Add(npcQingJiaoXiaoHaoData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcQingJiaoXiaoHaoData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
