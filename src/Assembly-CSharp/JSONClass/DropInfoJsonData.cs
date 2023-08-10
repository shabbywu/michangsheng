using System;
using System.Collections.Generic;

namespace JSONClass;

public class DropInfoJsonData : IJSONClass
{
	public static Dictionary<int, DropInfoJsonData> DataDict = new Dictionary<int, DropInfoJsonData>();

	public static List<DropInfoJsonData> DataList = new List<DropInfoJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int dropType;

	public int loseHp;

	public int round;

	public int moneydrop;

	public int backpack;

	public int wepen;

	public int cloth;

	public int ring;

	public string Title;

	public string TextDesc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DropInfoJsonData.list)
		{
			try
			{
				DropInfoJsonData dropInfoJsonData = new DropInfoJsonData();
				dropInfoJsonData.id = item["id"].I;
				dropInfoJsonData.dropType = item["dropType"].I;
				dropInfoJsonData.loseHp = item["loseHp"].I;
				dropInfoJsonData.round = item["round"].I;
				dropInfoJsonData.moneydrop = item["moneydrop"].I;
				dropInfoJsonData.backpack = item["backpack"].I;
				dropInfoJsonData.wepen = item["wepen"].I;
				dropInfoJsonData.cloth = item["cloth"].I;
				dropInfoJsonData.ring = item["ring"].I;
				dropInfoJsonData.Title = item["Title"].Str;
				dropInfoJsonData.TextDesc = item["TextDesc"].Str;
				if (DataDict.ContainsKey(dropInfoJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DropInfoJsonData.DataDict添加数据时出现重复的键，Key:{dropInfoJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(dropInfoJsonData.id, dropInfoJsonData);
				DataList.Add(dropInfoJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DropInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
