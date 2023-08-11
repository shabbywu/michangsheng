using System;
using System.Collections.Generic;

namespace JSONClass;

public class XinJinJsonData : IJSONClass
{
	public static Dictionary<int, XinJinJsonData> DataDict = new Dictionary<int, XinJinJsonData>();

	public static List<XinJinJsonData> DataList = new List<XinJinJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Max;

	public string Text;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.XinJinJsonData.list)
		{
			try
			{
				XinJinJsonData xinJinJsonData = new XinJinJsonData();
				xinJinJsonData.id = item["id"].I;
				xinJinJsonData.Max = item["Max"].I;
				xinJinJsonData.Text = item["Text"].Str;
				if (DataDict.ContainsKey(xinJinJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典XinJinJsonData.DataDict添加数据时出现重复的键，Key:{xinJinJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(xinJinJsonData.id, xinJinJsonData);
				DataList.Add(xinJinJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典XinJinJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
