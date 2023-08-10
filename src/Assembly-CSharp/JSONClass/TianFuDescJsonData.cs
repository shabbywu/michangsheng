using System;
using System.Collections.Generic;

namespace JSONClass;

public class TianFuDescJsonData : IJSONClass
{
	public static Dictionary<int, TianFuDescJsonData> DataDict = new Dictionary<int, TianFuDescJsonData>();

	public static List<TianFuDescJsonData> DataList = new List<TianFuDescJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string Title;

	public string Desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TianFuDescJsonData.list)
		{
			try
			{
				TianFuDescJsonData tianFuDescJsonData = new TianFuDescJsonData();
				tianFuDescJsonData.id = item["id"].I;
				tianFuDescJsonData.Title = item["Title"].Str;
				tianFuDescJsonData.Desc = item["Desc"].Str;
				if (DataDict.ContainsKey(tianFuDescJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TianFuDescJsonData.DataDict添加数据时出现重复的键，Key:{tianFuDescJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(tianFuDescJsonData.id, tianFuDescJsonData);
				DataList.Add(tianFuDescJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TianFuDescJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
