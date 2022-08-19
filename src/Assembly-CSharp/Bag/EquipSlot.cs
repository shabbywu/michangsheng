using System;
using Tab;
using UnityEngine.EventSystems;

namespace Bag
{
	// Token: 0x020009B8 RID: 2488
	[Serializable]
	public class EquipSlot : SlotBase
	{
		// Token: 0x0600452A RID: 17706 RVA: 0x001D5C56 File Offset: 0x001D3E56
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

		// Token: 0x0600452B RID: 17707 RVA: 0x001D5C94 File Offset: 0x001D3E94
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

		// Token: 0x040046C9 RID: 18121
		public EquipSlotType EquipSlotType;
	}
}
