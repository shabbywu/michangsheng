using System;
using System.Collections.Generic;

namespace JSONClass;

public class AllMapCaiJiMiaoShuBiao : IJSONClass
{
	public static Dictionary<int, AllMapCaiJiMiaoShuBiao> DataDict = new Dictionary<int, AllMapCaiJiMiaoShuBiao>();

	public static List<AllMapCaiJiMiaoShuBiao> DataList = new List<AllMapCaiJiMiaoShuBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int ID;

	public string desc1;

	public string desc2;

	public string desc3;

	public string desc4;

	public string desc5;

	public string desc6;

	public string desc7;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AllMapCaiJiMiaoShuBiao.list)
		{
			try
			{
				AllMapCaiJiMiaoShuBiao allMapCaiJiMiaoShuBiao = new AllMapCaiJiMiaoShuBiao();
				allMapCaiJiMiaoShuBiao.ID = item["ID"].I;
				allMapCaiJiMiaoShuBiao.desc1 = item["desc1"].Str;
				allMapCaiJiMiaoShuBiao.desc2 = item["desc2"].Str;
				allMapCaiJiMiaoShuBiao.desc3 = item["desc3"].Str;
				allMapCaiJiMiaoShuBiao.desc4 = item["desc4"].Str;
				allMapCaiJiMiaoShuBiao.desc5 = item["desc5"].Str;
				allMapCaiJiMiaoShuBiao.desc6 = item["desc6"].Str;
				allMapCaiJiMiaoShuBiao.desc7 = item["desc7"].Str;
				if (DataDict.ContainsKey(allMapCaiJiMiaoShuBiao.ID))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AllMapCaiJiMiaoShuBiao.DataDict添加数据时出现重复的键，Key:{allMapCaiJiMiaoShuBiao.ID}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(allMapCaiJiMiaoShuBiao.ID, allMapCaiJiMiaoShuBiao);
				DataList.Add(allMapCaiJiMiaoShuBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AllMapCaiJiMiaoShuBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
