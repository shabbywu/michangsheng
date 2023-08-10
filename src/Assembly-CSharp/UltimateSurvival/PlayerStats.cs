using System.Collections.Generic;

namespace UltimateSurvival;

public class PlayerStats : PlayerBehaviour
{
	private List<ItemHolder> m_EquipmentHolders;

	private void Start()
	{
		MonoSingleton<InventoryController>.Instance.EquipmentChanged.AddListener(On_EquipmentChanged);
		m_EquipmentHolders = MonoSingleton<InventoryController>.Instance.GetEquipmentHolders();
	}

	private void On_EquipmentChanged(ItemHolder holder)
	{
		int num = 0;
		foreach (ItemHolder equipmentHolder in m_EquipmentHolders)
		{
			if (equipmentHolder.HasItem && equipmentHolder.CurrentItem.HasProperty("Defense"))
			{
				num += equipmentHolder.CurrentItem.GetPropertyValue("Defense").Int.Current;
			}
		}
		base.Player.Defense.Set(num);
	}
}
