using System;
using System.Collections.Generic;

namespace JSONClass;

public class TianJieLeiJieShangHai : IJSONClass
{
	public static Dictionary<int, TianJieLeiJieShangHai> DataDict = new Dictionary<int, TianJieLeiJieShangHai>();

	public static List<TianJieLeiJieShangHai> DataList = new List<TianJieLeiJieShangHai>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Damage;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TianJieLeiJieShangHai.list)
		{
			try
			{
				TianJieLeiJieShangHai tianJieLeiJieShangHai = new TianJieLeiJieShangHai();
				tianJieLeiJieShangHai.id = item["id"].I;
				tianJieLeiJieShangHai.Damage = item["Damage"].I;
				if (DataDict.ContainsKey(tianJieLeiJieShangHai.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TianJieLeiJieShangHai.DataDict添加数据时出现重复的键，Key:{tianJieLeiJieShangHai.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(tianJieLeiJieShangHai.id, tianJieLeiJieShangHai);
				DataList.Add(tianJieLeiJieShangHai);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TianJieLeiJieShangHai.DataDict添加数据时出现异常，已跳过，请检查配表");
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
