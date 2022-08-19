using System;
using Bag;
using UnityEngine.EventSystems;

namespace script.Submit
{
	// Token: 0x020009D2 RID: 2514
	public class SubmitSlot : SlotBase
	{
		// Token: 0x060045D4 RID: 17876 RVA: 0x001D92A8 File Offset: 0x001D74A8
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

		// Token: 0x060045D5 RID: 17877 RVA: 0x001D932C File Offset: 0x001D752C
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
					SubmitUIMag.Inst.PutItem(this, null);
				}
				else
				{
					SubmitUIMag.Inst.BackItem(this, null);
				}
			}
			this._selectPanel.SetActive(false);
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x0018C39D File Offset: 0x0018A59D
		public override bool CanDrag()
		{
			return !base.IsNull() && this.Item.CanSale && base.CanDrag();
		}

		// Token: 0x04004741 RID: 18241
		public bool IsPlayer;

		// Token: 0x04004742 RID: 18242
		public bool IsInBag;
	}
}
