using System;
using Bag;
using UnityEngine.EventSystems;

namespace LianQi
{
	// Token: 0x02000720 RID: 1824
	public class LianQiSlot : SlotBase
	{
		// Token: 0x06003A42 RID: 14914 RVA: 0x0019032C File Offset: 0x0018E52C
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

		// Token: 0x06003A43 RID: 14915 RVA: 0x001903A0 File Offset: 0x0018E5A0
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

		// Token: 0x06003A44 RID: 14916 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanDrag()
		{
			return false;
		}

		// Token: 0x04003271 RID: 12913
		public bool IsPlayer;

		// Token: 0x04003272 RID: 12914
		public bool IsInBag;
	}
}
