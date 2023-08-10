using System;
using System.Collections.Generic;

namespace JSONClass;

public class StaticSkillSeidJsonData1 : IJSONClass
{
	public static int SEIDID = 1;

	public static Dictionary<int, StaticSkillSeidJsonData1> DataDict = new Dictionary<int, StaticSkillSeidJsonData1>();

	public static List<StaticSkillSeidJsonData1> DataList = new List<StaticSkillSeidJsonData1>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public int target;

	public List<int> value1 = new List<int>();

	public List<int> value2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.StaticSkillSeidJsonData[1].list)
		{
			try
			{
				StaticSkillSeidJsonData1 staticSkillSeidJsonData = new StaticSkillSeidJsonData1();
				staticSkillSeidJsonData.skillid = item["skillid"].I;
				staticSkillSeidJsonData.target = item["target"].I;
				staticSkillSeidJsonData.value1 = item["value1"].ToList();
				staticSkillSeidJsonData.value2 = item["value2"].ToList();
				if (DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典StaticSkillSeidJsonData1.DataDict添加数据时出现重复的键，Key:{staticSkillSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
				DataList.Add(staticSkillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
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
