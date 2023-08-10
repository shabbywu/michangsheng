using System;
using System.Collections.Generic;

namespace JSONClass;

public class ChuanWenTypeData : IJSONClass
{
	public static Dictionary<int, ChuanWenTypeData> DataDict = new Dictionary<int, ChuanWenTypeData>();

	public static List<ChuanWenTypeData> DataList = new List<ChuanWenTypeData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ChuanWenType;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ChuanWenTypeData.list)
		{
			try
			{
				ChuanWenTypeData chuanWenTypeData = new ChuanWenTypeData();
				chuanWenTypeData.id = item["id"].I;
				chuanWenTypeData.ChuanWenType = item["ChuanWenType"].I;
				if (DataDict.ContainsKey(chuanWenTypeData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ChuanWenTypeData.DataDict添加数据时出现重复的键，Key:{chuanWenTypeData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(chuanWenTypeData.id, chuanWenTypeData);
				DataList.Add(chuanWenTypeData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ChuanWenTypeData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
