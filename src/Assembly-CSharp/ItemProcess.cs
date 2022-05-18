using System;
using KBEngine;
using UltimateSurvival;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class ItemProcess : MonoBehaviour
{
	// Token: 0x06001706 RID: 5894 RVA: 0x000CC134 File Offset: 0x000CA334
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

	// Token: 0x06001707 RID: 5895 RVA: 0x000CC204 File Offset: 0x000CA404
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

	// Token: 0x06001708 RID: 5896 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x000CC284 File Offset: 0x000CA484
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

	// Token: 0x0600170A RID: 5898 RVA: 0x000145CD File Offset: 0x000127CD
	public void equipItemRequest_re(ITEM_INFO itemInfo, ITEM_INFO equipItemInfo)
	{
		int itemIndex = itemInfo.itemIndex;
		int itemIndex2 = equipItemInfo.itemIndex;
		ulong uuid = itemInfo.UUID;
		ulong uuid2 = equipItemInfo.UUID;
	}

	// Token: 0x04001262 RID: 4706
	public Tooltip tooltip;

	// Token: 0x04001263 RID: 4707
	public EquipmentSystem eS;

	// Token: 0x04001264 RID: 4708
	private Inventory equipInventory;

	// Token: 0x04001265 RID: 4709
	private Inventory itemInventory;
}
