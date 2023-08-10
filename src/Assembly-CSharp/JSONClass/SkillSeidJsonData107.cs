using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData107 : IJSONClass
{
	public static int SEIDID = 107;

	public static Dictionary<int, SkillSeidJsonData107> DataDict = new Dictionary<int, SkillSeidJsonData107>();

	public static List<SkillSeidJsonData107> DataList = new List<SkillSeidJsonData107>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public int target;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[107].list)
		{
			try
			{
				SkillSeidJsonData107 skillSeidJsonData = new SkillSeidJsonData107();
				skillSeidJsonData.skillid = item["skillid"].I;
				skillSeidJsonData.target = item["target"].I;
				skillSeidJsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(skillSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData107.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData107.DataDict添加数据时出现异常，已跳过，请检查配表");
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
