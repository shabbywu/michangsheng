using System;
using System.Collections.Generic;

namespace JSONClass;

public class EquipSeidJsonData2 : IJSONClass
{
	public static int SEIDID = 2;

	public static Dictionary<int, EquipSeidJsonData2> DataDict = new Dictionary<int, EquipSeidJsonData2>();

	public static List<EquipSeidJsonData2> DataList = new List<EquipSeidJsonData2>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.EquipSeidJsonData[2].list)
		{
			try
			{
				EquipSeidJsonData2 equipSeidJsonData = new EquipSeidJsonData2();
				equipSeidJsonData.id = item["id"].I;
				equipSeidJsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(equipSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典EquipSeidJsonData2.DataDict添加数据时出现重复的键，Key:{equipSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(equipSeidJsonData.id, equipSeidJsonData);
				DataList.Add(equipSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典EquipSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
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
