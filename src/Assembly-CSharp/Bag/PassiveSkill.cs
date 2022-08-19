using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x020009B5 RID: 2485
	[Serializable]
	public class PassiveSkill : BaseSkill
	{
		// Token: 0x06004519 RID: 17689 RVA: 0x001D58B8 File Offset: 0x001D3AB8
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

		// Token: 0x0600451A RID: 17690 RVA: 0x001D5980 File Offset: 0x001D3B80
		public override BaseSkill Clone()
		{
			PassiveSkill passiveSkill = new PassiveSkill();
			passiveSkill.SetSkill(this.SkillId, this.Level);
			passiveSkill.CanPutSlotType = this.CanPutSlotType;
			return passiveSkill;
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x001D59A8 File Offset: 0x001D3BA8
		public override Sprite GetIconSprite()
		{
			Sprite sprite = ResManager.inst.LoadSprite("StaticSkill Icon/" + this.SkillId);
			if (sprite == null)
			{
				sprite = ResManager.inst.LoadSprite("Skill Icon/0");
			}
			return sprite;
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x001D59F0 File Offset: 0x001D3BF0
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

		// Token: 0x0600451D RID: 17693 RVA: 0x001D5A7C File Offset: 0x001D3C7C
		public override string GetDesc1()
		{
			return StaticSkillJsonData.DataDict[this.Id].descr;
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x001D5A93 File Offset: 0x001D3C93
		public int GetSpeed()
		{
			return StaticSkillJsonData.DataDict[this.Id].Skill_Speed;
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x001D5AAA File Offset: 0x001D3CAA
		public override string GetTypeName()
		{
			return StrTextJsonData.DataDict["gongfaleibie" + this.AttackType].ChinaText;
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x001D5AD0 File Offset: 0x001D3CD0
		public override List<int> GetCiZhuiList()
		{
			List<int> list = new List<int>();
			foreach (int item in StaticSkillJsonData.DataDict[this.Id].Affix)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x001D5B3C File Offset: 0x001D3D3C
		public override bool SkillTypeIsEqual(int skIllType)
		{
			return this.AttackType == skIllType;
		}

		// Token: 0x040046C7 RID: 18119
		public int AttackType;
	}
}
