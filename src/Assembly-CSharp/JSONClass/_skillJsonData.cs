using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000747 RID: 1863
	public class _skillJsonData : IJSONClass
	{
		// Token: 0x06003B30 RID: 15152 RVA: 0x001971D4 File Offset: 0x001953D4
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

		// Token: 0x06003B31 RID: 15153 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003399 RID: 13209
		public static Dictionary<int, _skillJsonData> DataDict = new Dictionary<int, _skillJsonData>();

		// Token: 0x0400339A RID: 13210
		public static List<_skillJsonData> DataList = new List<_skillJsonData>();

		// Token: 0x0400339B RID: 13211
		public static Action OnInitFinishAction = new Action(_skillJsonData.OnInitFinish);

		// Token: 0x0400339C RID: 13212
		public int id;

		// Token: 0x0400339D RID: 13213
		public int Skill_ID;

		// Token: 0x0400339E RID: 13214
		public int Skill_Lv;

		// Token: 0x0400339F RID: 13215
		public int Skill_Type;

		// Token: 0x040033A0 RID: 13216
		public int qingjiaotype;

		// Token: 0x040033A1 RID: 13217
		public int HP;

		// Token: 0x040033A2 RID: 13218
		public int speed;

		// Token: 0x040033A3 RID: 13219
		public int icon;

		// Token: 0x040033A4 RID: 13220
		public int Skill_DisplayType;

		// Token: 0x040033A5 RID: 13221
		public int Skill_LV;

		// Token: 0x040033A6 RID: 13222
		public int typePinJie;

		// Token: 0x040033A7 RID: 13223
		public int DF;

		// Token: 0x040033A8 RID: 13224
		public int TuJianType;

		// Token: 0x040033A9 RID: 13225
		public int Skill_Open;

		// Token: 0x040033AA RID: 13226
		public int Skill_castTime;

		// Token: 0x040033AB RID: 13227
		public int canUseDistMax;

		// Token: 0x040033AC RID: 13228
		public int CD;

		// Token: 0x040033AD RID: 13229
		public string skillEffect;

		// Token: 0x040033AE RID: 13230
		public string name;

		// Token: 0x040033AF RID: 13231
		public string descr;

		// Token: 0x040033B0 RID: 13232
		public string TuJiandescr;

		// Token: 0x040033B1 RID: 13233
		public string script;

		// Token: 0x040033B2 RID: 13234
		public List<int> seid = new List<int>();

		// Token: 0x040033B3 RID: 13235
		public List<int> Affix = new List<int>();

		// Token: 0x040033B4 RID: 13236
		public List<int> Affix2 = new List<int>();

		// Token: 0x040033B5 RID: 13237
		public List<int> AttackType = new List<int>();

		// Token: 0x040033B6 RID: 13238
		public List<int> skill_SameCastNum = new List<int>();

		// Token: 0x040033B7 RID: 13239
		public List<int> skill_CastType = new List<int>();

		// Token: 0x040033B8 RID: 13240
		public List<int> skill_Cast = new List<int>();
	}
}
