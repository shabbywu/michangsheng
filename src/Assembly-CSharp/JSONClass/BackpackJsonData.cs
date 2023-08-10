using System;
using System.Collections.Generic;

namespace JSONClass;

public class BackpackJsonData : IJSONClass
{
	public static Dictionary<int, BackpackJsonData> DataDict = new Dictionary<int, BackpackJsonData>();

	public static List<BackpackJsonData> DataList = new List<BackpackJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int AvatrID;

	public int Type;

	public int quality;

	public int CanSell;

	public int SellPercent;

	public int CanDrop;

	public string BackpackName;

	public List<int> ItemID = new List<int>();

	public List<int> randomNum = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BackpackJsonData.list)
		{
			try
			{
				BackpackJsonData backpackJsonData = new BackpackJsonData();
				backpackJsonData.id = item["id"].I;
				backpackJsonData.AvatrID = item["AvatrID"].I;
				backpackJsonData.Type = item["Type"].I;
				backpackJsonData.quality = item["quality"].I;
				backpackJsonData.CanSell = item["CanSell"].I;
				backpackJsonData.SellPercent = item["SellPercent"].I;
				backpackJsonData.CanDrop = item["CanDrop"].I;
				backpackJsonData.BackpackName = item["BackpackName"].Str;
				backpackJsonData.ItemID = item["ItemID"].ToList();
				backpackJsonData.randomNum = item["randomNum"].ToList();
				if (DataDict.ContainsKey(backpackJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BackpackJsonData.DataDict添加数据时出现重复的键，Key:{backpackJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(backpackJsonData.id, backpackJsonData);
				DataList.Add(backpackJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BackpackJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
