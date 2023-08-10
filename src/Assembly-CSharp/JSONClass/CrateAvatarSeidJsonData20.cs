using System;
using System.Collections.Generic;

namespace JSONClass;

public class CrateAvatarSeidJsonData20 : IJSONClass
{
	public static int SEIDID = 20;

	public static Dictionary<int, CrateAvatarSeidJsonData20> DataDict = new Dictionary<int, CrateAvatarSeidJsonData20>();

	public static List<CrateAvatarSeidJsonData20> DataList = new List<CrateAvatarSeidJsonData20>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> value1 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CrateAvatarSeidJsonData[20].list)
		{
			try
			{
				CrateAvatarSeidJsonData20 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData20();
				crateAvatarSeidJsonData.id = item["id"].I;
				crateAvatarSeidJsonData.value1 = item["value1"].ToList();
				if (DataDict.ContainsKey(crateAvatarSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CrateAvatarSeidJsonData20.DataDict添加数据时出现重复的键，Key:{crateAvatarSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
				DataList.Add(crateAvatarSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData20.DataDict添加数据时出现异常，已跳过，请检查配表");
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
