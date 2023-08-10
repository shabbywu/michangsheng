using System;
using System.Collections.Generic;

namespace JSONClass;

public class LingGuangJson : IJSONClass
{
	public static Dictionary<int, LingGuangJson> DataDict = new Dictionary<int, LingGuangJson>();

	public static List<LingGuangJson> DataList = new List<LingGuangJson>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int type;

	public int studyTime;

	public int guoqiTime;

	public int quality;

	public string name;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LingGuangJson.list)
		{
			try
			{
				LingGuangJson lingGuangJson = new LingGuangJson();
				lingGuangJson.id = item["id"].I;
				lingGuangJson.type = item["type"].I;
				lingGuangJson.studyTime = item["studyTime"].I;
				lingGuangJson.guoqiTime = item["guoqiTime"].I;
				lingGuangJson.quality = item["quality"].I;
				lingGuangJson.name = item["name"].Str;
				lingGuangJson.desc = item["desc"].Str;
				if (DataDict.ContainsKey(lingGuangJson.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LingGuangJson.DataDict添加数据时出现重复的键，Key:{lingGuangJson.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lingGuangJson.id, lingGuangJson);
				DataList.Add(lingGuangJson);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LingGuangJson.DataDict添加数据时出现异常，已跳过，请检查配表");
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
