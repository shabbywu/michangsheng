using System;
using System.Collections.Generic;

namespace JSONClass;

public class TaskInfoJsonData : IJSONClass
{
	public static Dictionary<int, TaskInfoJsonData> DataDict = new Dictionary<int, TaskInfoJsonData>();

	public static List<TaskInfoJsonData> DataList = new List<TaskInfoJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int TaskID;

	public int TaskIndex;

	public int mapIndex;

	public int IsFinal;

	public string Desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TaskInfoJsonData.list)
		{
			try
			{
				TaskInfoJsonData taskInfoJsonData = new TaskInfoJsonData();
				taskInfoJsonData.id = item["id"].I;
				taskInfoJsonData.TaskID = item["TaskID"].I;
				taskInfoJsonData.TaskIndex = item["TaskIndex"].I;
				taskInfoJsonData.mapIndex = item["mapIndex"].I;
				taskInfoJsonData.IsFinal = item["IsFinal"].I;
				taskInfoJsonData.Desc = item["Desc"].Str;
				if (DataDict.ContainsKey(taskInfoJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TaskInfoJsonData.DataDict添加数据时出现重复的键，Key:{taskInfoJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(taskInfoJsonData.id, taskInfoJsonData);
				DataList.Add(taskInfoJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TaskInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
