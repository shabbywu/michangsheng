using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuDaoSeidJsonData1 : IJSONClass
{
	public static int SEIDID = 1;

	public static Dictionary<int, WuDaoSeidJsonData1> DataDict = new Dictionary<int, WuDaoSeidJsonData1>();

	public static List<WuDaoSeidJsonData1> DataList = new List<WuDaoSeidJsonData1>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public int target;

	public List<int> value1 = new List<int>();

	public List<int> value2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoSeidJsonData[1].list)
		{
			try
			{
				WuDaoSeidJsonData1 wuDaoSeidJsonData = new WuDaoSeidJsonData1();
				wuDaoSeidJsonData.skillid = item["skillid"].I;
				wuDaoSeidJsonData.target = item["target"].I;
				wuDaoSeidJsonData.value1 = item["value1"].ToList();
				wuDaoSeidJsonData.value2 = item["value2"].ToList();
				if (DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuDaoSeidJsonData1.DataDict添加数据时出现重复的键，Key:{wuDaoSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
				DataList.Add(wuDaoSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
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
