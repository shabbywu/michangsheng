using System;
using Bag;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;

namespace script.ExchangeMeeting.UI.UI
{
	// Token: 0x02000A24 RID: 2596
	public sealed class PublishDataUI : IPublishDataUI
	{
		// Token: 0x060047A1 RID: 18337 RVA: 0x001E4C44 File Offset: 0x001E2E44
		public PublishDataUI(GameObject gameObject)
		{
			this._go = gameObject;
			this.Init();
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x001E4C59 File Offset: 0x001E2E59
		protected override void Init()
		{
			base.Init();
			this.DisablePublish = base.Get("禁止发布", true);
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x001E4C74 File Offset: 0x001E2E74
		public override void Clear()
		{
			this.NeedItem.SetNull();
			foreach (BaseSlot baseSlot in this.GiveItems)
			{
				baseSlot.SetNull();
			}
			this.抽成.SetText("0");
			this.Btn.transform.parent.gameObject.SetActive(false);
			this.DisablePublish.SetActive(true);
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x001E4D08 File Offset: 0x001E2F08
		public override void UpdateUI()
		{
			base.UpdateUI();
			if (IExchangeUIMag.Inst == null)
			{
				Debug.LogError("IExchangeUIMag.Inst is null In PublishDataUI UpdateUI");
				return;
			}
			if (this.Btn == null || this.DisablePublish == null)
			{
				Debug.LogError("Btn or DisablePublish is null In PublishDataUI UpdateUI");
				return;
			}
			if (IExchangeUIMag.Inst.PublishCtr.CheckCanClickPublish())
			{
				this.Btn.transform.parent.gameObject.SetActive(true);
				this.DisablePublish.SetActive(false);
			}
			else
			{
				this.Btn.transform.parent.gameObject.SetActive(false);
				this.DisablePublish.SetActive(true);
			}
			if (this.NeedItem == null || this.NeedItem.IsNull())
			{
				this.DrawMoney = 0;
			}
			else
			{
				this.DrawMoney = this.NeedItem.Item.BasePrice * 2 / 100;
			}
			this.抽成.SetText(this.DrawMoney.ToString());
		}

		// Token: 0x04004896 RID: 18582
		public GameObject DisablePublish;
	}
}
