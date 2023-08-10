using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShuangXiuLianHuaSuDu : IJSONClass
{
	public static Dictionary<int, ShuangXiuLianHuaSuDu> DataDict = new Dictionary<int, ShuangXiuLianHuaSuDu>();

	public static List<ShuangXiuLianHuaSuDu> DataList = new List<ShuangXiuLianHuaSuDu>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int speed;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShuangXiuLianHuaSuDu.list)
		{
			try
			{
				ShuangXiuLianHuaSuDu shuangXiuLianHuaSuDu = new ShuangXiuLianHuaSuDu();
				shuangXiuLianHuaSuDu.id = item["id"].I;
				shuangXiuLianHuaSuDu.speed = item["speed"].I;
				if (DataDict.ContainsKey(shuangXiuLianHuaSuDu.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ShuangXiuLianHuaSuDu.DataDict添加数据时出现重复的键，Key:{shuangXiuLianHuaSuDu.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shuangXiuLianHuaSuDu.id, shuangXiuLianHuaSuDu);
				DataList.Add(shuangXiuLianHuaSuDu);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShuangXiuLianHuaSuDu.DataDict添加数据时出现异常，已跳过，请检查配表");
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
