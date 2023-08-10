using UnityEngine;
using UnityEngine.EventSystems;

namespace Bag;

public class BaseSlot : SlotBase
{
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
		if (!eventData.dragging)
		{
			if ((Object)(object)ToolTipsMag.Inst == (Object)null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(((Component)NewUICanvas.Inst).transform);
			}
			ToolTipsMag.Inst.Show(Item);
		}
		_selectPanel.SetActive(true);
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		_selectPanel.SetActive(false);
	}

	public override bool CanDrag()
	{
		if (IsNull())
		{
			return false;
		}
		if (!Item.CanSale)
		{
			return false;
		}
		return base.CanDrag();
	}
}
