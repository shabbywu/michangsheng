using System;
using System.Collections.Generic;

namespace JSONClass;

public class TianJieMiShuData : IJSONClass
{
	public static Dictionary<string, TianJieMiShuData> DataDict = new Dictionary<string, TianJieMiShuData>();

	public static List<TianJieMiShuData> DataList = new List<TianJieMiShuData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int Skill_ID;

	public int Type;

	public int RoundLimit;

	public int StaticValueID;

	public int StuTime;

	public int GongBi;

	public int DiYiXiang;

	public int XiuZhengZhi;

	public string id;

	public string StartFightAction;

	public string PanDing;

	public string desc;

	public string ShuoMing;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TianJieMiShuData.list)
		{
			try
			{
				TianJieMiShuData tianJieMiShuData = new TianJieMiShuData();
				tianJieMiShuData.Skill_ID = item["Skill_ID"].I;
				tianJieMiShuData.Type = item["Type"].I;
				tianJieMiShuData.RoundLimit = item["RoundLimit"].I;
				tianJieMiShuData.StaticValueID = item["StaticValueID"].I;
				tianJieMiShuData.StuTime = item["StuTime"].I;
				tianJieMiShuData.GongBi = item["GongBi"].I;
				tianJieMiShuData.DiYiXiang = item["DiYiXiang"].I;
				tianJieMiShuData.XiuZhengZhi = item["XiuZhengZhi"].I;
				tianJieMiShuData.id = item["id"].Str;
				tianJieMiShuData.StartFightAction = item["StartFightAction"].Str;
				tianJieMiShuData.PanDing = item["PanDing"].Str;
				tianJieMiShuData.desc = item["desc"].Str;
				tianJieMiShuData.ShuoMing = item["ShuoMing"].Str;
				if (DataDict.ContainsKey(tianJieMiShuData.id))
				{
					PreloadManager.LogException("!!!错误!!!向字典TianJieMiShuData.DataDict添加数据时出现重复的键，Key:" + tianJieMiShuData.id + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(tianJieMiShuData.id, tianJieMiShuData);
				DataList.Add(tianJieMiShuData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TianJieMiShuData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
