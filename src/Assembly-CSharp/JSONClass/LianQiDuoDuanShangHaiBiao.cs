using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianQiDuoDuanShangHaiBiao : IJSONClass
{
	public static Dictionary<int, LianQiDuoDuanShangHaiBiao> DataDict = new Dictionary<int, LianQiDuoDuanShangHaiBiao>();

	public static List<LianQiDuoDuanShangHaiBiao> DataList = new List<LianQiDuoDuanShangHaiBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int seid;

	public int value1;

	public int value2;

	public int value3;

	public int cast;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianQiDuoDuanShangHaiBiao.list)
		{
			try
			{
				LianQiDuoDuanShangHaiBiao lianQiDuoDuanShangHaiBiao = new LianQiDuoDuanShangHaiBiao();
				lianQiDuoDuanShangHaiBiao.id = item["id"].I;
				lianQiDuoDuanShangHaiBiao.seid = item["seid"].I;
				lianQiDuoDuanShangHaiBiao.value1 = item["value1"].I;
				lianQiDuoDuanShangHaiBiao.value2 = item["value2"].I;
				lianQiDuoDuanShangHaiBiao.value3 = item["value3"].I;
				lianQiDuoDuanShangHaiBiao.cast = item["cast"].I;
				lianQiDuoDuanShangHaiBiao.desc = item["desc"].Str;
				if (DataDict.ContainsKey(lianQiDuoDuanShangHaiBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianQiDuoDuanShangHaiBiao.DataDict添加数据时出现重复的键，Key:{lianQiDuoDuanShangHaiBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianQiDuoDuanShangHaiBiao.id, lianQiDuoDuanShangHaiBiao);
				DataList.Add(lianQiDuoDuanShangHaiBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianQiDuoDuanShangHaiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
