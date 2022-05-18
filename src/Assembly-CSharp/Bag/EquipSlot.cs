using System;
using Tab;
using UnityEngine.EventSystems;

namespace Bag
{
	// Token: 0x02000D40 RID: 3392
	[Serializable]
	public class EquipSlot : SlotBase
	{
		// Token: 0x0600508D RID: 20621 RVA: 0x00039FF4 File Offset: 0x000381F4
		public override void OnEndDrag(PointerEventData eventData)
		{
			if (!this.CanDrag())
			{
				return;
			}
			if (!this.IsIn && !DragMag.Inst.EndDrag())
			{
				SingletonMono<TabUIMag>.Instance.WuPingPanel.RmoveEquip(this.EquipSlotType);
			}
			DragMag.Inst.Clear();
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x00219FF0 File Offset: 0x002181F0
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				return;
			}
			if (eventData.button == 1 && !base.IsNull())
			{
				SingletonMono<TabUIMag>.Instance.WuPingPanel.RmoveEquip(this.EquipSlotType);
				this._selectPanel.SetActive(false);
				ToolTipsMag.Inst.Close();
			}
		}

		// Token: 0x040051CB RID: 20939
		public EquipSlotType EquipSlotType;
	}
}
