using System;
using System.Collections.Generic;

namespace JSONClass;

public class TouXiangPianYi : IJSONClass
{
	public static Dictionary<string, TouXiangPianYi> DataDict = new Dictionary<string, TouXiangPianYi>();

	public static List<TouXiangPianYi> DataList = new List<TouXiangPianYi>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int PX;

	public int PY;

	public int SX;

	public int SY;

	public string id;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TouXiangPianYi.list)
		{
			try
			{
				TouXiangPianYi touXiangPianYi = new TouXiangPianYi();
				touXiangPianYi.PX = item["PX"].I;
				touXiangPianYi.PY = item["PY"].I;
				touXiangPianYi.SX = item["SX"].I;
				touXiangPianYi.SY = item["SY"].I;
				touXiangPianYi.id = item["id"].Str;
				if (DataDict.ContainsKey(touXiangPianYi.id))
				{
					PreloadManager.LogException("!!!错误!!!向字典TouXiangPianYi.DataDict添加数据时出现重复的键，Key:" + touXiangPianYi.id + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(touXiangPianYi.id, touXiangPianYi);
				DataList.Add(touXiangPianYi);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TouXiangPianYi.DataDict添加数据时出现异常，已跳过，请检查配表");
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
