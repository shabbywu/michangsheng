using System;
using System.Collections.Generic;

namespace JSONClass;

public class AllMapCastTimeJsonData : IJSONClass
{
	public static Dictionary<int, AllMapCastTimeJsonData> DataDict = new Dictionary<int, AllMapCastTimeJsonData>();

	public static List<AllMapCastTimeJsonData> DataList = new List<AllMapCastTimeJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int dunSu;

	public int XiaoHao;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AllMapCastTimeJsonData.list)
		{
			try
			{
				AllMapCastTimeJsonData allMapCastTimeJsonData = new AllMapCastTimeJsonData();
				allMapCastTimeJsonData.id = item["id"].I;
				allMapCastTimeJsonData.dunSu = item["dunSu"].I;
				allMapCastTimeJsonData.XiaoHao = item["XiaoHao"].I;
				if (DataDict.ContainsKey(allMapCastTimeJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AllMapCastTimeJsonData.DataDict添加数据时出现重复的键，Key:{allMapCastTimeJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(allMapCastTimeJsonData.id, allMapCastTimeJsonData);
				DataList.Add(allMapCastTimeJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AllMapCastTimeJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
