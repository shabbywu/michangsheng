using System;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace script.ExchangeMeeting.UI.Base
{
	// Token: 0x02000A38 RID: 2616
	public class ExchangeSlotB : ExchangeSlot
	{
		// Token: 0x060047F6 RID: 18422 RVA: 0x001E6454 File Offset: 0x001E4654
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (!this.IsInBag)
			{
				if (eventData.button == null)
				{
					IExchangeUIMag.Inst.PlayerBag.ToSlot = this;
					IExchangeUIMag.Inst.PlayerBag.Open();
				}
				else if (eventData.button == 1 && !base.IsNull())
				{
					IExchangeUIMag.Inst.PublishCtr.BackGiveItem(this);
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
			if (IExchangeUIMag.Inst.PlayerBag.ToSlot == null)
			{
				Debug.LogError("IExchangeUIMag.Inst.PlayerBag.ToSlot is null");
				return;
			}
			IExchangeUIMag.Inst.PublishCtr.PutGiveItem(this);
			IExchangeUIMag.Inst.PlayerBag.Hide();
			this._selectPanel.SetActive(false);
		}
	}
}
