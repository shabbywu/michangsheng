using System;
using System.Collections.Generic;

namespace JSONClass;

public class DaDiTuYinCangJsonData : IJSONClass
{
	public static Dictionary<int, DaDiTuYinCangJsonData> DataDict = new Dictionary<int, DaDiTuYinCangJsonData>();

	public static List<DaDiTuYinCangJsonData> DataList = new List<DaDiTuYinCangJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public string fuhao;

	public string StartTime;

	public string EndTime;

	public List<int> EventValue = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DaDiTuYinCangJsonData.list)
		{
			try
			{
				DaDiTuYinCangJsonData daDiTuYinCangJsonData = new DaDiTuYinCangJsonData();
				daDiTuYinCangJsonData.id = item["id"].I;
				daDiTuYinCangJsonData.Type = item["Type"].I;
				daDiTuYinCangJsonData.fuhao = item["fuhao"].Str;
				daDiTuYinCangJsonData.StartTime = item["StartTime"].Str;
				daDiTuYinCangJsonData.EndTime = item["EndTime"].Str;
				daDiTuYinCangJsonData.EventValue = item["EventValue"].ToList();
				if (DataDict.ContainsKey(daDiTuYinCangJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DaDiTuYinCangJsonData.DataDict添加数据时出现重复的键，Key:{daDiTuYinCangJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(daDiTuYinCangJsonData.id, daDiTuYinCangJsonData);
				DataList.Add(daDiTuYinCangJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DaDiTuYinCangJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
