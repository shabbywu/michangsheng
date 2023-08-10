using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData149 : IJSONClass
{
	public static int SEIDID = 149;

	public static Dictionary<int, SkillSeidJsonData149> DataDict = new Dictionary<int, SkillSeidJsonData149>();

	public static List<SkillSeidJsonData149> DataList = new List<SkillSeidJsonData149>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public int target;

	public int value1;

	public int value2;

	public string panduan;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[149].list)
		{
			try
			{
				SkillSeidJsonData149 skillSeidJsonData = new SkillSeidJsonData149();
				skillSeidJsonData.skillid = item["skillid"].I;
				skillSeidJsonData.target = item["target"].I;
				skillSeidJsonData.value1 = item["value1"].I;
				skillSeidJsonData.value2 = item["value2"].I;
				skillSeidJsonData.panduan = item["panduan"].Str;
				if (DataDict.ContainsKey(skillSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillSeidJsonData149.DataDict添加数据时出现重复的键，Key:{skillSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
				DataList.Add(skillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData149.DataDict添加数据时出现异常，已跳过，请检查配表");
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
