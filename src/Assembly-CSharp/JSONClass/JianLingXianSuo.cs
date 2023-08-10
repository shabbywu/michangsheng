using System;
using System.Collections.Generic;

namespace JSONClass;

public class JianLingXianSuo : IJSONClass
{
	public static Dictionary<string, JianLingXianSuo> DataDict = new Dictionary<string, JianLingXianSuo>();

	public static List<JianLingXianSuo> DataList = new List<JianLingXianSuo>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int Type;

	public int JiYi;

	public int XianSuoLV;

	public string id;

	public string desc;

	public string XianSuoDuiHua1;

	public string XianSuoDuiHua2;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.JianLingXianSuo.list)
		{
			try
			{
				JianLingXianSuo jianLingXianSuo = new JianLingXianSuo();
				jianLingXianSuo.Type = item["Type"].I;
				jianLingXianSuo.JiYi = item["JiYi"].I;
				jianLingXianSuo.XianSuoLV = item["XianSuoLV"].I;
				jianLingXianSuo.id = item["id"].Str;
				jianLingXianSuo.desc = item["desc"].Str;
				jianLingXianSuo.XianSuoDuiHua1 = item["XianSuoDuiHua1"].Str;
				jianLingXianSuo.XianSuoDuiHua2 = item["XianSuoDuiHua2"].Str;
				if (DataDict.ContainsKey(jianLingXianSuo.id))
				{
					PreloadManager.LogException("!!!错误!!!向字典JianLingXianSuo.DataDict添加数据时出现重复的键，Key:" + jianLingXianSuo.id + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(jianLingXianSuo.id, jianLingXianSuo);
				DataList.Add(jianLingXianSuo);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典JianLingXianSuo.DataDict添加数据时出现异常，已跳过，请检查配表");
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
