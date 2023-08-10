using System;
using Bag;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITianJieSkillSlot : SlotBase
{
	public Action<UITianJieSkillSlot> OnLiftClick;

	public new Action<UITianJieSkillSlot> OnRightClick;

	public bool IsSelected;

	public bool IsEquipSlot;

	public void SetSelected(bool value)
	{
		IsSelected = value;
		_selectPanel.SetActive(value);
	}

	public override void OnPointerEnter(PointerEventData eventData)
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
		if (TianJieSkill.IsLingWu && !eventData.dragging)
		{
			if ((Object)(object)ToolTipsMag.Inst == (Object)null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(((Component)NewUICanvas.Inst).transform);
			}
			ToolTipsMag.Inst.Show(TianJieSkill.BindSkill);
		}
		_selectPanel.SetActive(true);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		if (IsSelected)
		{
			_selectPanel.SetActive(true);
		}
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Invalid comparison between Unknown and I4
		if (!IsSelected)
		{
			_selectPanel.SetActive(false);
		}
		if ((int)eventData.button == 0 && OnLiftClick != null)
		{
			OnLiftClick(this);
		}
		if ((int)eventData.button == 1 && OnRightClick != null)
		{
			OnRightClick(this);
		}
	}

	public override void OnEndDrag(PointerEventData eventData)
	{
		if (CanDrag())
		{
			bool flag = DragMag.Inst.EndDrag();
			if (IsEquipSlot && !flag)
			{
				UIDuJieZhunBei.Inst.ClearTiaoZhengSlotByID(TianJieSkill.MiShu.id);
				UIDuJieZhunBei.Inst.SaveTiaoZheng();
			}
		}
	}

	public override bool CanDrag()
	{
		if (IsNull())
		{
			return false;
		}
		return base.CanDrag();
	}
}
