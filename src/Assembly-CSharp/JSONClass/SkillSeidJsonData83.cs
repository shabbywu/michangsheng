using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData83 : IJSONClass
{
	public static int SEIDID = 83;

	public static Dictionary<int, SkillSeidJsonData83> DataDict = new Dictionary<int, SkillSeidJsonData83>();

	public static List<SkillSeidJsonData83> DataList = new List<SkillSeidJsonData83>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public List<int> value1 = new List<int>();

	public List<int> value2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[83].list)
		{
			try
			{
				SkillSeidJsonData83 skillSeidJsonData = new SkillSeidJsonData83();
				skillSeidJsonData.skillid = item["skillid"].I;
				skillSeidJsonData.value1 = item["value1"].ToList();
				skillSeidJsonData.value2 = item["value2"].ToList();
				if (DataDict.ContainsKey(skillSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData83.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData83.DataDict添加数据时出现异常，已跳过，请检查配表");
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
