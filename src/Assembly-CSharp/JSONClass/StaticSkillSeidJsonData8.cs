using System;
using System.Collections.Generic;

namespace JSONClass;

public class StaticSkillSeidJsonData8 : IJSONClass
{
	public static int SEIDID = 8;

	public static Dictionary<int, StaticSkillSeidJsonData8> DataDict = new Dictionary<int, StaticSkillSeidJsonData8>();

	public static List<StaticSkillSeidJsonData8> DataList = new List<StaticSkillSeidJsonData8>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.StaticSkillSeidJsonData[8].list)
		{
			try
			{
				StaticSkillSeidJsonData8 staticSkillSeidJsonData = new StaticSkillSeidJsonData8();
				staticSkillSeidJsonData.skillid = item["skillid"].I;
				staticSkillSeidJsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典StaticSkillSeidJsonData8.DataDict添加数据时出现重复的键，Key:{staticSkillSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
				DataList.Add(staticSkillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData8.DataDict添加数据时出现异常，已跳过，请检查配表");
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
