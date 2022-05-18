using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000617 RID: 1559
public class itemSetDanLu : MonoBehaviour
{
	// Token: 0x060026C0 RID: 9920 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060026C1 RID: 9921 RVA: 0x0012FE18 File Offset: 0x0012E018
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

	// Token: 0x060026C2 RID: 9922 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002110 RID: 8464
	public Inventory2 showInventory;
}
