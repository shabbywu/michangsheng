using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag;

[Serializable]
public class PassiveSkill : BaseSkill
{
	public int AttackType;

	public override void SetSkill(int id, int level)
	{
		foreach (StaticSkillJsonData data in StaticSkillJsonData.DataList)
		{
			if (data.Skill_ID == id && level == data.Skill_Lv)
			{
				Id = data.id;
				SkillId = id;
				Level = level;
				Quality = data.Skill_LV;
				Name = data.name.RemoveNumber();
				AttackType = data.AttackType;
				PinJie = data.typePinJie;
				break;
			}
		}
		if (AttackType == 6)
		{
			CanPutSlotType = CanSlotType.遁术;
		}
		else
		{
			CanPutSlotType = CanSlotType.功法;
		}
	}

	public override BaseSkill Clone()
	{
		PassiveSkill passiveSkill = new PassiveSkill();
		passiveSkill.SetSkill(SkillId, Level);
		passiveSkill.CanPutSlotType = CanPutSlotType;
		return passiveSkill;
	}

	public override Sprite GetIconSprite()
	{
		Sprite val = ResManager.inst.LoadSprite("StaticSkill Icon/" + SkillId);
		if ((Object)(object)val == (Object)null)
		{
			val = ResManager.inst.LoadSprite("Skill Icon/0");
		}
		return val;
	}

	public override string GetDesc2()
	{
		foreach (_ItemJsonData data in _ItemJsonData.DataList)
		{
			if (data.desc.Replace(".0", "") == SkillId.ToString() && data.type == 4)
			{
				return data.desc2;
			}
		}
		return "暂无";
	}

	public override string GetDesc1()
	{
		return StaticSkillJsonData.DataDict[Id].descr;
	}

	public int GetSpeed()
	{
		return StaticSkillJsonData.DataDict[Id].Skill_Speed;
	}

	public override string GetTypeName()
	{
		return StrTextJsonData.DataDict["gongfaleibie" + AttackType].ChinaText;
	}

	public override List<int> GetCiZhuiList()
	{
		List<int> list = new List<int>();
		foreach (int item in StaticSkillJsonData.DataDict[Id].Affix)
		{
			list.Add(item);
		}
		return list;
	}

	public override bool SkillTypeIsEqual(int skIllType)
	{
		if (AttackType == skIllType)
		{
			return true;
		}
		return false;
	}
}
