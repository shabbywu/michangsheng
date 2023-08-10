using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianDanDanFangBiao : IJSONClass
{
	public static Dictionary<int, LianDanDanFangBiao> DataDict = new Dictionary<int, LianDanDanFangBiao>();

	public static List<LianDanDanFangBiao> DataList = new List<LianDanDanFangBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ItemID;

	public int value1;

	public int num1;

	public int value2;

	public int num2;

	public int value3;

	public int num3;

	public int value4;

	public int num4;

	public int value5;

	public int num5;

	public int castTime;

	public string name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianDanDanFangBiao.list)
		{
			try
			{
				LianDanDanFangBiao lianDanDanFangBiao = new LianDanDanFangBiao();
				lianDanDanFangBiao.id = item["id"].I;
				lianDanDanFangBiao.ItemID = item["ItemID"].I;
				lianDanDanFangBiao.value1 = item["value1"].I;
				lianDanDanFangBiao.num1 = item["num1"].I;
				lianDanDanFangBiao.value2 = item["value2"].I;
				lianDanDanFangBiao.num2 = item["num2"].I;
				lianDanDanFangBiao.value3 = item["value3"].I;
				lianDanDanFangBiao.num3 = item["num3"].I;
				lianDanDanFangBiao.value4 = item["value4"].I;
				lianDanDanFangBiao.num4 = item["num4"].I;
				lianDanDanFangBiao.value5 = item["value5"].I;
				lianDanDanFangBiao.num5 = item["num5"].I;
				lianDanDanFangBiao.castTime = item["castTime"].I;
				lianDanDanFangBiao.name = item["name"].Str;
				if (DataDict.ContainsKey(lianDanDanFangBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianDanDanFangBiao.DataDict添加数据时出现重复的键，Key:{lianDanDanFangBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianDanDanFangBiao.id, lianDanDanFangBiao);
				DataList.Add(lianDanDanFangBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianDanDanFangBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
