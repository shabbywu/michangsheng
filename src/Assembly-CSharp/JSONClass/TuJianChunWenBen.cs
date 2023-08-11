using System;
using System.Collections.Generic;

namespace JSONClass;

public class TuJianChunWenBen : IJSONClass
{
	public static Dictionary<int, TuJianChunWenBen> DataDict = new Dictionary<int, TuJianChunWenBen>();

	public static List<TuJianChunWenBen> DataList = new List<TuJianChunWenBen>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int typenum;

	public int type;

	public string name1;

	public string name2;

	public string descr;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TuJianChunWenBen.list)
		{
			try
			{
				TuJianChunWenBen tuJianChunWenBen = new TuJianChunWenBen();
				tuJianChunWenBen.id = item["id"].I;
				tuJianChunWenBen.typenum = item["typenum"].I;
				tuJianChunWenBen.type = item["type"].I;
				tuJianChunWenBen.name1 = item["name1"].Str;
				tuJianChunWenBen.name2 = item["name2"].Str;
				tuJianChunWenBen.descr = item["descr"].Str;
				if (DataDict.ContainsKey(tuJianChunWenBen.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TuJianChunWenBen.DataDict添加数据时出现重复的键，Key:{tuJianChunWenBen.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(tuJianChunWenBen.id, tuJianChunWenBen);
				DataList.Add(tuJianChunWenBen);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TuJianChunWenBen.DataDict添加数据时出现异常，已跳过，请检查配表");
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
