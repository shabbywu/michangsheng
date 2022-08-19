using System;
using UnityEngine.EventSystems;

namespace Bag
{
	// Token: 0x020009B7 RID: 2487
	public class BaseSlot : SlotBase
	{
		// Token: 0x06004526 RID: 17702 RVA: 0x001D5BC4 File Offset: 0x001D3DC4
		public override void OnPointerEnter(PointerEventData eventData)
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
			if (!eventData.dragging)
			{
				if (ToolTipsMag.Inst == null)
				{
					ResManager.inst.LoadPrefab("ToolTips").Inst(NewUICanvas.Inst.transform);
				}
				ToolTipsMag.Inst.Show(this.Item);
			}
			this._selectPanel.SetActive(true);
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x001D5C48 File Offset: 0x001D3E48
		public override void OnPointerUp(PointerEventData eventData)
		{
			this._selectPanel.SetActive(false);
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x0018C39D File Offset: 0x0018A59D
		public override bool CanDrag()
		{
			return !base.IsNull() && this.Item.CanSale && base.CanDrag();
		}
	}
}
