using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianDanItemLeiXin : IJSONClass
{
	public static Dictionary<int, LianDanItemLeiXin> DataDict = new Dictionary<int, LianDanItemLeiXin>();

	public static List<LianDanItemLeiXin> DataList = new List<LianDanItemLeiXin>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string name;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianDanItemLeiXin.list)
		{
			try
			{
				LianDanItemLeiXin lianDanItemLeiXin = new LianDanItemLeiXin();
				lianDanItemLeiXin.id = item["id"].I;
				lianDanItemLeiXin.name = item["name"].Str;
				lianDanItemLeiXin.desc = item["desc"].Str;
				if (DataDict.ContainsKey(lianDanItemLeiXin.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianDanItemLeiXin.DataDict添加数据时出现重复的键，Key:{lianDanItemLeiXin.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianDanItemLeiXin.id, lianDanItemLeiXin);
				DataList.Add(lianDanItemLeiXin);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianDanItemLeiXin.DataDict添加数据时出现异常，已跳过，请检查配表");
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
