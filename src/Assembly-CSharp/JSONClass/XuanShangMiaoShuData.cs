using System;
using System.Collections.Generic;

namespace JSONClass;

public class XuanShangMiaoShuData : IJSONClass
{
	public static Dictionary<int, XuanShangMiaoShuData> DataDict = new Dictionary<int, XuanShangMiaoShuData>();

	public static List<XuanShangMiaoShuData> DataList = new List<XuanShangMiaoShuData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string Info;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.XuanShangMiaoShuData.list)
		{
			try
			{
				XuanShangMiaoShuData xuanShangMiaoShuData = new XuanShangMiaoShuData();
				xuanShangMiaoShuData.id = item["id"].I;
				xuanShangMiaoShuData.Info = item["Info"].Str;
				if (DataDict.ContainsKey(xuanShangMiaoShuData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典XuanShangMiaoShuData.DataDict添加数据时出现重复的键，Key:{xuanShangMiaoShuData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(xuanShangMiaoShuData.id, xuanShangMiaoShuData);
				DataList.Add(xuanShangMiaoShuData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典XuanShangMiaoShuData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
