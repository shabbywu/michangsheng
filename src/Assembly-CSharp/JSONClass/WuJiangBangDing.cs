using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuJiangBangDing : IJSONClass
{
	public static Dictionary<int, WuJiangBangDing> DataDict = new Dictionary<int, WuJiangBangDing>();

	public static List<WuJiangBangDing> DataList = new List<WuJiangBangDing>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Image;

	public int PaiMaiHang;

	public string TimeStart;

	public string TimeEnd;

	public string Name;

	public string Title;

	public List<int> avatar = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuJiangBangDing.list)
		{
			try
			{
				WuJiangBangDing wuJiangBangDing = new WuJiangBangDing();
				wuJiangBangDing.id = item["id"].I;
				wuJiangBangDing.Image = item["Image"].I;
				wuJiangBangDing.PaiMaiHang = item["PaiMaiHang"].I;
				wuJiangBangDing.TimeStart = item["TimeStart"].Str;
				wuJiangBangDing.TimeEnd = item["TimeEnd"].Str;
				wuJiangBangDing.Name = item["Name"].Str;
				wuJiangBangDing.Title = item["Title"].Str;
				wuJiangBangDing.avatar = item["avatar"].ToList();
				if (DataDict.ContainsKey(wuJiangBangDing.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuJiangBangDing.DataDict添加数据时出现重复的键，Key:{wuJiangBangDing.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuJiangBangDing.id, wuJiangBangDing);
				DataList.Add(wuJiangBangDing);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuJiangBangDing.DataDict添加数据时出现异常，已跳过，请检查配表");
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
