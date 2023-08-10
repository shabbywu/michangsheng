using System;
using System.Collections.Generic;

namespace JSONClass;

public class LiShiChuanWen : IJSONClass
{
	public static Dictionary<int, LiShiChuanWen> DataDict = new Dictionary<int, LiShiChuanWen>();

	public static List<LiShiChuanWen> DataList = new List<LiShiChuanWen>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int TypeID;

	public int StartTime;

	public int getChuanWen;

	public int cunZaiShiJian;

	public int NTaskID;

	public string EventName;

	public string text;

	public string fuhao;

	public List<int> EventLv = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LiShiChuanWen.list)
		{
			try
			{
				LiShiChuanWen liShiChuanWen = new LiShiChuanWen();
				liShiChuanWen.id = item["id"].I;
				liShiChuanWen.TypeID = item["TypeID"].I;
				liShiChuanWen.StartTime = item["StartTime"].I;
				liShiChuanWen.getChuanWen = item["getChuanWen"].I;
				liShiChuanWen.cunZaiShiJian = item["cunZaiShiJian"].I;
				liShiChuanWen.NTaskID = item["NTaskID"].I;
				liShiChuanWen.EventName = item["EventName"].Str;
				liShiChuanWen.text = item["text"].Str;
				liShiChuanWen.fuhao = item["fuhao"].Str;
				liShiChuanWen.EventLv = item["EventLv"].ToList();
				if (DataDict.ContainsKey(liShiChuanWen.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LiShiChuanWen.DataDict添加数据时出现重复的键，Key:{liShiChuanWen.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(liShiChuanWen.id, liShiChuanWen);
				DataList.Add(liShiChuanWen);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LiShiChuanWen.DataDict添加数据时出现异常，已跳过，请检查配表");
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
