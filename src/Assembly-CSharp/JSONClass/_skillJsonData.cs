using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000ADF RID: 2783
	public class _skillJsonData : IJSONClass
	{
		// Token: 0x060046E6 RID: 18150 RVA: 0x001E54B0 File Offset: 0x001E36B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._skillJsonData.list)
			{
				try
				{
					_skillJsonData skillJsonData = new _skillJsonData();
					skillJsonData.id = jsonobject["id"].I;
					skillJsonData.Skill_ID = jsonobject["Skill_ID"].I;
					skillJsonData.Skill_Lv = jsonobject["Skill_Lv"].I;
					skillJsonData.Skill_Type = jsonobject["Skill_Type"].I;
					skillJsonData.qingjiaotype = jsonobject["qingjiaotype"].I;
					skillJsonData.HP = jsonobject["HP"].I;
					skillJsonData.speed = jsonobject["speed"].I;
					skillJsonData.icon = jsonobject["icon"].I;
					skillJsonData.Skill_DisplayType = jsonobject["Skill_DisplayType"].I;
					skillJsonData.Skill_LV = jsonobject["Skill_LV"].I;
					skillJsonData.typePinJie = jsonobject["typePinJie"].I;
					skillJsonData.DF = jsonobject["DF"].I;
					skillJsonData.TuJianType = jsonobject["TuJianType"].I;
					skillJsonData.Skill_Open = jsonobject["Skill_Open"].I;
					skillJsonData.Skill_castTime = jsonobject["Skill_castTime"].I;
					skillJsonData.canUseDistMax = jsonobject["canUseDistMax"].I;
					skillJsonData.CD = jsonobject["CD"].I;
					skillJsonData.skillEffect = jsonobject["skillEffect"].Str;
					skillJsonData.name = jsonobject["name"].Str;
					skillJsonData.descr = jsonobject["descr"].Str;
					skillJsonData.TuJiandescr = jsonobject["TuJiandescr"].Str;
					skillJsonData.script = jsonobject["script"].Str;
					skillJsonData.seid = jsonobject["seid"].ToList();
					skillJsonData.Affix = jsonobject["Affix"].ToList();
					skillJsonData.Affix2 = jsonobject["Affix2"].ToList();
					skillJsonData.AttackType = jsonobject["AttackType"].ToList();
					skillJsonData.skill_SameCastNum = jsonobject["skill_SameCastNum"].ToList();
					skillJsonData.skill_CastType = jsonobject["skill_CastType"].ToList();
					skillJsonData.skill_Cast = jsonobject["skill_Cast"].ToList();
					if (_skillJsonData.DataDict.ContainsKey(skillJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_skillJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillJsonData.id));
					}
					else
					{
						_skillJsonData.DataDict.Add(skillJsonData.id, skillJsonData);
						_skillJsonData.DataList.Add(skillJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_skillJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_skillJsonData.OnInitFinishAction != null)
			{
				_skillJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F32 RID: 16178
		public static Dictionary<int, _skillJsonData> DataDict = new Dictionary<int, _skillJsonData>();

		// Token: 0x04003F33 RID: 16179
		public static List<_skillJsonData> DataList = new List<_skillJsonData>();

		// Token: 0x04003F34 RID: 16180
		public static Action OnInitFinishAction = new Action(_skillJsonData.OnInitFinish);

		// Token: 0x04003F35 RID: 16181
		public int id;

		// Token: 0x04003F36 RID: 16182
		public int Skill_ID;

		// Token: 0x04003F37 RID: 16183
		public int Skill_Lv;

		// Token: 0x04003F38 RID: 16184
		public int Skill_Type;

		// Token: 0x04003F39 RID: 16185
		public int qingjiaotype;

		// Token: 0x04003F3A RID: 16186
		public int HP;

		// Token: 0x04003F3B RID: 16187
		public int speed;

		// Token: 0x04003F3C RID: 16188
		public int icon;

		// Token: 0x04003F3D RID: 16189
		public int Skill_DisplayType;

		// Token: 0x04003F3E RID: 16190
		public int Skill_LV;

		// Token: 0x04003F3F RID: 16191
		public int typePinJie;

		// Token: 0x04003F40 RID: 16192
		public int DF;

		// Token: 0x04003F41 RID: 16193
		public int TuJianType;

		// Token: 0x04003F42 RID: 16194
		public int Skill_Open;

		// Token: 0x04003F43 RID: 16195
		public int Skill_castTime;

		// Token: 0x04003F44 RID: 16196
		public int canUseDistMax;

		// Token: 0x04003F45 RID: 16197
		public int CD;

		// Token: 0x04003F46 RID: 16198
		public string skillEffect;

		// Token: 0x04003F47 RID: 16199
		public string name;

		// Token: 0x04003F48 RID: 16200
		public string descr;

		// Token: 0x04003F49 RID: 16201
		public string TuJiandescr;

		// Token: 0x04003F4A RID: 16202
		public string script;

		// Token: 0x04003F4B RID: 16203
		public List<int> seid = new List<int>();

		// Token: 0x04003F4C RID: 16204
		public List<int> Affix = new List<int>();

		// Token: 0x04003F4D RID: 16205
		public List<int> Affix2 = new List<int>();

		// Token: 0x04003F4E RID: 16206
		public List<int> AttackType = new List<int>();

		// Token: 0x04003F4F RID: 16207
		public List<int> skill_SameCastNum = new List<int>();

		// Token: 0x04003F50 RID: 16208
		public List<int> skill_CastType = new List<int>();

		// Token: 0x04003F51 RID: 16209
		public List<int> skill_Cast = new List<int>();
	}
}
