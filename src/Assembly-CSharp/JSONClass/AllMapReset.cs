using System;
using System.Collections.Generic;

namespace JSONClass;

public class AllMapReset : IJSONClass
{
	public static Dictionary<int, AllMapReset> DataDict = new Dictionary<int, AllMapReset>();

	public static List<AllMapReset> DataList = new List<AllMapReset>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public int resetTiem;

	public int CanSame;

	public int percent;

	public int max;

	public string name;

	public string Icon;

	public string Act;

	public List<int> qujian = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AllMapReset.list)
		{
			try
			{
				AllMapReset allMapReset = new AllMapReset();
				allMapReset.id = item["id"].I;
				allMapReset.Type = item["Type"].I;
				allMapReset.resetTiem = item["resetTiem"].I;
				allMapReset.CanSame = item["CanSame"].I;
				allMapReset.percent = item["percent"].I;
				allMapReset.max = item["max"].I;
				allMapReset.name = item["name"].Str;
				allMapReset.Icon = item["Icon"].Str;
				allMapReset.Act = item["Act"].Str;
				allMapReset.qujian = item["qujian"].ToList();
				if (DataDict.ContainsKey(allMapReset.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AllMapReset.DataDict添加数据时出现重复的键，Key:{allMapReset.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(allMapReset.id, allMapReset);
				DataList.Add(allMapReset);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AllMapReset.DataDict添加数据时出现异常，已跳过，请检查配表");
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
