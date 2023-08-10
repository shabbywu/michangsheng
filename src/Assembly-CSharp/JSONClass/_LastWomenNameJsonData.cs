using System;
using System.Collections.Generic;

namespace JSONClass;

public class _LastWomenNameJsonData : IJSONClass
{
	public static Dictionary<int, _LastWomenNameJsonData> DataDict = new Dictionary<int, _LastWomenNameJsonData>();

	public static List<_LastWomenNameJsonData> DataList = new List<_LastWomenNameJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance._LastWomenNameJsonData.list)
		{
			try
			{
				_LastWomenNameJsonData lastWomenNameJsonData = new _LastWomenNameJsonData();
				lastWomenNameJsonData.id = item["id"].I;
				lastWomenNameJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(lastWomenNameJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典_LastWomenNameJsonData.DataDict添加数据时出现重复的键，Key:{lastWomenNameJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lastWomenNameJsonData.id, lastWomenNameJsonData);
				DataList.Add(lastWomenNameJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典_LastWomenNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
