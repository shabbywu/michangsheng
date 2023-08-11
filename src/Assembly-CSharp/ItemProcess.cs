using KBEngine;
using UltimateSurvival;
using UnityEngine;

public class ItemProcess : MonoBehaviour
{
	public Tooltip tooltip;

	public EquipmentSystem eS;

	private Inventory equipInventory;

	private Inventory itemInventory;

	private void Start()
	{
		GameObject val = GameObject.FindGameObjectWithTag("Canvas");
		if ((Object)(object)val.transform.Find("Tooltip - Inventory(Clone)") != (Object)null)
		{
			tooltip = ((Component)val.transform.Find("Tooltip - Inventory(Clone)")).GetComponent<Tooltip>();
		}
		eS = ((Component)((Component)this).transform).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		equipInventory = ((Component)((Component)this).transform).GetComponent<PlayerInventory>().characterSystem.GetComponent<Inventory>();
		itemInventory = ((Component)((Component)this).transform).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		Event.registerOut("dropItem_re", this, "dropItem_re");
		Event.registerOut("pickUp_re", this, "pickUp_re");
		Event.registerOut("equipItemRequest_re", this, "equipItemRequest_re");
	}

	public void dropItem_re(int itemIndex, ulong itemUUId)
	{
		if ((Object)(object)itemInventory == (Object)null)
		{
			itemInventory = ((Component)((Component)this).transform).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		if ((Object)(object)itemInventory != (Object)null)
		{
			itemInventory.deleteItemByIndex(itemIndex);
			itemInventory.updateItemList();
			itemInventory.stackableSettings();
			tooltip.deactivateTooltip();
		}
		UltimateSurvival.MonoSingleton<InventoryController>.Instance.RemoveItems(1, itemUUId);
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void pickUp_re(ITEM_INFO itemInfo)
	{
		if ((Object)(object)itemInventory == (Object)null)
		{
			itemInventory = ((Component)((Component)this).transform).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		if ((Object)(object)itemInventory != (Object)null)
		{
			itemInventory.addItemToInventory(itemInfo.itemId, itemInfo.UUID, (int)itemInfo.itemCount, itemInfo.itemIndex);
			itemInventory.updateItemList();
			itemInventory.stackableSettings();
		}
	}

	public void equipItemRequest_re(ITEM_INFO itemInfo, ITEM_INFO equipItemInfo)
	{
		_ = itemInfo.itemIndex;
		_ = equipItemInfo.itemIndex;
		_ = itemInfo.UUID;
		_ = equipItemInfo.UUID;
	}
}
