using System;
using System.Collections.Generic;

namespace JSONClass;

public class CyNpcDuiBaiData : IJSONClass
{
	public static Dictionary<int, CyNpcDuiBaiData> DataDict = new Dictionary<int, CyNpcDuiBaiData>();

	public static List<CyNpcDuiBaiData> DataList = new List<CyNpcDuiBaiData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public int XingGe;

	public string dir1;

	public string dir2;

	public string dir3;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CyNpcDuiBaiData.list)
		{
			try
			{
				CyNpcDuiBaiData cyNpcDuiBaiData = new CyNpcDuiBaiData();
				cyNpcDuiBaiData.id = item["id"].I;
				cyNpcDuiBaiData.Type = item["Type"].I;
				cyNpcDuiBaiData.XingGe = item["XingGe"].I;
				cyNpcDuiBaiData.dir1 = item["dir1"].Str;
				cyNpcDuiBaiData.dir2 = item["dir2"].Str;
				cyNpcDuiBaiData.dir3 = item["dir3"].Str;
				if (DataDict.ContainsKey(cyNpcDuiBaiData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CyNpcDuiBaiData.DataDict添加数据时出现重复的键，Key:{cyNpcDuiBaiData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(cyNpcDuiBaiData.id, cyNpcDuiBaiData);
				DataList.Add(cyNpcDuiBaiData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CyNpcDuiBaiData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
