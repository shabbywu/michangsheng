using System;
using GUIPackage;
using UnityEngine;

// Token: 0x0200045D RID: 1117
public class itemSetDanLu : MonoBehaviour
{
	// Token: 0x0600230D RID: 8973 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x000EF8EC File Offset: 0x000EDAEC
	private void OnPress()
	{
		ItemCellEX component = base.GetComponent<ItemCellEX>();
		component.inventory.showTooltip = false;
		this.showInventory.showTooltip = false;
		if (component.inventory.inventory[int.Parse(base.name)].itemID != -1)
		{
			this.showInventory.inventory[0] = component.inventory.inventory[int.Parse(base.name)];
			LianDanMag.instence.CloseChoiceDanlu();
			LianDanMag.instence.showDanlu();
			LianDanMag.instence.inventoryCaiLiao.resteAllInventoryItem();
			LianDanMag.instence.inventoryCaiLiao.LoadInventory();
			return;
		}
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C40 RID: 7232
	public Inventory2 showInventory;
}
