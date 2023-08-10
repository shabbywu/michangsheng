using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcBeiBaoTypeData : IJSONClass
{
	public static Dictionary<int, NpcBeiBaoTypeData> DataDict = new Dictionary<int, NpcBeiBaoTypeData>();

	public static List<NpcBeiBaoTypeData> DataList = new List<NpcBeiBaoTypeData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int BagTpye;

	public int JinJie;

	public List<int> ShopType = new List<int>();

	public List<int> quality = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcBeiBaoTypeData.list)
		{
			try
			{
				NpcBeiBaoTypeData npcBeiBaoTypeData = new NpcBeiBaoTypeData();
				npcBeiBaoTypeData.id = item["id"].I;
				npcBeiBaoTypeData.BagTpye = item["BagTpye"].I;
				npcBeiBaoTypeData.JinJie = item["JinJie"].I;
				npcBeiBaoTypeData.ShopType = item["ShopType"].ToList();
				npcBeiBaoTypeData.quality = item["quality"].ToList();
				if (DataDict.ContainsKey(npcBeiBaoTypeData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcBeiBaoTypeData.DataDict添加数据时出现重复的键，Key:{npcBeiBaoTypeData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcBeiBaoTypeData.id, npcBeiBaoTypeData);
				DataList.Add(npcBeiBaoTypeData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcBeiBaoTypeData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
