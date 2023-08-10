using System;
using System.Collections.Generic;

namespace JSONClass;

public class ItemsSeidJsonData12 : IJSONClass
{
	public static int SEIDID = 12;

	public static Dictionary<int, ItemsSeidJsonData12> DataDict = new Dictionary<int, ItemsSeidJsonData12>();

	public static List<ItemsSeidJsonData12> DataList = new List<ItemsSeidJsonData12>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> value1 = new List<int>();

	public List<int> value2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ItemsSeidJsonData[12].list)
		{
			try
			{
				ItemsSeidJsonData12 itemsSeidJsonData = new ItemsSeidJsonData12();
				itemsSeidJsonData.id = item["id"].I;
				itemsSeidJsonData.value1 = item["value1"].ToList();
				itemsSeidJsonData.value2 = item["value2"].ToList();
				if (DataDict.ContainsKey(itemsSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ItemsSeidJsonData12.DataDict添加数据时出现重复的键，Key:{itemsSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
				DataList.Add(itemsSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
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
