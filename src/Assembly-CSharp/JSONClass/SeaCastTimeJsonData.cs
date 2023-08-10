using System;
using System.Collections.Generic;

namespace JSONClass;

public class SeaCastTimeJsonData : IJSONClass
{
	public static Dictionary<int, SeaCastTimeJsonData> DataDict = new Dictionary<int, SeaCastTimeJsonData>();

	public static List<SeaCastTimeJsonData> DataList = new List<SeaCastTimeJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int dunSu;

	public int XiaoHao;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SeaCastTimeJsonData.list)
		{
			try
			{
				SeaCastTimeJsonData seaCastTimeJsonData = new SeaCastTimeJsonData();
				seaCastTimeJsonData.id = item["id"].I;
				seaCastTimeJsonData.dunSu = item["dunSu"].I;
				seaCastTimeJsonData.XiaoHao = item["XiaoHao"].I;
				if (DataDict.ContainsKey(seaCastTimeJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SeaCastTimeJsonData.DataDict添加数据时出现重复的键，Key:{seaCastTimeJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(seaCastTimeJsonData.id, seaCastTimeJsonData);
				DataList.Add(seaCastTimeJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SeaCastTimeJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
