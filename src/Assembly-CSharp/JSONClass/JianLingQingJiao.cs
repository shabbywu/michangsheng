using System;
using System.Collections.Generic;

namespace JSONClass;

public class JianLingQingJiao : IJSONClass
{
	public static Dictionary<string, JianLingQingJiao> DataDict = new Dictionary<string, JianLingQingJiao>();

	public static List<JianLingQingJiao> DataList = new List<JianLingQingJiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int JiYi;

	public int SkillID;

	public int StaticSkillID;

	public string id;

	public string QingJiaoDuiHuaQian;

	public string QingJiaoDuiHuaZhong;

	public string QingJiaoDuiHuaHou;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.JianLingQingJiao.list)
		{
			try
			{
				JianLingQingJiao jianLingQingJiao = new JianLingQingJiao();
				jianLingQingJiao.JiYi = item["JiYi"].I;
				jianLingQingJiao.SkillID = item["SkillID"].I;
				jianLingQingJiao.StaticSkillID = item["StaticSkillID"].I;
				jianLingQingJiao.id = item["id"].Str;
				jianLingQingJiao.QingJiaoDuiHuaQian = item["QingJiaoDuiHuaQian"].Str;
				jianLingQingJiao.QingJiaoDuiHuaZhong = item["QingJiaoDuiHuaZhong"].Str;
				jianLingQingJiao.QingJiaoDuiHuaHou = item["QingJiaoDuiHuaHou"].Str;
				if (DataDict.ContainsKey(jianLingQingJiao.id))
				{
					PreloadManager.LogException("!!!错误!!!向字典JianLingQingJiao.DataDict添加数据时出现重复的键，Key:" + jianLingQingJiao.id + "，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(jianLingQingJiao.id, jianLingQingJiao);
				DataList.Add(jianLingQingJiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典JianLingQingJiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
