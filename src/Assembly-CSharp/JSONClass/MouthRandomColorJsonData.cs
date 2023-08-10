using System;
using System.Collections.Generic;

namespace JSONClass;

public class MouthRandomColorJsonData : IJSONClass
{
	public static Dictionary<int, MouthRandomColorJsonData> DataDict = new Dictionary<int, MouthRandomColorJsonData>();

	public static List<MouthRandomColorJsonData> DataList = new List<MouthRandomColorJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int R;

	public int G;

	public int B;

	public string beizhu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.MouthRandomColorJsonData.list)
		{
			try
			{
				MouthRandomColorJsonData mouthRandomColorJsonData = new MouthRandomColorJsonData();
				mouthRandomColorJsonData.id = item["id"].I;
				mouthRandomColorJsonData.R = item["R"].I;
				mouthRandomColorJsonData.G = item["G"].I;
				mouthRandomColorJsonData.B = item["B"].I;
				mouthRandomColorJsonData.beizhu = item["beizhu"].Str;
				if (DataDict.ContainsKey(mouthRandomColorJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典MouthRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{mouthRandomColorJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(mouthRandomColorJsonData.id, mouthRandomColorJsonData);
				DataList.Add(mouthRandomColorJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典MouthRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
