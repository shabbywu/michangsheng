using System;
using System.Collections.Generic;

namespace JSONClass;

public class SuiJiTouXiangGeShuJsonData : IJSONClass
{
	public static Dictionary<string, SuiJiTouXiangGeShuJsonData> DataDict = new Dictionary<string, SuiJiTouXiangGeShuJsonData>();

	public static List<SuiJiTouXiangGeShuJsonData> DataList = new List<SuiJiTouXiangGeShuJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int All;

	public string StrID;

	public string colorset;

	public string Type;

	public string ChildType;

	public string ImageName;

	public List<int> AvatarSex1 = new List<int>();

	public List<int> SuiJiSex1 = new List<int>();

	public List<int> Sex1 = new List<int>();

	public List<int> AvatarSex2 = new List<int>();

	public List<int> SuiJiSex2 = new List<int>();

	public List<int> Sex2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SuiJiTouXiangGeShuJsonData.list)
		{
			try
			{
				SuiJiTouXiangGeShuJsonData suiJiTouXiangGeShuJsonData = new SuiJiTouXiangGeShuJsonData();
				suiJiTouXiangGeShuJsonData.All = item["All"].I;
				suiJiTouXiangGeShuJsonData.StrID = item["StrID"].Str;
				suiJiTouXiangGeShuJsonData.colorset = item["colorset"].Str;
				suiJiTouXiangGeShuJsonData.Type = item["Type"].Str;
				suiJiTouXiangGeShuJsonData.ChildType = item["ChildType"].Str;
				suiJiTouXiangGeShuJsonData.ImageName = item["ImageName"].Str;
				suiJiTouXiangGeShuJsonData.AvatarSex1 = item["AvatarSex1"].ToList();
				suiJiTouXiangGeShuJsonData.SuiJiSex1 = item["SuiJiSex1"].ToList();
				suiJiTouXiangGeShuJsonData.Sex1 = item["Sex1"].ToList();
				suiJiTouXiangGeShuJsonData.AvatarSex2 = item["AvatarSex2"].ToList();
				suiJiTouXiangGeShuJsonData.SuiJiSex2 = item["SuiJiSex2"].ToList();
				suiJiTouXiangGeShuJsonData.Sex2 = item["Sex2"].ToList();
				if (DataDict.ContainsKey(suiJiTouXiangGeShuJsonData.StrID))
				{
					PreloadManager.LogException("!!!错误!!!向字典SuiJiTouXiangGeShuJsonData.DataDict添加数据时出现重复的键，Key:" + suiJiTouXiangGeShuJsonData.StrID + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(suiJiTouXiangGeShuJsonData.StrID, suiJiTouXiangGeShuJsonData);
				DataList.Add(suiJiTouXiangGeShuJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典SuiJiTouXiangGeShuJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
