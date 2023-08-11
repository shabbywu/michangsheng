using System;
using System.Collections.Generic;

namespace JSONClass;

public class JieDanSeidJsonData1 : IJSONClass
{
	public static int SEIDID = 1;

	public static Dictionary<int, JieDanSeidJsonData1> DataDict = new Dictionary<int, JieDanSeidJsonData1>();

	public static List<JieDanSeidJsonData1> DataList = new List<JieDanSeidJsonData1>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public int target;

	public List<int> value1 = new List<int>();

	public List<int> value2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.JieDanSeidJsonData[1].list)
		{
			try
			{
				JieDanSeidJsonData1 jieDanSeidJsonData = new JieDanSeidJsonData1();
				jieDanSeidJsonData.skillid = item["skillid"].I;
				jieDanSeidJsonData.target = item["target"].I;
				jieDanSeidJsonData.value1 = item["value1"].ToList();
				jieDanSeidJsonData.value2 = item["value2"].ToList();
				if (DataDict.ContainsKey(jieDanSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典JieDanSeidJsonData1.DataDict添加数据时出现重复的键，Key:{jieDanSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(jieDanSeidJsonData.skillid, jieDanSeidJsonData);
				DataList.Add(jieDanSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典JieDanSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
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
