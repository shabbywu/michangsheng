using System;
using System.Collections.Generic;

namespace JSONClass;

public class DongTaiChuanWenBaio : IJSONClass
{
	public static Dictionary<int, DongTaiChuanWenBaio> DataDict = new Dictionary<int, DongTaiChuanWenBaio>();

	public static List<DongTaiChuanWenBaio> DataList = new List<DongTaiChuanWenBaio>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int cunZaiShiJian;

	public int isshili;

	public string text;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DongTaiChuanWenBaio.list)
		{
			try
			{
				DongTaiChuanWenBaio dongTaiChuanWenBaio = new DongTaiChuanWenBaio();
				dongTaiChuanWenBaio.id = item["id"].I;
				dongTaiChuanWenBaio.cunZaiShiJian = item["cunZaiShiJian"].I;
				dongTaiChuanWenBaio.isshili = item["isshili"].I;
				dongTaiChuanWenBaio.text = item["text"].Str;
				if (DataDict.ContainsKey(dongTaiChuanWenBaio.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DongTaiChuanWenBaio.DataDict添加数据时出现重复的键，Key:{dongTaiChuanWenBaio.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(dongTaiChuanWenBaio.id, dongTaiChuanWenBaio);
				DataList.Add(dongTaiChuanWenBaio);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DongTaiChuanWenBaio.DataDict添加数据时出现异常，已跳过，请检查配表");
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
