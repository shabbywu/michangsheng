using System;
using Bag;
using UnityEngine.EventSystems;

namespace PaiMai
{
	// Token: 0x0200070C RID: 1804
	public class PaiMaiSlot : SlotBase
	{
		// Token: 0x060039D5 RID: 14805 RVA: 0x0018C2A4 File Offset: 0x0018A4A4
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

		// Token: 0x060039D6 RID: 14806 RVA: 0x0018C330 File Offset: 0x0018A530
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

		// Token: 0x060039D7 RID: 14807 RVA: 0x0018C39D File Offset: 0x0018A59D
		public override bool CanDrag()
		{
			return !base.IsNull() && this.Item.CanSale && base.CanDrag();
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x0018C3BE File Offset: 0x0018A5BE
		public override void SetNull()
		{
			base.SetNull();
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
		}

		// Token: 0x040031ED RID: 12781
		public bool IsPlayer;

		// Token: 0x040031EE RID: 12782
		public bool IsInBag;
	}
}
