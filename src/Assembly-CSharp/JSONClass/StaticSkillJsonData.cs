using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE4 RID: 3300
	public class StaticSkillJsonData : IJSONClass
	{
		// Token: 0x06004EF8 RID: 20216 RVA: 0x0021229C File Offset: 0x0021049C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillJsonData.list)
			{
				try
				{
					StaticSkillJsonData staticSkillJsonData = new StaticSkillJsonData();
					staticSkillJsonData.id = jsonobject["id"].I;
					staticSkillJsonData.Skill_ID = jsonobject["Skill_ID"].I;
					staticSkillJsonData.Skill_Lv = jsonobject["Skill_Lv"].I;
					staticSkillJsonData.qingjiaotype = jsonobject["qingjiaotype"].I;
					staticSkillJsonData.AttackType = jsonobject["AttackType"].I;
					staticSkillJsonData.icon = jsonobject["icon"].I;
					staticSkillJsonData.Skill_LV = jsonobject["Skill_LV"].I;
					staticSkillJsonData.typePinJie = jsonobject["typePinJie"].I;
					staticSkillJsonData.Skill_castTime = jsonobject["Skill_castTime"].I;
					staticSkillJsonData.Skill_Speed = jsonobject["Skill_Speed"].I;
					staticSkillJsonData.DF = jsonobject["DF"].I;
					staticSkillJsonData.TuJianType = jsonobject["TuJianType"].I;
					staticSkillJsonData.name = jsonobject["name"].Str;
					staticSkillJsonData.TuJiandescr = jsonobject["TuJiandescr"].Str;
					staticSkillJsonData.descr = jsonobject["descr"].Str;
					staticSkillJsonData.Affix = jsonobject["Affix"].ToList();
					staticSkillJsonData.seid = jsonobject["seid"].ToList();
					if (StaticSkillJsonData.DataDict.ContainsKey(staticSkillJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillJsonData.id));
					}
					else
					{
						StaticSkillJsonData.DataDict.Add(staticSkillJsonData.id, staticSkillJsonData);
						StaticSkillJsonData.DataList.Add(staticSkillJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillJsonData.OnInitFinishAction != null)
			{
				StaticSkillJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F6D RID: 20333
		public static Dictionary<int, StaticSkillJsonData> DataDict = new Dictionary<int, StaticSkillJsonData>();

		// Token: 0x04004F6E RID: 20334
		public static List<StaticSkillJsonData> DataList = new List<StaticSkillJsonData>();

		// Token: 0x04004F6F RID: 20335
		public static Action OnInitFinishAction = new Action(StaticSkillJsonData.OnInitFinish);

		// Token: 0x04004F70 RID: 20336
		public int id;

		// Token: 0x04004F71 RID: 20337
		public int Skill_ID;

		// Token: 0x04004F72 RID: 20338
		public int Skill_Lv;

		// Token: 0x04004F73 RID: 20339
		public int qingjiaotype;

		// Token: 0x04004F74 RID: 20340
		public int AttackType;

		// Token: 0x04004F75 RID: 20341
		public int icon;

		// Token: 0x04004F76 RID: 20342
		public int Skill_LV;

		// Token: 0x04004F77 RID: 20343
		public int typePinJie;

		// Token: 0x04004F78 RID: 20344
		public int Skill_castTime;

		// Token: 0x04004F79 RID: 20345
		public int Skill_Speed;

		// Token: 0x04004F7A RID: 20346
		public int DF;

		// Token: 0x04004F7B RID: 20347
		public int TuJianType;

		// Token: 0x04004F7C RID: 20348
		public string name;

		// Token: 0x04004F7D RID: 20349
		public string TuJiandescr;

		// Token: 0x04004F7E RID: 20350
		public string descr;

		// Token: 0x04004F7F RID: 20351
		public List<int> Affix = new List<int>();

		// Token: 0x04004F80 RID: 20352
		public List<int> seid = new List<int>();
	}
}
