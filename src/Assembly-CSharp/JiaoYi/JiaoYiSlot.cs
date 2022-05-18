using System;
using Bag;
using UnityEngine.EventSystems;

namespace JiaoYi
{
	// Token: 0x02000A90 RID: 2704
	public class JiaoYiSlot : SlotBase
	{
		// Token: 0x06004559 RID: 17753 RVA: 0x001DAAE8 File Offset: 0x001D8CE8
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
				ToolTipsMag.Inst.Show(this.Item, this.Item.GetJiaoYiPrice(JiaoYiUIMag.Inst.NpcId, this.IsPlayer, false), this.IsPlayer);
			}
			this._selectPanel.SetActive(true);
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x001DAB90 File Offset: 0x001D8D90
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				return;
			}
			if (base.IsNull())
			{
				return;
			}
			if (!this.Item.CanSale)
			{
				return;
			}
			if (eventData.button == 1)
			{
				if (this.IsInBag)
				{
					JiaoYiUIMag.Inst.SellItem(this, null);
				}
				else
				{
					JiaoYiUIMag.Inst.BackItem(this, null);
				}
			}
			this._selectPanel.SetActive(false);
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x00030EE2 File Offset: 0x0002F0E2
		public override bool CanDrag()
		{
			return !base.IsNull() && this.Item.CanSale && base.CanDrag();
		}

		// Token: 0x04003D81 RID: 15745
		public bool IsPlayer;

		// Token: 0x04003D82 RID: 15746
		public bool IsInBag;
	}
}
