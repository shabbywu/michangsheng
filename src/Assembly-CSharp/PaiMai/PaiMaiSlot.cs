using System;
using Bag;
using UnityEngine.EventSystems;

namespace PaiMai
{
	// Token: 0x02000A5C RID: 2652
	public class PaiMaiSlot : SlotBase
	{
		// Token: 0x06004461 RID: 17505 RVA: 0x001D3B14 File Offset: 0x001D1D14
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
				ToolTipsMag.Inst.Show(this.Item, this.IsPlayer);
			}
			this._selectPanel.SetActive(true);
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x001D3BA0 File Offset: 0x001D1DA0
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
			if (!this.IsPlayer)
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
					NewPaiMaiJoin.Inst.PutItem(this, null);
				}
				else
				{
					NewPaiMaiJoin.Inst.BackItem(this, null);
				}
			}
			this._selectPanel.SetActive(false);
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x00030EE2 File Offset: 0x0002F0E2
		public override bool CanDrag()
		{
			return !base.IsNull() && this.Item.CanSale && base.CanDrag();
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x00030F03 File Offset: 0x0002F103
		public override void SetNull()
		{
			base.SetNull();
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
		}

		// Token: 0x04003C74 RID: 15476
		public bool IsPlayer;

		// Token: 0x04003C75 RID: 15477
		public bool IsInBag;
	}
}
