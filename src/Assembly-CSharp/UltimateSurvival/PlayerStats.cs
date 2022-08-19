using System;
using System.Collections.Generic;

namespace UltimateSurvival
{
	// Token: 0x02000615 RID: 1557
	public class PlayerStats : PlayerBehaviour
	{
		// Token: 0x060031BA RID: 12730 RVA: 0x00160ECC File Offset: 0x0015F0CC
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.EquipmentChanged.AddListener(new Action<ItemHolder>(this.On_EquipmentChanged));
			this.m_EquipmentHolders = MonoSingleton<InventoryController>.Instance.GetEquipmentHolders();
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x00160EFC File Offset: 0x0015F0FC
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

		// Token: 0x04002C12 RID: 11282
		private List<ItemHolder> m_EquipmentHolders;
	}
}
