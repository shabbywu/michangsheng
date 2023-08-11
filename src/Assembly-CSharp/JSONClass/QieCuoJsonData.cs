using System;
using System.Collections.Generic;

namespace JSONClass;

public class QieCuoJsonData : IJSONClass
{
	public static Dictionary<int, QieCuoJsonData> DataDict = new Dictionary<int, QieCuoJsonData>();

	public static List<QieCuoJsonData> DataList = new List<QieCuoJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int AvatarID;

	public int tisheng;

	public string name;

	public string lose;

	public string win;

	public string jieshou;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.QieCuoJsonData.list)
		{
			try
			{
				QieCuoJsonData qieCuoJsonData = new QieCuoJsonData();
				qieCuoJsonData.id = item["id"].I;
				qieCuoJsonData.AvatarID = item["AvatarID"].I;
				qieCuoJsonData.tisheng = item["tisheng"].I;
				qieCuoJsonData.name = item["name"].Str;
				qieCuoJsonData.lose = item["lose"].Str;
				qieCuoJsonData.win = item["win"].Str;
				qieCuoJsonData.jieshou = item["jieshou"].Str;
				if (DataDict.ContainsKey(qieCuoJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典QieCuoJsonData.DataDict添加数据时出现重复的键，Key:{qieCuoJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(qieCuoJsonData.id, qieCuoJsonData);
				DataList.Add(qieCuoJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典QieCuoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
