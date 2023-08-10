using System;
using System.Collections.Generic;

namespace JSONClass;

public class AllMapCaiJiBiao : IJSONClass
{
	public static Dictionary<int, AllMapCaiJiBiao> DataDict = new Dictionary<int, AllMapCaiJiBiao>();

	public static List<AllMapCaiJiBiao> DataList = new List<AllMapCaiJiBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int ID;

	public int Item;

	public int percent;

	public int Monstar;

	public int MaiFuTime;

	public int MaiFuMonstar;

	public List<int> Num = new List<int>();

	public List<int> Level = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.AllMapCaiJiBiao.list)
		{
			try
			{
				AllMapCaiJiBiao allMapCaiJiBiao = new AllMapCaiJiBiao();
				allMapCaiJiBiao.ID = item["ID"].I;
				allMapCaiJiBiao.Item = item["Item"].I;
				allMapCaiJiBiao.percent = item["percent"].I;
				allMapCaiJiBiao.Monstar = item["Monstar"].I;
				allMapCaiJiBiao.MaiFuTime = item["MaiFuTime"].I;
				allMapCaiJiBiao.MaiFuMonstar = item["MaiFuMonstar"].I;
				allMapCaiJiBiao.Num = item["Num"].ToList();
				allMapCaiJiBiao.Level = item["Level"].ToList();
				if (DataDict.ContainsKey(allMapCaiJiBiao.ID))
				{
					PreloadManager.LogException($"!!!错误!!!向字典AllMapCaiJiBiao.DataDict添加数据时出现重复的键，Key:{allMapCaiJiBiao.ID}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(allMapCaiJiBiao.ID, allMapCaiJiBiao);
				DataList.Add(allMapCaiJiBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典AllMapCaiJiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
