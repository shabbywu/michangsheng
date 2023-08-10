using System;
using System.Collections.Generic;

namespace JSONClass;

public class _skillJsonData : IJSONClass
{
	public static Dictionary<int, _skillJsonData> DataDict = new Dictionary<int, _skillJsonData>();

	public static List<_skillJsonData> DataList = new List<_skillJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int Skill_ID;

	public int Skill_Lv;

	public int Skill_Type;

	public int qingjiaotype;

	public int HP;

	public int speed;

	public int icon;

	public int Skill_DisplayType;

	public int Skill_LV;

	public int typePinJie;

	public int DF;

	public int TuJianType;

	public int Skill_Open;

	public int Skill_castTime;

	public int canUseDistMax;

	public int CD;

	public string skillEffect;

	public string name;

	public string descr;

	public string TuJiandescr;

	public string script;

	public List<int> seid = new List<int>();

	public List<int> Affix = new List<int>();

	public List<int> Affix2 = new List<int>();

	public List<int> AttackType = new List<int>();

	public List<int> skill_SameCastNum = new List<int>();

	public List<int> skill_CastType = new List<int>();

	public List<int> skill_Cast = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance._skillJsonData.list)
		{
			try
			{
				_skillJsonData skillJsonData = new _skillJsonData();
				skillJsonData.id = item["id"].I;
				skillJsonData.Skill_ID = item["Skill_ID"].I;
				skillJsonData.Skill_Lv = item["Skill_Lv"].I;
				skillJsonData.Skill_Type = item["Skill_Type"].I;
				skillJsonData.qingjiaotype = item["qingjiaotype"].I;
				skillJsonData.HP = item["HP"].I;
				skillJsonData.speed = item["speed"].I;
				skillJsonData.icon = item["icon"].I;
				skillJsonData.Skill_DisplayType = item["Skill_DisplayType"].I;
				skillJsonData.Skill_LV = item["Skill_LV"].I;
				skillJsonData.typePinJie = item["typePinJie"].I;
				skillJsonData.DF = item["DF"].I;
				skillJsonData.TuJianType = item["TuJianType"].I;
				skillJsonData.Skill_Open = item["Skill_Open"].I;
				skillJsonData.Skill_castTime = item["Skill_castTime"].I;
				skillJsonData.canUseDistMax = item["canUseDistMax"].I;
				skillJsonData.CD = item["CD"].I;
				skillJsonData.skillEffect = item["skillEffect"].Str;
				skillJsonData.name = item["name"].Str;
				skillJsonData.descr = item["descr"].Str;
				skillJsonData.TuJiandescr = item["TuJiandescr"].Str;
				skillJsonData.script = item["script"].Str;
				skillJsonData.seid = item["seid"].ToList();
				skillJsonData.Affix = item["Affix"].ToList();
				skillJsonData.Affix2 = item["Affix2"].ToList();
				skillJsonData.AttackType = item["AttackType"].ToList();
				skillJsonData.skill_SameCastNum = item["skill_SameCastNum"].ToList();
				skillJsonData.skill_CastType = item["skill_CastType"].ToList();
				skillJsonData.skill_Cast = item["skill_Cast"].ToList();
				if (DataDict.ContainsKey(skillJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典_skillJsonData.DataDict添加数据时出现重复的键，Key:{skillJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(skillJsonData.id, skillJsonData);
				DataList.Add(skillJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典_skillJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
