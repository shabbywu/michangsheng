using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData111 : IJSONClass
{
	public static int SEIDID = 111;

	public static Dictionary<int, SkillSeidJsonData111> DataDict = new Dictionary<int, SkillSeidJsonData111>();

	public static List<SkillSeidJsonData111> DataList = new List<SkillSeidJsonData111>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[111].list)
		{
			try
			{
				SkillSeidJsonData111 skillSeidJsonData = new SkillSeidJsonData111();
				skillSeidJsonData.id = item["id"].I;
				skillSeidJsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(skillSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData111.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData111.DataDict添加数据时出现异常，已跳过，请检查配表");
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
