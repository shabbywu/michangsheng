using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x020009B1 RID: 2481
	[Serializable]
	public class ActiveSkill : BaseSkill
	{
		// Token: 0x060044FB RID: 17659 RVA: 0x001D5294 File Offset: 0x001D3494
		public override void SetSkill(int id, int level)
		{
			foreach (_skillJsonData skillJsonData in _skillJsonData.DataList)
			{
				if (skillJsonData.Skill_ID == id && level == skillJsonData.Skill_Lv)
				{
					this.Id = skillJsonData.id;
					this.SkillId = id;
					this.Level = level;
					this.Quality = skillJsonData.Skill_LV;
					this.Name = skillJsonData.name.RemoveNumber();
					this.AttackType = new List<int>(skillJsonData.AttackType);
					this.PinJie = skillJsonData.typePinJie;
					break;
				}
			}
			this.CanPutSlotType = CanSlotType.技能;
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x001D5350 File Offset: 0x001D3550
		public override BaseSkill Clone()
		{
			ActiveSkill activeSkill = new ActiveSkill();
			activeSkill.SetSkill(this.SkillId, this.Level);
			activeSkill.CanPutSlotType = this.CanPutSlotType;
			return activeSkill;
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x001D5378 File Offset: 0x001D3578
		public override Sprite GetIconSprite()
		{
			Sprite sprite = ResManager.inst.LoadSprite("Skill Icon/" + this.SkillId);
			if (sprite == null)
			{
				sprite = ResManager.inst.LoadSprite("Skill Icon/0");
			}
			return sprite;
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x001D53C0 File Offset: 0x001D35C0
		public override string GetDesc1()
		{
			_skillJsonData skillJsonData = _skillJsonData.DataDict[this.Id];
			return skillJsonData.descr.Replace("（attack）", skillJsonData.HP.ToString()).STVarReplace();
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x001D5400 File Offset: 0x001D3600
		public override string GetTypeName()
		{
			string text = "";
			foreach (int num in this.AttackType)
			{
				text += StrTextJsonData.DataDict["xibie" + num].ChinaText;
			}
			return text;
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x001D547C File Offset: 0x001D367C
		public override List<int> GetCiZhuiList()
		{
			List<int> list = new List<int>();
			foreach (int item in _skillJsonData.DataDict[this.Id].Affix2)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x001D54E8 File Offset: 0x001D36E8
		public override string GetDesc2()
		{
			foreach (_ItemJsonData itemJsonData in _ItemJsonData.DataList)
			{
				if (itemJsonData.desc.Replace(".0", "") == this.SkillId.ToString() && itemJsonData.type == 3)
				{
					return itemJsonData.desc2;
				}
			}
			return "暂无";
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x001D5574 File Offset: 0x001D3774
		public override bool SkillTypeIsEqual(int skIllType)
		{
			return (skIllType == 9 && this.AttackType.Count > 0 && this.AttackType[0] >= 9) || this.AttackType.Contains(skIllType);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x001D55AC File Offset: 0x001D37AC
		public List<int> GetCostList()
		{
			List<int> list = new List<int>();
			_skillJsonData skillJsonData = _skillJsonData.DataDict[this.Id];
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

		// Token: 0x06004504 RID: 17668 RVA: 0x001D5688 File Offset: 0x001D3888
		public List<SkillCost> GetSkillCost()
		{
			List<SkillCost> list = new List<SkillCost>();
			_skillJsonData skillJsonData = _skillJsonData.DataDict[this.Id];
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

		// Token: 0x040046BD RID: 18109
		public List<int> AttackType;
	}
}
