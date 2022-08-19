using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A71 RID: 2673
	public class StoreCell : MonoBehaviour
	{
		// Token: 0x06004B21 RID: 19233 RVA: 0x001FF340 File Offset: 0x001FD540
		private void Update()
		{
			if (Singleton.store.store[this.storeID].itemID != -1)
			{
				this.Icon.GetComponent<UITexture>().mainTexture = Singleton.store.store[this.storeID].itemIcon;
				this.Price.GetComponent<UILabel>().text = Singleton.store.store[this.storeID].itemPrice.ToString();
				this.Name.GetComponent<UILabel>().text = Singleton.store.store[this.storeID].itemNameCN;
			}
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x001FF3F0 File Offset: 0x001FD5F0
		private void OnHover(bool isOver)
		{
			if (isOver)
			{
				Singleton.inventory.Show_Tooltip(Singleton.store.store[this.storeID], 0, 0);
				Singleton.inventory.showTooltip = true;
				return;
			}
			Singleton.inventory.showTooltip = false;
		}

		// Token: 0x04004A44 RID: 19012
		public GameObject Icon;

		// Token: 0x04004A45 RID: 19013
		public GameObject Price;

		// Token: 0x04004A46 RID: 19014
		public GameObject Name;

		// Token: 0x04004A47 RID: 19015
		public int storeID;
	}
}
