using System;
using System.Collections.Generic;

namespace JSONClass;

public class ItemGoodSeid1JsonData : IJSONClass
{
	public static Dictionary<int, ItemGoodSeid1JsonData> DataDict = new Dictionary<int, ItemGoodSeid1JsonData>();

	public static List<ItemGoodSeid1JsonData> DataList = new List<ItemGoodSeid1JsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ItemGoodSeid1JsonData.list)
		{
			try
			{
				ItemGoodSeid1JsonData itemGoodSeid1JsonData = new ItemGoodSeid1JsonData();
				itemGoodSeid1JsonData.id = item["id"].I;
				itemGoodSeid1JsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(itemGoodSeid1JsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ItemGoodSeid1JsonData.DataDict添加数据时出现重复的键，Key:{itemGoodSeid1JsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(itemGoodSeid1JsonData.id, itemGoodSeid1JsonData);
				DataList.Add(itemGoodSeid1JsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ItemGoodSeid1JsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
