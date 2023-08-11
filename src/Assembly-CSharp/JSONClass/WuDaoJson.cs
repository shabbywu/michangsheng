using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuDaoJson : IJSONClass
{
	public static Dictionary<int, WuDaoJson> DataDict = new Dictionary<int, WuDaoJson>();

	public static List<WuDaoJson> DataList = new List<WuDaoJson>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Cast;

	public int Lv;

	public string icon;

	public string name;

	public string desc;

	public string xiaoguo;

	public List<int> Type = new List<int>();

	public List<int> seid = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoJson.list)
		{
			try
			{
				WuDaoJson wuDaoJson = new WuDaoJson();
				wuDaoJson.id = item["id"].I;
				wuDaoJson.Cast = item["Cast"].I;
				wuDaoJson.Lv = item["Lv"].I;
				wuDaoJson.icon = item["icon"].Str;
				wuDaoJson.name = item["name"].Str;
				wuDaoJson.desc = item["desc"].Str;
				wuDaoJson.xiaoguo = item["xiaoguo"].Str;
				wuDaoJson.Type = item["Type"].ToList();
				wuDaoJson.seid = item["seid"].ToList();
				if (DataDict.ContainsKey(wuDaoJson.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuDaoJson.DataDict添加数据时出现重复的键，Key:{wuDaoJson.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuDaoJson.id, wuDaoJson);
				DataList.Add(wuDaoJson);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuDaoJson.DataDict添加数据时出现异常，已跳过，请检查配表");
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
