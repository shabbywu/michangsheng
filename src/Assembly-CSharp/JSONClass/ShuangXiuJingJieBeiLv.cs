using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShuangXiuJingJieBeiLv : IJSONClass
{
	public static Dictionary<int, ShuangXiuJingJieBeiLv> DataDict = new Dictionary<int, ShuangXiuJingJieBeiLv>();

	public static List<ShuangXiuJingJieBeiLv> DataList = new List<ShuangXiuJingJieBeiLv>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int BeiLv;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShuangXiuJingJieBeiLv.list)
		{
			try
			{
				ShuangXiuJingJieBeiLv shuangXiuJingJieBeiLv = new ShuangXiuJingJieBeiLv();
				shuangXiuJingJieBeiLv.id = item["id"].I;
				shuangXiuJingJieBeiLv.BeiLv = item["BeiLv"].I;
				if (DataDict.ContainsKey(shuangXiuJingJieBeiLv.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ShuangXiuJingJieBeiLv.DataDict添加数据时出现重复的键，Key:{shuangXiuJingJieBeiLv.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shuangXiuJingJieBeiLv.id, shuangXiuJingJieBeiLv);
				DataList.Add(shuangXiuJingJieBeiLv);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShuangXiuJingJieBeiLv.DataDict添加数据时出现异常，已跳过，请检查配表");
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
