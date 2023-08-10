using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianQiShuXinLeiBie : IJSONClass
{
	public static Dictionary<int, LianQiShuXinLeiBie> DataDict = new Dictionary<int, LianQiShuXinLeiBie>();

	public static List<LianQiShuXinLeiBie> DataList = new List<LianQiShuXinLeiBie>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int AttackType;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianQiShuXinLeiBie.list)
		{
			try
			{
				LianQiShuXinLeiBie lianQiShuXinLeiBie = new LianQiShuXinLeiBie();
				lianQiShuXinLeiBie.id = item["id"].I;
				lianQiShuXinLeiBie.AttackType = item["AttackType"].I;
				lianQiShuXinLeiBie.desc = item["desc"].Str;
				if (DataDict.ContainsKey(lianQiShuXinLeiBie.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianQiShuXinLeiBie.DataDict添加数据时出现重复的键，Key:{lianQiShuXinLeiBie.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianQiShuXinLeiBie.id, lianQiShuXinLeiBie);
				DataList.Add(lianQiShuXinLeiBie);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianQiShuXinLeiBie.DataDict添加数据时出现异常，已跳过，请检查配表");
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
