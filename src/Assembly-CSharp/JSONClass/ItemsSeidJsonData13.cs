using System;
using System.Collections.Generic;

namespace JSONClass;

public class ItemsSeidJsonData13 : IJSONClass
{
	public static int SEIDID = 13;

	public static Dictionary<int, ItemsSeidJsonData13> DataDict = new Dictionary<int, ItemsSeidJsonData13>();

	public static List<ItemsSeidJsonData13> DataList = new List<ItemsSeidJsonData13>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ItemsSeidJsonData[13].list)
		{
			try
			{
				ItemsSeidJsonData13 itemsSeidJsonData = new ItemsSeidJsonData13();
				itemsSeidJsonData.id = item["id"].I;
				itemsSeidJsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(itemsSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ItemsSeidJsonData13.DataDict添加数据时出现重复的键，Key:{itemsSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
				DataList.Add(itemsSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData13.DataDict添加数据时出现异常，已跳过，请检查配表");
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