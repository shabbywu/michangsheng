using System;
using Bag;
using UnityEngine.EventSystems;

namespace LianQi
{
	// Token: 0x02000A75 RID: 2677
	public class LianQiSlot : SlotBase
	{
		// Token: 0x060044DF RID: 17631 RVA: 0x001D79E8 File Offset: 0x001D5BE8
		public override void OnPointerEnter(PointerEventData eventData)
		{
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

		// Token: 0x060044E0 RID: 17632 RVA: 0x001D7A5C File Offset: 0x001D5C5C
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				return;
			}
			if (base.IsNull() && this.IsInBag)
			{
				return;
			}
			if (this.IsInBag)
			{
				LianQiTotalManager.inst.PutItem(this);
			}
			else if (eventData.button == null)
			{
				LianQiTotalManager.inst.ToSlot = this;
				LianQiTotalManager.inst.OpenBag();
			}
			else if (eventData.button == 1)
			{
				if (base.IsNull())
				{
					return;
				}
				LianQiTotalManager.inst.Bag.AddTempItem(this.Item.Clone(), 1);
				this.SetNull();
				LianQiTotalManager.inst.putCaiLiaoCallBack();
			}
			this._selectPanel.SetActive(false);
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanDrag()
		{
			return false;
		}

		// Token: 0x04003D09 RID: 15625
		public bool IsPlayer;

		// Token: 0x04003D0A RID: 15626
		public bool IsInBag;
	}
}
