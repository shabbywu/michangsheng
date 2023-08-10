using System;
using System.Collections.Generic;

namespace JSONClass;

public class SaiHonRandomColorJsonData : IJSONClass
{
	public static Dictionary<int, SaiHonRandomColorJsonData> DataDict = new Dictionary<int, SaiHonRandomColorJsonData>();

	public static List<SaiHonRandomColorJsonData> DataList = new List<SaiHonRandomColorJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int R;

	public int G;

	public int B;

	public string beizhu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SaiHonRandomColorJsonData.list)
		{
			try
			{
				SaiHonRandomColorJsonData saiHonRandomColorJsonData = new SaiHonRandomColorJsonData();
				saiHonRandomColorJsonData.id = item["id"].I;
				saiHonRandomColorJsonData.R = item["R"].I;
				saiHonRandomColorJsonData.G = item["G"].I;
				saiHonRandomColorJsonData.B = item["B"].I;
				saiHonRandomColorJsonData.beizhu = item["beizhu"].Str;
				if (DataDict.ContainsKey(saiHonRandomColorJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SaiHonRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{saiHonRandomColorJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(saiHonRandomColorJsonData.id, saiHonRandomColorJsonData);
				DataList.Add(saiHonRandomColorJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SaiHonRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
