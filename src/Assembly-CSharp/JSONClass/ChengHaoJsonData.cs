using System;
using System.Collections.Generic;

namespace JSONClass;

public class ChengHaoJsonData : IJSONClass
{
	public static Dictionary<int, ChengHaoJsonData> DataDict = new Dictionary<int, ChengHaoJsonData>();

	public static List<ChengHaoJsonData> DataList = new List<ChengHaoJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ChengHaoJsonData.list)
		{
			try
			{
				ChengHaoJsonData chengHaoJsonData = new ChengHaoJsonData();
				chengHaoJsonData.id = item["id"].I;
				chengHaoJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(chengHaoJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ChengHaoJsonData.DataDict添加数据时出现重复的键，Key:{chengHaoJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(chengHaoJsonData.id, chengHaoJsonData);
				DataList.Add(chengHaoJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ChengHaoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
