using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A52 RID: 2642
	[Serializable]
	public class StaticSkill
	{
		// Token: 0x0600499A RID: 18842 RVA: 0x001F3F04 File Offset: 0x001F2104
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

		// Token: 0x0600499B RID: 18843 RVA: 0x001F4054 File Offset: 0x001F2254
		public StaticSkill()
		{
			this.skill_ID = -1;
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x001ED257 File Offset: 0x001EB457
		public Skill Clone()
		{
			return base.MemberwiseClone() as Skill;
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x001F4079 File Offset: 0x001F2279
		public virtual JSONObject getJsonData()
		{
			return jsonData.instance.StaticSkillJsonData;
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x001F4088 File Offset: 0x001F2288
		public void PutingSkill(Entity _attaker, Entity _receiver, int type = 0)
		{
			List<int> flag = new List<int>();
			foreach (JSONObject jsonobject in this.getJsonData()[string.Concat(this.skill_ID)]["seid"].list)
			{
				this.realizeSeid((int)jsonobject.n, flag, _attaker, _receiver, type);
			}
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x001F4110 File Offset: 0x001F2310
		public void putingStudySkill(Entity _attaker, Entity _receiver, int type = 0)
		{
			List<int> flag = new List<int>();
			foreach (JSONObject jsonobject in this.getJsonData()[string.Concat(this.skill_ID)]["seid"].list)
			{
				this.StudySkillSeid((int)jsonobject.n, flag, _attaker, _receiver, type);
			}
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x001F4198 File Offset: 0x001F2398
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

		// Token: 0x060049A1 RID: 18849 RVA: 0x001F422D File Offset: 0x001F242D
		public void Puting(Entity _attaker, Entity _receiver, int type = 0)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			this.PutingSkill(_attaker, _receiver, type);
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x001F4248 File Offset: 0x001F2448
		public static void resetSeid(Avatar attaker)
		{
			attaker.StaticSkillSeidFlag.Clear();
			foreach (SkillItem skillItem in attaker.equipStaticSkillList)
			{
				new StaticSkill(Tools.instance.getStaticSkillKeyByID(skillItem.itemId), 0, 5).Puting(attaker, attaker, 2);
			}
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x001F42C0 File Offset: 0x001F24C0
		public virtual string getMethodName()
		{
			return "realizeSeid";
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x001F42C7 File Offset: 0x001F24C7
		public virtual string getStudyMethodName()
		{
			return "StudtRealizeSeid";
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x001F42CE File Offset: 0x001F24CE
		public virtual Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
		{
			return attaker.StaticSkillSeidFlag;
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x001F42D8 File Offset: 0x001F24D8
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

		// Token: 0x060049A7 RID: 18855 RVA: 0x001F4371 File Offset: 0x001F2571
		public void findKey(int seid, Avatar attaker)
		{
			if (!this.getSeidFlag(attaker).ContainsKey(seid))
			{
				this.getSeidFlag(attaker).Add(seid, new Dictionary<int, int>());
				this.getSeidFlag(attaker)[seid].Add(this.skill_ID, 0);
			}
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x001F43B0 File Offset: 0x001F25B0
		public virtual JSONObject getSeidJson(int seid)
		{
			JSONObject jsonobject = jsonData.instance.StaticSkillSeidJsonData[seid][this.skill_ID.ToString()];
			if (jsonobject == null)
			{
				Debug.LogError(string.Format("功法特性{0}的配表中不存在技能{1}的数据，请检查配表", seid, this.skill_ID));
			}
			return jsonobject;
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x001F43FC File Offset: 0x001F25FC
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

		// Token: 0x060049AA RID: 18858 RVA: 0x001F4480 File Offset: 0x001F2680
		public void setSeidFlag(int seid, int Num, Avatar attaker)
		{
			this.findKey(seid, attaker);
			if (!this.getSeidFlag(attaker)[seid].ContainsKey(this.skill_ID))
			{
				this.getSeidFlag(attaker)[seid].Add(this.skill_ID, 0);
			}
			this.getSeidFlag(attaker)[seid][this.skill_ID] = Num;
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x001F44E1 File Offset: 0x001F26E1
		public Avatar getTargetAvatar(int seid, Avatar attker)
		{
			if (this.getSeidJson(seid)["target"].I == 1)
			{
				return attker;
			}
			return attker.OtherAvatar;
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x001F4504 File Offset: 0x001F2704
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

		// Token: 0x060049AD RID: 18861 RVA: 0x001E9C0B File Offset: 0x001E7E0B
		public void realizeSeid2(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x001F4584 File Offset: 0x001F2784
		public virtual void realizeSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
			if (!attaker.isPlayer())
			{
				attaker.setHP(attaker.HP + (int)this.getSeidJson(seid)["value1"].n);
			}
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x001F45BA File Offset: 0x001F27BA
		public void realizeSeid4(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.shouYuan += (uint)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x001F45E0 File Offset: 0x001F27E0
		public void realizeSeid5(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker._xinjin += this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x001E9C0B File Offset: 0x001E7E0B
		public void realizeSeid6(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x001E9C0B File Offset: 0x001E7E0B
		public void realizeSeid7(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x001E9C0B File Offset: 0x001E7E0B
		public void realizeSeid8(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x001E9C0B File Offset: 0x001E7E0B
		public void realizeSeid10(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x001E9C0B File Offset: 0x001E7E0B
		public void realizeSeid11(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x001E9C0B File Offset: 0x001E7E0B
		public void realizeSeid12(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x001F4605 File Offset: 0x001F2805
		public void realizeSeid13(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.setSeidFlag(seid, 1, attaker);
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x001E9C0B File Offset: 0x001E7E0B
		public void realizeSeid14(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x001F4605 File Offset: 0x001F2805
		public void realizeSeid15(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.setSeidFlag(seid, 1, attaker);
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x001F4605 File Offset: 0x001F2805
		public void realizeSeid17(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.setSeidFlag(seid, 1, attaker);
		}

		// Token: 0x04004939 RID: 18745
		public string skill_Name;

		// Token: 0x0400493A RID: 18746
		public int skill_ID;

		// Token: 0x0400493B RID: 18747
		public string skill_Desc;

		// Token: 0x0400493C RID: 18748
		public Texture2D skill_Icon;

		// Token: 0x0400493D RID: 18749
		public Texture2D SkillPingZhi;

		// Token: 0x0400493E RID: 18750
		public Sprite skillIconSprite;

		// Token: 0x0400493F RID: 18751
		public Sprite SkillPingZhiSprite;

		// Token: 0x04004940 RID: 18752
		public int skill_level;

		// Token: 0x04004941 RID: 18753
		public int Max_level;

		// Token: 0x04004942 RID: 18754
		public float CoolDown;

		// Token: 0x04004943 RID: 18755
		public float CurCD;

		// Token: 0x04004944 RID: 18756
		public Dictionary<int, int> skillCast = new Dictionary<int, int>();

		// Token: 0x04004945 RID: 18757
		public Dictionary<int, int> skillSameCast = new Dictionary<int, int>();

		// Token: 0x0200158D RID: 5517
		public enum StaticSeidAll
		{
			// Token: 0x04006FB7 RID: 28599
			StaticSEID2 = 2,
			// Token: 0x04006FB8 RID: 28600
			StaticSEID3,
			// Token: 0x04006FB9 RID: 28601
			StaticSEID5 = 5,
			// Token: 0x04006FBA RID: 28602
			StaticSEID6,
			// Token: 0x04006FBB RID: 28603
			StaticSEID7,
			// Token: 0x04006FBC RID: 28604
			StaticSEID8,
			// Token: 0x04006FBD RID: 28605
			StaticSEID9,
			// Token: 0x04006FBE RID: 28606
			StaticSEID10,
			// Token: 0x04006FBF RID: 28607
			StaticSEID11,
			// Token: 0x04006FC0 RID: 28608
			StaticSEID12,
			// Token: 0x04006FC1 RID: 28609
			StaticSEID13,
			// Token: 0x04006FC2 RID: 28610
			StaticSEID14,
			// Token: 0x04006FC3 RID: 28611
			StaticSEID15,
			// Token: 0x04006FC4 RID: 28612
			StaticSEID16,
			// Token: 0x04006FC5 RID: 28613
			StaticSEID17
		}
	}
}
