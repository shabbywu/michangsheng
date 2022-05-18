using System;
using System.Collections.Generic;

// Token: 0x02000398 RID: 920
public class UINPCQingJiaoSkillData
{
	// Token: 0x060019CC RID: 6604 RVA: 0x000E4D28 File Offset: 0x000E2F28
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
				if (i > 100000)
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

	// Token: 0x060019CD RID: 6605 RVA: 0x000E4F50 File Offset: 0x000E3150
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

	// Token: 0x04001515 RID: 5397
	public int LiuPai;

	// Token: 0x04001516 RID: 5398
	public int NPCLevel;

	// Token: 0x04001517 RID: 5399
	public static Dictionary<int, int> SkillItemDict = new Dictionary<int, int>();

	// Token: 0x04001518 RID: 5400
	public static Dictionary<int, int> GongFaItemDict = new Dictionary<int, int>();

	// Token: 0x04001519 RID: 5401
	public static List<UINPCQingJiaoSkillData.SData> SkillList = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x0400151A RID: 5402
	public static List<UINPCQingJiaoSkillData.SData> StaticSkillList = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x0400151B RID: 5403
	public List<UINPCQingJiaoSkillData.SData> Skills = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x0400151C RID: 5404
	public List<UINPCQingJiaoSkillData.SData> StaticSkills = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x0400151D RID: 5405
	public UINPCQingJiaoSkillData.SData YuanYingStaticSkill;

	// Token: 0x0400151E RID: 5406
	private static bool isinited;

	// Token: 0x02000399 RID: 921
	public class SData
	{
		// Token: 0x0400151F RID: 5407
		public int ID;

		// Token: 0x04001520 RID: 5408
		public int SkillID;

		// Token: 0x04001521 RID: 5409
		public int Quality;
	}
}
