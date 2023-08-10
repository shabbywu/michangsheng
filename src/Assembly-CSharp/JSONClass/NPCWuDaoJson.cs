using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCWuDaoJson : IJSONClass
{
	public static Dictionary<int, NPCWuDaoJson> DataDict = new Dictionary<int, NPCWuDaoJson>();

	public static List<NPCWuDaoJson> DataList = new List<NPCWuDaoJson>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public int lv;

	public int value1;

	public int value2;

	public int value3;

	public int value4;

	public int value5;

	public int value6;

	public int value7;

	public int value8;

	public int value9;

	public int value10;

	public int value11;

	public int value12;

	public List<int> wudaoID = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCWuDaoJson.list)
		{
			try
			{
				NPCWuDaoJson nPCWuDaoJson = new NPCWuDaoJson();
				nPCWuDaoJson.id = item["id"].I;
				nPCWuDaoJson.Type = item["Type"].I;
				nPCWuDaoJson.lv = item["lv"].I;
				nPCWuDaoJson.value1 = item["value1"].I;
				nPCWuDaoJson.value2 = item["value2"].I;
				nPCWuDaoJson.value3 = item["value3"].I;
				nPCWuDaoJson.value4 = item["value4"].I;
				nPCWuDaoJson.value5 = item["value5"].I;
				nPCWuDaoJson.value6 = item["value6"].I;
				nPCWuDaoJson.value7 = item["value7"].I;
				nPCWuDaoJson.value8 = item["value8"].I;
				nPCWuDaoJson.value9 = item["value9"].I;
				nPCWuDaoJson.value10 = item["value10"].I;
				nPCWuDaoJson.value11 = item["value11"].I;
				nPCWuDaoJson.value12 = item["value12"].I;
				nPCWuDaoJson.wudaoID = item["wudaoID"].ToList();
				if (DataDict.ContainsKey(nPCWuDaoJson.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCWuDaoJson.DataDict添加数据时出现重复的键，Key:{nPCWuDaoJson.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCWuDaoJson.id, nPCWuDaoJson);
				DataList.Add(nPCWuDaoJson);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCWuDaoJson.DataDict添加数据时出现异常，已跳过，请检查配表");
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
