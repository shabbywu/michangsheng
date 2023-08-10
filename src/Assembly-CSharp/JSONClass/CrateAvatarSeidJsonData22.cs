using System;
using System.Collections.Generic;

namespace JSONClass;

public class CrateAvatarSeidJsonData22 : IJSONClass
{
	public static int SEIDID = 22;

	public static Dictionary<int, CrateAvatarSeidJsonData22> DataDict = new Dictionary<int, CrateAvatarSeidJsonData22>();

	public static List<CrateAvatarSeidJsonData22> DataList = new List<CrateAvatarSeidJsonData22>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> value1 = new List<int>();

	public List<int> value2 = new List<int>();

	public List<int> value3 = new List<int>();

	public List<int> value4 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CrateAvatarSeidJsonData[22].list)
		{
			try
			{
				CrateAvatarSeidJsonData22 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData22();
				crateAvatarSeidJsonData.id = item["id"].I;
				crateAvatarSeidJsonData.value1 = item["value1"].ToList();
				crateAvatarSeidJsonData.value2 = item["value2"].ToList();
				crateAvatarSeidJsonData.value3 = item["value3"].ToList();
				crateAvatarSeidJsonData.value4 = item["value4"].ToList();
				if (DataDict.ContainsKey(crateAvatarSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CrateAvatarSeidJsonData22.DataDict添加数据时出现重复的键，Key:{crateAvatarSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
				DataList.Add(crateAvatarSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData22.DataDict添加数据时出现异常，已跳过，请检查配表");
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
