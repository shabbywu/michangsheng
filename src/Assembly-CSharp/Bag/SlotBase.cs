using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x02000D43 RID: 3395
	public class SlotBase : MonoBehaviour, ISlot, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x06005098 RID: 20632 RVA: 0x0021A044 File Offset: 0x00218244
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

		// Token: 0x06005099 RID: 20633 RVA: 0x0003A0AB File Offset: 0x000382AB
		public void SetAccptType(CanSlotType slotType)
		{
			this.AcceptType = slotType;
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x0003A0B4 File Offset: 0x000382B4
		protected virtual void SetItem(BaseItem item)
		{
			this.Item = item;
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x0003A0BD File Offset: 0x000382BD
		private void SetActiveSkill(BaseSkill activeSkill)
		{
			this.Skill = activeSkill;
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x0003A0BD File Offset: 0x000382BD
		private void SetPassiveSkill(BaseSkill passiveSkill)
		{
			this.Skill = passiveSkill;
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x0003A0C6 File Offset: 0x000382C6
		private void SetTianJieSkill(BagTianJieSkill tianJieSkill)
		{
			this.TianJieSkill = tianJieSkill;
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x0021A0D8 File Offset: 0x002182D8
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

		// Token: 0x0600509F RID: 20639 RVA: 0x0003A0CF File Offset: 0x000382CF
		public bool IsNull()
		{
			return this.SlotType == SlotType.空;
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x0021A130 File Offset: 0x00218330
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

		// Token: 0x060050A1 RID: 20641 RVA: 0x0021A1EC File Offset: 0x002183EC
		public void UpdateUI()
		{
			if (this._nullPanel == null)
			{
				this.InitUI();
			}
			this._nullPanel.SetActive(false);
			this._hasPanel.SetActive(true);
			switch (this.SlotType)
			{
			case SlotType.物品:
				this.UpdateItemUI();
				return;
			case SlotType.技能:
			case SlotType.功法:
				this.UpdateSkillUI();
				return;
			case SlotType.天劫秘术:
				this.UpdateTianJieSkillUI();
				return;
			default:
				return;
			}
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x0021A25C File Offset: 0x0021845C
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

		// Token: 0x060050A3 RID: 20643 RVA: 0x0021A334 File Offset: 0x00218534
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

		// Token: 0x060050A4 RID: 20644 RVA: 0x0021A40C File Offset: 0x0021860C
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

		// Token: 0x060050A5 RID: 20645 RVA: 0x0021A460 File Offset: 0x00218660
		private void UpdateSkillUI()
		{
			this._count.gameObject.SetActive(false);
			this._icon.sprite = this.Skill.GetIconSprite();
			this._qualityBg.sprite = this.Skill.GetQualitySprite();
			this._qualityLine.sprite = this.Skill.GetQualityUpSprite();
			this.SetName(this.Skill.Name, ColorEx.ItemQualityColor[this.Skill.GetImgQuality() - 1]);
			this._jiaoBiaoPanel.SetActive(false);
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x0021A4F4 File Offset: 0x002186F4
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

		// Token: 0x060050A7 RID: 20647 RVA: 0x0021A5C4 File Offset: 0x002187C4
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

		// Token: 0x060050A8 RID: 20648 RVA: 0x0021A640 File Offset: 0x00218840
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

		// Token: 0x060050A9 RID: 20649 RVA: 0x0021A6E8 File Offset: 0x002188E8
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

		// Token: 0x060050AA RID: 20650 RVA: 0x0021A740 File Offset: 0x00218940
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

		// Token: 0x060050AB RID: 20651 RVA: 0x0003A0DA File Offset: 0x000382DA
		public void SetGrey(bool grey)
		{
			this._icon.material = (grey ? GreyMatManager.Grey1 : null);
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x0003A0F2 File Offset: 0x000382F2
		public Sprite GetIcon()
		{
			return this._icon.sprite;
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x0003A0FF File Offset: 0x000382FF
		public void SetName(string targetName, string color)
		{
			this._name.SetText(targetName, color);
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x0003A10E File Offset: 0x0003830E
		public void SetName(string targetName, Color color)
		{
			this._name.SetText(targetName, color.ColorToString());
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x00017634 File Offset: 0x00015834
		private void OnDestroy()
		{
			if (ToolTipsMag.Inst != null)
			{
				ToolTipsMag.Inst.Close();
			}
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0003A122 File Offset: 0x00038322
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.CanDrag())
			{
				return;
			}
			DragMag.Inst.StartDrag(this);
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x0003A138 File Offset: 0x00038338
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.CanDrag())
			{
				return;
			}
			DragMag.Inst.UpdatePostion(eventData.position);
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0003A158 File Offset: 0x00038358
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.CanDrag())
			{
				return;
			}
			DragMag.Inst.EndDrag();
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x0021A7FC File Offset: 0x002189FC
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

		// Token: 0x060050B4 RID: 20660 RVA: 0x000042DD File Offset: 0x000024DD
		public void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x040051CD RID: 20941
		public BaseItem Item;

		// Token: 0x040051CE RID: 20942
		public BaseSkill Skill;

		// Token: 0x040051CF RID: 20943
		public BagTianJieSkill TianJieSkill;

		// Token: 0x040051D0 RID: 20944
		public int Group;

		// Token: 0x040051D1 RID: 20945
		public bool IsCanDrag = true;

		// Token: 0x040051D2 RID: 20946
		public bool IsIn;

		// Token: 0x040051D3 RID: 20947
		public bool CanUse = true;

		// Token: 0x040051D4 RID: 20948
		public SlotType SlotType;

		// Token: 0x040051D5 RID: 20949
		public CanSlotType AcceptType;

		// Token: 0x040051D6 RID: 20950
		protected GameObject _nullPanel;

		// Token: 0x040051D7 RID: 20951
		protected GameObject _hasPanel;

		// Token: 0x040051D8 RID: 20952
		protected GameObject _selectPanel;

		// Token: 0x040051D9 RID: 20953
		protected GameObject _jiaoBiaoPanel;

		// Token: 0x040051DA RID: 20954
		protected Image _jiaoBiao;

		// Token: 0x040051DB RID: 20955
		protected Image _qualityBg;

		// Token: 0x040051DC RID: 20956
		protected Image _qualityLine;

		// Token: 0x040051DD RID: 20957
		protected Image _icon;

		// Token: 0x040051DE RID: 20958
		protected Text _name;

		// Token: 0x040051DF RID: 20959
		protected Text _count;

		// Token: 0x040051E0 RID: 20960
		private Dictionary<string, object> _objDict = new Dictionary<string, object>();

		// Token: 0x040051E1 RID: 20961
		public bool HideTooltip;

		// Token: 0x040051E2 RID: 20962
		public UnityEvent OnLeftClick;

		// Token: 0x040051E3 RID: 20963
		public UnityEvent OnRightClick;
	}
}
