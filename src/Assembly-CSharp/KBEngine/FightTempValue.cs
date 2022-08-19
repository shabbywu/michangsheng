using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using JSONClass;

namespace KBEngine
{
	// Token: 0x02000C6E RID: 3182
	public class FightTempValue
	{
		// Token: 0x060057B5 RID: 22453 RVA: 0x00246FC0 File Offset: 0x002451C0
		public void realizedNextSeid()
		{
			this.WaitSkill.triggerStartSeid(this.WaitSeid, this.WaitDamage, this.WaitAttacker, this.WaitReceiver, 0);
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x002470BA File Offset: 0x002452BA
		public void SetRoundReceiveDamage(Avatar avatar, int damage, int SkillID)
		{
			this.RoundReceiveDamage[0] += damage;
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x002470CD File Offset: 0x002452CD
		public void SetRoundLossHP(int loss)
		{
			this.RoundLossHP[0] += loss;
			this.TotalLossHP += loss;
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x002470EE File Offset: 0x002452EE
		public void SetHealHP(int hp)
		{
			this.TotalHealHP += hp;
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x002470FE File Offset: 0x002452FE
		public void SetAddLingQi(int lingQiType, int LingQiCount)
		{
			this.TotalAddLingQi += LingQiCount;
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x00247110 File Offset: 0x00245310
		public void SetRoundDamage(Avatar avatar, int damage, int SkillID)
		{
			if (damage >= 0)
			{
				this.lastRoundDamage[0] += damage;
				this.lastRoundDamageCount[0]++;
			}
			this.AllDamage += damage;
			this.NowRoundDamageSkills.Add(SkillID);
			foreach (int num in _skillJsonData.DataDict[SkillID].AttackType)
			{
				this.SetAttackTypeRoundDamage(num, damage);
				try
				{
					SteamChengJiu.ints.FightSkillAllDamageSetStat(avatar, num, damage);
					SteamChengJiu.ints.FightSkillOnceDamageSetStat(avatar, num, damage);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x002471DC File Offset: 0x002453DC
		private void SetAttackTypeRoundDamage(int attackType, int damage)
		{
			if (!this.lastRoundDamageDict.ContainsKey(attackType))
			{
				this.lastRoundDamageDict.Add(attackType, new int[2]);
			}
			this.lastRoundDamageDict[attackType][0] += damage;
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x00247215 File Offset: 0x00245415
		public int GetAttackTypeRoundDamage(int attackType)
		{
			if (this.lastRoundDamageDict.ContainsKey(attackType))
			{
				return this.lastRoundDamageDict[attackType][0];
			}
			return 0;
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x00247238 File Offset: 0x00245438
		public void ResetRound(Avatar avatar)
		{
			this.lastRoundDamage[1] = this.lastRoundDamage[0];
			this.lastRoundDamage[0] = 0;
			this.lastRoundDamageCount[1] = this.lastRoundDamage[0];
			this.lastRoundDamageCount[0] = 0;
			this.RoundReceiveDamage[1] = this.RoundReceiveDamage[0];
			this.RoundReceiveDamage[0] = 0;
			this.RoundLossHP[1] = this.RoundLossHP[0];
			this.RoundLossHP[0] = 0;
			foreach (int key in this.lastRoundDamageDict.Keys.ToList<int>())
			{
				this.lastRoundDamageDict[key][1] = this.lastRoundDamageDict[key][0];
				this.lastRoundDamageDict[key][0] = 0;
			}
			this.NowRoundDamageSkills = new List<int>();
			this.NowRoundUsedCard = new List<int>();
			this.NowRoundUsedSkills = new List<int>();
			List<int> list = new List<int>();
			foreach (List<int> list2 in avatar.bufflist)
			{
				list.Add(list2[2]);
			}
			this.RoundHasBuff.Add(list);
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x0024739C File Offset: 0x0024559C
		public void UseSkill(int id)
		{
			this.NowRoundUsedSkills.Add(id);
			this.UsedSkills.Add(id);
			if (!this.SkillUseCountDict.ContainsKey(id))
			{
				this.SkillUseCountDict.Add(id, 0);
			}
			Dictionary<int, int> skillUseCountDict = this.SkillUseCountDict;
			int num = skillUseCountDict[id];
			skillUseCountDict[id] = num + 1;
			_skillJsonData skillJsonData = _skillJsonData.DataDict[id];
			if (!this.SkillUseCountDictWithSkillID.ContainsKey(skillJsonData.Skill_ID))
			{
				this.SkillUseCountDictWithSkillID.Add(skillJsonData.Skill_ID, 0);
			}
			Dictionary<int, int> skillUseCountDictWithSkillID = this.SkillUseCountDictWithSkillID;
			num = skillJsonData.Skill_ID;
			int num2 = skillUseCountDictWithSkillID[num];
			skillUseCountDictWithSkillID[num] = num2 + 1;
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x00247444 File Offset: 0x00245644
		public int GetSkillUseCount(int skillID)
		{
			if (this.SkillUseCountDictWithSkillID.ContainsKey(skillID))
			{
				return this.SkillUseCountDictWithSkillID[skillID];
			}
			return 0;
		}

		// Token: 0x040051D3 RID: 20947
		public Dictionary<int, int> tempShenShi = new Dictionary<int, int>();

		// Token: 0x040051D4 RID: 20948
		public Dictionary<int, int> tempDunSu = new Dictionary<int, int>();

		// Token: 0x040051D5 RID: 20949
		public Dictionary<int, int> TempHP_Max = new Dictionary<int, int>();

		// Token: 0x040051D6 RID: 20950
		public int showNowHp;

		// Token: 0x040051D7 RID: 20951
		public int MonstarID;

		// Token: 0x040051D8 RID: 20952
		public int tempNowCard;

		// Token: 0x040051D9 RID: 20953
		public bool useAI;

		// Token: 0x040051DA RID: 20954
		public List<int> NowRoundUsedCard = new List<int>();

		// Token: 0x040051DB RID: 20955
		public List<int> NowRoundUsedSkills = new List<int>();

		// Token: 0x040051DC RID: 20956
		public List<int> NowRoundDamageSkills = new List<int>();

		// Token: 0x040051DD RID: 20957
		public List<List<int>> RoundHasBuff = new List<List<int>>();

		// Token: 0x040051DE RID: 20958
		public List<int> UsedSkills = new List<int>();

		// Token: 0x040051DF RID: 20959
		public Dictionary<int, int> SkillUseCountDict = new Dictionary<int, int>();

		// Token: 0x040051E0 RID: 20960
		public Dictionary<int, int> SkillUseCountDictWithSkillID = new Dictionary<int, int>();

		// Token: 0x040051E1 RID: 20961
		public int[] lastRoundDamage = new int[2];

		// Token: 0x040051E2 RID: 20962
		public int[] lastRoundDamageCount = new int[2];

		// Token: 0x040051E3 RID: 20963
		public Dictionary<int, int[]> lastRoundDamageDict = new Dictionary<int, int[]>();

		// Token: 0x040051E4 RID: 20964
		public int[] RoundReceiveDamage = new int[2];

		// Token: 0x040051E5 RID: 20965
		public int[] RoundLossHP = new int[2];

		// Token: 0x040051E6 RID: 20966
		public int AllDamage;

		// Token: 0x040051E7 RID: 20967
		public int TotalAddLingQi;

		// Token: 0x040051E8 RID: 20968
		public int TotalHealHP;

		// Token: 0x040051E9 RID: 20969
		public int TotalLossHP;

		// Token: 0x040051EA RID: 20970
		public Dictionary<int, JSONObject> LianQiBuffEquipDictionary = new Dictionary<int, JSONObject>();

		// Token: 0x040051EB RID: 20971
		public Dictionary<int, JSONObject> LianQiEquipDictionary = new Dictionary<int, JSONObject>();

		// Token: 0x040051EC RID: 20972
		public bool IsSkillWait;

		// Token: 0x040051ED RID: 20973
		public Avatar WaitAttacker;

		// Token: 0x040051EE RID: 20974
		public Avatar WaitReceiver;

		// Token: 0x040051EF RID: 20975
		public List<int> WaitDamage;

		// Token: 0x040051F0 RID: 20976
		public List<int> WaitSeid;

		// Token: 0x040051F1 RID: 20977
		public Skill WaitSkill;
	}
}
