using System;
using System.Collections.Generic;

namespace JSONClass;

public class StaticSkillJsonData : IJSONClass
{
	public static Dictionary<int, StaticSkillJsonData> DataDict = new Dictionary<int, StaticSkillJsonData>();

	public static List<StaticSkillJsonData> DataList = new List<StaticSkillJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Skill_ID;

	public int Skill_Lv;

	public int qingjiaotype;

	public int AttackType;

	public int icon;

	public int Skill_LV;

	public int typePinJie;

	public int Skill_castTime;

	public int Skill_Speed;

	public int DF;

	public int TuJianType;

	public string name;

	public string TuJiandescr;

	public string descr;

	public List<int> Affix = new List<int>();

	public List<int> seid = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.StaticSkillJsonData.list)
		{
			try
			{
				StaticSkillJsonData staticSkillJsonData = new StaticSkillJsonData();
				staticSkillJsonData.id = item["id"].I;
				staticSkillJsonData.Skill_ID = item["Skill_ID"].I;
				staticSkillJsonData.Skill_Lv = item["Skill_Lv"].I;
				staticSkillJsonData.qingjiaotype = item["qingjiaotype"].I;
				staticSkillJsonData.AttackType = item["AttackType"].I;
				staticSkillJsonData.icon = item["icon"].I;
				staticSkillJsonData.Skill_LV = item["Skill_LV"].I;
				staticSkillJsonData.typePinJie = item["typePinJie"].I;
				staticSkillJsonData.Skill_castTime = item["Skill_castTime"].I;
				staticSkillJsonData.Skill_Speed = item["Skill_Speed"].I;
				staticSkillJsonData.DF = item["DF"].I;
				staticSkillJsonData.TuJianType = item["TuJianType"].I;
				staticSkillJsonData.name = item["name"].Str;
				staticSkillJsonData.TuJiandescr = item["TuJiandescr"].Str;
				staticSkillJsonData.descr = item["descr"].Str;
				staticSkillJsonData.Affix = item["Affix"].ToList();
				staticSkillJsonData.seid = item["seid"].ToList();
				if (DataDict.ContainsKey(staticSkillJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典StaticSkillJsonData.DataDict添加数据时出现重复的键，Key:{staticSkillJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(staticSkillJsonData.id, staticSkillJsonData);
				DataList.Add(staticSkillJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典StaticSkillJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
