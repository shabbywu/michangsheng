using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcYaoShouDrop : IJSONClass
{
	public static Dictionary<int, NpcYaoShouDrop> DataDict = new Dictionary<int, NpcYaoShouDrop>();

	public static List<NpcYaoShouDrop> DataList = new List<NpcYaoShouDrop>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int avatarid;

	public int jingjie;

	public int NingZhou;

	public int HaiShang;

	public List<int> chanchu = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcYaoShouDrop.list)
		{
			try
			{
				NpcYaoShouDrop npcYaoShouDrop = new NpcYaoShouDrop();
				npcYaoShouDrop.id = item["id"].I;
				npcYaoShouDrop.avatarid = item["avatarid"].I;
				npcYaoShouDrop.jingjie = item["jingjie"].I;
				npcYaoShouDrop.NingZhou = item["NingZhou"].I;
				npcYaoShouDrop.HaiShang = item["HaiShang"].I;
				npcYaoShouDrop.chanchu = item["chanchu"].ToList();
				if (DataDict.ContainsKey(npcYaoShouDrop.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcYaoShouDrop.DataDict添加数据时出现重复的键，Key:{npcYaoShouDrop.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcYaoShouDrop.id, npcYaoShouDrop);
				DataList.Add(npcYaoShouDrop);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcYaoShouDrop.DataDict添加数据时出现异常，已跳过，请检查配表");
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
