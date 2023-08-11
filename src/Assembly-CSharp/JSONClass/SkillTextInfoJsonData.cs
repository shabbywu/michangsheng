using System;
using System.Collections.Generic;

namespace JSONClass;

public class SkillTextInfoJsonData : IJSONClass
{
	public static Dictionary<int, SkillTextInfoJsonData> DataDict = new Dictionary<int, SkillTextInfoJsonData>();

	public static List<SkillTextInfoJsonData> DataList = new List<SkillTextInfoJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string name;

	public string descr;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillTextInfoJsonData.list)
		{
			try
			{
				SkillTextInfoJsonData skillTextInfoJsonData = new SkillTextInfoJsonData();
				skillTextInfoJsonData.id = item["id"].I;
				skillTextInfoJsonData.name = item["name"].Str;
				skillTextInfoJsonData.descr = item["descr"].Str;
				if (DataDict.ContainsKey(skillTextInfoJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SkillTextInfoJsonData.DataDict添加数据时出现重复的键，Key:{skillTextInfoJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillTextInfoJsonData.id, skillTextInfoJsonData);
				DataList.Add(skillTextInfoJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SkillTextInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
