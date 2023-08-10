using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianQiJieSuoBiao : IJSONClass
{
	public static Dictionary<int, LianQiJieSuoBiao> DataDict = new Dictionary<int, LianQiJieSuoBiao>();

	public static List<LianQiJieSuoBiao> DataList = new List<LianQiJieSuoBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string desc;

	public List<int> content = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianQiJieSuoBiao.list)
		{
			try
			{
				LianQiJieSuoBiao lianQiJieSuoBiao = new LianQiJieSuoBiao();
				lianQiJieSuoBiao.id = item["id"].I;
				lianQiJieSuoBiao.desc = item["desc"].Str;
				lianQiJieSuoBiao.content = item["content"].ToList();
				if (DataDict.ContainsKey(lianQiJieSuoBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianQiJieSuoBiao.DataDict添加数据时出现重复的键，Key:{lianQiJieSuoBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianQiJieSuoBiao.id, lianQiJieSuoBiao);
				DataList.Add(lianQiJieSuoBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianQiJieSuoBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
