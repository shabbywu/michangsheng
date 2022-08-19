using System;
using Bag;
using UnityEngine.EventSystems;

namespace JiaoYi
{
	// Token: 0x02000730 RID: 1840
	public class JiaoYiSlot : SlotBase
	{
		// Token: 0x06003A9F RID: 15007 RVA: 0x00192FB8 File Offset: 0x001911B8
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

		// Token: 0x06003AA0 RID: 15008 RVA: 0x00193060 File Offset: 0x00191260
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

		// Token: 0x06003AA1 RID: 15009 RVA: 0x0018C39D File Offset: 0x0018A59D
		public override bool CanDrag()
		{
			return !base.IsNull() && this.Item.CanSale && base.CanDrag();
		}

		// Token: 0x040032D0 RID: 13008
		public bool IsPlayer;

		// Token: 0x040032D1 RID: 13009
		public bool IsInBag;
	}
}
