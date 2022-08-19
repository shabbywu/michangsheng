using System;
using Bag;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace script.ExchangeMeeting.UI.Base
{
	// Token: 0x02000A37 RID: 2615
	public class ExchangeSlot : BaseSlot
	{
		// Token: 0x060047EF RID: 18415 RVA: 0x001E62CC File Offset: 0x001E44CC
		public override void InitUI()
		{
			if (!this.IsInBag)
			{
				this.bg = base.Get<Image>("Null/BG");
			}
			base.InitUI();
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanDrag()
		{
			return false;
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x001E62ED File Offset: 0x001E44ED
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.IsInBag)
			{
				this.bg.sprite = this.mouseDownSprite;
			}
			base.OnPointerDown(eventData);
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x001E6310 File Offset: 0x001E4510
		public override void OnPointerEnter(PointerEventData eventData)
		{
			this.IsIn = true;
			if (!this.IsInBag)
			{
				this.bg.sprite = this.mouseEnterSprite;
			}
			if (base.IsNull())
			{
				return;
			}
			if (this.IsInBag)
			{
				this._selectPanel.SetActive(true);
			}
			if (ToolTipsMag.Inst == null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(NewUICanvas.Inst.transform);
			}
			ToolTipsMag.Inst.Show(this.Item);
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x001E6398 File Offset: 0x001E4598
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (!this.IsInBag)
			{
				if (eventData.button == null)
				{
					IExchangeUIMag.Inst.NeedBag.Open();
				}
				else if (eventData.button == 1 && !base.IsNull())
				{
					IExchangeUIMag.Inst.PublishCtr.BackNeedItem();
				}
				this.bg.sprite = this.nomalSprite;
				return;
			}
			if (base.IsNull())
			{
				return;
			}
			IExchangeUIMag.Inst.PublishCtr.PutNeedItem(this.Item);
			IExchangeUIMag.Inst.NeedBag.Hide();
			this._selectPanel.SetActive(false);
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x001E6431 File Offset: 0x001E4631
		public override void OnPointerExit(PointerEventData eventData)
		{
			if (!this.IsInBag)
			{
				this.bg.sprite = this.nomalSprite;
			}
			base.OnPointerExit(eventData);
		}

		// Token: 0x040048B8 RID: 18616
		public Sprite nomalSprite;

		// Token: 0x040048B9 RID: 18617
		public Sprite mouseEnterSprite;

		// Token: 0x040048BA RID: 18618
		public Sprite mouseDownSprite;

		// Token: 0x040048BB RID: 18619
		protected Image bg;

		// Token: 0x040048BC RID: 18620
		public bool IsInBag;
	}
}
