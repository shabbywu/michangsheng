using System;
using System.Collections.Generic;

namespace JSONClass;

public class SeaJiZhiXingXiang : IJSONClass
{
	public static Dictionary<int, SeaJiZhiXingXiang> DataDict = new Dictionary<int, SeaJiZhiXingXiang>();

	public static List<SeaJiZhiXingXiang> DataList = new List<SeaJiZhiXingXiang>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int OffsetX;

	public int OffsetY;

	public int ScaleX;

	public int ScaleY;

	public string Skin;

	public string Anim;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SeaJiZhiXingXiang.list)
		{
			try
			{
				SeaJiZhiXingXiang seaJiZhiXingXiang = new SeaJiZhiXingXiang();
				seaJiZhiXingXiang.id = item["id"].I;
				seaJiZhiXingXiang.OffsetX = item["OffsetX"].I;
				seaJiZhiXingXiang.OffsetY = item["OffsetY"].I;
				seaJiZhiXingXiang.ScaleX = item["ScaleX"].I;
				seaJiZhiXingXiang.ScaleY = item["ScaleY"].I;
				seaJiZhiXingXiang.Skin = item["Skin"].Str;
				seaJiZhiXingXiang.Anim = item["Anim"].Str;
				if (DataDict.ContainsKey(seaJiZhiXingXiang.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SeaJiZhiXingXiang.DataDict添加数据时出现重复的键，Key:{seaJiZhiXingXiang.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(seaJiZhiXingXiang.id, seaJiZhiXingXiang);
				DataList.Add(seaJiZhiXingXiang);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SeaJiZhiXingXiang.DataDict添加数据时出现异常，已跳过，请检查配表");
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
