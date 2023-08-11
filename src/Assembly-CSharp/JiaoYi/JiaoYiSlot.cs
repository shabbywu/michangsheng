using Bag;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JiaoYi;

public class JiaoYiSlot : SlotBase
{
	public bool IsPlayer;

	public bool IsInBag;

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
			ToolTipsMag.Inst.Show(Item, Item.GetJiaoYiPrice(JiaoYiUIMag.Inst.NpcId, IsPlayer), IsPlayer);
		}
		_selectPanel.SetActive(true);
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Invalid comparison between Unknown and I4
		if (eventData.dragging || IsNull() || !Item.CanSale)
		{
			return;
		}
		if ((int)eventData.button == 1)
		{
			if (IsInBag)
			{
				JiaoYiUIMag.Inst.SellItem(this);
			}
			else
			{
				JiaoYiUIMag.Inst.BackItem(this);
			}
		}
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
