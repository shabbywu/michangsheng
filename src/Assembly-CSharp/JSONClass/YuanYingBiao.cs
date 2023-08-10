using System;
using System.Collections.Generic;

namespace JSONClass;

public class YuanYingBiao : IJSONClass
{
	public static Dictionary<int, YuanYingBiao> DataDict = new Dictionary<int, YuanYingBiao>();

	public static List<YuanYingBiao> DataList = new List<YuanYingBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public int value2;

	public int target;

	public string desc;

	public List<int> value3 = new List<int>();

	public List<int> value4 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.YuanYingBiao.list)
		{
			try
			{
				YuanYingBiao yuanYingBiao = new YuanYingBiao();
				yuanYingBiao.id = item["id"].I;
				yuanYingBiao.value1 = item["value1"].I;
				yuanYingBiao.value2 = item["value2"].I;
				yuanYingBiao.target = item["target"].I;
				yuanYingBiao.desc = item["desc"].Str;
				yuanYingBiao.value3 = item["value3"].ToList();
				yuanYingBiao.value4 = item["value4"].ToList();
				if (DataDict.ContainsKey(yuanYingBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典YuanYingBiao.DataDict添加数据时出现重复的键，Key:{yuanYingBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(yuanYingBiao.id, yuanYingBiao);
				DataList.Add(yuanYingBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典YuanYingBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
