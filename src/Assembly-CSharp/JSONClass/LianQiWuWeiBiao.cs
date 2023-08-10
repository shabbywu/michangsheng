using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianQiWuWeiBiao : IJSONClass
{
	public static Dictionary<int, LianQiWuWeiBiao> DataDict = new Dictionary<int, LianQiWuWeiBiao>();

	public static List<LianQiWuWeiBiao> DataList = new List<LianQiWuWeiBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public int value2;

	public int value3;

	public int value4;

	public int value5;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianQiWuWeiBiao.list)
		{
			try
			{
				LianQiWuWeiBiao lianQiWuWeiBiao = new LianQiWuWeiBiao();
				lianQiWuWeiBiao.id = item["id"].I;
				lianQiWuWeiBiao.value1 = item["value1"].I;
				lianQiWuWeiBiao.value2 = item["value2"].I;
				lianQiWuWeiBiao.value3 = item["value3"].I;
				lianQiWuWeiBiao.value4 = item["value4"].I;
				lianQiWuWeiBiao.value5 = item["value5"].I;
				lianQiWuWeiBiao.desc = item["desc"].Str;
				if (DataDict.ContainsKey(lianQiWuWeiBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianQiWuWeiBiao.DataDict添加数据时出现重复的键，Key:{lianQiWuWeiBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianQiWuWeiBiao.id, lianQiWuWeiBiao);
				DataList.Add(lianQiWuWeiBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianQiWuWeiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
