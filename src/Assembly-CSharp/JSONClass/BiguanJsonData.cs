using System;
using System.Collections.Generic;

namespace JSONClass;

public class BiguanJsonData : IJSONClass
{
	public static Dictionary<int, BiguanJsonData> DataDict = new Dictionary<int, BiguanJsonData>();

	public static List<BiguanJsonData> DataList = new List<BiguanJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int speed;

	public string Text;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BiguanJsonData.list)
		{
			try
			{
				BiguanJsonData biguanJsonData = new BiguanJsonData();
				biguanJsonData.id = item["id"].I;
				biguanJsonData.speed = item["speed"].I;
				biguanJsonData.Text = item["Text"].Str;
				if (DataDict.ContainsKey(biguanJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典BiguanJsonData.DataDict添加数据时出现重复的键，Key:{biguanJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(biguanJsonData.id, biguanJsonData);
				DataList.Add(biguanJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典BiguanJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
