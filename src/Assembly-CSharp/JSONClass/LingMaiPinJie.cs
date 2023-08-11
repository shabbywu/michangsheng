using System;
using System.Collections.Generic;

namespace JSONClass;

public class LingMaiPinJie : IJSONClass
{
	public static Dictionary<int, LingMaiPinJie> DataDict = new Dictionary<int, LingMaiPinJie>();

	public static List<LingMaiPinJie> DataList = new List<LingMaiPinJie>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int Id;

	public int ShouYiLv;

	public int LingHeLv;

	public int ShengShiLv;

	public string ShouYiDesc;

	public string LingHeDesc;

	public string ShengShiDesc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LingMaiPinJie.list)
		{
			try
			{
				LingMaiPinJie lingMaiPinJie = new LingMaiPinJie();
				lingMaiPinJie.Id = item["Id"].I;
				lingMaiPinJie.ShouYiLv = item["ShouYiLv"].I;
				lingMaiPinJie.LingHeLv = item["LingHeLv"].I;
				lingMaiPinJie.ShengShiLv = item["ShengShiLv"].I;
				lingMaiPinJie.ShouYiDesc = item["ShouYiDesc"].Str;
				lingMaiPinJie.LingHeDesc = item["LingHeDesc"].Str;
				lingMaiPinJie.ShengShiDesc = item["ShengShiDesc"].Str;
				if (DataDict.ContainsKey(lingMaiPinJie.Id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LingMaiPinJie.DataDict添加数据时出现重复的键，Key:{lingMaiPinJie.Id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lingMaiPinJie.Id, lingMaiPinJie);
				DataList.Add(lingMaiPinJie);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LingMaiPinJie.DataDict添加数据时出现异常，已跳过，请检查配表");
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
