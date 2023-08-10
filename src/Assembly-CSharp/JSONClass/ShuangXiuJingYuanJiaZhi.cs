using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShuangXiuJingYuanJiaZhi : IJSONClass
{
	public static Dictionary<int, ShuangXiuJingYuanJiaZhi> DataDict = new Dictionary<int, ShuangXiuJingYuanJiaZhi>();

	public static List<ShuangXiuJingYuanJiaZhi> DataList = new List<ShuangXiuJingYuanJiaZhi>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int jiazhi;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShuangXiuJingYuanJiaZhi.list)
		{
			try
			{
				ShuangXiuJingYuanJiaZhi shuangXiuJingYuanJiaZhi = new ShuangXiuJingYuanJiaZhi();
				shuangXiuJingYuanJiaZhi.id = item["id"].I;
				shuangXiuJingYuanJiaZhi.jiazhi = item["jiazhi"].I;
				if (DataDict.ContainsKey(shuangXiuJingYuanJiaZhi.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ShuangXiuJingYuanJiaZhi.DataDict添加数据时出现重复的键，Key:{shuangXiuJingYuanJiaZhi.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shuangXiuJingYuanJiaZhi.id, shuangXiuJingYuanJiaZhi);
				DataList.Add(shuangXiuJingYuanJiaZhi);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShuangXiuJingYuanJiaZhi.DataDict添加数据时出现异常，已跳过，请检查配表");
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
