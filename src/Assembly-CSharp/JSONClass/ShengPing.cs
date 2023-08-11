using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShengPing : IJSONClass
{
	public static Dictionary<string, ShengPing> DataDict = new Dictionary<string, ShengPing>();

	public static List<ShengPing> DataList = new List<ShengPing>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int IsChongfu;

	public int priority;

	public string id;

	public string descr;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShengPing.list)
		{
			try
			{
				ShengPing shengPing = new ShengPing();
				shengPing.IsChongfu = item["IsChongfu"].I;
				shengPing.priority = item["priority"].I;
				shengPing.id = item["id"].Str;
				shengPing.descr = item["descr"].Str;
				if (DataDict.ContainsKey(shengPing.id))
				{
					PreloadManager.LogException("!!!错误!!!向字典ShengPing.DataDict添加数据时出现重复的键，Key:" + shengPing.id + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shengPing.id, shengPing);
				DataList.Add(shengPing);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShengPing.DataDict添加数据时出现异常，已跳过，请检查配表");
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
