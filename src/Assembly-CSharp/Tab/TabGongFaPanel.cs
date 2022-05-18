using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000A40 RID: 2624
	[Serializable]
	public class TabGongFaPanel : ITabPanelBase
	{
		// Token: 0x060043CB RID: 17355 RVA: 0x000307F3 File Offset: 0x0002E9F3
		public TabGongFaPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this._isInit = false;
			this.player = Tools.instance.getPlayer();
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x001CF7BC File Offset: 0x001CD9BC
		private void Init()
		{
			Transform transform = base.Get("SkillList", true).transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				PasstiveSkillSlot component = transform.GetChild(i).GetComponent<PasstiveSkillSlot>();
				this.PasstiveSkillDict.Add((int)component.SkillSlotType, component);
			}
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x001CF810 File Offset: 0x001CDA10
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

		// Token: 0x060043CE RID: 17358 RVA: 0x001CF894 File Offset: 0x001CDA94
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

		// Token: 0x060043CF RID: 17359 RVA: 0x001CF920 File Offset: 0x001CDB20
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

		// Token: 0x060043D0 RID: 17360 RVA: 0x001CF988 File Offset: 0x001CDB88
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

		// Token: 0x060043D1 RID: 17361 RVA: 0x001CF9F0 File Offset: 0x001CDBF0
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

		// Token: 0x060043D2 RID: 17362 RVA: 0x001CFA7C File Offset: 0x001CDC7C
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

		// Token: 0x060043D3 RID: 17363 RVA: 0x001CFAEC File Offset: 0x001CDCEC
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

		// Token: 0x060043D4 RID: 17364 RVA: 0x001CFBE8 File Offset: 0x001CDDE8
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

		// Token: 0x060043D5 RID: 17365 RVA: 0x001CFC50 File Offset: 0x001CDE50
		public void RemoveAll()
		{
			foreach (int key in this.PasstiveSkillDict.Keys)
			{
				this.PasstiveSkillDict[key].SetNull();
			}
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x001CFCB4 File Offset: 0x001CDEB4
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

		// Token: 0x04003BD4 RID: 15316
		private bool _isInit;

		// Token: 0x04003BD5 RID: 15317
		public Dictionary<int, PasstiveSkillSlot> PasstiveSkillDict = new Dictionary<int, PasstiveSkillSlot>();

		// Token: 0x04003BD6 RID: 15318
		private Avatar player;
	}
}
