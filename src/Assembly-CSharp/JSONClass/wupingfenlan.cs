using System;
using System.Collections.Generic;

namespace JSONClass;

public class wupingfenlan : IJSONClass
{
	public static Dictionary<int, wupingfenlan> DataDict = new Dictionary<int, wupingfenlan>();

	public static List<wupingfenlan> DataList = new List<wupingfenlan>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> ItemFlag = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.wupingfenlan.list)
		{
			try
			{
				wupingfenlan wupingfenlan2 = new wupingfenlan();
				wupingfenlan2.id = item["id"].I;
				wupingfenlan2.ItemFlag = item["ItemFlag"].ToList();
				if (DataDict.ContainsKey(wupingfenlan2.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典wupingfenlan.DataDict添加数据时出现重复的键，Key:{wupingfenlan2.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wupingfenlan2.id, wupingfenlan2);
				DataList.Add(wupingfenlan2);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典wupingfenlan.DataDict添加数据时出现异常，已跳过，请检查配表");
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
