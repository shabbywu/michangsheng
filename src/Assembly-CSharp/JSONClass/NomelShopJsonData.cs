using System;
using System.Collections.Generic;

namespace JSONClass;

public class NomelShopJsonData : IJSONClass
{
	public static Dictionary<int, NomelShopJsonData> DataDict = new Dictionary<int, NomelShopJsonData>();

	public static List<NomelShopJsonData> DataList = new List<NomelShopJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int threeScene;

	public int SType;

	public int shopType;

	public int price;

	public int ExShopID;

	public string ChildTitle;

	public string Title;

	public List<int> items = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NomelShopJsonData.list)
		{
			try
			{
				NomelShopJsonData nomelShopJsonData = new NomelShopJsonData();
				nomelShopJsonData.id = item["id"].I;
				nomelShopJsonData.threeScene = item["threeScene"].I;
				nomelShopJsonData.SType = item["SType"].I;
				nomelShopJsonData.shopType = item["shopType"].I;
				nomelShopJsonData.price = item["price"].I;
				nomelShopJsonData.ExShopID = item["ExShopID"].I;
				nomelShopJsonData.ChildTitle = item["ChildTitle"].Str;
				nomelShopJsonData.Title = item["Title"].Str;
				nomelShopJsonData.items = item["items"].ToList();
				if (DataDict.ContainsKey(nomelShopJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NomelShopJsonData.DataDict添加数据时出现重复的键，Key:{nomelShopJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nomelShopJsonData.id, nomelShopJsonData);
				DataList.Add(nomelShopJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NomelShopJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
