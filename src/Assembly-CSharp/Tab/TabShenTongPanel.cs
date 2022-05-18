using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using UnityEngine;

namespace Tab
{
	// Token: 0x02000A42 RID: 2626
	[Serializable]
	public class TabShenTongPanel : ITabPanelBase
	{
		// Token: 0x060043DA RID: 17370 RVA: 0x001CFD2C File Offset: 0x001CDF2C
		public TabShenTongPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this._isInit = false;
			this.player = Tools.instance.getPlayer();
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x001CFD84 File Offset: 0x001CDF84
		private void Init()
		{
			Transform transform = base.Get("SkillList", true).transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				ActiveSkillSlot component = transform.GetChild(i).GetComponent<ActiveSkillSlot>();
				this.AciveSkillDict.Add(i, component);
			}
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x0003085D File Offset: 0x0002EA5D
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			SingletonMono<TabUIMag>.Instance.TabBag.OpenBag(BagType.技能);
			this.LoadSkillData();
			this._go.SetActive(true);
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x001CFDD0 File Offset: 0x001CDFD0
		public void LoadSkillData()
		{
			this.RemoveAll();
			foreach (SkillItem skillItem in this.player.equipSkillList)
			{
				BaseSkill baseSkill = new ActiveSkill();
				baseSkill.SetSkill(skillItem.itemId, Tools.instance.getPlayer().getLevelType());
				this.AciveSkillDict[skillItem.itemIndex].SetSlotData(baseSkill);
			}
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x001CFE60 File Offset: 0x001CE060
		public void AddSkill(int index, BaseSkill baseSkill)
		{
			if (this.AciveSkillDict.ContainsKey(index))
			{
				this.AciveSkillDict[index].SetSlotData(baseSkill);
				this.player.equipSkill(baseSkill.SkillId, index);
				return;
			}
			Debug.LogError(string.Format("不存在当前Key{0}", index));
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x001CFEB8 File Offset: 0x001CE0B8
		public void ExSkill(int index1, int index2)
		{
			if (this.AciveSkillDict[index2].IsNull())
			{
				this.AciveSkillDict[index2].SetSlotData(this.AciveSkillDict[index1].Skill.Clone());
				this.RemoveSkill(index1);
			}
			else
			{
				BaseSkill slotData = this.AciveSkillDict[index1].Skill.Clone();
				BaseSkill slotData2 = this.AciveSkillDict[index2].Skill.Clone();
				this.RemoveSkill(index1);
				this.RemoveSkill(index2);
				this.AciveSkillDict[index1].SetSlotData(slotData2);
				this.AciveSkillDict[index2].SetSlotData(slotData);
				this.player.equipSkill(this.AciveSkillDict[index1].Skill.SkillId, index1);
			}
			this.player.equipSkill(this.AciveSkillDict[index2].Skill.SkillId, index2);
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x001CFFB0 File Offset: 0x001CE1B0
		public bool CanAddSkill(BaseSkill baseSkill)
		{
			foreach (SkillItem skillItem in this.player.equipSkillList)
			{
				if (baseSkill.SkillId == skillItem.itemId)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x00030896 File Offset: 0x0002EA96
		public void RemoveSkill(int index)
		{
			this.player.UnEquipSkill(this.AciveSkillDict[index].Skill.SkillId);
			this.AciveSkillDict[index].SetNull();
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x001D0018 File Offset: 0x001CE218
		public void RemoveAll()
		{
			foreach (int key in this.AciveSkillDict.Keys)
			{
				this.AciveSkillDict[key].SetNull();
			}
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x001D007C File Offset: 0x001CE27C
		public SlotBase GetNullSlot()
		{
			foreach (int key in this.AciveSkillDict.Keys)
			{
				if (this.AciveSkillDict[key].IsNull())
				{
					return this.AciveSkillDict[key];
				}
			}
			return null;
		}

		// Token: 0x04003BD8 RID: 15320
		private bool _isInit;

		// Token: 0x04003BD9 RID: 15321
		public Dictionary<int, ActiveSkillSlot> AciveSkillDict = new Dictionary<int, ActiveSkillSlot>();

		// Token: 0x04003BDA RID: 15322
		private Avatar player;

		// Token: 0x04003BDB RID: 15323
		private FangAnData FangAnData = Tools.instance.getPlayer().StreamData.FangAnData;
	}
}
