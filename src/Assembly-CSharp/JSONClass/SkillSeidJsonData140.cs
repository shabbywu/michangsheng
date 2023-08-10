using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData140 : IJSONClass
{
	public static int SEIDID = 140;

	public static Dictionary<int, SkillSeidJsonData140> DataDict = new Dictionary<int, SkillSeidJsonData140>();

	public static List<SkillSeidJsonData140> DataList = new List<SkillSeidJsonData140>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public List<int> value1 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[140].list)
		{
			try
			{
				SkillSeidJsonData140 skillSeidJsonData = new SkillSeidJsonData140();
				skillSeidJsonData.skillid = item["skillid"].I;
				skillSeidJsonData.value1 = item["value1"].ToList();
				if (DataDict.ContainsKey(skillSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData140.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData140.DataDict添加数据时出现异常，已跳过，请检查配表");
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
