using System;
using System.Collections.Generic;

namespace JSONClass;

public class NvZuJianBiao : IJSONClass
{
	public static Dictionary<int, NvZuJianBiao> DataDict = new Dictionary<int, NvZuJianBiao>();

	public static List<NvZuJianBiao> DataList = new List<NvZuJianBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string StrID;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NvZuJianBiao.list)
		{
			try
			{
				NvZuJianBiao nvZuJianBiao = new NvZuJianBiao();
				nvZuJianBiao.id = item["id"].I;
				nvZuJianBiao.StrID = item["StrID"].Str;
				if (DataDict.ContainsKey(nvZuJianBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NvZuJianBiao.DataDict添加数据时出现重复的键，Key:{nvZuJianBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nvZuJianBiao.id, nvZuJianBiao);
				DataList.Add(nvZuJianBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NvZuJianBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
