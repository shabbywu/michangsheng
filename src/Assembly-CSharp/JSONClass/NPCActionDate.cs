using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCActionDate : IJSONClass
{
	public static Dictionary<int, NPCActionDate> DataDict = new Dictionary<int, NPCActionDate>();

	public static List<NPCActionDate> DataList = new List<NPCActionDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int QuanZhong;

	public int PanDing;

	public int AllMap;

	public int FuBen;

	public int IsTask;

	public string ThreeSence;

	public string GuanLianTalk;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCActionDate.list)
		{
			try
			{
				NPCActionDate nPCActionDate = new NPCActionDate();
				nPCActionDate.id = item["id"].I;
				nPCActionDate.QuanZhong = item["QuanZhong"].I;
				nPCActionDate.PanDing = item["PanDing"].I;
				nPCActionDate.AllMap = item["AllMap"].I;
				nPCActionDate.FuBen = item["FuBen"].I;
				nPCActionDate.IsTask = item["IsTask"].I;
				nPCActionDate.ThreeSence = item["ThreeSence"].Str;
				nPCActionDate.GuanLianTalk = item["GuanLianTalk"].Str;
				if (DataDict.ContainsKey(nPCActionDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCActionDate.DataDict添加数据时出现重复的键，Key:{nPCActionDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCActionDate.id, nPCActionDate);
				DataList.Add(nPCActionDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCActionDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
