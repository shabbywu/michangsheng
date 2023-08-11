using System;
using System.Collections.Generic;

namespace JSONClass;

public class MianWenYanSeRandomColorJsonData : IJSONClass
{
	public static Dictionary<int, MianWenYanSeRandomColorJsonData> DataDict = new Dictionary<int, MianWenYanSeRandomColorJsonData>();

	public static List<MianWenYanSeRandomColorJsonData> DataList = new List<MianWenYanSeRandomColorJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int R;

	public int G;

	public int B;

	public string beizhu;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.MianWenYanSeRandomColorJsonData.list)
		{
			try
			{
				MianWenYanSeRandomColorJsonData mianWenYanSeRandomColorJsonData = new MianWenYanSeRandomColorJsonData();
				mianWenYanSeRandomColorJsonData.id = item["id"].I;
				mianWenYanSeRandomColorJsonData.R = item["R"].I;
				mianWenYanSeRandomColorJsonData.G = item["G"].I;
				mianWenYanSeRandomColorJsonData.B = item["B"].I;
				mianWenYanSeRandomColorJsonData.beizhu = item["beizhu"].Str;
				if (DataDict.ContainsKey(mianWenYanSeRandomColorJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典MianWenYanSeRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{mianWenYanSeRandomColorJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(mianWenYanSeRandomColorJsonData.id, mianWenYanSeRandomColorJsonData);
				DataList.Add(mianWenYanSeRandomColorJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典MianWenYanSeRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
