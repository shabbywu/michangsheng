using System;
using System.Collections.Generic;

namespace JSONClass;

public class CyRandomTaskFailData : IJSONClass
{
	public static Dictionary<int, CyRandomTaskFailData> DataDict = new Dictionary<int, CyRandomTaskFailData>();

	public static List<CyRandomTaskFailData> DataList = new List<CyRandomTaskFailData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ShiBaiInfo2;

	public int ShiBaiInfo3;

	public int ShiBaiInfo4;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CyRandomTaskFailData.list)
		{
			try
			{
				CyRandomTaskFailData cyRandomTaskFailData = new CyRandomTaskFailData();
				cyRandomTaskFailData.id = item["id"].I;
				cyRandomTaskFailData.ShiBaiInfo2 = item["ShiBaiInfo2"].I;
				cyRandomTaskFailData.ShiBaiInfo3 = item["ShiBaiInfo3"].I;
				cyRandomTaskFailData.ShiBaiInfo4 = item["ShiBaiInfo4"].I;
				if (DataDict.ContainsKey(cyRandomTaskFailData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CyRandomTaskFailData.DataDict添加数据时出现重复的键，Key:{cyRandomTaskFailData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(cyRandomTaskFailData.id, cyRandomTaskFailData);
				DataList.Add(cyRandomTaskFailData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CyRandomTaskFailData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
