using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCChengHaoData : IJSONClass
{
	public static Dictionary<int, NPCChengHaoData> DataDict = new Dictionary<int, NPCChengHaoData>();

	public static List<NPCChengHaoData> DataList = new List<NPCChengHaoData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int NPCType;

	public int GongXian;

	public int IsOnly;

	public int ChengHaoLv;

	public int MaxLevel;

	public int ChengHaoType;

	public string ChengHao;

	public List<int> Level = new List<int>();

	public List<int> Change = new List<int>();

	public List<int> ChangeTo = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCChengHaoData.list)
		{
			try
			{
				NPCChengHaoData nPCChengHaoData = new NPCChengHaoData();
				nPCChengHaoData.id = item["id"].I;
				nPCChengHaoData.NPCType = item["NPCType"].I;
				nPCChengHaoData.GongXian = item["GongXian"].I;
				nPCChengHaoData.IsOnly = item["IsOnly"].I;
				nPCChengHaoData.ChengHaoLv = item["ChengHaoLv"].I;
				nPCChengHaoData.MaxLevel = item["MaxLevel"].I;
				nPCChengHaoData.ChengHaoType = item["ChengHaoType"].I;
				nPCChengHaoData.ChengHao = item["ChengHao"].Str;
				nPCChengHaoData.Level = item["Level"].ToList();
				nPCChengHaoData.Change = item["Change"].ToList();
				nPCChengHaoData.ChangeTo = item["ChangeTo"].ToList();
				if (DataDict.ContainsKey(nPCChengHaoData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCChengHaoData.DataDict添加数据时出现重复的键，Key:{nPCChengHaoData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCChengHaoData.id, nPCChengHaoData);
				DataList.Add(nPCChengHaoData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCChengHaoData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
