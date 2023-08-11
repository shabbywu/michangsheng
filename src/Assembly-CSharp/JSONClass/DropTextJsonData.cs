using System;
using System.Collections.Generic;

namespace JSONClass;

public class DropTextJsonData : IJSONClass
{
	public static Dictionary<int, DropTextJsonData> DataDict = new Dictionary<int, DropTextJsonData>();

	public static List<DropTextJsonData> DataList = new List<DropTextJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string Text;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DropTextJsonData.list)
		{
			try
			{
				DropTextJsonData dropTextJsonData = new DropTextJsonData();
				dropTextJsonData.id = item["id"].I;
				dropTextJsonData.Text = item["Text"].Str;
				if (DataDict.ContainsKey(dropTextJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DropTextJsonData.DataDict添加数据时出现重复的键，Key:{dropTextJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(dropTextJsonData.id, dropTextJsonData);
				DataList.Add(dropTextJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DropTextJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
