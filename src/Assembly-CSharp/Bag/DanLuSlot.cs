using System;
using script.NewLianDan;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x020009BF RID: 2495
	public class DanLuSlot : BaseSlot
	{
		// Token: 0x0600456D RID: 17773 RVA: 0x001D7284 File Offset: 0x001D5484
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (this.IsInBag)
			{
				if (base.IsNull())
				{
					return;
				}
				LianDanUIMag.Instance.LianDanPanel.PutDanLu(this);
				LianDanUIMag.Instance.DanLuBag.Close();
				LianDanUIMag.Instance.PutDanLuPanel.Hide();
				LianDanUIMag.Instance.LianDanPanel.Show();
			}
			else if (eventData.button == null)
			{
				LianDanUIMag.Instance.DanLuBag.UpdateItem(false);
				LianDanUIMag.Instance.DanLuBag.Open();
			}
			this._selectPanel.SetActive(false);
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x001D7314 File Offset: 0x001D5514
		public override void SetSlotData(object data)
		{
			base.SetSlotData(data);
			if (!this.IsInBag)
			{
				base.Get<Text>("Bg/耐久").SetText(string.Format("{0}/100", this.Item.Seid["NaiJiu"].I));
			}
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x001D736C File Offset: 0x001D556C
		public void UpdateNaiJiu()
		{
			if (!this.IsInBag)
			{
				if (base.IsNull())
				{
					base.Get<Text>("Bg/耐久").SetText("0/100");
					return;
				}
				base.Get<Text>("Bg/耐久").SetText(string.Format("{0}/100", this.Item.Seid["NaiJiu"].I));
			}
		}

		// Token: 0x040046FC RID: 18172
		public bool IsInBag;
	}
}
