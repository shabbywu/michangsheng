using System;
using System.Collections.Generic;

namespace JSONClass;

public class CrateAvatarSeidJsonData11 : IJSONClass
{
	public static int SEIDID = 11;

	public static Dictionary<int, CrateAvatarSeidJsonData11> DataDict = new Dictionary<int, CrateAvatarSeidJsonData11>();

	public static List<CrateAvatarSeidJsonData11> DataList = new List<CrateAvatarSeidJsonData11>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public int value2;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CrateAvatarSeidJsonData[11].list)
		{
			try
			{
				CrateAvatarSeidJsonData11 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData11();
				crateAvatarSeidJsonData.id = item["id"].I;
				crateAvatarSeidJsonData.value1 = item["value1"].I;
				crateAvatarSeidJsonData.value2 = item["value2"].I;
				if (DataDict.ContainsKey(crateAvatarSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CrateAvatarSeidJsonData11.DataDict添加数据时出现重复的键，Key:{crateAvatarSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
				DataList.Add(crateAvatarSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
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
