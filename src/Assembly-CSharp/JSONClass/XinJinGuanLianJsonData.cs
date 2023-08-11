using System;
using System.Collections.Generic;

namespace JSONClass;

public class XinJinGuanLianJsonData : IJSONClass
{
	public static Dictionary<int, XinJinGuanLianJsonData> DataDict = new Dictionary<int, XinJinGuanLianJsonData>();

	public static List<XinJinGuanLianJsonData> DataList = new List<XinJinGuanLianJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int speed;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.XinJinGuanLianJsonData.list)
		{
			try
			{
				XinJinGuanLianJsonData xinJinGuanLianJsonData = new XinJinGuanLianJsonData();
				xinJinGuanLianJsonData.id = item["id"].I;
				xinJinGuanLianJsonData.speed = item["speed"].I;
				if (DataDict.ContainsKey(xinJinGuanLianJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典XinJinGuanLianJsonData.DataDict添加数据时出现重复的键，Key:{xinJinGuanLianJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(xinJinGuanLianJsonData.id, xinJinGuanLianJsonData);
				DataList.Add(xinJinGuanLianJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典XinJinGuanLianJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
