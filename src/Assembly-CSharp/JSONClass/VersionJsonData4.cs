using System;
using System.Collections.Generic;

namespace JSONClass;

public class VersionJsonData4 : IJSONClass
{
	public static int SEIDID = 4;

	public static Dictionary<int, VersionJsonData4> DataDict = new Dictionary<int, VersionJsonData4>();

	public static List<VersionJsonData4> DataList = new List<VersionJsonData4>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int XueLiang;

	public int ShenShi;

	public int DunSu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.VersionJsonData[4].list)
		{
			try
			{
				VersionJsonData4 versionJsonData = new VersionJsonData4();
				versionJsonData.id = item["id"].I;
				versionJsonData.XueLiang = item["XueLiang"].I;
				versionJsonData.ShenShi = item["ShenShi"].I;
				versionJsonData.DunSu = item["DunSu"].I;
				if (DataDict.ContainsKey(versionJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典VersionJsonData4.DataDict添加数据时出现重复的键，Key:{versionJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(versionJsonData.id, versionJsonData);
				DataList.Add(versionJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典VersionJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
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
