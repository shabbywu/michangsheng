using System;
using Bag;
using JSONClass;
using UnityEngine;

namespace JiaoYi;

[Serializable]
public class JiaoYiMiJi : MiJiItem
{
	public JiaoYiSkillType GetJiaoYiType()
	{
		if (MiJiType == MiJiType.技能)
		{
			return JiaoYiSkillType.神通;
		}
		if (MiJiType == MiJiType.功法)
		{
			return JiaoYiSkillType.功法;
		}
		return JiaoYiSkillType.其他;
	}

	public bool SkillTypeIsEqual(int skIllType)
	{
		int id = int.Parse(_ItemJsonData.DataDict[Id].desc.Replace(".0", ""));
		if (MiJiType != MiJiType.技能 && MiJiType != MiJiType.功法)
		{
			Debug.LogError((object)"此方法必须只能技能或功法类型物品使用");
			return false;
		}
		if (MiJiType == MiJiType.技能)
		{
			ActiveSkill activeSkill = new ActiveSkill();
			activeSkill.SetSkill(id, Tools.instance.getPlayer().getLevelType());
			return activeSkill.SkillTypeIsEqual(skIllType);
		}
		PassiveSkill passiveSkill = new PassiveSkill();
		passiveSkill.SetSkill(id, Tools.instance.getPlayer().getLevelType());
		return passiveSkill.SkillTypeIsEqual(skIllType);
	}
}
