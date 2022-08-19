using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using UnityEngine;

namespace Tab
{
	// Token: 0x020006F8 RID: 1784
	[Serializable]
	public class TabGongFaPanel : ITabPanelBase
	{
		// Token: 0x06003952 RID: 14674 RVA: 0x00187EC3 File Offset: 0x001860C3
		public TabGongFaPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this._isInit = false;
			this.player = Tools.instance.getPlayer();
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x00187EF4 File Offset: 0x001860F4
		private void Init()
		{
			Transform transform = base.Get("SkillList", true).transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				PasstiveSkillSlot component = transform.GetChild(i).GetComponent<PasstiveSkillSlot>();
				this.PasstiveSkillDict.Add((int)component.SkillSlotType, component);
			}
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x00187F48 File Offset: 0x00186148
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			SingletonMono<TabUIMag>.Instance.TabBag.OpenBag(BagType.功法);
			if (this.player.getLevelType() <= 3)
			{
				this.PasstiveSkillDict[6].gameObject.SetActive(false);
			}
			else
			{
				this.PasstiveSkillDict[6].gameObject.SetActive(true);
			}
			this.LoadSkillData();
			this._go.SetActive(true);
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x00187FCC File Offset: 0x001861CC
		public void LoadSkillData()
		{
			this.RemoveAll();
			foreach (SkillItem skillItem in this.player.equipStaticSkillList)
			{
				BaseSkill baseSkill = new PassiveSkill();
				baseSkill.SetSkill(skillItem.itemId, this.GetGongFaLevel(skillItem.itemId));
				this.PasstiveSkillDict[skillItem.itemIndex].SetSlotData(baseSkill);
			}
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x00188058 File Offset: 0x00186258
		public int GetGongFaLevel(int skillId)
		{
			int result = 1;
			foreach (SkillItem skillItem in this.player.hasStaticSkillList)
			{
				if (skillItem.itemId == skillId)
				{
					result = skillItem.level;
					break;
				}
			}
			return result;
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x001880C0 File Offset: 0x001862C0
		public bool CanAddSkill(BaseSkill baseSkill)
		{
			foreach (SkillItem skillItem in this.player.equipStaticSkillList)
			{
				if (baseSkill.SkillId == skillItem.itemId)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x00188128 File Offset: 0x00186328
		public int GetSameSkillIndex(BaseSkill baseSkill)
		{
			foreach (int num in this.PasstiveSkillDict.Keys)
			{
				if (!this.PasstiveSkillDict[num].IsNull() && this.PasstiveSkillDict[num].Skill.SkillId == baseSkill.SkillId)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x001881B4 File Offset: 0x001863B4
		public void AddSkill(GongFaSlotType slotType, BaseSkill baseSkill, int index2 = -1)
		{
			if (this.PasstiveSkillDict.ContainsKey((int)slotType))
			{
				this.player.equipStaticSkill(baseSkill.SkillId, (int)slotType);
				this.PasstiveSkillDict[(int)slotType].SetSlotData(baseSkill);
				if (index2 != -1 && index2 != (int)slotType)
				{
					this.PasstiveSkillDict[index2].SetNull();
					return;
				}
			}
			else
			{
				Debug.LogError(string.Format("不存在当前Key{0}", (int)slotType));
			}
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x00188224 File Offset: 0x00186424
		public void ExSkill(GongFaSlotType slotType1, GongFaSlotType slotType2)
		{
			if (this.PasstiveSkillDict[(int)slotType2].IsNull())
			{
				this.PasstiveSkillDict[(int)slotType2].SetSlotData(this.PasstiveSkillDict[(int)slotType1].Skill.Clone());
				this.RemoveSkill(slotType1);
			}
			else
			{
				BaseSkill slotData = this.PasstiveSkillDict[(int)slotType1].Skill.Clone();
				BaseSkill slotData2 = this.PasstiveSkillDict[(int)slotType2].Skill.Clone();
				this.RemoveSkill(slotType1);
				this.RemoveSkill(slotType2);
				this.PasstiveSkillDict[(int)slotType1].SetSlotData(slotData2);
				this.PasstiveSkillDict[(int)slotType2].SetSlotData(slotData);
				this.player.equipStaticSkill(this.PasstiveSkillDict[(int)slotType1].Skill.SkillId, (int)slotType1);
			}
			this.player.equipStaticSkill(this.PasstiveSkillDict[(int)slotType2].Skill.SkillId, (int)slotType2);
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x00188320 File Offset: 0x00186520
		public void RemoveSkill(GongFaSlotType slotType)
		{
			if (this.PasstiveSkillDict.ContainsKey((int)slotType))
			{
				this.player.UnEquipStaticSkill(this.PasstiveSkillDict[(int)slotType].Skill.SkillId);
				this.PasstiveSkillDict[(int)slotType].SetNull();
				return;
			}
			Debug.LogError(string.Format("不存在当前Key{0}", (int)slotType));
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x00188388 File Offset: 0x00186588
		public void RemoveAll()
		{
			foreach (int key in this.PasstiveSkillDict.Keys)
			{
				this.PasstiveSkillDict[key].SetNull();
			}
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x001883EC File Offset: 0x001865EC
		public SlotBase GetNullSlot()
		{
			foreach (int key in this.PasstiveSkillDict.Keys)
			{
				if (this.PasstiveSkillDict[key].IsNull())
				{
					return this.PasstiveSkillDict[key];
				}
			}
			return null;
		}

		// Token: 0x04003161 RID: 12641
		private bool _isInit;

		// Token: 0x04003162 RID: 12642
		public Dictionary<int, PasstiveSkillSlot> PasstiveSkillDict = new Dictionary<int, PasstiveSkillSlot>();

		// Token: 0x04003163 RID: 12643
		private Avatar player;
	}
}
