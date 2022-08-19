using System;
using KBEngine;
using UltimateSurvival;
using UnityEngine;

// Token: 0x020001EA RID: 490
public class ItemProcess : MonoBehaviour
{
	// Token: 0x0600145C RID: 5212 RVA: 0x00083174 File Offset: 0x00081374
	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
		if (gameObject.transform.Find("Tooltip - Inventory(Clone)") != null)
		{
			this.tooltip = gameObject.transform.Find("Tooltip - Inventory(Clone)").GetComponent<Tooltip>();
		}
		this.eS = base.transform.GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		this.equipInventory = base.transform.GetComponent<PlayerInventory>().characterSystem.GetComponent<Inventory>();
		this.itemInventory = base.transform.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		Event.registerOut("dropItem_re", this, "dropItem_re");
		Event.registerOut("pickUp_re", this, "pickUp_re");
		Event.registerOut("equipItemRequest_re", this, "equipItemRequest_re");
	}

	// Token: 0x0600145D RID: 5213 RVA: 0x00083244 File Offset: 0x00081444
	public void dropItem_re(int itemIndex, ulong itemUUId)
	{
		if (this.itemInventory == null)
		{
			this.itemInventory = base.transform.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		if (this.itemInventory != null)
		{
			this.itemInventory.deleteItemByIndex(itemIndex);
			this.itemInventory.updateItemList();
			this.itemInventory.stackableSettings();
			this.tooltip.deactivateTooltip();
		}
		UltimateSurvival.MonoSingleton<InventoryController>.Instance.RemoveItems(1, itemUUId);
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x000832C4 File Offset: 0x000814C4
	public void pickUp_re(ITEM_INFO itemInfo)
	{
		if (this.itemInventory == null)
		{
			this.itemInventory = base.transform.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		if (this.itemInventory != null)
		{
			this.itemInventory.addItemToInventory(itemInfo.itemId, itemInfo.UUID, (int)itemInfo.itemCount, itemInfo.itemIndex);
			this.itemInventory.updateItemList();
			this.itemInventory.stackableSettings();
		}
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x00083342 File Offset: 0x00081542
	public void equipItemRequest_re(ITEM_INFO itemInfo, ITEM_INFO equipItemInfo)
	{
		int itemIndex = itemInfo.itemIndex;
		int itemIndex2 = equipItemInfo.itemIndex;
		ulong uuid = itemInfo.UUID;
		ulong uuid2 = equipItemInfo.UUID;
	}

	// Token: 0x04000F1F RID: 3871
	public Tooltip tooltip;

	// Token: 0x04000F20 RID: 3872
	public EquipmentSystem eS;

	// Token: 0x04000F21 RID: 3873
	private Inventory equipInventory;

	// Token: 0x04000F22 RID: 3874
	private Inventory itemInventory;
}
