using System;
using System.Collections.Generic;

namespace JSONClass;

public class ItemFlagData : IJSONClass
{
	public static Dictionary<int, ItemFlagData> DataDict = new Dictionary<int, ItemFlagData>();

	public static List<ItemFlagData> DataList = new List<ItemFlagData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ItemFlagData.list)
		{
			try
			{
				ItemFlagData itemFlagData = new ItemFlagData();
				itemFlagData.id = item["id"].I;
				itemFlagData.name = item["name"].Str;
				if (DataDict.ContainsKey(itemFlagData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ItemFlagData.DataDict添加数据时出现重复的键，Key:{itemFlagData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(itemFlagData.id, itemFlagData);
				DataList.Add(itemFlagData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ItemFlagData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
