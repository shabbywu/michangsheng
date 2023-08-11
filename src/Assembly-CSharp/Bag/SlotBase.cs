using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bag;

public class SlotBase : MonoBehaviour, ISlot, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public BaseItem Item;

	public BaseSkill Skill;

	public BagTianJieSkill TianJieSkill;

	public int Group;

	public bool IsCanDrag = true;

	public bool IsIn;

	public bool CanUse = true;

	public SlotType SlotType;

	public CanSlotType AcceptType;

	protected GameObject _nullPanel;

	protected GameObject _hasPanel;

	protected GameObject _selectPanel;

	protected GameObject _jiaoBiaoPanel;

	protected Image _jiaoBiao;

	protected Image _qualityBg;

	protected Image _qualityLine;

	protected Image _icon;

	protected Text _name;

	protected Text _count;

	private Dictionary<string, object> _objDict = new Dictionary<string, object>();

	public bool HideTooltip;

	public UnityEvent OnLeftClick;

	public UnityEvent OnRightClick;

	public virtual void SetSlotData(object data)
	{
		Item = null;
		Skill = null;
		if (data is BaseItem)
		{
			SetItem((BaseItem)data);
			SlotType = SlotType.物品;
		}
		else if (data is ActiveSkill)
		{
			SetActiveSkill((BaseSkill)data);
			SlotType = SlotType.技能;
		}
		else if (data is PassiveSkill)
		{
			SetPassiveSkill((BaseSkill)data);
			SlotType = SlotType.功法;
		}
		else if (data is BagTianJieSkill)
		{
			SetTianJieSkill((BagTianJieSkill)data);
			SlotType = SlotType.天劫秘术;
		}
		UpdateUI();
	}

	public void SetAccptType(CanSlotType slotType)
	{
		AcceptType = slotType;
	}

	protected virtual void SetItem(BaseItem item)
	{
		Item = item;
	}

	private void SetActiveSkill(BaseSkill activeSkill)
	{
		Skill = activeSkill;
	}

	private void SetPassiveSkill(BaseSkill passiveSkill)
	{
		Skill = passiveSkill;
	}

	private void SetTianJieSkill(BagTianJieSkill tianJieSkill)
	{
		TianJieSkill = tianJieSkill;
	}

	public virtual void SetNull()
	{
		Item = null;
		Skill = null;
		TianJieSkill = null;
		SlotType = SlotType.空;
		if ((Object)(object)_nullPanel == (Object)null)
		{
			InitUI();
		}
		_nullPanel.SetActive(true);
		_hasPanel.SetActive(false);
	}

	public bool IsNull()
	{
		return SlotType == SlotType.空;
	}

	public virtual void InitUI()
	{
		_nullPanel = Get("Null");
		_hasPanel = Get("HasItem");
		_selectPanel = Get("Selected");
		_jiaoBiaoPanel = Get("HasItem/LeftUpMask");
		_qualityBg = Get<Image>("HasItem/Quality");
		_qualityLine = Get<Image>("HasItem/Quality/QualityUp");
		_jiaoBiao = Get<Image>("HasItem/LeftUpMask/JiaoBiao");
		_icon = Get<Image>("HasItem/IconMask/Icon");
		_name = Get<Text>("HasItem/NameMask/NameText");
		_count = Get<Text>("HasItem/CountText");
	}

	public void UpdateUI()
	{
		if ((Object)(object)_nullPanel == (Object)null)
		{
			InitUI();
		}
		_nullPanel.SetActive(false);
		_hasPanel.SetActive(true);
		try
		{
			switch (SlotType)
			{
			case SlotType.技能:
			case SlotType.功法:
				UpdateSkillUI();
				break;
			case SlotType.物品:
				UpdateItemUI();
				break;
			case SlotType.天劫秘术:
				UpdateTianJieSkillUI();
				break;
			}
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"刷新格子出现异常:{arg}");
			BaseItem baseItem = BaseItem.Create(10000, 1, Guid.NewGuid().ToString(), null);
			switch (SlotType)
			{
			case SlotType.技能:
			case SlotType.功法:
				if (Skill != null)
				{
					baseItem.Desc1 += $"错误的技能ID:{Skill.Id}";
				}
				break;
			case SlotType.物品:
				if (Item != null)
				{
					PlayerEx.AddErrorItemID(Item.Id);
					baseItem.Desc1 += $"错误的物品ID:{Item.Id}";
				}
				break;
			case SlotType.天劫秘术:
				if (TianJieSkill != null && TianJieSkill.MiShu != null)
				{
					baseItem.Desc1 = baseItem.Desc1 + "错误的秘术ID:" + TianJieSkill.MiShu.id;
				}
				break;
			}
			SetSlotData(baseItem);
		}
	}

	private void UpdateTianJieSkillUI()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		((Component)_count).gameObject.SetActive(false);
		_icon.sprite = TianJieSkill.GetIconSprite();
		_qualityBg.sprite = TianJieSkill.GetQualitySprite();
		_qualityLine.sprite = TianJieSkill.GetQualityUpSprite();
		bool flag = false;
		if (TianJieSkill.MiShu.Type == 0)
		{
			if (TianJieSkill.IsGanYing)
			{
				flag = true;
			}
		}
		else
		{
			flag = true;
		}
		Color color = ColorEx.ItemQualityColor[TianJieSkill.BindSkill.GetImgQuality() - 1];
		if (flag)
		{
			SetName(TianJieSkill.MiShu.id, color);
		}
		else
		{
			SetName("???", color);
		}
		_jiaoBiaoPanel.SetActive(false);
	}

	private void UpdateItemUI()
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		if (Item.Count == 1)
		{
			((Component)_count).gameObject.SetActive(false);
		}
		else
		{
			((Component)_count).gameObject.SetActive(true);
			_count.SetText(Item.Count);
		}
		_icon.sprite = Item.GetIconSprite();
		_qualityBg.sprite = Item.GetQualitySprite();
		_qualityLine.sprite = Item.GetQualityUpSprite();
		SetName(Item.GetName(), ColorEx.ItemQualityColor[Item.GetImgQuality() - 1]);
		SetJiaoBiao(Item.GetJiaoBiaoType());
	}

	public void SetJiaoBiao(JiaoBiaoType type)
	{
		int num = (int)type;
		switch (type)
		{
		case JiaoBiaoType.无:
			_jiaoBiaoPanel.SetActive(false);
			break;
		case JiaoBiaoType.秘:
		case JiaoBiaoType.耐:
		case JiaoBiaoType.悟:
		case JiaoBiaoType.拍:
			_jiaoBiao.sprite = BagMag.Inst.JiaoBiaoDict[num.ToString()];
			_jiaoBiaoPanel.SetActive(true);
			break;
		}
	}

	private void UpdateSkillUI()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		((Component)_count).gameObject.SetActive(false);
		_icon.sprite = Skill.GetIconSprite();
		_qualityBg.sprite = Skill.GetQualitySprite();
		_qualityLine.sprite = Skill.GetQualityUpSprite();
		SetName(Skill.Name, ColorEx.ItemQualityColor[Skill.GetImgQuality() - 1]);
		_jiaoBiaoPanel.SetActive(false);
	}

	protected T Get<T>(string path)
	{
		string key = path + "_" + typeof(T).Name;
		if (!_objDict.ContainsKey(key))
		{
			Transform val = ((Component)this).gameObject.transform.Find(path);
			if ((Object)(object)val == (Object)null)
			{
				Debug.LogError((object)("不存在该对象,路径:" + path));
				return default(T);
			}
			T component = ((Component)val).GetComponent<T>();
			if (component == null)
			{
				Debug.LogError((object)("不存在该组件" + typeof(T).Name + ",路径:" + path));
				return default(T);
			}
			_objDict.Add(key, component);
		}
		return (T)_objDict[key];
	}

	protected GameObject Get(string path, bool showError = true)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		string key = path + "_GameObject";
		if (!_objDict.ContainsKey(key))
		{
			Transform val = ((Component)this).gameObject.transform.Find(path);
			if ((Object)(object)val == (Object)null)
			{
				if (showError)
				{
					Debug.LogError((object)("不存在该对象,路径:" + path));
				}
				return null;
			}
			_objDict.Add(key, ((Component)val).gameObject);
		}
		return (GameObject)_objDict[key];
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (DragMag.Inst.IsDraging)
		{
			DragMag.Inst.ToSlot = this;
		}
		if (SlotType == SlotType.空)
		{
			return;
		}
		IsIn = true;
		if (!eventData.dragging && !HideTooltip)
		{
			if ((Object)(object)ToolTipsMag.Inst == (Object)null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(((Component)NewUICanvas.Inst).transform);
			}
			if (SlotType == SlotType.物品)
			{
				ToolTipsMag.Inst.Show(Item);
			}
			else
			{
				ToolTipsMag.Inst.Show(Skill);
			}
		}
		_selectPanel.SetActive(true);
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if (DragMag.Inst.IsDraging)
		{
			DragMag.Inst.ToSlot = null;
		}
		if (SlotType != 0)
		{
			IsIn = false;
			_selectPanel.SetActive(false);
			if ((Object)(object)ToolTipsMag.Inst != (Object)null)
			{
				ToolTipsMag.Inst.Close();
			}
		}
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Invalid comparison between Unknown and I4
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Invalid comparison between Unknown and I4
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (eventData.dragging || IsNull())
		{
			return;
		}
		if ((int)eventData.button == 1 && CanUse)
		{
			DragMag.Inst.DragSlot = this;
			if (Item != null)
			{
				Item.Use();
				if (Item != null && Item.Count > 0)
				{
					UpdateItemUI();
				}
			}
			_selectPanel.SetActive(false);
			ToolTipsMag.Inst.Close();
		}
		if ((int)eventData.button == 1 && OnRightClick != null)
		{
			OnRightClick.Invoke();
		}
		if ((int)eventData.button == 0 && OnLeftClick != null)
		{
			OnLeftClick.Invoke();
		}
	}

	public void SetGrey(bool grey)
	{
		((Graphic)_icon).material = (grey ? GreyMatManager.Grey1 : null);
	}

	public Sprite GetIcon()
	{
		return _icon.sprite;
	}

	public void SetName(string targetName, string color)
	{
		_name.SetText(targetName, color);
	}

	public void SetName(string targetName, Color color)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_name.SetText(targetName, color.ColorToString());
	}

	private void OnDestroy()
	{
		if ((Object)(object)ToolTipsMag.Inst != (Object)null)
		{
			ToolTipsMag.Inst.Close();
		}
	}

	public virtual void OnBeginDrag(PointerEventData eventData)
	{
		if (CanDrag())
		{
			DragMag.Inst.StartDrag(this);
		}
	}

	public virtual void OnDrag(PointerEventData eventData)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (CanDrag())
		{
			DragMag.Inst.UpdatePostion(Vector2.op_Implicit(eventData.position));
		}
	}

	public virtual void OnEndDrag(PointerEventData eventData)
	{
		if (CanDrag())
		{
			DragMag.Inst.EndDrag();
		}
	}

	public virtual bool CanDrag()
	{
		if (IsNull())
		{
			return false;
		}
		if (Skill != null && Skill is ActiveSkill)
		{
			ActiveSkill activeSkill = (ActiveSkill)Skill;
			if (activeSkill.AttackType.Count > 0 && activeSkill.AttackType[0] >= 12)
			{
				return false;
			}
		}
		if (!IsCanDrag)
		{
			return false;
		}
		return true;
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
	}
}
