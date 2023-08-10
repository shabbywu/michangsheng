using System;
using System.Collections.Generic;

namespace JSONClass;

public class ItemsSeidJsonData15 : IJSONClass
{
	public static int SEIDID = 15;

	public static Dictionary<int, ItemsSeidJsonData15> DataDict = new Dictionary<int, ItemsSeidJsonData15>();

	public static List<ItemsSeidJsonData15> DataList = new List<ItemsSeidJsonData15>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public int value2;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ItemsSeidJsonData[15].list)
		{
			try
			{
				ItemsSeidJsonData15 itemsSeidJsonData = new ItemsSeidJsonData15();
				itemsSeidJsonData.id = item["id"].I;
				itemsSeidJsonData.value1 = item["value1"].I;
				itemsSeidJsonData.value2 = item["value2"].I;
				if (DataDict.ContainsKey(itemsSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ItemsSeidJsonData15.DataDict添加数据时出现重复的键，Key:{itemsSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
				DataList.Add(itemsSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData15.DataDict添加数据时出现异常，已跳过，请检查配表");
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
