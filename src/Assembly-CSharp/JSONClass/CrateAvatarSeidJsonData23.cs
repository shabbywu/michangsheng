using System;
using System.Collections.Generic;

namespace JSONClass;

public class CrateAvatarSeidJsonData23 : IJSONClass
{
	public static int SEIDID = 23;

	public static Dictionary<int, CrateAvatarSeidJsonData23> DataDict = new Dictionary<int, CrateAvatarSeidJsonData23>();

	public static List<CrateAvatarSeidJsonData23> DataList = new List<CrateAvatarSeidJsonData23>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> value1 = new List<int>();

	public List<int> value2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CrateAvatarSeidJsonData[23].list)
		{
			try
			{
				CrateAvatarSeidJsonData23 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData23();
				crateAvatarSeidJsonData.id = item["id"].I;
				crateAvatarSeidJsonData.value1 = item["value1"].ToList();
				crateAvatarSeidJsonData.value2 = item["value2"].ToList();
				if (DataDict.ContainsKey(crateAvatarSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CrateAvatarSeidJsonData23.DataDict添加数据时出现重复的键，Key:{crateAvatarSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
				DataList.Add(crateAvatarSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData23.DataDict添加数据时出现异常，已跳过，请检查配表");
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
