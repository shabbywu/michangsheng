using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData151 : IJSONClass
{
	public static int SEIDID = 151;

	public static Dictionary<int, SkillSeidJsonData151> DataDict = new Dictionary<int, SkillSeidJsonData151>();

	public static List<SkillSeidJsonData151> DataList = new List<SkillSeidJsonData151>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public int value1;

	public List<int> value2 = new List<int>();

	public List<int> value3 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[151].list)
		{
			try
			{
				SkillSeidJsonData151 skillSeidJsonData = new SkillSeidJsonData151();
				skillSeidJsonData.skillid = item["skillid"].I;
				skillSeidJsonData.value1 = item["value1"].I;
				skillSeidJsonData.value2 = item["value2"].ToList();
				skillSeidJsonData.value3 = item["value3"].ToList();
				if (DataDict.ContainsKey(skillSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData151.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData151.DataDict添加数据时出现异常，已跳过，请检查配表");
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
