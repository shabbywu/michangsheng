using System;
using System.Collections.Generic;

namespace JSONClass;

public class NanZuJianBiao : IJSONClass
{
	public static Dictionary<int, NanZuJianBiao> DataDict = new Dictionary<int, NanZuJianBiao>();

	public static List<NanZuJianBiao> DataList = new List<NanZuJianBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string StrID;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NanZuJianBiao.list)
		{
			try
			{
				NanZuJianBiao nanZuJianBiao = new NanZuJianBiao();
				nanZuJianBiao.id = item["id"].I;
				nanZuJianBiao.StrID = item["StrID"].Str;
				if (DataDict.ContainsKey(nanZuJianBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NanZuJianBiao.DataDict添加数据时出现重复的键，Key:{nanZuJianBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nanZuJianBiao.id, nanZuJianBiao);
				DataList.Add(nanZuJianBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NanZuJianBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
