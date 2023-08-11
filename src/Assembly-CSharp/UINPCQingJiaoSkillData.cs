using System.Collections.Generic;

public class UINPCQingJiaoSkillData
{
	public class SData
	{
		public int ID;

		public int SkillID;

		public int Quality;
	}

	public int LiuPai;

	public int NPCLevel;

	public static Dictionary<int, int> SkillItemDict = new Dictionary<int, int>();

	public static Dictionary<int, int> GongFaItemDict = new Dictionary<int, int>();

	public static List<SData> SkillList = new List<SData>();

	public static List<SData> StaticSkillList = new List<SData>();

	public List<SData> Skills = new List<SData>();

	public List<SData> StaticSkills = new List<SData>();

	public SData YuanYingStaticSkill;

	private static bool isinited;

	private static void Init()
	{
		if (isinited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance._skillJsonData.list)
		{
			SData sData = new SData();
			sData.ID = item["id"].I;
			sData.SkillID = item["Skill_ID"].I;
			sData.Quality = item["Skill_LV"].I;
			SkillList.Add(sData);
		}
		foreach (JSONObject item2 in jsonData.instance.StaticSkillJsonData.list)
		{
			SData sData2 = new SData();
			sData2.ID = item2["id"].I;
			sData2.SkillID = item2["Skill_ID"].I;
			sData2.Quality = item2["Skill_LV"].I;
			StaticSkillList.Add(sData2);
		}
		foreach (JSONObject item3 in jsonData.instance._ItemJsonData.list)
		{
			int i = item3["id"].I;
			if (i > jsonData.QingJiaoItemIDSegment)
			{
				switch (item3["type"].I)
				{
				case 3:
				{
					int key2 = (int)float.Parse(item3["desc"].str);
					SkillItemDict.Add(key2, i);
					break;
				}
				case 4:
				{
					int key = (int)float.Parse(item3["desc"].str);
					GongFaItemDict.Add(key, i);
					break;
				}
				}
			}
		}
		isinited = true;
	}

	public UINPCQingJiaoSkillData(JSONObject json)
	{
		Init();
		LiuPai = json["LiuPai"].I;
		NPCLevel = json["Level"].I;
		foreach (JSONObject d in json["skills"].list)
		{
			SData item = SkillList.Find((SData s) => s.SkillID == d.I);
			Skills.Add(item);
		}
		foreach (JSONObject d2 in json["staticSkills"].list)
		{
			SData item2 = StaticSkillList.Find((SData s) => s.ID == d2.I);
			StaticSkills.Add(item2);
		}
		int yuanying = json["yuanying"].I;
		if (yuanying > 0)
		{
			YuanYingStaticSkill = StaticSkillList.Find((SData s) => s.ID == yuanying);
			StaticSkills.Add(YuanYingStaticSkill);
		}
	}
}
