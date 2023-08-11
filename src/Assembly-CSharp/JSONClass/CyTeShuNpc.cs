using System;
using System.Collections.Generic;

namespace JSONClass;

public class CyTeShuNpc : IJSONClass
{
	public static Dictionary<int, CyTeShuNpc> DataDict = new Dictionary<int, CyTeShuNpc>();

	public static List<CyTeShuNpc> DataList = new List<CyTeShuNpc>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public int PaiMaiID;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CyTeShuNpc.list)
		{
			try
			{
				CyTeShuNpc cyTeShuNpc = new CyTeShuNpc();
				cyTeShuNpc.id = item["id"].I;
				cyTeShuNpc.Type = item["Type"].I;
				cyTeShuNpc.PaiMaiID = item["PaiMaiID"].I;
				if (DataDict.ContainsKey(cyTeShuNpc.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CyTeShuNpc.DataDict添加数据时出现重复的键，Key:{cyTeShuNpc.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(cyTeShuNpc.id, cyTeShuNpc);
				DataList.Add(cyTeShuNpc);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CyTeShuNpc.DataDict添加数据时出现异常，已跳过，请检查配表");
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
