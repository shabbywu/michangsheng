using System;
using System.Collections.Generic;

namespace JSONClass;

public class SeaJiZhiID : IJSONClass
{
	public static Dictionary<int, SeaJiZhiID> DataDict = new Dictionary<int, SeaJiZhiID>();

	public static List<SeaJiZhiID> DataList = new List<SeaJiZhiID>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Type;

	public int TalkID;

	public int FuBenType;

	public int XingXiang;

	public List<int> AvatarID = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SeaJiZhiID.list)
		{
			try
			{
				SeaJiZhiID seaJiZhiID = new SeaJiZhiID();
				seaJiZhiID.id = item["id"].I;
				seaJiZhiID.Type = item["Type"].I;
				seaJiZhiID.TalkID = item["TalkID"].I;
				seaJiZhiID.FuBenType = item["FuBenType"].I;
				seaJiZhiID.XingXiang = item["XingXiang"].I;
				seaJiZhiID.AvatarID = item["AvatarID"].ToList();
				if (DataDict.ContainsKey(seaJiZhiID.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典SeaJiZhiID.DataDict添加数据时出现重复的键，Key:{seaJiZhiID.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(seaJiZhiID.id, seaJiZhiID);
				DataList.Add(seaJiZhiID);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SeaJiZhiID.DataDict添加数据时出现异常，已跳过，请检查配表");
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
