using System;
using System.Collections.Generic;

namespace UltimateSurvival
{
	// Token: 0x020008FA RID: 2298
	public class PlayerStats : PlayerBehaviour
	{
		// Token: 0x06003AE3 RID: 15075 RVA: 0x0002ABFF File Offset: 0x00028DFF
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.EquipmentChanged.AddListener(new Action<ItemHolder>(this.On_EquipmentChanged));
			this.m_EquipmentHolders = MonoSingleton<InventoryController>.Instance.GetEquipmentHolders();
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x001AA86C File Offset: 0x001A8A6C
		private void On_EquipmentChanged(ItemHolder holder)
		{
			int num = 0;
			foreach (ItemHolder itemHolder in this.m_EquipmentHolders)
			{
				if (itemHolder.HasItem && itemHolder.CurrentItem.HasProperty("Defense"))
				{
					num += itemHolder.CurrentItem.GetPropertyValue("Defense").Int.Current;
				}
			}
			base.Player.Defense.Set(num);
		}

		// Token: 0x04003530 RID: 13616
		private List<ItemHolder> m_EquipmentHolders;
	}
}
