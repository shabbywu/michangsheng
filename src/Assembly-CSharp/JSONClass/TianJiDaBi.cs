using System;
using System.Collections.Generic;

namespace JSONClass;

public class TianJiDaBi : IJSONClass
{
	public static Dictionary<int, TianJiDaBi> DataDict = new Dictionary<int, TianJiDaBi>();

	public static List<TianJiDaBi> DataList = new List<TianJiDaBi>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int YouXian;

	public int LiuPai;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TianJiDaBi.list)
		{
			try
			{
				TianJiDaBi tianJiDaBi = new TianJiDaBi();
				tianJiDaBi.id = item["id"].I;
				tianJiDaBi.YouXian = item["YouXian"].I;
				tianJiDaBi.LiuPai = item["LiuPai"].I;
				if (DataDict.ContainsKey(tianJiDaBi.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TianJiDaBi.DataDict添加数据时出现重复的键，Key:{tianJiDaBi.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(tianJiDaBi.id, tianJiDaBi);
				DataList.Add(tianJiDaBi);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TianJiDaBi.DataDict添加数据时出现异常，已跳过，请检查配表");
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
