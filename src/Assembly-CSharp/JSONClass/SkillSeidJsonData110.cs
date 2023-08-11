using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData110 : IJSONClass
{
	public static int SEIDID = 110;

	public static Dictionary<int, SkillSeidJsonData110> DataDict = new Dictionary<int, SkillSeidJsonData110>();

	public static List<SkillSeidJsonData110> DataList = new List<SkillSeidJsonData110>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[110].list)
		{
			try
			{
				SkillSeidJsonData110 skillSeidJsonData = new SkillSeidJsonData110();
				skillSeidJsonData.id = item["id"].I;
				skillSeidJsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(skillSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData110.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData110.DataDict添加数据时出现异常，已跳过，请检查配表");
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
