using System;
using Bag;
using JSONClass;
using UnityEngine;

namespace JiaoYi
{
	// Token: 0x0200072E RID: 1838
	[Serializable]
	public class JiaoYiMiJi : MiJiItem
	{
		// Token: 0x06003A97 RID: 14999 RVA: 0x00192CAF File Offset: 0x00190EAF
		public JiaoYiSkillType GetJiaoYiType()
		{
			if (this.MiJiType == MiJiType.技能)
			{
				return JiaoYiSkillType.神通;
			}
			if (this.MiJiType == MiJiType.功法)
			{
				return JiaoYiSkillType.功法;
			}
			return JiaoYiSkillType.其他;
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x00192CC8 File Offset: 0x00190EC8
		public bool SkillTypeIsEqual(int skIllType)
		{
			int id = int.Parse(_ItemJsonData.DataDict[this.Id].desc.Replace(".0", ""));
			if (this.MiJiType != MiJiType.技能 && this.MiJiType != MiJiType.功法)
			{
				Debug.LogError("此方法必须只能技能或功法类型物品使用");
				return false;
			}
			if (this.MiJiType == MiJiType.技能)
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
}
