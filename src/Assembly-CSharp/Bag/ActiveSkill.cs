using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag;

[Serializable]
public class ActiveSkill : BaseSkill
{
	public List<int> AttackType;

	public override void SetSkill(int id, int level)
	{
		foreach (_skillJsonData data in _skillJsonData.DataList)
		{
			if (data.Skill_ID == id && level == data.Skill_Lv)
			{
				Id = data.id;
				SkillId = id;
				Level = level;
				Quality = data.Skill_LV;
				Name = data.name.RemoveNumber();
				AttackType = new List<int>(data.AttackType);
				PinJie = data.typePinJie;
				break;
			}
		}
		CanPutSlotType = CanSlotType.技能;
	}

	public override BaseSkill Clone()
	{
		ActiveSkill activeSkill = new ActiveSkill();
		activeSkill.SetSkill(SkillId, Level);
		activeSkill.CanPutSlotType = CanPutSlotType;
		return activeSkill;
	}

	public override Sprite GetIconSprite()
	{
		Sprite val = ResManager.inst.LoadSprite("Skill Icon/" + SkillId);
		if ((Object)(object)val == (Object)null)
		{
			val = ResManager.inst.LoadSprite("Skill Icon/0");
		}
		return val;
	}

	public override string GetDesc1()
	{
		_skillJsonData skillJsonData = _skillJsonData.DataDict[Id];
		return skillJsonData.descr.Replace("（attack）", skillJsonData.HP.ToString()).STVarReplace();
	}

	public override string GetTypeName()
	{
		string text = "";
		foreach (int item in AttackType)
		{
			text += StrTextJsonData.DataDict["xibie" + item].ChinaText;
		}
		return text;
	}

	public override List<int> GetCiZhuiList()
	{
		List<int> list = new List<int>();
		foreach (int item in _skillJsonData.DataDict[Id].Affix2)
		{
			list.Add(item);
		}
		return list;
	}

	public override string GetDesc2()
	{
		foreach (_ItemJsonData data in _ItemJsonData.DataList)
		{
			if (data.desc.Replace(".0", "") == SkillId.ToString() && data.type == 3)
			{
				return data.desc2;
			}
		}
		return "暂无";
	}

	public override bool SkillTypeIsEqual(int skIllType)
	{
		if (skIllType == 9 && AttackType.Count > 0 && AttackType[0] >= 9)
		{
			return true;
		}
		if (AttackType.Contains(skIllType))
		{
			return true;
		}
		return false;
	}

	public List<int> GetCostList()
	{
		List<int> list = new List<int>();
		_skillJsonData skillJsonData = _skillJsonData.DataDict[Id];
		for (int i = 0; i < skillJsonData.skill_SameCastNum.Count; i++)
		{
			for (int j = 0; j < skillJsonData.skill_SameCastNum[i]; j++)
			{
				list.Add(6);
			}
			list.Add(-11);
		}
		for (int k = 0; k < skillJsonData.skill_CastType.Count; k++)
		{
			for (int l = 0; l < skillJsonData.skill_Cast[k]; l++)
			{
				list.Add(skillJsonData.skill_CastType[k]);
			}
			list.Add(-11);
		}
		if (list.Count > 0 && list[list.Count - 1] == -11)
		{
			list.RemoveAt(list.Count - 1);
		}
		return list;
	}

	public List<SkillCost> GetSkillCost()
	{
		List<SkillCost> list = new List<SkillCost>();
		_skillJsonData skillJsonData = _skillJsonData.DataDict[Id];
		for (int i = 0; i < skillJsonData.skill_SameCastNum.Count; i++)
		{
			list.Add(new SkillCost
			{
				Id = 6,
				Num = skillJsonData.skill_SameCastNum[i]
			});
		}
		for (int j = 0; j < skillJsonData.skill_CastType.Count; j++)
		{
			list.Add(new SkillCost
			{
				Id = skillJsonData.skill_CastType[j],
				Num = skillJsonData.skill_Cast[j]
			});
		}
		return list;
	}
}
