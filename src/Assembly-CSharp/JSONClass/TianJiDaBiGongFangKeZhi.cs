using System;
using System.Collections.Generic;

namespace JSONClass;

public class TianJiDaBiGongFangKeZhi : IJSONClass
{
	public static Dictionary<int, TianJiDaBiGongFangKeZhi> DataDict = new Dictionary<int, TianJiDaBiGongFangKeZhi>();

	public static List<TianJiDaBiGongFangKeZhi> DataList = new List<TianJiDaBiGongFangKeZhi>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int AttackType1;

	public int AttackType2;

	public int AttackType3;

	public int AttackType4;

	public int AttackType5;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TianJiDaBiGongFangKeZhi.list)
		{
			try
			{
				TianJiDaBiGongFangKeZhi tianJiDaBiGongFangKeZhi = new TianJiDaBiGongFangKeZhi();
				tianJiDaBiGongFangKeZhi.id = item["id"].I;
				tianJiDaBiGongFangKeZhi.AttackType1 = item["AttackType1"].I;
				tianJiDaBiGongFangKeZhi.AttackType2 = item["AttackType2"].I;
				tianJiDaBiGongFangKeZhi.AttackType3 = item["AttackType3"].I;
				tianJiDaBiGongFangKeZhi.AttackType4 = item["AttackType4"].I;
				tianJiDaBiGongFangKeZhi.AttackType5 = item["AttackType5"].I;
				if (DataDict.ContainsKey(tianJiDaBiGongFangKeZhi.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TianJiDaBiGongFangKeZhi.DataDict添加数据时出现重复的键，Key:{tianJiDaBiGongFangKeZhi.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(tianJiDaBiGongFangKeZhi.id, tianJiDaBiGongFangKeZhi);
				DataList.Add(tianJiDaBiGongFangKeZhi);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TianJiDaBiGongFangKeZhi.DataDict添加数据时出现异常，已跳过，请检查配表");
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
