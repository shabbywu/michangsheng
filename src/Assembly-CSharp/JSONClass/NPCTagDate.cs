using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCTagDate : IJSONClass
{
	public static Dictionary<int, NPCTagDate> DataDict = new Dictionary<int, NPCTagDate>();

	public static List<NPCTagDate> DataList = new List<NPCTagDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int zhengxie;

	public string GuanLianTalk;

	public List<int> WuDao = new List<int>();

	public List<int> Change = new List<int>();

	public List<int> ChangeTo = new List<int>();

	public List<int> BeiBaoType = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCTagDate.list)
		{
			try
			{
				NPCTagDate nPCTagDate = new NPCTagDate();
				nPCTagDate.id = item["id"].I;
				nPCTagDate.zhengxie = item["zhengxie"].I;
				nPCTagDate.GuanLianTalk = item["GuanLianTalk"].Str;
				nPCTagDate.WuDao = item["WuDao"].ToList();
				nPCTagDate.Change = item["Change"].ToList();
				nPCTagDate.ChangeTo = item["ChangeTo"].ToList();
				nPCTagDate.BeiBaoType = item["BeiBaoType"].ToList();
				if (DataDict.ContainsKey(nPCTagDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCTagDate.DataDict添加数据时出现重复的键，Key:{nPCTagDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCTagDate.id, nPCTagDate);
				DataList.Add(nPCTagDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCTagDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
