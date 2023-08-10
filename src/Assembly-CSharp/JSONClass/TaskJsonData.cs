using System;
using System.Collections.Generic;

namespace JSONClass;

public class TaskJsonData : IJSONClass
{
	public static Dictionary<int, TaskJsonData> DataDict = new Dictionary<int, TaskJsonData>();

	public static List<TaskJsonData> DataList = new List<TaskJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public int variable;

	public int circulation;

	public int mapIndex;

	public int continueTime;

	public int isFinsh;

	public string Name;

	public string Title;

	public string Desc;

	public string StarTime;

	public string EndTime;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TaskJsonData.list)
		{
			try
			{
				TaskJsonData taskJsonData = new TaskJsonData();
				taskJsonData.id = item["id"].I;
				taskJsonData.Type = item["Type"].I;
				taskJsonData.variable = item["variable"].I;
				taskJsonData.circulation = item["circulation"].I;
				taskJsonData.mapIndex = item["mapIndex"].I;
				taskJsonData.continueTime = item["continueTime"].I;
				taskJsonData.isFinsh = item["isFinsh"].I;
				taskJsonData.Name = item["Name"].Str;
				taskJsonData.Title = item["Title"].Str;
				taskJsonData.Desc = item["Desc"].Str;
				taskJsonData.StarTime = item["StarTime"].Str;
				taskJsonData.EndTime = item["EndTime"].Str;
				if (DataDict.ContainsKey(taskJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TaskJsonData.DataDict添加数据时出现重复的键，Key:{taskJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(taskJsonData.id, taskJsonData);
				DataList.Add(taskJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TaskJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
