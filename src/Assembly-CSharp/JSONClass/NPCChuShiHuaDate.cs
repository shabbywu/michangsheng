using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCChuShiHuaDate : IJSONClass
{
	public static Dictionary<int, NPCChuShiHuaDate> DataDict = new Dictionary<int, NPCChuShiHuaDate>();

	public static List<NPCChuShiHuaDate> DataList = new List<NPCChuShiHuaDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int LiuPai;

	public List<int> Level = new List<int>();

	public List<int> Num = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCChuShiHuaDate.list)
		{
			try
			{
				NPCChuShiHuaDate nPCChuShiHuaDate = new NPCChuShiHuaDate();
				nPCChuShiHuaDate.id = item["id"].I;
				nPCChuShiHuaDate.LiuPai = item["LiuPai"].I;
				nPCChuShiHuaDate.Level = item["Level"].ToList();
				nPCChuShiHuaDate.Num = item["Num"].ToList();
				if (DataDict.ContainsKey(nPCChuShiHuaDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCChuShiHuaDate.DataDict添加数据时出现重复的键，Key:{nPCChuShiHuaDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCChuShiHuaDate.id, nPCChuShiHuaDate);
				DataList.Add(nPCChuShiHuaDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCChuShiHuaDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
