using System;
using System.Collections.Generic;

namespace JSONClass;

public class DiYuShengWangData : IJSONClass
{
	public static Dictionary<int, DiYuShengWangData> DataDict = new Dictionary<int, DiYuShengWangData>();

	public static List<DiYuShengWangData> DataList = new List<DiYuShengWangData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ShiLi;

	public int ShengWangLV;

	public int ShenFen;

	public string TeQuan;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DiYuShengWangData.list)
		{
			try
			{
				DiYuShengWangData diYuShengWangData = new DiYuShengWangData();
				diYuShengWangData.id = item["id"].I;
				diYuShengWangData.ShiLi = item["ShiLi"].I;
				diYuShengWangData.ShengWangLV = item["ShengWangLV"].I;
				diYuShengWangData.ShenFen = item["ShenFen"].I;
				diYuShengWangData.TeQuan = item["TeQuan"].Str;
				if (DataDict.ContainsKey(diYuShengWangData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DiYuShengWangData.DataDict添加数据时出现重复的键，Key:{diYuShengWangData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(diYuShengWangData.id, diYuShengWangData);
				DataList.Add(diYuShengWangData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DiYuShengWangData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
