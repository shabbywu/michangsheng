using System;
using System.Collections.Generic;

namespace JSONClass;

public class DFLingYanLevel : IJSONClass
{
	public static Dictionary<int, DFLingYanLevel> DataDict = new Dictionary<int, DFLingYanLevel>();

	public static List<DFLingYanLevel> DataList = new List<DFLingYanLevel>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int xiuliansudu;

	public int lingtiansudu;

	public string name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DFLingYanLevel.list)
		{
			try
			{
				DFLingYanLevel dFLingYanLevel = new DFLingYanLevel();
				dFLingYanLevel.id = item["id"].I;
				dFLingYanLevel.xiuliansudu = item["xiuliansudu"].I;
				dFLingYanLevel.lingtiansudu = item["lingtiansudu"].I;
				dFLingYanLevel.name = item["name"].Str;
				if (DataDict.ContainsKey(dFLingYanLevel.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DFLingYanLevel.DataDict添加数据时出现重复的键，Key:{dFLingYanLevel.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(dFLingYanLevel.id, dFLingYanLevel);
				DataList.Add(dFLingYanLevel);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DFLingYanLevel.DataDict添加数据时出现异常，已跳过，请检查配表");
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
