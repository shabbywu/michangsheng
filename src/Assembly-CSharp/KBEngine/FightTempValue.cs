using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using JSONClass;

namespace KBEngine;

public class FightTempValue
{
	public Dictionary<int, int> tempShenShi = new Dictionary<int, int>();

	public Dictionary<int, int> tempDunSu = new Dictionary<int, int>();

	public Dictionary<int, int> TempHP_Max = new Dictionary<int, int>();

	public int showNowHp;

	public int MonstarID;

	public int tempNowCard;

	public bool useAI;

	public List<int> NowRoundUsedCard = new List<int>();

	public List<int> NowRoundUsedSkills = new List<int>();

	public List<int> NowRoundDamageSkills = new List<int>();

	public List<List<int>> RoundHasBuff = new List<List<int>>();

	public List<int> UsedSkills = new List<int>();

	public Dictionary<int, int> SkillUseCountDict = new Dictionary<int, int>();

	public Dictionary<int, int> SkillUseCountDictWithSkillID = new Dictionary<int, int>();

	public int[] lastRoundDamage = new int[2];

	public int[] lastRoundDamageCount = new int[2];

	public Dictionary<int, int[]> lastRoundDamageDict = new Dictionary<int, int[]>();

	public int[] RoundReceiveDamage = new int[2];

	public int[] RoundLossHP = new int[2];

	public int AllDamage;

	public int TotalAddLingQi;

	public int TotalHealHP;

	public int TotalLossHP;

	public Dictionary<int, JSONObject> LianQiBuffEquipDictionary = new Dictionary<int, JSONObject>();

	public Dictionary<int, JSONObject> LianQiEquipDictionary = new Dictionary<int, JSONObject>();

	public bool IsSkillWait;

	public Avatar WaitAttacker;

	public Avatar WaitReceiver;

	public List<int> WaitDamage;

	public List<int> WaitSeid;

	public GUIPackage.Skill WaitSkill;

	public void realizedNextSeid()
	{
		WaitSkill.triggerStartSeid(WaitSeid, WaitDamage, WaitAttacker, WaitReceiver, 0);
	}

	public void SetRoundReceiveDamage(Avatar avatar, int damage, int SkillID)
	{
		RoundReceiveDamage[0] += damage;
	}

	public void SetRoundLossHP(int loss)
	{
		RoundLossHP[0] += loss;
		TotalLossHP += loss;
	}

	public void SetHealHP(int hp)
	{
		TotalHealHP += hp;
	}

	public void SetAddLingQi(int lingQiType, int LingQiCount)
	{
		TotalAddLingQi += LingQiCount;
	}

	public void SetRoundDamage(Avatar avatar, int damage, int SkillID)
	{
		if (damage >= 0)
		{
			lastRoundDamage[0] += damage;
			lastRoundDamageCount[0]++;
		}
		AllDamage += damage;
		NowRoundDamageSkills.Add(SkillID);
		foreach (int item in _skillJsonData.DataDict[SkillID].AttackType)
		{
			SetAttackTypeRoundDamage(item, damage);
			try
			{
				SteamChengJiu.ints.FightSkillAllDamageSetStat(avatar, item, damage);
				SteamChengJiu.ints.FightSkillOnceDamageSetStat(avatar, item, damage);
			}
			catch (Exception)
			{
			}
		}
	}

	private void SetAttackTypeRoundDamage(int attackType, int damage)
	{
		if (!lastRoundDamageDict.ContainsKey(attackType))
		{
			lastRoundDamageDict.Add(attackType, new int[2]);
		}
		lastRoundDamageDict[attackType][0] += damage;
	}

	public int GetAttackTypeRoundDamage(int attackType)
	{
		if (lastRoundDamageDict.ContainsKey(attackType))
		{
			return lastRoundDamageDict[attackType][0];
		}
		return 0;
	}

	public void ResetRound(Avatar avatar)
	{
		lastRoundDamage[1] = lastRoundDamage[0];
		lastRoundDamage[0] = 0;
		lastRoundDamageCount[1] = lastRoundDamage[0];
		lastRoundDamageCount[0] = 0;
		RoundReceiveDamage[1] = RoundReceiveDamage[0];
		RoundReceiveDamage[0] = 0;
		RoundLossHP[1] = RoundLossHP[0];
		RoundLossHP[0] = 0;
		foreach (int item in lastRoundDamageDict.Keys.ToList())
		{
			lastRoundDamageDict[item][1] = lastRoundDamageDict[item][0];
			lastRoundDamageDict[item][0] = 0;
		}
		NowRoundDamageSkills = new List<int>();
		NowRoundUsedCard = new List<int>();
		NowRoundUsedSkills = new List<int>();
		List<int> list = new List<int>();
		foreach (List<int> item2 in avatar.bufflist)
		{
			list.Add(item2[2]);
		}
		RoundHasBuff.Add(list);
	}

	public void UseSkill(int id)
	{
		NowRoundUsedSkills.Add(id);
		UsedSkills.Add(id);
		if (!SkillUseCountDict.ContainsKey(id))
		{
			SkillUseCountDict.Add(id, 0);
		}
		SkillUseCountDict[id]++;
		_skillJsonData skillJsonData = _skillJsonData.DataDict[id];
		if (!SkillUseCountDictWithSkillID.ContainsKey(skillJsonData.Skill_ID))
		{
			SkillUseCountDictWithSkillID.Add(skillJsonData.Skill_ID, 0);
		}
		SkillUseCountDictWithSkillID[skillJsonData.Skill_ID]++;
	}

	public int GetSkillUseCount(int skillID)
	{
		if (SkillUseCountDictWithSkillID.ContainsKey(skillID))
		{
			return SkillUseCountDictWithSkillID[skillID];
		}
		return 0;
	}
}
