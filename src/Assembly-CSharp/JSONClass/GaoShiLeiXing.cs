using System;
using System.Collections.Generic;

namespace JSONClass;

public class GaoShiLeiXing : IJSONClass
{
	public static Dictionary<string, GaoShiLeiXing> DataDict = new Dictionary<string, GaoShiLeiXing>();

	public static List<GaoShiLeiXing> DataList = new List<GaoShiLeiXing>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int cd;

	public string id;

	public string name;

	public List<int> num = new List<int>();

	public List<int> qujian = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.GaoShiLeiXing.list)
		{
			try
			{
				GaoShiLeiXing gaoShiLeiXing = new GaoShiLeiXing();
				gaoShiLeiXing.cd = item["cd"].I;
				gaoShiLeiXing.id = item["id"].Str;
				gaoShiLeiXing.name = item["name"].Str;
				gaoShiLeiXing.num = item["num"].ToList();
				gaoShiLeiXing.qujian = item["qujian"].ToList();
				if (DataDict.ContainsKey(gaoShiLeiXing.id))
				{
					PreloadManager.LogException("!!!错误!!!向字典GaoShiLeiXing.DataDict添加数据时出现重复的键，Key:" + gaoShiLeiXing.id + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(gaoShiLeiXing.id, gaoShiLeiXing);
				DataList.Add(gaoShiLeiXing);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典GaoShiLeiXing.DataDict添加数据时出现异常，已跳过，请检查配表");
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
