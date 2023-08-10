using System;
using System.Collections.Generic;

namespace JSONClass;

public class LinGenZiZhiJsonData : IJSONClass
{
	public static Dictionary<int, LinGenZiZhiJsonData> DataDict = new Dictionary<int, LinGenZiZhiJsonData>();

	public static List<LinGenZiZhiJsonData> DataList = new List<LinGenZiZhiJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int qujian;

	public string Title;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LinGenZiZhiJsonData.list)
		{
			try
			{
				LinGenZiZhiJsonData linGenZiZhiJsonData = new LinGenZiZhiJsonData();
				linGenZiZhiJsonData.id = item["id"].I;
				linGenZiZhiJsonData.qujian = item["qujian"].I;
				linGenZiZhiJsonData.Title = item["Title"].Str;
				if (DataDict.ContainsKey(linGenZiZhiJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LinGenZiZhiJsonData.DataDict添加数据时出现重复的键，Key:{linGenZiZhiJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(linGenZiZhiJsonData.id, linGenZiZhiJsonData);
				DataList.Add(linGenZiZhiJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LinGenZiZhiJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
