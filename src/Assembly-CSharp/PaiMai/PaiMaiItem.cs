using System;
using UnityEngine;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000A65 RID: 2661
	public class PaiMaiItem : MonoBehaviour
	{
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x00031076 File Offset: 0x0002F276
		// (set) Token: 0x06004490 RID: 17552 RVA: 0x0003107E File Offset: 0x0002F27E
		public PaiMaiAvatar Owner { get; private set; }

		// Token: 0x06004491 RID: 17553 RVA: 0x001D4E90 File Offset: 0x001D3090
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

		// Token: 0x06004492 RID: 17554 RVA: 0x001D4FBC File Offset: 0x001D31BC
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

		// Token: 0x04003C93 RID: 15507
		[SerializeField]
		private UIIconShow _curItem;

		// Token: 0x04003C94 RID: 15508
		[SerializeField]
		private Text _curPrice;

		// Token: 0x04003C95 RID: 15509
		[SerializeField]
		private Text _curAvatarName;

		// Token: 0x04003C96 RID: 15510
		[SerializeField]
		private Text _mayPrice;
	}
}
