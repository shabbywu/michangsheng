using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D3D RID: 3389
	[Serializable]
	public class PassiveSkill : BaseSkill
	{
		// Token: 0x0600507C RID: 20604 RVA: 0x00219DE8 File Offset: 0x00217FE8
		public override void SetSkill(int id, int level)
		{
			foreach (StaticSkillJsonData staticSkillJsonData in StaticSkillJsonData.DataList)
			{
				if (staticSkillJsonData.Skill_ID == id && level == staticSkillJsonData.Skill_Lv)
				{
					this.Id = staticSkillJsonData.id;
					this.SkillId = id;
					this.Level = level;
					this.Quality = staticSkillJsonData.Skill_LV;
					this.Name = staticSkillJsonData.name.RemoveNumber();
					this.AttackType = staticSkillJsonData.AttackType;
					this.PinJie = staticSkillJsonData.typePinJie;
					break;
				}
			}
			if (this.AttackType == 6)
			{
				this.CanPutSlotType = CanSlotType.遁术;
				return;
			}
			this.CanPutSlotType = CanSlotType.功法;
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x00039EE6 File Offset: 0x000380E6
		public override BaseSkill Clone()
		{
			PassiveSkill passiveSkill = new PassiveSkill();
			passiveSkill.SetSkill(this.SkillId, this.Level);
			passiveSkill.CanPutSlotType = this.CanPutSlotType;
			return passiveSkill;
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x00219EB0 File Offset: 0x002180B0
		public override Sprite GetIconSprite()
		{
			Sprite sprite = ResManager.inst.LoadSprite("StaticSkill Icon/" + this.SkillId);
			if (sprite == null)
			{
				sprite = ResManager.inst.LoadSprite("Skill Icon/0");
			}
			return sprite;
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x00219EF8 File Offset: 0x002180F8
		public override string GetDesc2()
		{
			foreach (_ItemJsonData itemJsonData in _ItemJsonData.DataList)
			{
				if (itemJsonData.desc.Replace(".0", "") == this.SkillId.ToString() && itemJsonData.type == 4)
				{
					return itemJsonData.desc2;
				}
			}
			return "暂无";
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x00039F0B File Offset: 0x0003810B
		public override string GetDesc1()
		{
			return StaticSkillJsonData.DataDict[this.Id].descr;
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x00039F22 File Offset: 0x00038122
		public int GetSpeed()
		{
			return StaticSkillJsonData.DataDict[this.Id].Skill_Speed;
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x00039F39 File Offset: 0x00038139
		public override string GetTypeName()
		{
			return StrTextJsonData.DataDict["gongfaleibie" + this.AttackType].ChinaText;
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x00219F84 File Offset: 0x00218184
		public override List<int> GetCiZhuiList()
		{
			List<int> list = new List<int>();
			foreach (int item in StaticSkillJsonData.DataDict[this.Id].Affix)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x00039F5F File Offset: 0x0003815F
		public override bool SkillTypeIsEqual(int skIllType)
		{
			return this.AttackType == skIllType;
		}

		// Token: 0x040051C9 RID: 20937
		public int AttackType;
	}
}
