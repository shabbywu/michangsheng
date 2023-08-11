using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShengWangShangJinData : IJSONClass
{
	public static Dictionary<int, ShengWangShangJinData> DataDict = new Dictionary<int, ShengWangShangJinData>();

	public static List<ShengWangShangJinData> DataList = new List<ShengWangShangJinData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ShengWang;

	public int ShiJiShangJin;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShengWangShangJinData.list)
		{
			try
			{
				ShengWangShangJinData shengWangShangJinData = new ShengWangShangJinData();
				shengWangShangJinData.id = item["id"].I;
				shengWangShangJinData.ShengWang = item["ShengWang"].I;
				shengWangShangJinData.ShiJiShangJin = item["ShiJiShangJin"].I;
				if (DataDict.ContainsKey(shengWangShangJinData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ShengWangShangJinData.DataDict添加数据时出现重复的键，Key:{shengWangShangJinData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shengWangShangJinData.id, shengWangShangJinData);
				DataList.Add(shengWangShangJinData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShengWangShangJinData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
