using System;
using System.Collections.Generic;

namespace JSONClass;

public class _LastNameJsonData : IJSONClass
{
	public static Dictionary<int, _LastNameJsonData> DataDict = new Dictionary<int, _LastNameJsonData>();

	public static List<_LastNameJsonData> DataList = new List<_LastNameJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance._LastNameJsonData.list)
		{
			try
			{
				_LastNameJsonData lastNameJsonData = new _LastNameJsonData();
				lastNameJsonData.id = item["id"].I;
				lastNameJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(lastNameJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典_LastNameJsonData.DataDict添加数据时出现重复的键，Key:{lastNameJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lastNameJsonData.id, lastNameJsonData);
				DataList.Add(lastNameJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典_LastNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
