using System;
using System.Collections.Generic;

namespace JSONClass;

public class helpTextJsonData : IJSONClass
{
	public static Dictionary<int, helpTextJsonData> DataDict = new Dictionary<int, helpTextJsonData>();

	public static List<helpTextJsonData> DataList = new List<helpTextJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int link;

	public string Titile;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.helpTextJsonData.list)
		{
			try
			{
				helpTextJsonData helpTextJsonData2 = new helpTextJsonData();
				helpTextJsonData2.id = item["id"].I;
				helpTextJsonData2.link = item["link"].I;
				helpTextJsonData2.Titile = item["Titile"].Str;
				helpTextJsonData2.desc = item["desc"].Str;
				if (DataDict.ContainsKey(helpTextJsonData2.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典helpTextJsonData.DataDict添加数据时出现重复的键，Key:{helpTextJsonData2.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(helpTextJsonData2.id, helpTextJsonData2);
				DataList.Add(helpTextJsonData2);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典helpTextJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
