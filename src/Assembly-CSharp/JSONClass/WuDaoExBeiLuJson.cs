using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuDaoExBeiLuJson : IJSONClass
{
	public static Dictionary<int, WuDaoExBeiLuJson> DataDict = new Dictionary<int, WuDaoExBeiLuJson>();

	public static List<WuDaoExBeiLuJson> DataList = new List<WuDaoExBeiLuJson>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int gongfa;

	public int linwu;

	public int tupo;

	public int kanshu;

	public int lingguang1;

	public int lingguang2;

	public int lingguang3;

	public int lingguang4;

	public int lingguang5;

	public int lingguang6;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoExBeiLuJson.list)
		{
			try
			{
				WuDaoExBeiLuJson wuDaoExBeiLuJson = new WuDaoExBeiLuJson();
				wuDaoExBeiLuJson.id = item["id"].I;
				wuDaoExBeiLuJson.gongfa = item["gongfa"].I;
				wuDaoExBeiLuJson.linwu = item["linwu"].I;
				wuDaoExBeiLuJson.tupo = item["tupo"].I;
				wuDaoExBeiLuJson.kanshu = item["kanshu"].I;
				wuDaoExBeiLuJson.lingguang1 = item["lingguang1"].I;
				wuDaoExBeiLuJson.lingguang2 = item["lingguang2"].I;
				wuDaoExBeiLuJson.lingguang3 = item["lingguang3"].I;
				wuDaoExBeiLuJson.lingguang4 = item["lingguang4"].I;
				wuDaoExBeiLuJson.lingguang5 = item["lingguang5"].I;
				wuDaoExBeiLuJson.lingguang6 = item["lingguang6"].I;
				if (DataDict.ContainsKey(wuDaoExBeiLuJson.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuDaoExBeiLuJson.DataDict添加数据时出现重复的键，Key:{wuDaoExBeiLuJson.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuDaoExBeiLuJson.id, wuDaoExBeiLuJson);
				DataList.Add(wuDaoExBeiLuJson);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuDaoExBeiLuJson.DataDict添加数据时出现异常，已跳过，请检查配表");
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
