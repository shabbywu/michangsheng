using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcJinHuoData : IJSONClass
{
	public static Dictionary<int, NpcJinHuoData> DataDict = new Dictionary<int, NpcJinHuoData>();

	public static List<NpcJinHuoData> DataList = new List<NpcJinHuoData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> Type = new List<int>();

	public List<int> quality = new List<int>();

	public List<int> quanzhong = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcJinHuoData.list)
		{
			try
			{
				NpcJinHuoData npcJinHuoData = new NpcJinHuoData();
				npcJinHuoData.id = item["id"].I;
				npcJinHuoData.Type = item["Type"].ToList();
				npcJinHuoData.quality = item["quality"].ToList();
				npcJinHuoData.quanzhong = item["quanzhong"].ToList();
				if (DataDict.ContainsKey(npcJinHuoData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcJinHuoData.DataDict添加数据时出现重复的键，Key:{npcJinHuoData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcJinHuoData.id, npcJinHuoData);
				DataList.Add(npcJinHuoData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcJinHuoData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
