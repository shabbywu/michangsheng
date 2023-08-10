using System;
using System.Collections.Generic;

namespace JSONClass;

public class EquipSeidJsonData1 : IJSONClass
{
	public static int SEIDID = 1;

	public static Dictionary<int, EquipSeidJsonData1> DataDict = new Dictionary<int, EquipSeidJsonData1>();

	public static List<EquipSeidJsonData1> DataList = new List<EquipSeidJsonData1>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int value1;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.EquipSeidJsonData[1].list)
		{
			try
			{
				EquipSeidJsonData1 equipSeidJsonData = new EquipSeidJsonData1();
				equipSeidJsonData.id = item["id"].I;
				equipSeidJsonData.value1 = item["value1"].I;
				if (DataDict.ContainsKey(equipSeidJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典EquipSeidJsonData1.DataDict添加数据时出现重复的键，Key:{equipSeidJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(equipSeidJsonData.id, equipSeidJsonData);
				DataList.Add(equipSeidJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典EquipSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
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
