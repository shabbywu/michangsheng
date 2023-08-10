using System;
using System.Collections.Generic;

namespace JSONClass;

public class DanduMiaoShu : IJSONClass
{
	public static Dictionary<int, DanduMiaoShu> DataDict = new Dictionary<int, DanduMiaoShu>();

	public static List<DanduMiaoShu> DataList = new List<DanduMiaoShu>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int jiexianzhi;

	public string name;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DanduMiaoShu.list)
		{
			try
			{
				DanduMiaoShu danduMiaoShu = new DanduMiaoShu();
				danduMiaoShu.id = item["id"].I;
				danduMiaoShu.jiexianzhi = item["jiexianzhi"].I;
				danduMiaoShu.name = item["name"].Str;
				danduMiaoShu.desc = item["desc"].Str;
				if (DataDict.ContainsKey(danduMiaoShu.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DanduMiaoShu.DataDict添加数据时出现重复的键，Key:{danduMiaoShu.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(danduMiaoShu.id, danduMiaoShu);
				DataList.Add(danduMiaoShu);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DanduMiaoShu.DataDict添加数据时出现异常，已跳过，请检查配表");
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
