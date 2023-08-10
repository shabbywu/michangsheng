using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianQiLingWenBiao : IJSONClass
{
	public static Dictionary<int, LianQiLingWenBiao> DataDict = new Dictionary<int, LianQiLingWenBiao>();

	public static List<LianQiLingWenBiao> DataList = new List<LianQiLingWenBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int type;

	public int value1;

	public int value2;

	public int value3;

	public int value4;

	public int seid;

	public int Itemseid;

	public string name;

	public string desc;

	public string xiangxidesc;

	public List<int> Affix = new List<int>();

	public List<int> listvalue1 = new List<int>();

	public List<int> listvalue2 = new List<int>();

	public List<int> listvalue3 = new List<int>();

	public List<int> Itemintvalue1 = new List<int>();

	public List<int> Itemintvalue2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianQiLingWenBiao.list)
		{
			try
			{
				LianQiLingWenBiao lianQiLingWenBiao = new LianQiLingWenBiao();
				lianQiLingWenBiao.id = item["id"].I;
				lianQiLingWenBiao.type = item["type"].I;
				lianQiLingWenBiao.value1 = item["value1"].I;
				lianQiLingWenBiao.value2 = item["value2"].I;
				lianQiLingWenBiao.value3 = item["value3"].I;
				lianQiLingWenBiao.value4 = item["value4"].I;
				lianQiLingWenBiao.seid = item["seid"].I;
				lianQiLingWenBiao.Itemseid = item["Itemseid"].I;
				lianQiLingWenBiao.name = item["name"].Str;
				lianQiLingWenBiao.desc = item["desc"].Str;
				lianQiLingWenBiao.xiangxidesc = item["xiangxidesc"].Str;
				lianQiLingWenBiao.Affix = item["Affix"].ToList();
				lianQiLingWenBiao.listvalue1 = item["listvalue1"].ToList();
				lianQiLingWenBiao.listvalue2 = item["listvalue2"].ToList();
				lianQiLingWenBiao.listvalue3 = item["listvalue3"].ToList();
				lianQiLingWenBiao.Itemintvalue1 = item["Itemintvalue1"].ToList();
				lianQiLingWenBiao.Itemintvalue2 = item["Itemintvalue2"].ToList();
				if (DataDict.ContainsKey(lianQiLingWenBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianQiLingWenBiao.DataDict添加数据时出现重复的键，Key:{lianQiLingWenBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianQiLingWenBiao.id, lianQiLingWenBiao);
				DataList.Add(lianQiLingWenBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianQiLingWenBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
