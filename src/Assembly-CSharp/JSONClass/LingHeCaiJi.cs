using System;
using System.Collections.Generic;

namespace JSONClass;

public class LingHeCaiJi : IJSONClass
{
	public static Dictionary<int, LingHeCaiJi> DataDict = new Dictionary<int, LingHeCaiJi>();

	public static List<LingHeCaiJi> DataList = new List<LingHeCaiJi>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int MapIndex;

	public int ShouYiLv;

	public int LingHe;

	public int ShengShiLimit;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LingHeCaiJi.list)
		{
			try
			{
				LingHeCaiJi lingHeCaiJi = new LingHeCaiJi();
				lingHeCaiJi.MapIndex = item["MapIndex"].I;
				lingHeCaiJi.ShouYiLv = item["ShouYiLv"].I;
				lingHeCaiJi.LingHe = item["LingHe"].I;
				lingHeCaiJi.ShengShiLimit = item["ShengShiLimit"].I;
				if (DataDict.ContainsKey(lingHeCaiJi.MapIndex))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LingHeCaiJi.DataDict添加数据时出现重复的键，Key:{lingHeCaiJi.MapIndex}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lingHeCaiJi.MapIndex, lingHeCaiJi);
				DataList.Add(lingHeCaiJi);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LingHeCaiJi.DataDict添加数据时出现异常，已跳过，请检查配表");
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
