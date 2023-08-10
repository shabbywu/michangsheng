using System;
using System.Collections.Generic;

namespace JSONClass;

public class YanZhuYanSeRandomColorJsonData : IJSONClass
{
	public static Dictionary<int, YanZhuYanSeRandomColorJsonData> DataDict = new Dictionary<int, YanZhuYanSeRandomColorJsonData>();

	public static List<YanZhuYanSeRandomColorJsonData> DataList = new List<YanZhuYanSeRandomColorJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int R;

	public int G;

	public int B;

	public string beizhu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.YanZhuYanSeRandomColorJsonData.list)
		{
			try
			{
				YanZhuYanSeRandomColorJsonData yanZhuYanSeRandomColorJsonData = new YanZhuYanSeRandomColorJsonData();
				yanZhuYanSeRandomColorJsonData.id = item["id"].I;
				yanZhuYanSeRandomColorJsonData.R = item["R"].I;
				yanZhuYanSeRandomColorJsonData.G = item["G"].I;
				yanZhuYanSeRandomColorJsonData.B = item["B"].I;
				yanZhuYanSeRandomColorJsonData.beizhu = item["beizhu"].Str;
				if (DataDict.ContainsKey(yanZhuYanSeRandomColorJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典YanZhuYanSeRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{yanZhuYanSeRandomColorJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(yanZhuYanSeRandomColorJsonData.id, yanZhuYanSeRandomColorJsonData);
				DataList.Add(yanZhuYanSeRandomColorJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典YanZhuYanSeRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
