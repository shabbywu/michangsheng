using System;
using System.Collections.Generic;

// Token: 0x02000277 RID: 631
public class UINPCQingJiaoSkillData
{
	// Token: 0x06001707 RID: 5895 RVA: 0x0009D378 File Offset: 0x0009B578
	private static void Init()
	{
		if (!UINPCQingJiaoSkillData.isinited)
		{
			foreach (JSONObject jsonobject in jsonData.instance._skillJsonData.list)
			{
				UINPCQingJiaoSkillData.SData sdata = new UINPCQingJiaoSkillData.SData();
				sdata.ID = jsonobject["id"].I;
				sdata.SkillID = jsonobject["Skill_ID"].I;
				sdata.Quality = jsonobject["Skill_LV"].I;
				UINPCQingJiaoSkillData.SkillList.Add(sdata);
			}
			foreach (JSONObject jsonobject2 in jsonData.instance.StaticSkillJsonData.list)
			{
				UINPCQingJiaoSkillData.SData sdata2 = new UINPCQingJiaoSkillData.SData();
				sdata2.ID = jsonobject2["id"].I;
				sdata2.SkillID = jsonobject2["Skill_ID"].I;
				sdata2.Quality = jsonobject2["Skill_LV"].I;
				UINPCQingJiaoSkillData.StaticSkillList.Add(sdata2);
			}
			foreach (JSONObject jsonobject3 in jsonData.instance._ItemJsonData.list)
			{
				int i = jsonobject3["id"].I;
				if (i > jsonData.QingJiaoItemIDSegment)
				{
					int i2 = jsonobject3["type"].I;
					if (i2 == 3)
					{
						int key = (int)float.Parse(jsonobject3["desc"].str);
						UINPCQingJiaoSkillData.SkillItemDict.Add(key, i);
					}
					else if (i2 == 4)
					{
						int key2 = (int)float.Parse(jsonobject3["desc"].str);
						UINPCQingJiaoSkillData.GongFaItemDict.Add(key2, i);
					}
				}
			}
			UINPCQingJiaoSkillData.isinited = true;
		}
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x0009D5A0 File Offset: 0x0009B7A0
	public UINPCQingJiaoSkillData(JSONObject json)
	{
		UINPCQingJiaoSkillData.Init();
		this.LiuPai = json["LiuPai"].I;
		this.NPCLevel = json["Level"].I;
		using (List<JSONObject>.Enumerator enumerator = json["skills"].list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JSONObject d = enumerator.Current;
				UINPCQingJiaoSkillData.SData item = UINPCQingJiaoSkillData.SkillList.Find((UINPCQingJiaoSkillData.SData s) => s.SkillID == d.I);
				this.Skills.Add(item);
			}
		}
		using (List<JSONObject>.Enumerator enumerator = json["staticSkills"].list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JSONObject d = enumerator.Current;
				UINPCQingJiaoSkillData.SData item2 = UINPCQingJiaoSkillData.StaticSkillList.Find((UINPCQingJiaoSkillData.SData s) => s.ID == d.I);
				this.StaticSkills.Add(item2);
			}
		}
		int yuanying = json["yuanying"].I;
		if (yuanying > 0)
		{
			this.YuanYingStaticSkill = UINPCQingJiaoSkillData.StaticSkillList.Find((UINPCQingJiaoSkillData.SData s) => s.ID == yuanying);
			this.StaticSkills.Add(this.YuanYingStaticSkill);
		}
	}

	// Token: 0x040011AB RID: 4523
	public int LiuPai;

	// Token: 0x040011AC RID: 4524
	public int NPCLevel;

	// Token: 0x040011AD RID: 4525
	public static Dictionary<int, int> SkillItemDict = new Dictionary<int, int>();

	// Token: 0x040011AE RID: 4526
	public static Dictionary<int, int> GongFaItemDict = new Dictionary<int, int>();

	// Token: 0x040011AF RID: 4527
	public static List<UINPCQingJiaoSkillData.SData> SkillList = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x040011B0 RID: 4528
	public static List<UINPCQingJiaoSkillData.SData> StaticSkillList = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x040011B1 RID: 4529
	public List<UINPCQingJiaoSkillData.SData> Skills = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x040011B2 RID: 4530
	public List<UINPCQingJiaoSkillData.SData> StaticSkills = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x040011B3 RID: 4531
	public UINPCQingJiaoSkillData.SData YuanYingStaticSkill;

	// Token: 0x040011B4 RID: 4532
	private static bool isinited;

	// Token: 0x020012F2 RID: 4850
	public class SData
	{
		// Token: 0x0400671B RID: 26395
		public int ID;

		// Token: 0x0400671C RID: 26396
		public int SkillID;

		// Token: 0x0400671D RID: 26397
		public int Quality;
	}
}
