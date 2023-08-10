using System;
using System.Collections.Generic;

namespace JSONClass;

public class ShuangXiuMiShu : IJSONClass
{
	public static Dictionary<int, ShuangXiuMiShu> DataDict = new Dictionary<int, ShuangXiuMiShu>();

	public static List<ShuangXiuMiShu> DataList = new List<ShuangXiuMiShu>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int pinjietype;

	public int ningliantype;

	public int jingyuanbeilv;

	public int jingyuantype;

	public string name;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ShuangXiuMiShu.list)
		{
			try
			{
				ShuangXiuMiShu shuangXiuMiShu = new ShuangXiuMiShu();
				shuangXiuMiShu.id = item["id"].I;
				shuangXiuMiShu.pinjietype = item["pinjietype"].I;
				shuangXiuMiShu.ningliantype = item["ningliantype"].I;
				shuangXiuMiShu.jingyuanbeilv = item["jingyuanbeilv"].I;
				shuangXiuMiShu.jingyuantype = item["jingyuantype"].I;
				shuangXiuMiShu.name = item["name"].Str;
				shuangXiuMiShu.desc = item["desc"].Str;
				if (DataDict.ContainsKey(shuangXiuMiShu.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ShuangXiuMiShu.DataDict添加数据时出现重复的键，Key:{shuangXiuMiShu.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(shuangXiuMiShu.id, shuangXiuMiShu);
				DataList.Add(shuangXiuMiShu);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ShuangXiuMiShu.DataDict添加数据时出现异常，已跳过，请检查配表");
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
