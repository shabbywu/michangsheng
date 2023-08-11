using System;
using System.Collections.Generic;

namespace JSONClass;

public class CaiYaoDiaoLuo : IJSONClass
{
	public static Dictionary<int, CaiYaoDiaoLuo> DataDict = new Dictionary<int, CaiYaoDiaoLuo>();

	public static List<CaiYaoDiaoLuo> DataList = new List<CaiYaoDiaoLuo>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int type;

	public int MapIndex;

	public int ThreeSence;

	public int value1;

	public int value2;

	public int value3;

	public int value4;

	public int value5;

	public int value6;

	public int value7;

	public int value8;

	public string name;

	public string FuBen;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CaiYaoDiaoLuo.list)
		{
			try
			{
				CaiYaoDiaoLuo caiYaoDiaoLuo = new CaiYaoDiaoLuo();
				caiYaoDiaoLuo.id = item["id"].I;
				caiYaoDiaoLuo.type = item["type"].I;
				caiYaoDiaoLuo.MapIndex = item["MapIndex"].I;
				caiYaoDiaoLuo.ThreeSence = item["ThreeSence"].I;
				caiYaoDiaoLuo.value1 = item["value1"].I;
				caiYaoDiaoLuo.value2 = item["value2"].I;
				caiYaoDiaoLuo.value3 = item["value3"].I;
				caiYaoDiaoLuo.value4 = item["value4"].I;
				caiYaoDiaoLuo.value5 = item["value5"].I;
				caiYaoDiaoLuo.value6 = item["value6"].I;
				caiYaoDiaoLuo.value7 = item["value7"].I;
				caiYaoDiaoLuo.value8 = item["value8"].I;
				caiYaoDiaoLuo.name = item["name"].Str;
				caiYaoDiaoLuo.FuBen = item["FuBen"].Str;
				if (DataDict.ContainsKey(caiYaoDiaoLuo.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CaiYaoDiaoLuo.DataDict添加数据时出现重复的键，Key:{caiYaoDiaoLuo.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(caiYaoDiaoLuo.id, caiYaoDiaoLuo);
				DataList.Add(caiYaoDiaoLuo);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CaiYaoDiaoLuo.DataDict添加数据时出现异常，已跳过，请检查配表");
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
