using System;
using System.Collections.Generic;

namespace JSONClass;

public class NewTaskMagData : IJSONClass
{
	public static Dictionary<int, NewTaskMagData> DataDict = new Dictionary<int, NewTaskMagData>();

	public static List<NewTaskMagData> DataList = new List<NewTaskMagData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ShiBaiLevel;

	public int continueTime;

	public string EndTime;

	public List<int> ShiBaiType = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NewTaskMagData.list)
		{
			try
			{
				NewTaskMagData newTaskMagData = new NewTaskMagData();
				newTaskMagData.id = item["id"].I;
				newTaskMagData.ShiBaiLevel = item["ShiBaiLevel"].I;
				newTaskMagData.continueTime = item["continueTime"].I;
				newTaskMagData.EndTime = item["EndTime"].Str;
				newTaskMagData.ShiBaiType = item["ShiBaiType"].ToList();
				if (DataDict.ContainsKey(newTaskMagData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NewTaskMagData.DataDict添加数据时出现重复的键，Key:{newTaskMagData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(newTaskMagData.id, newTaskMagData);
				DataList.Add(newTaskMagData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NewTaskMagData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
