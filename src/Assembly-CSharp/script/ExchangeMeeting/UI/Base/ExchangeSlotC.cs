using System;
using Bag;
using script.ExchangeMeeting.UI.Interface;
using script.ExchangeMeeting.UI.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace script.ExchangeMeeting.UI.Base
{
	// Token: 0x02000A39 RID: 2617
	public class ExchangeSlotC : ExchangeSlot
	{
		// Token: 0x060047F8 RID: 18424 RVA: 0x001E6535 File Offset: 0x001E4735
		public void Init(SysExchangeDataUI ui)
		{
			this.UI = ui;
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x001E6540 File Offset: 0x001E4740
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (!this.IsInBag)
			{
				if (eventData.button == null)
				{
					IExchangeUIMag.Inst.SubmitBag.ToSlot = this;
					IExchangeUIMag.Inst.SubmitBag.FiddlerAction = new Func<BaseItem, bool>(this.UI.IsNeedItem);
					IExchangeUIMag.Inst.SubmitBag.Open();
					IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI = this.UI;
				}
				else if (eventData.button == 1 && !base.IsNull())
				{
					IExchangeUIMag.Inst.SubmitBag.RemoveTempItem(this.Item.Uid, this.Item.Count);
					this.SetNull();
					this.UI.UpdateCanSubmit();
				}
				this.bg.sprite = this.nomalSprite;
				return;
			}
			if (base.IsNull())
			{
				return;
			}
			if (IExchangeUIMag.Inst == null)
			{
				Debug.LogError("IExchangeUIMag.Inst is null");
				return;
			}
			if (IExchangeUIMag.Inst.SubmitBag.ToSlot == null)
			{
				Debug.LogError("IExchangeUIMag.Inst.SubmitBag.ToSlot is null");
				return;
			}
			if (IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI == null)
			{
				Debug.LogError("IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI is null");
				return;
			}
			int putNum = IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI.GetPutNum(this.Item);
			if (putNum > 0 && PlayerEx.Player.getItemNum(this.Item.Id) >= putNum)
			{
				BaseItem baseItem = this.Item.Clone();
				baseItem.Count = putNum;
				IExchangeUIMag.Inst.SubmitBag.ToSlot.SetSlotData(baseItem);
				IExchangeUIMag.Inst.SubmitBag.RemoveTempItem(baseItem.Uid, putNum);
				IExchangeUIMag.Inst.SubmitBag.Hide();
				IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI.UpdateCanSubmit();
			}
			else
			{
				UIPopTip.Inst.Pop("物品数量不足，请重新放入", PopTipIconType.叹号);
				Debug.LogError("物品数量异常");
			}
			this._selectPanel.SetActive(false);
		}

		// Token: 0x040048BD RID: 18621
		public SysExchangeDataUI UI;
	}
}
