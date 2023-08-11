using System;
using System.Collections.Generic;

namespace JSONClass;

public class ZhuChengRenWu : IJSONClass
{
	public static Dictionary<int, ZhuChengRenWu> DataDict = new Dictionary<int, ZhuChengRenWu>();

	public static List<ZhuChengRenWu> DataList = new List<ZhuChengRenWu>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int Id;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ZhuChengRenWu.list)
		{
			try
			{
				ZhuChengRenWu zhuChengRenWu = new ZhuChengRenWu();
				zhuChengRenWu.Id = item["Id"].I;
				if (DataDict.ContainsKey(zhuChengRenWu.Id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ZhuChengRenWu.DataDict添加数据时出现重复的键，Key:{zhuChengRenWu.Id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(zhuChengRenWu.Id, zhuChengRenWu);
				DataList.Add(zhuChengRenWu);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ZhuChengRenWu.DataDict添加数据时出现异常，已跳过，请检查配表");
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
