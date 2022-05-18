using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000DA0 RID: 3488
	public class StoreCell : MonoBehaviour
	{
		// Token: 0x06005435 RID: 21557 RVA: 0x0023102C File Offset: 0x0022F22C
		private void Update()
		{
			if (Singleton.store.store[this.storeID].itemID != -1)
			{
				this.Icon.GetComponent<UITexture>().mainTexture = Singleton.store.store[this.storeID].itemIcon;
				this.Price.GetComponent<UILabel>().text = Singleton.store.store[this.storeID].itemPrice.ToString();
				this.Name.GetComponent<UILabel>().text = Singleton.store.store[this.storeID].itemNameCN;
			}
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x0003C420 File Offset: 0x0003A620
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

		// Token: 0x040053E9 RID: 21481
		public GameObject Icon;

		// Token: 0x040053EA RID: 21482
		public GameObject Price;

		// Token: 0x040053EB RID: 21483
		public GameObject Name;

		// Token: 0x040053EC RID: 21484
		public int storeID;
	}
}
