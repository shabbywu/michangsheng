using System;
using UnityEngine;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000715 RID: 1813
	public class PaiMaiItem : MonoBehaviour
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x0018D7B1 File Offset: 0x0018B9B1
		// (set) Token: 0x06003A04 RID: 14852 RVA: 0x0018D7B9 File Offset: 0x0018B9B9
		public PaiMaiAvatar Owner { get; private set; }

		// Token: 0x06003A05 RID: 14853 RVA: 0x0018D7C4 File Offset: 0x0018B9C4
		public void UpdateItem()
		{
			if (SingletonMono<PaiMaiUiMag>.Instance.CurShop == null)
			{
				Debug.LogError("当前商品为");
				return;
			}
			if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid.HasField("quality"))
			{
				this._curItem.SetItem(SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid);
			}
			else
			{
				this._curItem.SetItem(SingletonMono<PaiMaiUiMag>.Instance.CurShop.ShopId);
				if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid.HasField("NaiJiu"))
				{
					this._curItem.tmpItem.Seid = SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid;
				}
			}
			this._curItem.CanDrag = false;
			this._curItem.Count = SingletonMono<PaiMaiUiMag>.Instance.CurShop.Count;
			this._mayPrice.text = SingletonMono<PaiMaiUiMag>.Instance.CurShop.MayPrice.ToString();
			this._curAvatarName.text = "";
			this._curPrice.text = SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice.ToString();
			this.Owner = null;
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x0018D8F0 File Offset: 0x0018BAF0
		public void UpdateUI()
		{
			this.Owner = SingletonMono<PaiMaiUiMag>.Instance.CurAvatar;
			this._curPrice.text = SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice.ToString();
			if (this.Owner != null)
			{
				this._curAvatarName.text = this.Owner.Name;
				return;
			}
			this._curAvatarName.text = "";
		}

		// Token: 0x0400320C RID: 12812
		[SerializeField]
		private UIIconShow _curItem;

		// Token: 0x0400320D RID: 12813
		[SerializeField]
		private Text _curPrice;

		// Token: 0x0400320E RID: 12814
		[SerializeField]
		private Text _curAvatarName;

		// Token: 0x0400320F RID: 12815
		[SerializeField]
		private Text _mayPrice;
	}
}
