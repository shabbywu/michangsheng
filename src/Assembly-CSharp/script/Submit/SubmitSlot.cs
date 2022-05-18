using System;
using Bag;
using UnityEngine.EventSystems;

namespace script.Submit
{
	// Token: 0x02000ABB RID: 2747
	public class SubmitSlot : SlotBase
	{
		// Token: 0x06004635 RID: 17973 RVA: 0x001DF02C File Offset: 0x001DD22C
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

		// Token: 0x06004636 RID: 17974 RVA: 0x001DF0B0 File Offset: 0x001DD2B0
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

		// Token: 0x06004637 RID: 17975 RVA: 0x00030EE2 File Offset: 0x0002F0E2
		public override bool CanDrag()
		{
			return !base.IsNull() && this.Item.CanSale && base.CanDrag();
		}

		// Token: 0x04003E57 RID: 15959
		public bool IsPlayer;

		// Token: 0x04003E58 RID: 15960
		public bool IsInBag;
	}
}
