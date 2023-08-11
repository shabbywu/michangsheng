using System;
using System.Collections.Generic;

namespace JSONClass;

public class CaiLiaoNengLiangBiao : IJSONClass
{
	public static Dictionary<int, CaiLiaoNengLiangBiao> DataDict = new Dictionary<int, CaiLiaoNengLiangBiao>();

	public static List<CaiLiaoNengLiangBiao> DataList = new List<CaiLiaoNengLiangBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CaiLiaoNengLiangBiao.list)
		{
			try
			{
				CaiLiaoNengLiangBiao caiLiaoNengLiangBiao = new CaiLiaoNengLiangBiao();
				caiLiaoNengLiangBiao.id = item["id"].I;
				caiLiaoNengLiangBiao.value1 = item["value1"].I;
				if (DataDict.ContainsKey(caiLiaoNengLiangBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CaiLiaoNengLiangBiao.DataDict添加数据时出现重复的键，Key:{caiLiaoNengLiangBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(caiLiaoNengLiangBiao.id, caiLiaoNengLiangBiao);
				DataList.Add(caiLiaoNengLiangBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CaiLiaoNengLiangBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
