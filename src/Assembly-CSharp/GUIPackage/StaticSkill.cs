using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D69 RID: 3433
	[Serializable]
	public class StaticSkill
	{
		// Token: 0x06005272 RID: 21106 RVA: 0x00226AAC File Offset: 0x00224CAC
		public StaticSkill(int id, int level, int max)
		{
			JSONObject jsonobject = jsonData.instance.StaticSkillJsonData[string.Concat(id)];
			if (jsonobject == null)
			{
				return;
			}
			string text = Regex.Unescape(jsonobject["name"].str);
			this.skill_Name = text;
			this.skill_ID = id;
			string text2 = Regex.Unescape(jsonobject["descr"].str);
			this.skill_Desc = text2;
			Texture2D texture2D = ResManager.inst.LoadTexture2D("StaticSkill Icon/" + Tools.instance.getStaticSkillIDByKey(this.skill_ID));
			if (texture2D)
			{
				this.skill_Icon = texture2D;
			}
			else
			{
				this.skill_Icon = ResManager.inst.LoadTexture2D("StaticSkill Icon/0");
			}
			Sprite sprite = ResManager.inst.LoadSprite("StaticSkill Icon/" + Tools.instance.getStaticSkillIDByKey(this.skill_ID));
			if (sprite)
			{
				this.skillIconSprite = sprite;
			}
			else
			{
				this.skillIconSprite = ResManager.inst.LoadSprite("StaticSkill Icon/0");
			}
			this.skill_level = level;
			this.Max_level = max;
			this.CoolDown = 10000f;
			this.CurCD = 0f;
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0003B16A File Offset: 0x0003936A
		public StaticSkill()
		{
			this.skill_ID = -1;
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x0003AC15 File Offset: 0x00038E15
		public Skill Clone()
		{
			return base.MemberwiseClone() as Skill;
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x0003B18F File Offset: 0x0003938F
		public virtual JSONObject getJsonData()
		{
			return jsonData.instance.StaticSkillJsonData;
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x00226BFC File Offset: 0x00224DFC
		public void PutingSkill(Entity _attaker, Entity _receiver, int type = 0)
		{
			List<int> flag = new List<int>();
			foreach (JSONObject jsonobject in this.getJsonData()[string.Concat(this.skill_ID)]["seid"].list)
			{
				this.realizeSeid((int)jsonobject.n, flag, _attaker, _receiver, type);
			}
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00226C84 File Offset: 0x00224E84
		public void putingStudySkill(Entity _attaker, Entity _receiver, int type = 0)
		{
			List<int> flag = new List<int>();
			foreach (JSONObject jsonobject in this.getJsonData()[string.Concat(this.skill_ID)]["seid"].list)
			{
				this.StudySkillSeid((int)jsonobject.n, flag, _attaker, _receiver, type);
			}
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x00226D0C File Offset: 0x00224F0C
		public void StudySkillSeid(int seid, List<int> flag, Entity _attaker, Entity _receiver, int type)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			float n = jsonData.instance.StaticSkillTypeJsonData[seid.ToString()]["value1"].n;
			MethodInfo method = base.GetType().GetMethod(this.getStudyMethodName() + seid);
			if (method != null)
			{
				method.Invoke(this, new object[]
				{
					seid,
					flag,
					avatar,
					avatar2,
					type
				});
			}
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0003B19B File Offset: 0x0003939B
		public void Puting(Entity _attaker, Entity _receiver, int type = 0)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			this.PutingSkill(_attaker, _receiver, type);
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00226DA4 File Offset: 0x00224FA4
		public static void resetSeid(Avatar attaker)
		{
			attaker.StaticSkillSeidFlag.Clear();
			foreach (SkillItem skillItem in attaker.equipStaticSkillList)
			{
				new StaticSkill(Tools.instance.getStaticSkillKeyByID(skillItem.itemId), 0, 5).Puting(attaker, attaker, 2);
			}
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x0003B1B4 File Offset: 0x000393B4
		public virtual string getMethodName()
		{
			return "realizeSeid";
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x0003B1BB File Offset: 0x000393BB
		public virtual string getStudyMethodName()
		{
			return "StudtRealizeSeid";
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x0003B1C2 File Offset: 0x000393C2
		public virtual Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
		{
			return attaker.StaticSkillSeidFlag;
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x00226E1C File Offset: 0x0022501C
		public virtual void realizeSeid(int seid, List<int> flag, Entity _attaker, Entity _receiver, int type)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			if ((int)jsonData.instance.StaticSkillTypeJsonData[seid.ToString()]["value1"].n == type)
			{
				MethodInfo method = base.GetType().GetMethod(this.getMethodName() + seid);
				if (method != null)
				{
					method.Invoke(this, new object[]
					{
						seid,
						flag,
						avatar,
						avatar2,
						type
					});
				}
			}
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x0003B1CA File Offset: 0x000393CA
		public void findKey(int seid, Avatar attaker)
		{
			if (!this.getSeidFlag(attaker).ContainsKey(seid))
			{
				this.getSeidFlag(attaker).Add(seid, new Dictionary<int, int>());
				this.getSeidFlag(attaker)[seid].Add(this.skill_ID, 0);
			}
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x00226EB8 File Offset: 0x002250B8
		public virtual JSONObject getSeidJson(int seid)
		{
			JSONObject jsonobject = jsonData.instance.StaticSkillSeidJsonData[seid][this.skill_ID.ToString()];
			if (jsonobject == null)
			{
				Debug.LogError(string.Format("功法特性{0}的配表中不存在技能{1}的数据，请检查配表", seid, this.skill_ID));
			}
			return jsonobject;
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x00226F04 File Offset: 0x00225104
		public virtual void resetSeidFlag(int seid, Avatar attaker)
		{
			this.findKey(seid, attaker);
			if (!this.getSeidFlag(attaker)[seid].ContainsKey(this.skill_ID))
			{
				this.getSeidFlag(attaker)[seid].Add(this.skill_ID, 0);
			}
			Dictionary<int, int> dictionary = this.getSeidFlag(attaker)[seid];
			int key = this.skill_ID;
			dictionary[key] += (int)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x00226F88 File Offset: 0x00225188
		public void setSeidFlag(int seid, int Num, Avatar attaker)
		{
			this.findKey(seid, attaker);
			if (!this.getSeidFlag(attaker)[seid].ContainsKey(this.skill_ID))
			{
				this.getSeidFlag(attaker)[seid].Add(this.skill_ID, 0);
			}
			this.getSeidFlag(attaker)[seid][this.skill_ID] = Num;
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x0003B206 File Offset: 0x00039406
		public Avatar getTargetAvatar(int seid, Avatar attker)
		{
			if (this.getSeidJson(seid)["target"].I == 1)
			{
				return attker;
			}
			return attker.OtherAvatar;
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00226FEC File Offset: 0x002251EC
		public void realizeSeid1(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			Avatar targetAvatar = this.getTargetAvatar(seid, attaker);
			for (int i = 0; i < this.getSeidJson(seid)["value1"].Count; i++)
			{
				int buffid = (int)this.getSeidJson(seid)["value1"][i].n;
				int time = (int)this.getSeidJson(seid)["value2"][i].n;
				targetAvatar.spell.addDBuff(buffid, time);
			}
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x0003A805 File Offset: 0x00038A05
		public void realizeSeid2(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x0003B229 File Offset: 0x00039429
		public virtual void realizeSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
			if (!attaker.isPlayer())
			{
				attaker.setHP(attaker.HP + (int)this.getSeidJson(seid)["value1"].n);
			}
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x0003B25F File Offset: 0x0003945F
		public void realizeSeid4(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.shouYuan += (uint)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x0003B285 File Offset: 0x00039485
		public void realizeSeid5(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker._xinjin += this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x0003A805 File Offset: 0x00038A05
		public void realizeSeid6(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x0003A805 File Offset: 0x00038A05
		public void realizeSeid7(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x0003A805 File Offset: 0x00038A05
		public void realizeSeid8(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x0003A805 File Offset: 0x00038A05
		public void realizeSeid10(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x0003A805 File Offset: 0x00038A05
		public void realizeSeid11(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x0003A805 File Offset: 0x00038A05
		public void realizeSeid12(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x0003B2AA File Offset: 0x000394AA
		public void realizeSeid13(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.setSeidFlag(seid, 1, attaker);
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x0003A805 File Offset: 0x00038A05
		public void realizeSeid14(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x0003B2AA File Offset: 0x000394AA
		public void realizeSeid15(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.setSeidFlag(seid, 1, attaker);
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0003B2AA File Offset: 0x000394AA
		public void realizeSeid17(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.setSeidFlag(seid, 1, attaker);
		}

		// Token: 0x040052A1 RID: 21153
		public string skill_Name;

		// Token: 0x040052A2 RID: 21154
		public int skill_ID;

		// Token: 0x040052A3 RID: 21155
		public string skill_Desc;

		// Token: 0x040052A4 RID: 21156
		public Texture2D skill_Icon;

		// Token: 0x040052A5 RID: 21157
		public Texture2D SkillPingZhi;

		// Token: 0x040052A6 RID: 21158
		public Sprite skillIconSprite;

		// Token: 0x040052A7 RID: 21159
		public Sprite SkillPingZhiSprite;

		// Token: 0x040052A8 RID: 21160
		public int skill_level;

		// Token: 0x040052A9 RID: 21161
		public int Max_level;

		// Token: 0x040052AA RID: 21162
		public float CoolDown;

		// Token: 0x040052AB RID: 21163
		public float CurCD;

		// Token: 0x040052AC RID: 21164
		public Dictionary<int, int> skillCast = new Dictionary<int, int>();

		// Token: 0x040052AD RID: 21165
		public Dictionary<int, int> skillSameCast = new Dictionary<int, int>();

		// Token: 0x02000D6A RID: 3434
		public enum StaticSeidAll
		{
			// Token: 0x040052AF RID: 21167
			StaticSEID2 = 2,
			// Token: 0x040052B0 RID: 21168
			StaticSEID3,
			// Token: 0x040052B1 RID: 21169
			StaticSEID5 = 5,
			// Token: 0x040052B2 RID: 21170
			StaticSEID6,
			// Token: 0x040052B3 RID: 21171
			StaticSEID7,
			// Token: 0x040052B4 RID: 21172
			StaticSEID8,
			// Token: 0x040052B5 RID: 21173
			StaticSEID9,
			// Token: 0x040052B6 RID: 21174
			StaticSEID10,
			// Token: 0x040052B7 RID: 21175
			StaticSEID11,
			// Token: 0x040052B8 RID: 21176
			StaticSEID12,
			// Token: 0x040052B9 RID: 21177
			StaticSEID13,
			// Token: 0x040052BA RID: 21178
			StaticSEID14,
			// Token: 0x040052BB RID: 21179
			StaticSEID15,
			// Token: 0x040052BC RID: 21180
			StaticSEID16,
			// Token: 0x040052BD RID: 21181
			StaticSEID17
		}
	}
}
