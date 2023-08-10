using System;
using System.Collections.Generic;

namespace JSONClass;

public class WenShenRandomColorJsonData : IJSONClass
{
	public static Dictionary<int, WenShenRandomColorJsonData> DataDict = new Dictionary<int, WenShenRandomColorJsonData>();

	public static List<WenShenRandomColorJsonData> DataList = new List<WenShenRandomColorJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int R;

	public int G;

	public int B;

	public string beizhu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WenShenRandomColorJsonData.list)
		{
			try
			{
				WenShenRandomColorJsonData wenShenRandomColorJsonData = new WenShenRandomColorJsonData();
				wenShenRandomColorJsonData.id = item["id"].I;
				wenShenRandomColorJsonData.R = item["R"].I;
				wenShenRandomColorJsonData.G = item["G"].I;
				wenShenRandomColorJsonData.B = item["B"].I;
				wenShenRandomColorJsonData.beizhu = item["beizhu"].Str;
				if (DataDict.ContainsKey(wenShenRandomColorJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WenShenRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{wenShenRandomColorJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wenShenRandomColorJsonData.id, wenShenRandomColorJsonData);
				DataList.Add(wenShenRandomColorJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WenShenRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
