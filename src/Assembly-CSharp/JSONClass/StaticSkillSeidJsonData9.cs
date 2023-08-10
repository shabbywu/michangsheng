using System;
using System.Collections.Generic;

namespace JSONClass;

public class StaticSkillSeidJsonData9 : IJSONClass
{
	public static int SEIDID = 9;

	public static Dictionary<int, StaticSkillSeidJsonData9> DataDict = new Dictionary<int, StaticSkillSeidJsonData9>();

	public static List<StaticSkillSeidJsonData9> DataList = new List<StaticSkillSeidJsonData9>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int skillid;

	public string Spine;

	public string OnMoveEnter;

	public string OnMoveExit;

	public string OnLoopMoveEnter;

	public string OnLoopMoveExit;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.StaticSkillSeidJsonData[9].list)
		{
			try
			{
				StaticSkillSeidJsonData9 staticSkillSeidJsonData = new StaticSkillSeidJsonData9();
				staticSkillSeidJsonData.skillid = item["skillid"].I;
				staticSkillSeidJsonData.Spine = item["Spine"].Str;
				staticSkillSeidJsonData.OnMoveEnter = item["OnMoveEnter"].Str;
				staticSkillSeidJsonData.OnMoveExit = item["OnMoveExit"].Str;
				staticSkillSeidJsonData.OnLoopMoveEnter = item["OnLoopMoveEnter"].Str;
				staticSkillSeidJsonData.OnLoopMoveExit = item["OnLoopMoveExit"].Str;
				if (DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典StaticSkillSeidJsonData9.DataDict添加数据时出现重复的键，Key:{staticSkillSeidJsonData.skillid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
				DataList.Add(staticSkillSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData9.DataDict添加数据时出现异常，已跳过，请检查配表");
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
