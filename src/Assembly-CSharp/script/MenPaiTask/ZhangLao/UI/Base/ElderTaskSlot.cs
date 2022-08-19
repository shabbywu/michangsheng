using System;
using Bag;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace script.MenPaiTask.ZhangLao.UI.Base
{
	// Token: 0x02000A14 RID: 2580
	public class ElderTaskSlot : BaseSlot
	{
		// Token: 0x06004768 RID: 18280 RVA: 0x001E38F8 File Offset: 0x001E1AF8
		public override void InitUI()
		{
			if (!this.IsInBag)
			{
				this.bg = base.Get<Image>("Null/BG");
			}
			base.InitUI();
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanDrag()
		{
			return false;
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x001E3919 File Offset: 0x001E1B19
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.IsInBag)
			{
				this.bg.sprite = this.mouseDownSprite;
			}
			base.OnPointerDown(eventData);
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x001E393C File Offset: 0x001E1B3C
		public override void OnPointerEnter(PointerEventData eventData)
		{
			this.IsIn = true;
			if (!this.IsInBag)
			{
				this.bg.sprite = this.mouseDownSprite;
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

		// Token: 0x0600476C RID: 18284 RVA: 0x001E39C4 File Offset: 0x001E1BC4
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (!this.IsInBag)
			{
				if (eventData.button == null)
				{
					ElderTaskUIMag.Inst.Bag.ToSlot = this;
					ElderTaskUIMag.Inst.Bag.Open();
				}
				else if (eventData.button == 1 && !base.IsNull())
				{
					ElderTaskUIMag.Inst.CreateElderTaskUI.Ctr.BackItem(this);
				}
				this.bg.sprite = this.nomalSprite;
				return;
			}
			if (base.IsNull())
			{
				return;
			}
			ElderTaskUIMag.Inst.CreateElderTaskUI.Ctr.PutItem(this);
			this._selectPanel.SetActive(false);
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x001E3A64 File Offset: 0x001E1C64
		public override void OnPointerExit(PointerEventData eventData)
		{
			if (!this.IsInBag)
			{
				this.bg.sprite = this.nomalSprite;
			}
			base.OnPointerExit(eventData);
		}

		// Token: 0x0400487F RID: 18559
		public Sprite nomalSprite;

		// Token: 0x04004880 RID: 18560
		public Sprite mouseEnterSprite;

		// Token: 0x04004881 RID: 18561
		public Sprite mouseDownSprite;

		// Token: 0x04004882 RID: 18562
		private Image bg;

		// Token: 0x04004883 RID: 18563
		public bool IsInBag;
	}
}
