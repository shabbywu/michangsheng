using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianDanSuccessItemLeiXin : IJSONClass
{
	public static Dictionary<int, LianDanSuccessItemLeiXin> DataDict = new Dictionary<int, LianDanSuccessItemLeiXin>();

	public static List<LianDanSuccessItemLeiXin> DataList = new List<LianDanSuccessItemLeiXin>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int zhonglei;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianDanSuccessItemLeiXin.list)
		{
			try
			{
				LianDanSuccessItemLeiXin lianDanSuccessItemLeiXin = new LianDanSuccessItemLeiXin();
				lianDanSuccessItemLeiXin.id = item["id"].I;
				lianDanSuccessItemLeiXin.zhonglei = item["zhonglei"].I;
				lianDanSuccessItemLeiXin.desc = item["desc"].Str;
				if (DataDict.ContainsKey(lianDanSuccessItemLeiXin.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianDanSuccessItemLeiXin.DataDict添加数据时出现重复的键，Key:{lianDanSuccessItemLeiXin.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianDanSuccessItemLeiXin.id, lianDanSuccessItemLeiXin);
				DataList.Add(lianDanSuccessItemLeiXin);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianDanSuccessItemLeiXin.DataDict添加数据时出现异常，已跳过，请检查配表");
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
