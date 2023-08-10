using System;
using System.Collections.Generic;

namespace JSONClass;

public class NTaskXiangXi : IJSONClass
{
	public static Dictionary<int, NTaskXiangXi> DataDict = new Dictionary<int, NTaskXiangXi>();

	public static List<NTaskXiangXi> DataList = new List<NTaskXiangXi>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int JiaoFuType;

	public int Type;

	public int percent;

	public int shiXian;

	public int ShiLIAdd;

	public int GeRenAdd;

	public int ShiLIReduce;

	public int GeRenReduce;

	public int shouYiLu;

	public string name;

	public string SayMiaoShu;

	public string zongmiaoshu;

	public string TaskZiXiang;

	public List<int> Level = new List<int>();

	public List<int> menpaihaogan = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NTaskXiangXi.list)
		{
			try
			{
				NTaskXiangXi nTaskXiangXi = new NTaskXiangXi();
				nTaskXiangXi.id = item["id"].I;
				nTaskXiangXi.JiaoFuType = item["JiaoFuType"].I;
				nTaskXiangXi.Type = item["Type"].I;
				nTaskXiangXi.percent = item["percent"].I;
				nTaskXiangXi.shiXian = item["shiXian"].I;
				nTaskXiangXi.ShiLIAdd = item["ShiLIAdd"].I;
				nTaskXiangXi.GeRenAdd = item["GeRenAdd"].I;
				nTaskXiangXi.ShiLIReduce = item["ShiLIReduce"].I;
				nTaskXiangXi.GeRenReduce = item["GeRenReduce"].I;
				nTaskXiangXi.shouYiLu = item["shouYiLu"].I;
				nTaskXiangXi.name = item["name"].Str;
				nTaskXiangXi.SayMiaoShu = item["SayMiaoShu"].Str;
				nTaskXiangXi.zongmiaoshu = item["zongmiaoshu"].Str;
				nTaskXiangXi.TaskZiXiang = item["TaskZiXiang"].Str;
				nTaskXiangXi.Level = item["Level"].ToList();
				nTaskXiangXi.menpaihaogan = item["menpaihaogan"].ToList();
				if (DataDict.ContainsKey(nTaskXiangXi.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NTaskXiangXi.DataDict添加数据时出现重复的键，Key:{nTaskXiangXi.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nTaskXiangXi.id, nTaskXiangXi);
				DataList.Add(nTaskXiangXi);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NTaskXiangXi.DataDict添加数据时出现异常，已跳过，请检查配表");
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
