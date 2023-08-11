using System;
using System.Collections.Generic;

namespace JSONClass;

public class ItemTypeExchangeData : IJSONClass
{
	public static Dictionary<int, ItemTypeExchangeData> DataDict = new Dictionary<int, ItemTypeExchangeData>();

	public static List<ItemTypeExchangeData> DataList = new List<ItemTypeExchangeData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int type;

	public List<int> quality = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ItemTypeExchangeData.list)
		{
			try
			{
				ItemTypeExchangeData itemTypeExchangeData = new ItemTypeExchangeData();
				itemTypeExchangeData.type = item["type"].I;
				itemTypeExchangeData.quality = item["quality"].ToList();
				if (DataDict.ContainsKey(itemTypeExchangeData.type))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ItemTypeExchangeData.DataDict添加数据时出现重复的键，Key:{itemTypeExchangeData.type}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(itemTypeExchangeData.type, itemTypeExchangeData);
				DataList.Add(itemTypeExchangeData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ItemTypeExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
