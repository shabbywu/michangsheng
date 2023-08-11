using System;
using System.Collections.Generic;

namespace JSONClass;

public class SeaHaiYuJiZhiShuaXin : IJSONClass
{
	public static Dictionary<int, SeaHaiYuJiZhiShuaXin> DataDict = new Dictionary<int, SeaHaiYuJiZhiShuaXin>();

	public static List<SeaHaiYuJiZhiShuaXin> DataList = new List<SeaHaiYuJiZhiShuaXin>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> ID = new List<int>();

	public List<int> CD = new List<int>();

	public List<int> WeiZhi = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SeaHaiYuJiZhiShuaXin.list)
		{
			try
			{
				SeaHaiYuJiZhiShuaXin seaHaiYuJiZhiShuaXin = new SeaHaiYuJiZhiShuaXin();
				seaHaiYuJiZhiShuaXin.id = item["id"].I;
				seaHaiYuJiZhiShuaXin.ID = item["ID"].ToList();
				seaHaiYuJiZhiShuaXin.CD = item["CD"].ToList();
				seaHaiYuJiZhiShuaXin.WeiZhi = item["WeiZhi"].ToList();
				if (DataDict.ContainsKey(seaHaiYuJiZhiShuaXin.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SeaHaiYuJiZhiShuaXin.DataDict添加数据时出现重复的键，Key:{seaHaiYuJiZhiShuaXin.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(seaHaiYuJiZhiShuaXin.id, seaHaiYuJiZhiShuaXin);
				DataList.Add(seaHaiYuJiZhiShuaXin);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SeaHaiYuJiZhiShuaXin.DataDict添加数据时出现异常，已跳过，请检查配表");
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
