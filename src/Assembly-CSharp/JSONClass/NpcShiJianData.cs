using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcShiJianData : IJSONClass
{
	public static Dictionary<int, NpcShiJianData> DataDict = new Dictionary<int, NpcShiJianData>();

	public static List<NpcShiJianData> DataList = new List<NpcShiJianData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string ShiJianType;

	public string ShiJianInfo;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcShiJianData.list)
		{
			try
			{
				NpcShiJianData npcShiJianData = new NpcShiJianData();
				npcShiJianData.id = item["id"].I;
				npcShiJianData.ShiJianType = item["ShiJianType"].Str;
				npcShiJianData.ShiJianInfo = item["ShiJianInfo"].Str;
				if (DataDict.ContainsKey(npcShiJianData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcShiJianData.DataDict添加数据时出现重复的键，Key:{npcShiJianData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcShiJianData.id, npcShiJianData);
				DataList.Add(npcShiJianData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcShiJianData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
