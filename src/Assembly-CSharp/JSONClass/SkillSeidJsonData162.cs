using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData162 : IJSONClass
{
	public static int SEIDID = 162;

	public static Dictionary<int, SkillSeidJsonData162> DataDict = new Dictionary<int, SkillSeidJsonData162>();

	public static List<SkillSeidJsonData162> DataList = new List<SkillSeidJsonData162>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int target;

	public int value1;

	public int value2;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[162].list)
		{
			try
			{
				SkillSeidJsonData162 skillSeidJsonData = new SkillSeidJsonData162();
				skillSeidJsonData.id = item["id"].I;
				skillSeidJsonData.target = item["target"].I;
				skillSeidJsonData.value1 = item["value1"].I;
				skillSeidJsonData.value2 = item["value2"].I;
				if (DataDict.ContainsKey(skillSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData162.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData162.DataDict添加数据时出现异常，已跳过，请检查配表");
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
