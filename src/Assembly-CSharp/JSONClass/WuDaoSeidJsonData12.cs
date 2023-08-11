using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuDaoSeidJsonData12 : IJSONClass
{
	public static int SEIDID = 12;

	public static Dictionary<int, WuDaoSeidJsonData12> DataDict = new Dictionary<int, WuDaoSeidJsonData12>();

	public static List<WuDaoSeidJsonData12> DataList = new List<WuDaoSeidJsonData12>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoSeidJsonData[12].list)
		{
			try
			{
				WuDaoSeidJsonData12 wuDaoSeidJsonData = new WuDaoSeidJsonData12();
				wuDaoSeidJsonData.skillid = item["skillid"].I;
				wuDaoSeidJsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuDaoSeidJsonData12.DataDict添加数据时出现重复的键，Key:{wuDaoSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
				DataList.Add(wuDaoSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
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
