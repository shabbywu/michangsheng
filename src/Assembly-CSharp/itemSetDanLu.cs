using GUIPackage;
using UnityEngine;

public class itemSetDanLu : MonoBehaviour
{
	public Inventory2 showInventory;

	private void Start()
	{
	}

	private void OnPress()
	{
		ItemCellEX component = ((Component)this).GetComponent<ItemCellEX>();
		component.inventory.showTooltip = false;
		showInventory.showTooltip = false;
		if (component.inventory.inventory[int.Parse(((Object)this).name)].itemID != -1)
		{
			showInventory.inventory[0] = component.inventory.inventory[int.Parse(((Object)this).name)];
			LianDanMag.instence.CloseChoiceDanlu();
			LianDanMag.instence.showDanlu();
			LianDanMag.instence.inventoryCaiLiao.resteAllInventoryItem();
			LianDanMag.instence.inventoryCaiLiao.LoadInventory();
		}
	}

	private void Update()
	{
	}
}
