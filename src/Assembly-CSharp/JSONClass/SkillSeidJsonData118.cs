using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData118 : IJSONClass
{
	public static int SEIDID = 118;

	public static Dictionary<int, SkillSeidJsonData118> DataDict = new Dictionary<int, SkillSeidJsonData118>();

	public static List<SkillSeidJsonData118> DataList = new List<SkillSeidJsonData118>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> value1 = new List<int>();

	public List<int> value2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[118].list)
		{
			try
			{
				SkillSeidJsonData118 skillSeidJsonData = new SkillSeidJsonData118();
				skillSeidJsonData.id = item["id"].I;
				skillSeidJsonData.value1 = item["value1"].ToList();
				skillSeidJsonData.value2 = item["value2"].ToList();
				if (DataDict.ContainsKey(skillSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData118.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData118.DataDict添加数据时出现异常，已跳过，请检查配表");
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
