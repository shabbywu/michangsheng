using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x020009BB RID: 2491
	public class SlotBase : MonoBehaviour, ISlot, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x06004535 RID: 17717 RVA: 0x001D5D60 File Offset: 0x001D3F60
		public virtual void SetSlotData(object data)
		{
			this.Item = null;
			this.Skill = null;
			if (data is BaseItem)
			{
				this.SetItem((BaseItem)data);
				this.SlotType = SlotType.物品;
			}
			else if (data is ActiveSkill)
			{
				this.SetActiveSkill((BaseSkill)data);
				this.SlotType = SlotType.技能;
			}
			else if (data is PassiveSkill)
			{
				this.SetPassiveSkill((BaseSkill)data);
				this.SlotType = SlotType.功法;
			}
			else if (data is BagTianJieSkill)
			{
				this.SetTianJieSkill((BagTianJieSkill)data);
				this.SlotType = SlotType.天劫秘术;
			}
			this.UpdateUI();
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x001D5DF3 File Offset: 0x001D3FF3
		public void SetAccptType(CanSlotType slotType)
		{
			this.AcceptType = slotType;
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x001D5DFC File Offset: 0x001D3FFC
		protected virtual void SetItem(BaseItem item)
		{
			this.Item = item;
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x001D5E05 File Offset: 0x001D4005
		private void SetActiveSkill(BaseSkill activeSkill)
		{
			this.Skill = activeSkill;
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x001D5E05 File Offset: 0x001D4005
		private void SetPassiveSkill(BaseSkill passiveSkill)
		{
			this.Skill = passiveSkill;
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x001D5E0E File Offset: 0x001D400E
		private void SetTianJieSkill(BagTianJieSkill tianJieSkill)
		{
			this.TianJieSkill = tianJieSkill;
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x001D5E18 File Offset: 0x001D4018
		public virtual void SetNull()
		{
			this.Item = null;
			this.Skill = null;
			this.TianJieSkill = null;
			this.SlotType = SlotType.空;
			if (this._nullPanel == null)
			{
				this.InitUI();
			}
			this._nullPanel.SetActive(true);
			this._hasPanel.SetActive(false);
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x001D5E6D File Offset: 0x001D406D
		public bool IsNull()
		{
			return this.SlotType == SlotType.空;
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x001D5E78 File Offset: 0x001D4078
		public virtual void InitUI()
		{
			this._nullPanel = this.Get("Null", true);
			this._hasPanel = this.Get("HasItem", true);
			this._selectPanel = this.Get("Selected", true);
			this._jiaoBiaoPanel = this.Get("HasItem/LeftUpMask", true);
			this._qualityBg = this.Get<Image>("HasItem/Quality");
			this._qualityLine = this.Get<Image>("HasItem/Quality/QualityUp");
			this._jiaoBiao = this.Get<Image>("HasItem/LeftUpMask/JiaoBiao");
			this._icon = this.Get<Image>("HasItem/IconMask/Icon");
			this._name = this.Get<Text>("HasItem/NameMask/NameText");
			this._count = this.Get<Text>("HasItem/CountText");
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x001D5F34 File Offset: 0x001D4134
		public void UpdateUI()
		{
			if (this._nullPanel == null)
			{
				this.InitUI();
			}
			this._nullPanel.SetActive(false);
			this._hasPanel.SetActive(true);
			try
			{
				switch (this.SlotType)
				{
				case SlotType.物品:
					this.UpdateItemUI();
					break;
				case SlotType.技能:
				case SlotType.功法:
					this.UpdateSkillUI();
					break;
				case SlotType.天劫秘术:
					this.UpdateTianJieSkillUI();
					break;
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("刷新格子出现异常:{0}", arg));
				BaseItem baseItem = BaseItem.Create(10000, 1, Guid.NewGuid().ToString(), null);
				switch (this.SlotType)
				{
				case SlotType.物品:
					if (this.Item != null)
					{
						PlayerEx.AddErrorItemID(this.Item.Id);
						BaseItem baseItem2 = baseItem;
						baseItem2.Desc1 += string.Format("错误的物品ID:{0}", this.Item.Id);
					}
					break;
				case SlotType.技能:
				case SlotType.功法:
					if (this.Skill != null)
					{
						BaseItem baseItem3 = baseItem;
						baseItem3.Desc1 += string.Format("错误的技能ID:{0}", this.Skill.Id);
					}
					break;
				case SlotType.天劫秘术:
					if (this.TianJieSkill != null && this.TianJieSkill.MiShu != null)
					{
						BaseItem baseItem4 = baseItem;
						baseItem4.Desc1 = baseItem4.Desc1 + "错误的秘术ID:" + this.TianJieSkill.MiShu.id;
					}
					break;
				}
				this.SetSlotData(baseItem);
			}
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x001D60E0 File Offset: 0x001D42E0
		private void UpdateTianJieSkillUI()
		{
			this._count.gameObject.SetActive(false);
			this._icon.sprite = this.TianJieSkill.GetIconSprite();
			this._qualityBg.sprite = this.TianJieSkill.GetQualitySprite();
			this._qualityLine.sprite = this.TianJieSkill.GetQualityUpSprite();
			bool flag = false;
			if (this.TianJieSkill.MiShu.Type == 0)
			{
				if (this.TianJieSkill.IsGanYing)
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			Color color = ColorEx.ItemQualityColor[this.TianJieSkill.BindSkill.GetImgQuality() - 1];
			if (flag)
			{
				this.SetName(this.TianJieSkill.MiShu.id, color);
			}
			else
			{
				this.SetName("???", color);
			}
			this._jiaoBiaoPanel.SetActive(false);
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x001D61B8 File Offset: 0x001D43B8
		private void UpdateItemUI()
		{
			if (this.Item.Count == 1)
			{
				this._count.gameObject.SetActive(false);
			}
			else
			{
				this._count.gameObject.SetActive(true);
				this._count.SetText(this.Item.Count);
			}
			this._icon.sprite = this.Item.GetIconSprite();
			this._qualityBg.sprite = this.Item.GetQualitySprite();
			this._qualityLine.sprite = this.Item.GetQualityUpSprite();
			this.SetName(this.Item.GetName(), ColorEx.ItemQualityColor[this.Item.GetImgQuality() - 1]);
			this.SetJiaoBiao(this.Item.GetJiaoBiaoType());
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x001D6290 File Offset: 0x001D4490
		public void SetJiaoBiao(JiaoBiaoType type)
		{
			int num = (int)type;
			if (type == JiaoBiaoType.无)
			{
				this._jiaoBiaoPanel.SetActive(false);
				return;
			}
			if (type - JiaoBiaoType.秘 > 3)
			{
				return;
			}
			this._jiaoBiao.sprite = BagMag.Inst.JiaoBiaoDict[num.ToString()];
			this._jiaoBiaoPanel.SetActive(true);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x001D62E4 File Offset: 0x001D44E4
		private void UpdateSkillUI()
		{
			this._count.gameObject.SetActive(false);
			this._icon.sprite = this.Skill.GetIconSprite();
			this._qualityBg.sprite = this.Skill.GetQualitySprite();
			this._qualityLine.sprite = this.Skill.GetQualityUpSprite();
			this.SetName(this.Skill.Name, ColorEx.ItemQualityColor[this.Skill.GetImgQuality() - 1]);
			this._jiaoBiaoPanel.SetActive(false);
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x001D6378 File Offset: 0x001D4578
		protected T Get<T>(string path)
		{
			string key = path + "_" + typeof(T).Name;
			if (!this._objDict.ContainsKey(key))
			{
				Transform transform = base.gameObject.transform.Find(path);
				if (transform == null)
				{
					Debug.LogError("不存在该对象,路径:" + path);
					return default(T);
				}
				T component = transform.GetComponent<T>();
				if (component == null)
				{
					Debug.LogError("不存在该组件" + typeof(T).Name + ",路径:" + path);
					return default(T);
				}
				this._objDict.Add(key, component);
			}
			return (T)((object)this._objDict[key]);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x001D6448 File Offset: 0x001D4648
		protected GameObject Get(string path, bool showError = true)
		{
			string key = path + "_GameObject";
			if (!this._objDict.ContainsKey(key))
			{
				Transform transform = base.gameObject.transform.Find(path);
				if (transform == null)
				{
					if (showError)
					{
						Debug.LogError("不存在该对象,路径:" + path);
					}
					return null;
				}
				this._objDict.Add(key, transform.gameObject);
			}
			return (GameObject)this._objDict[key];
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x001D64C4 File Offset: 0x001D46C4
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			if (DragMag.Inst.IsDraging)
			{
				DragMag.Inst.ToSlot = this;
			}
			if (this.SlotType == SlotType.空)
			{
				return;
			}
			this.IsIn = true;
			if (!eventData.dragging && !this.HideTooltip)
			{
				if (ToolTipsMag.Inst == null)
				{
					ResManager.inst.LoadPrefab("ToolTips").Inst(NewUICanvas.Inst.transform);
				}
				if (this.SlotType == SlotType.物品)
				{
					ToolTipsMag.Inst.Show(this.Item);
				}
				else
				{
					ToolTipsMag.Inst.Show(this.Skill);
				}
			}
			this._selectPanel.SetActive(true);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x001D656C File Offset: 0x001D476C
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			if (DragMag.Inst.IsDraging)
			{
				DragMag.Inst.ToSlot = null;
			}
			if (this.SlotType == SlotType.空)
			{
				return;
			}
			this.IsIn = false;
			this._selectPanel.SetActive(false);
			if (ToolTipsMag.Inst != null)
			{
				ToolTipsMag.Inst.Close();
			}
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x001D65C4 File Offset: 0x001D47C4
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				return;
			}
			if (!this.IsNull())
			{
				if (eventData.button == 1 && this.CanUse)
				{
					DragMag.Inst.DragSlot = this;
					if (this.Item != null)
					{
						this.Item.Use();
						if (this.Item != null && this.Item.Count > 0)
						{
							this.UpdateItemUI();
						}
					}
					this._selectPanel.SetActive(false);
					ToolTipsMag.Inst.Close();
				}
				if (eventData.button == 1 && this.OnRightClick != null)
				{
					this.OnRightClick.Invoke();
				}
				if (eventData.button == null && this.OnLeftClick != null)
				{
					this.OnLeftClick.Invoke();
				}
			}
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x001D667D File Offset: 0x001D487D
		public void SetGrey(bool grey)
		{
			this._icon.material = (grey ? GreyMatManager.Grey1 : null);
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x001D6695 File Offset: 0x001D4895
		public Sprite GetIcon()
		{
			return this._icon.sprite;
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x001D66A2 File Offset: 0x001D48A2
		public void SetName(string targetName, string color)
		{
			this._name.SetText(targetName, color);
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x001D66B1 File Offset: 0x001D48B1
		public void SetName(string targetName, Color color)
		{
			this._name.SetText(targetName, color.ColorToString());
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x000B3123 File Offset: 0x000B1323
		private void OnDestroy()
		{
			if (ToolTipsMag.Inst != null)
			{
				ToolTipsMag.Inst.Close();
			}
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x001D66C5 File Offset: 0x001D48C5
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.CanDrag())
			{
				return;
			}
			DragMag.Inst.StartDrag(this);
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x001D66DB File Offset: 0x001D48DB
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.CanDrag())
			{
				return;
			}
			DragMag.Inst.UpdatePostion(eventData.position);
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x001D66FB File Offset: 0x001D48FB
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.CanDrag())
			{
				return;
			}
			DragMag.Inst.EndDrag();
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x001D6714 File Offset: 0x001D4914
		public virtual bool CanDrag()
		{
			if (this.IsNull())
			{
				return false;
			}
			if (this.Skill != null && this.Skill is ActiveSkill)
			{
				ActiveSkill activeSkill = (ActiveSkill)this.Skill;
				if (activeSkill.AttackType.Count > 0 && activeSkill.AttackType[0] >= 12)
				{
					return false;
				}
			}
			return this.IsCanDrag;
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x040046CB RID: 18123
		public BaseItem Item;

		// Token: 0x040046CC RID: 18124
		public BaseSkill Skill;

		// Token: 0x040046CD RID: 18125
		public BagTianJieSkill TianJieSkill;

		// Token: 0x040046CE RID: 18126
		public int Group;

		// Token: 0x040046CF RID: 18127
		public bool IsCanDrag = true;

		// Token: 0x040046D0 RID: 18128
		public bool IsIn;

		// Token: 0x040046D1 RID: 18129
		public bool CanUse = true;

		// Token: 0x040046D2 RID: 18130
		public SlotType SlotType;

		// Token: 0x040046D3 RID: 18131
		public CanSlotType AcceptType;

		// Token: 0x040046D4 RID: 18132
		protected GameObject _nullPanel;

		// Token: 0x040046D5 RID: 18133
		protected GameObject _hasPanel;

		// Token: 0x040046D6 RID: 18134
		protected GameObject _selectPanel;

		// Token: 0x040046D7 RID: 18135
		protected GameObject _jiaoBiaoPanel;

		// Token: 0x040046D8 RID: 18136
		protected Image _jiaoBiao;

		// Token: 0x040046D9 RID: 18137
		protected Image _qualityBg;

		// Token: 0x040046DA RID: 18138
		protected Image _qualityLine;

		// Token: 0x040046DB RID: 18139
		protected Image _icon;

		// Token: 0x040046DC RID: 18140
		protected Text _name;

		// Token: 0x040046DD RID: 18141
		protected Text _count;

		// Token: 0x040046DE RID: 18142
		private Dictionary<string, object> _objDict = new Dictionary<string, object>();

		// Token: 0x040046DF RID: 18143
		public bool HideTooltip;

		// Token: 0x040046E0 RID: 18144
		public UnityEvent OnLeftClick;

		// Token: 0x040046E1 RID: 18145
		public UnityEvent OnRightClick;
	}
}
