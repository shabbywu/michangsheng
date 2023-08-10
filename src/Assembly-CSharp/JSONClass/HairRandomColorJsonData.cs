using System;
using System.Collections.Generic;

namespace JSONClass;

public class HairRandomColorJsonData : IJSONClass
{
	public static Dictionary<int, HairRandomColorJsonData> DataDict = new Dictionary<int, HairRandomColorJsonData>();

	public static List<HairRandomColorJsonData> DataList = new List<HairRandomColorJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int R;

	public int G;

	public int B;

	public string beizhu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.HairRandomColorJsonData.list)
		{
			try
			{
				HairRandomColorJsonData hairRandomColorJsonData = new HairRandomColorJsonData();
				hairRandomColorJsonData.id = item["id"].I;
				hairRandomColorJsonData.R = item["R"].I;
				hairRandomColorJsonData.G = item["G"].I;
				hairRandomColorJsonData.B = item["B"].I;
				hairRandomColorJsonData.beizhu = item["beizhu"].Str;
				if (DataDict.ContainsKey(hairRandomColorJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典HairRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{hairRandomColorJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(hairRandomColorJsonData.id, hairRandomColorJsonData);
				DataList.Add(hairRandomColorJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典HairRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
