using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData158 : IJSONClass
{
	public static int SEIDID = 158;

	public static Dictionary<int, SkillSeidJsonData158> DataDict = new Dictionary<int, SkillSeidJsonData158>();

	public static List<SkillSeidJsonData158> DataList = new List<SkillSeidJsonData158>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public int value3;

	public List<int> value2 = new List<int>();

	public List<int> value4 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[158].list)
		{
			try
			{
				SkillSeidJsonData158 skillSeidJsonData = new SkillSeidJsonData158();
				skillSeidJsonData.id = item["id"].I;
				skillSeidJsonData.value1 = item["value1"].I;
				skillSeidJsonData.value3 = item["value3"].I;
				skillSeidJsonData.value2 = item["value2"].ToList();
				skillSeidJsonData.value4 = item["value4"].ToList();
				if (DataDict.ContainsKey(skillSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData158.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData158.DataDict添加数据时出现异常，已跳过，请检查配表");
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
