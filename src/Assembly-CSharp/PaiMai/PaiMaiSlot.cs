using Bag;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PaiMai;

public class PaiMaiSlot : SlotBase
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
			ToolTipsMag.Inst.Show(Item, IsPlayer);
		}
		_selectPanel.SetActive(true);
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		if (eventData.dragging || IsNull() || !IsPlayer || !Item.CanSale)
		{
			return;
		}
		if ((int)eventData.button == 1)
		{
			if (IsInBag)
			{
				NewPaiMaiJoin.Inst.PutItem(this);
			}
			else
			{
				NewPaiMaiJoin.Inst.BackItem(this);
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

	public override void SetNull()
	{
		base.SetNull();
		if (!((Component)this).gameObject.activeSelf)
		{
			((Component)this).gameObject.SetActive(true);
		}
	}
}
