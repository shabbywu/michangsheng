using System;
using Tab;
using UnityEngine.EventSystems;

namespace Bag;

[Serializable]
public class EquipSlot : SlotBase
{
	public EquipSlotType EquipSlotType;

	public override void OnEndDrag(PointerEventData eventData)
	{
		if (CanDrag())
		{
			if (!IsIn && !DragMag.Inst.EndDrag())
			{
				SingletonMono<TabUIMag>.Instance.WuPingPanel.RmoveEquip(EquipSlotType);
			}
			DragMag.Inst.Clear();
		}
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		if (!eventData.dragging && (int)eventData.button == 1 && !IsNull())
		{
			SingletonMono<TabUIMag>.Instance.WuPingPanel.RmoveEquip(EquipSlotType);
			_selectPanel.SetActive(false);
			ToolTipsMag.Inst.Close();
		}
	}
}
