using System;
using System.Collections.Generic;

namespace JSONClass;

public class _firstNameJsonData : IJSONClass
{
	public static Dictionary<int, _firstNameJsonData> DataDict = new Dictionary<int, _firstNameJsonData>();

	public static List<_firstNameJsonData> DataList = new List<_firstNameJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance._firstNameJsonData.list)
		{
			try
			{
				_firstNameJsonData firstNameJsonData = new _firstNameJsonData();
				firstNameJsonData.id = item["id"].I;
				firstNameJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(firstNameJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典_firstNameJsonData.DataDict添加数据时出现重复的键，Key:{firstNameJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(firstNameJsonData.id, firstNameJsonData);
				DataList.Add(firstNameJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典_firstNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
