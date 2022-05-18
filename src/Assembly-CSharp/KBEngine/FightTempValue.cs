using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using JSONClass;

namespace KBEngine
{
	// Token: 0x0200100D RID: 4109
	public class FightTempValue
	{
		// Token: 0x0600622D RID: 25133 RVA: 0x000440BF File Offset: 0x000422BF
		public void realizedNextSeid()
		{
			this.WaitSkill.triggerStartSeid(this.WaitSeid, this.WaitDamage, this.WaitAttacker, this.WaitReceiver, 0);
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x000440E5 File Offset: 0x000422E5
		public void SetRoundReceiveDamage(Avatar avatar, int damage, int SkillID)
		{
			this.RoundReceiveDamage[0] += damage;
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x000440F8 File Offset: 0x000422F8
		public void SetRoundLossHP(int loss)
		{
			this.RoundLossHP[0] += loss;
			this.TotalLossHP += loss;
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x00044119 File Offset: 0x00042319
		public void SetHealHP(int hp)
		{
			this.TotalHealHP += hp;
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x00044129 File Offset: 0x00042329
		public void SetAddLingQi(int lingQiType, int LingQiCount)
		{
			this.TotalAddLingQi += LingQiCount;
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x00273360 File Offset: 0x00271560
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

		// Token: 0x06006234 RID: 25140 RVA: 0x00044139 File Offset: 0x00042339
		private void SetAttackTypeRoundDamage(int attackType, int damage)
		{
			if (!this.lastRoundDamageDict.ContainsKey(attackType))
			{
				this.lastRoundDamageDict.Add(attackType, new int[2]);
			}
			this.lastRoundDamageDict[attackType][0] += damage;
		}

		// Token: 0x06006235 RID: 25141 RVA: 0x00044172 File Offset: 0x00042372
		public int GetAttackTypeRoundDamage(int attackType)
		{
			if (this.lastRoundDamageDict.ContainsKey(attackType))
			{
				return this.lastRoundDamageDict[attackType][0];
			}
			return 0;
		}

		// Token: 0x06006236 RID: 25142 RVA: 0x0027342C File Offset: 0x0027162C
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

		// Token: 0x06006237 RID: 25143 RVA: 0x00273590 File Offset: 0x00271790
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

		// Token: 0x06006238 RID: 25144 RVA: 0x00044192 File Offset: 0x00042392
		public int GetSkillUseCount(int skillID)
		{
			if (this.SkillUseCountDictWithSkillID.ContainsKey(skillID))
			{
				return this.SkillUseCountDictWithSkillID[skillID];
			}
			return 0;
		}

		// Token: 0x04005CB1 RID: 23729
		public Dictionary<int, int> tempShenShi = new Dictionary<int, int>();

		// Token: 0x04005CB2 RID: 23730
		public Dictionary<int, int> tempDunSu = new Dictionary<int, int>();

		// Token: 0x04005CB3 RID: 23731
		public Dictionary<int, int> TempHP_Max = new Dictionary<int, int>();

		// Token: 0x04005CB4 RID: 23732
		public int showNowHp;

		// Token: 0x04005CB5 RID: 23733
		public int MonstarID;

		// Token: 0x04005CB6 RID: 23734
		public int tempNowCard;

		// Token: 0x04005CB7 RID: 23735
		public bool useAI;

		// Token: 0x04005CB8 RID: 23736
		public List<int> NowRoundUsedCard = new List<int>();

		// Token: 0x04005CB9 RID: 23737
		public List<int> NowRoundUsedSkills = new List<int>();

		// Token: 0x04005CBA RID: 23738
		public List<int> NowRoundDamageSkills = new List<int>();

		// Token: 0x04005CBB RID: 23739
		public List<List<int>> RoundHasBuff = new List<List<int>>();

		// Token: 0x04005CBC RID: 23740
		public List<int> UsedSkills = new List<int>();

		// Token: 0x04005CBD RID: 23741
		public Dictionary<int, int> SkillUseCountDict = new Dictionary<int, int>();

		// Token: 0x04005CBE RID: 23742
		public Dictionary<int, int> SkillUseCountDictWithSkillID = new Dictionary<int, int>();

		// Token: 0x04005CBF RID: 23743
		public int[] lastRoundDamage = new int[2];

		// Token: 0x04005CC0 RID: 23744
		public int[] lastRoundDamageCount = new int[2];

		// Token: 0x04005CC1 RID: 23745
		public Dictionary<int, int[]> lastRoundDamageDict = new Dictionary<int, int[]>();

		// Token: 0x04005CC2 RID: 23746
		public int[] RoundReceiveDamage = new int[2];

		// Token: 0x04005CC3 RID: 23747
		public int[] RoundLossHP = new int[2];

		// Token: 0x04005CC4 RID: 23748
		public int AllDamage;

		// Token: 0x04005CC5 RID: 23749
		public int TotalAddLingQi;

		// Token: 0x04005CC6 RID: 23750
		public int TotalHealHP;

		// Token: 0x04005CC7 RID: 23751
		public int TotalLossHP;

		// Token: 0x04005CC8 RID: 23752
		public Dictionary<int, JSONObject> LianQiBuffEquipDictionary = new Dictionary<int, JSONObject>();

		// Token: 0x04005CC9 RID: 23753
		public Dictionary<int, JSONObject> LianQiEquipDictionary = new Dictionary<int, JSONObject>();

		// Token: 0x04005CCA RID: 23754
		public bool IsSkillWait;

		// Token: 0x04005CCB RID: 23755
		public Avatar WaitAttacker;

		// Token: 0x04005CCC RID: 23756
		public Avatar WaitReceiver;

		// Token: 0x04005CCD RID: 23757
		public List<int> WaitDamage;

		// Token: 0x04005CCE RID: 23758
		public List<int> WaitSeid;

		// Token: 0x04005CCF RID: 23759
		public Skill WaitSkill;
	}
}
