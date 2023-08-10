using System;
using System.Collections.Generic;

namespace JSONClass;

public class ElderTaskDisableItem : IJSONClass
{
	public static Dictionary<int, ElderTaskDisableItem> DataDict = new Dictionary<int, ElderTaskDisableItem>();

	public static List<ElderTaskDisableItem> DataList = new List<ElderTaskDisableItem>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ElderTaskDisableItem.list)
		{
			try
			{
				ElderTaskDisableItem elderTaskDisableItem = new ElderTaskDisableItem();
				elderTaskDisableItem.id = item["id"].I;
				if (DataDict.ContainsKey(elderTaskDisableItem.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ElderTaskDisableItem.DataDict添加数据时出现重复的键，Key:{elderTaskDisableItem.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(elderTaskDisableItem.id, elderTaskDisableItem);
				DataList.Add(elderTaskDisableItem);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ElderTaskDisableItem.DataDict添加数据时出现异常，已跳过，请检查配表");
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
