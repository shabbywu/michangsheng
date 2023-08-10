using System;
using System.Collections.Generic;

namespace JSONClass;

public class DrawCardToLevelJsonData : IJSONClass
{
	public static Dictionary<int, DrawCardToLevelJsonData> DataDict = new Dictionary<int, DrawCardToLevelJsonData>();

	public static List<DrawCardToLevelJsonData> DataList = new List<DrawCardToLevelJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int StartCard;

	public int MaxDraw;

	public int rundDraw;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DrawCardToLevelJsonData.list)
		{
			try
			{
				DrawCardToLevelJsonData drawCardToLevelJsonData = new DrawCardToLevelJsonData();
				drawCardToLevelJsonData.id = item["id"].I;
				drawCardToLevelJsonData.StartCard = item["StartCard"].I;
				drawCardToLevelJsonData.MaxDraw = item["MaxDraw"].I;
				drawCardToLevelJsonData.rundDraw = item["rundDraw"].I;
				drawCardToLevelJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(drawCardToLevelJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DrawCardToLevelJsonData.DataDict添加数据时出现重复的键，Key:{drawCardToLevelJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(drawCardToLevelJsonData.id, drawCardToLevelJsonData);
				DataList.Add(drawCardToLevelJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DrawCardToLevelJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
