using System;
using script.NewLianDan;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x02000D48 RID: 3400
	public class DanLuSlot : BaseSlot
	{
		// Token: 0x060050D2 RID: 20690 RVA: 0x0021B22C File Offset: 0x0021942C
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

		// Token: 0x060050D3 RID: 20691 RVA: 0x0021B2BC File Offset: 0x002194BC
		public override void SetSlotData(object data)
		{
			base.SetSlotData(data);
			if (!this.IsInBag)
			{
				base.Get<Text>("Bg/耐久").SetText(string.Format("{0}/100", this.Item.Seid["NaiJiu"].I));
			}
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x0021B314 File Offset: 0x00219514
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

		// Token: 0x04005200 RID: 20992
		public bool IsInBag;
	}
}
