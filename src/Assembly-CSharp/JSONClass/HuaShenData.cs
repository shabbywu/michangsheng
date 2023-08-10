using System;
using System.Collections.Generic;

namespace JSONClass;

public class HuaShenData : IJSONClass
{
	public static Dictionary<int, HuaShenData> DataDict = new Dictionary<int, HuaShenData>();

	public static List<HuaShenData> DataList = new List<HuaShenData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Buff;

	public int Skill;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.HuaShenData.list)
		{
			try
			{
				HuaShenData huaShenData = new HuaShenData();
				huaShenData.id = item["id"].I;
				huaShenData.Buff = item["Buff"].I;
				huaShenData.Skill = item["Skill"].I;
				huaShenData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(huaShenData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典HuaShenData.DataDict添加数据时出现重复的键，Key:{huaShenData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(huaShenData.id, huaShenData);
				DataList.Add(huaShenData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典HuaShenData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
