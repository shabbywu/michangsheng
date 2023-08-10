using System;
using System.Collections.Generic;

namespace JSONClass;

public class MeiMaoYanSeRandomColorJsonData : IJSONClass
{
	public static Dictionary<int, MeiMaoYanSeRandomColorJsonData> DataDict = new Dictionary<int, MeiMaoYanSeRandomColorJsonData>();

	public static List<MeiMaoYanSeRandomColorJsonData> DataList = new List<MeiMaoYanSeRandomColorJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int R;

	public int G;

	public int B;

	public string beizhu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.MeiMaoYanSeRandomColorJsonData.list)
		{
			try
			{
				MeiMaoYanSeRandomColorJsonData meiMaoYanSeRandomColorJsonData = new MeiMaoYanSeRandomColorJsonData();
				meiMaoYanSeRandomColorJsonData.id = item["id"].I;
				meiMaoYanSeRandomColorJsonData.R = item["R"].I;
				meiMaoYanSeRandomColorJsonData.G = item["G"].I;
				meiMaoYanSeRandomColorJsonData.B = item["B"].I;
				meiMaoYanSeRandomColorJsonData.beizhu = item["beizhu"].Str;
				if (DataDict.ContainsKey(meiMaoYanSeRandomColorJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典MeiMaoYanSeRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{meiMaoYanSeRandomColorJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(meiMaoYanSeRandomColorJsonData.id, meiMaoYanSeRandomColorJsonData);
				DataList.Add(meiMaoYanSeRandomColorJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典MeiMaoYanSeRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
