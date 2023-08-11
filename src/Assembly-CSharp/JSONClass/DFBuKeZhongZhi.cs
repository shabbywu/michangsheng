using System;
using System.Collections.Generic;

namespace JSONClass;

public class DFBuKeZhongZhi : IJSONClass
{
	public static Dictionary<int, DFBuKeZhongZhi> DataDict = new Dictionary<int, DFBuKeZhongZhi>();

	public static List<DFBuKeZhongZhi> DataList = new List<DFBuKeZhongZhi>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DFBuKeZhongZhi.list)
		{
			try
			{
				DFBuKeZhongZhi dFBuKeZhongZhi = new DFBuKeZhongZhi();
				dFBuKeZhongZhi.id = item["id"].I;
				if (DataDict.ContainsKey(dFBuKeZhongZhi.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DFBuKeZhongZhi.DataDict添加数据时出现重复的键，Key:{dFBuKeZhongZhi.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(dFBuKeZhongZhi.id, dFBuKeZhongZhi);
				DataList.Add(dFBuKeZhongZhi);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DFBuKeZhongZhi.DataDict添加数据时出现异常，已跳过，请检查配表");
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
