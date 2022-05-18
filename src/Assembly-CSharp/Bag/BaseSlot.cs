using System;
using UnityEngine.EventSystems;

namespace Bag
{
	// Token: 0x02000D3F RID: 3391
	public class BaseSlot : SlotBase
	{
		// Token: 0x06005089 RID: 20617 RVA: 0x001DF02C File Offset: 0x001DD22C
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

		// Token: 0x0600508A RID: 20618 RVA: 0x00039FE6 File Offset: 0x000381E6
		public override void OnPointerUp(PointerEventData eventData)
		{
			this._selectPanel.SetActive(false);
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x00030EE2 File Offset: 0x0002F0E2
		public override bool CanDrag()
		{
			return !base.IsNull() && this.Item.CanSale && base.CanDrag();
		}
	}
}
