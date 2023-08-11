using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuDaoZhiJiaCheng : IJSONClass
{
	public static Dictionary<int, WuDaoZhiJiaCheng> DataDict = new Dictionary<int, WuDaoZhiJiaCheng>();

	public static List<WuDaoZhiJiaCheng> DataList = new List<WuDaoZhiJiaCheng>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int JiaCheng;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoZhiJiaCheng.list)
		{
			try
			{
				WuDaoZhiJiaCheng wuDaoZhiJiaCheng = new WuDaoZhiJiaCheng();
				wuDaoZhiJiaCheng.id = item["id"].I;
				wuDaoZhiJiaCheng.JiaCheng = item["JiaCheng"].I;
				if (DataDict.ContainsKey(wuDaoZhiJiaCheng.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuDaoZhiJiaCheng.DataDict添加数据时出现重复的键，Key:{wuDaoZhiJiaCheng.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuDaoZhiJiaCheng.id, wuDaoZhiJiaCheng);
				DataList.Add(wuDaoZhiJiaCheng);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuDaoZhiJiaCheng.DataDict添加数据时出现异常，已跳过，请检查配表");
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
