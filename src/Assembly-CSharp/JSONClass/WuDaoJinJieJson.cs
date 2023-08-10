using System;
using System.Collections.Generic;

namespace JSONClass;

public class WuDaoJinJieJson : IJSONClass
{
	public static Dictionary<int, WuDaoJinJieJson> DataDict = new Dictionary<int, WuDaoJinJieJson>();

	public static List<WuDaoJinJieJson> DataList = new List<WuDaoJinJieJson>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int LV;

	public int Max;

	public int JiaCheng;

	public int LianDan;

	public int LianQi;

	public string Text;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoJinJieJson.list)
		{
			try
			{
				WuDaoJinJieJson wuDaoJinJieJson = new WuDaoJinJieJson();
				wuDaoJinJieJson.id = item["id"].I;
				wuDaoJinJieJson.LV = item["LV"].I;
				wuDaoJinJieJson.Max = item["Max"].I;
				wuDaoJinJieJson.JiaCheng = item["JiaCheng"].I;
				wuDaoJinJieJson.LianDan = item["LianDan"].I;
				wuDaoJinJieJson.LianQi = item["LianQi"].I;
				wuDaoJinJieJson.Text = item["Text"].Str;
				if (DataDict.ContainsKey(wuDaoJinJieJson.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典WuDaoJinJieJson.DataDict添加数据时出现重复的键，Key:{wuDaoJinJieJson.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(wuDaoJinJieJson.id, wuDaoJinJieJson);
				DataList.Add(wuDaoJinJieJson);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典WuDaoJinJieJson.DataDict添加数据时出现异常，已跳过，请检查配表");
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
