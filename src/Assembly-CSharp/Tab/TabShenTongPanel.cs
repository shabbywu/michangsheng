using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using UnityEngine;

namespace Tab
{
	// Token: 0x020006FA RID: 1786
	[Serializable]
	public class TabShenTongPanel : ITabPanelBase
	{
		// Token: 0x06003961 RID: 14689 RVA: 0x001884A0 File Offset: 0x001866A0
		public TabShenTongPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this._isInit = false;
			this.player = Tools.instance.getPlayer();
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x001884F8 File Offset: 0x001866F8
		private void Init()
		{
			Transform transform = base.Get("SkillList", true).transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				ActiveSkillSlot component = transform.GetChild(i).GetComponent<ActiveSkillSlot>();
				this.AciveSkillDict.Add(i, component);
			}
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x00188544 File Offset: 0x00186744
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

		// Token: 0x06003964 RID: 14692 RVA: 0x00188580 File Offset: 0x00186780
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

		// Token: 0x06003965 RID: 14693 RVA: 0x00188610 File Offset: 0x00186810
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

		// Token: 0x06003966 RID: 14694 RVA: 0x00188668 File Offset: 0x00186868
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

		// Token: 0x06003967 RID: 14695 RVA: 0x00188760 File Offset: 0x00186960
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

		// Token: 0x06003968 RID: 14696 RVA: 0x001887C8 File Offset: 0x001869C8
		public void RemoveSkill(int index)
		{
			this.player.UnEquipSkill(this.AciveSkillDict[index].Skill.SkillId);
			this.AciveSkillDict[index].SetNull();
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x001887FC File Offset: 0x001869FC
		public void RemoveAll()
		{
			foreach (int key in this.AciveSkillDict.Keys)
			{
				this.AciveSkillDict[key].SetNull();
			}
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x00188860 File Offset: 0x00186A60
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

		// Token: 0x04003165 RID: 12645
		private bool _isInit;

		// Token: 0x04003166 RID: 12646
		public Dictionary<int, ActiveSkillSlot> AciveSkillDict = new Dictionary<int, ActiveSkillSlot>();

		// Token: 0x04003167 RID: 12647
		private Avatar player;

		// Token: 0x04003168 RID: 12648
		private FangAnData FangAnData = Tools.instance.getPlayer().StreamData.FangAnData;
	}
}
