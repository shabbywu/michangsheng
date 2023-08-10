using System;
using System.Collections.Generic;

namespace JSONClass;

public class TianJieLeiJieType : IJSONClass
{
	public static Dictionary<string, TianJieLeiJieType> DataDict = new Dictionary<string, TianJieLeiJieType>();

	public static List<TianJieLeiJieType> DataList = new List<TianJieLeiJieType>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int SkillId;

	public string id;

	public string XiangXi;

	public string CuLue;

	public string PanDing;

	public List<int> QuanZhong = new List<int>();

	public List<int> QuanZhongTiSheng = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TianJieLeiJieType.list)
		{
			try
			{
				TianJieLeiJieType tianJieLeiJieType = new TianJieLeiJieType();
				tianJieLeiJieType.SkillId = item["SkillId"].I;
				tianJieLeiJieType.id = item["id"].Str;
				tianJieLeiJieType.XiangXi = item["XiangXi"].Str;
				tianJieLeiJieType.CuLue = item["CuLue"].Str;
				tianJieLeiJieType.PanDing = item["PanDing"].Str;
				tianJieLeiJieType.QuanZhong = item["QuanZhong"].ToList();
				tianJieLeiJieType.QuanZhongTiSheng = item["QuanZhongTiSheng"].ToList();
				if (DataDict.ContainsKey(tianJieLeiJieType.id))
				{
					PreloadManager.LogException("!!!错误!!!向字典TianJieLeiJieType.DataDict添加数据时出现重复的键，Key:" + tianJieLeiJieType.id + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(tianJieLeiJieType.id, tianJieLeiJieType);
				DataList.Add(tianJieLeiJieType);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TianJieLeiJieType.DataDict添加数据时出现异常，已跳过，请检查配表");
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
