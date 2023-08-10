using System;
using System.Collections.Generic;

namespace JSONClass;

public class GaoShi : IJSONClass
{
	public static Dictionary<int, GaoShi> DataDict = new Dictionary<int, GaoShi>();

	public static List<GaoShi> DataList = new List<GaoShi>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int itemid;

	public int type;

	public int num;

	public int jiagexishu;

	public int shengwangid;

	public int shengwang;

	public int taskid;

	public int menpaihuobi;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.GaoShi.list)
		{
			try
			{
				GaoShi gaoShi = new GaoShi();
				gaoShi.id = item["id"].I;
				gaoShi.itemid = item["itemid"].I;
				gaoShi.type = item["type"].I;
				gaoShi.num = item["num"].I;
				gaoShi.jiagexishu = item["jiagexishu"].I;
				gaoShi.shengwangid = item["shengwangid"].I;
				gaoShi.shengwang = item["shengwang"].I;
				gaoShi.taskid = item["taskid"].I;
				gaoShi.menpaihuobi = item["menpaihuobi"].I;
				if (DataDict.ContainsKey(gaoShi.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典GaoShi.DataDict添加数据时出现重复的键，Key:{gaoShi.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(gaoShi.id, gaoShi);
				DataList.Add(gaoShi);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典GaoShi.DataDict添加数据时出现异常，已跳过，请检查配表");
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
