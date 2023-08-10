using System;
using System.Collections.Generic;

namespace JSONClass;

public class ElderTaskItemType : IJSONClass
{
	public static Dictionary<int, ElderTaskItemType> DataDict = new Dictionary<int, ElderTaskItemType>();

	public static List<ElderTaskItemType> DataList = new List<ElderTaskItemType>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int type;

	public int Xishu;

	public List<int> quality = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ElderTaskItemType.list)
		{
			try
			{
				ElderTaskItemType elderTaskItemType = new ElderTaskItemType();
				elderTaskItemType.type = item["type"].I;
				elderTaskItemType.Xishu = item["Xishu"].I;
				elderTaskItemType.quality = item["quality"].ToList();
				if (DataDict.ContainsKey(elderTaskItemType.type))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ElderTaskItemType.DataDict添加数据时出现重复的键，Key:{elderTaskItemType.type}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(elderTaskItemType.type, elderTaskItemType);
				DataList.Add(elderTaskItemType);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ElderTaskItemType.DataDict添加数据时出现异常，已跳过，请检查配表");
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
