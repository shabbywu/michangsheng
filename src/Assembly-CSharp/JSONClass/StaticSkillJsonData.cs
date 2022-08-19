using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000960 RID: 2400
	public class StaticSkillJsonData : IJSONClass
	{
		// Token: 0x06004392 RID: 17298 RVA: 0x001CC594 File Offset: 0x001CA794
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

		// Token: 0x06004393 RID: 17299 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400445D RID: 17501
		public static Dictionary<int, StaticSkillJsonData> DataDict = new Dictionary<int, StaticSkillJsonData>();

		// Token: 0x0400445E RID: 17502
		public static List<StaticSkillJsonData> DataList = new List<StaticSkillJsonData>();

		// Token: 0x0400445F RID: 17503
		public static Action OnInitFinishAction = new Action(StaticSkillJsonData.OnInitFinish);

		// Token: 0x04004460 RID: 17504
		public int id;

		// Token: 0x04004461 RID: 17505
		public int Skill_ID;

		// Token: 0x04004462 RID: 17506
		public int Skill_Lv;

		// Token: 0x04004463 RID: 17507
		public int qingjiaotype;

		// Token: 0x04004464 RID: 17508
		public int AttackType;

		// Token: 0x04004465 RID: 17509
		public int icon;

		// Token: 0x04004466 RID: 17510
		public int Skill_LV;

		// Token: 0x04004467 RID: 17511
		public int typePinJie;

		// Token: 0x04004468 RID: 17512
		public int Skill_castTime;

		// Token: 0x04004469 RID: 17513
		public int Skill_Speed;

		// Token: 0x0400446A RID: 17514
		public int DF;

		// Token: 0x0400446B RID: 17515
		public int TuJianType;

		// Token: 0x0400446C RID: 17516
		public string name;

		// Token: 0x0400446D RID: 17517
		public string TuJiandescr;

		// Token: 0x0400446E RID: 17518
		public string descr;

		// Token: 0x0400446F RID: 17519
		public List<int> Affix = new List<int>();

		// Token: 0x04004470 RID: 17520
		public List<int> seid = new List<int>();
	}
}
