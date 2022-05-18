using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D39 RID: 3385
	[Serializable]
	public class ActiveSkill : BaseSkill
	{
		// Token: 0x0600505E RID: 20574 RVA: 0x0021984C File Offset: 0x00217A4C
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

		// Token: 0x0600505F RID: 20575 RVA: 0x00039E64 File Offset: 0x00038064
		public override BaseSkill Clone()
		{
			ActiveSkill activeSkill = new ActiveSkill();
			activeSkill.SetSkill(this.SkillId, this.Level);
			activeSkill.CanPutSlotType = this.CanPutSlotType;
			return activeSkill;
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x00219908 File Offset: 0x00217B08
		public override Sprite GetIconSprite()
		{
			Sprite sprite = ResManager.inst.LoadSprite("Skill Icon/" + this.SkillId);
			if (sprite == null)
			{
				sprite = ResManager.inst.LoadSprite("Skill Icon/0");
			}
			return sprite;
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x00219950 File Offset: 0x00217B50
		public override string GetDesc1()
		{
			_skillJsonData skillJsonData = _skillJsonData.DataDict[this.Id];
			return skillJsonData.descr.Replace("（attack）", skillJsonData.HP.ToString()).STVarReplace();
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x00219990 File Offset: 0x00217B90
		public override string GetTypeName()
		{
			string text = "";
			foreach (int num in this.AttackType)
			{
				text += StrTextJsonData.DataDict["xibie" + num].ChinaText;
			}
			return text;
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x00219A0C File Offset: 0x00217C0C
		public override List<int> GetCiZhuiList()
		{
			List<int> list = new List<int>();
			foreach (int item in _skillJsonData.DataDict[this.Id].Affix2)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x00219A78 File Offset: 0x00217C78
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

		// Token: 0x06005065 RID: 20581 RVA: 0x00039E89 File Offset: 0x00038089
		public override bool SkillTypeIsEqual(int skIllType)
		{
			return (skIllType == 9 && this.AttackType.Count > 0 && this.AttackType[0] >= 9) || this.AttackType.Contains(skIllType);
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x00219B04 File Offset: 0x00217D04
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

		// Token: 0x06005067 RID: 20583 RVA: 0x00219BE0 File Offset: 0x00217DE0
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

		// Token: 0x040051BF RID: 20927
		public List<int> AttackType;
	}
}
