using System;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200013B RID: 315
public class ConsumeItem : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E94 RID: 3732 RVA: 0x0005743C File Offset: 0x0005563C
	private void Start()
	{
		this.item = base.GetComponent<ItemOnObject>().item;
		if (GameObject.FindGameObjectWithTag("Tooltip") != null)
		{
			ConsumeItem.tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
		}
		if (GameObject.FindGameObjectWithTag("EquipmentSystem") != null)
		{
			ConsumeItem.eS = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		}
		if (GameObject.FindGameObjectWithTag("MainInventory") != null)
		{
			ConsumeItem.mainInventory = GameObject.FindGameObjectWithTag("MainInventory");
		}
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x000574DC File Offset: 0x000556DC
	public void OnPointerDown(PointerEventData data)
	{
		if (base.gameObject.transform.parent.parent.parent.GetComponent<EquipmentSystem>() == null)
		{
			bool flag = false;
			Inventory component = base.transform.parent.parent.parent.GetComponent<Inventory>();
			if (ConsumeItem.eS != null)
			{
				this.itemTypeOfSlot = ConsumeItem.eS.itemTypeOfSlots;
			}
			if (data.button == 1)
			{
				if (base.transform.parent.GetComponent<CraftResultSlot>() != null)
				{
					if (!((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().checkIfItemAllreadyExist(this.item.itemID, this.item.itemValue))
					{
						((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().addItemToInventory(this.item.itemID, this.item.itemValue);
						((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().stackableSettings();
					}
					((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().craftSystem.GetComponent<CraftSystem>().deleteItems(this.item);
					((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().craftSystem.transform.GetChild(3).GetComponent<CraftResultSlot>().temp = 0;
					ConsumeItem.tooltip.deactivateTooltip();
					flag = true;
					GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().updateItemList();
				}
				else
				{
					bool flag2 = false;
					if (ConsumeItem.eS != null)
					{
						int i = 0;
						while (i < ConsumeItem.eS.slotsInTotal)
						{
							if (this.itemTypeOfSlot[i].Equals(this.item.itemType) && ConsumeItem.eS.transform.GetChild(1).GetChild(i).childCount == 0)
							{
								flag2 = true;
								if (!(ConsumeItem.eS.transform.GetChild(1).GetChild(i).parent.parent.GetComponent<EquipmentSystem>() != null) || !(base.gameObject.transform.parent.parent.parent.GetComponent<EquipmentSystem>() != null))
								{
									component.EquiptItem(this.item);
								}
								base.gameObject.transform.SetParent(ConsumeItem.eS.transform.GetChild(1).GetChild(i));
								base.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
								ConsumeItem.eS.gameObject.GetComponent<Inventory>().updateItemList();
								component.updateItemList();
								flag = true;
								if (this.duplication != null)
								{
									Object.Destroy(this.duplication.gameObject);
									break;
								}
								break;
							}
							else
							{
								i++;
							}
						}
						if (!flag2)
						{
							for (int j = 0; j < ConsumeItem.eS.slotsInTotal; j++)
							{
								if (this.itemTypeOfSlot[j].Equals(this.item.itemType) && ConsumeItem.eS.transform.GetChild(1).GetChild(j).childCount != 0)
								{
									GameObject gameObject = ConsumeItem.eS.transform.GetChild(1).GetChild(j).GetChild(0).gameObject;
									Item item = gameObject.GetComponent<ItemOnObject>().item;
									if (this.item.itemType == ItemType.UFPS_Weapon)
									{
										component.UnEquipItem1(gameObject.GetComponent<ItemOnObject>().item);
										component.EquiptItem(this.item);
									}
									else
									{
										component.EquiptItem(this.item);
										if (this.item.itemType != ItemType.Backpack)
										{
											component.UnEquipItem1(gameObject.GetComponent<ItemOnObject>().item);
										}
									}
									if (this == null)
									{
										GameObject gameObject2 = Object.Instantiate<GameObject>(item.itemModel);
										gameObject2.AddComponent<PickUpItem>();
										gameObject2.GetComponent<PickUpItem>().item = item;
										gameObject2.transform.localPosition = ((GameObject)KBEngineApp.app.player().renderObj).transform.localPosition;
										component.OnUpdateItemList();
									}
									else
									{
										gameObject.transform.SetParent(base.transform.parent);
										gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
										if (base.gameObject.transform.parent.parent.parent.GetComponent<Hotbar>() != null)
										{
											this.createDuplication(gameObject);
										}
										base.gameObject.transform.SetParent(ConsumeItem.eS.transform.GetChild(1).GetChild(j));
										base.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
									}
									flag = true;
									if (this.duplication != null)
									{
										Object.Destroy(this.duplication.gameObject);
									}
									ConsumeItem.eS.gameObject.GetComponent<Inventory>().updateItemList();
									component.OnUpdateItemList();
									break;
								}
							}
						}
					}
				}
				if (!flag && this.item.itemType != ItemType.UFPS_Ammo && this.item.itemType != ItemType.UFPS_Grenade)
				{
					Item item2 = null;
					if (this.duplication != null)
					{
						item2 = this.duplication.GetComponent<ItemOnObject>().item;
					}
					component.ConsumeItem(this.item);
					this.item.itemValue--;
					if (item2 != null)
					{
						this.duplication.GetComponent<ItemOnObject>().item.itemValue--;
						if (item2.itemValue <= 0)
						{
							if (ConsumeItem.tooltip != null)
							{
								ConsumeItem.tooltip.deactivateTooltip();
							}
							component.deleteItemFromInventory(this.item);
							Object.Destroy(this.duplication.gameObject);
						}
					}
					if (this.item.itemValue <= 0)
					{
						if (ConsumeItem.tooltip != null)
						{
							ConsumeItem.tooltip.deactivateTooltip();
						}
						component.deleteItemFromInventory(this.item);
						Object.Destroy(base.gameObject);
					}
				}
			}
		}
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x00057B28 File Offset: 0x00055D28
	public void consumeIt()
	{
		Inventory component = base.transform.parent.parent.parent.GetComponent<Inventory>();
		bool flag = false;
		if (GameObject.FindGameObjectWithTag("EquipmentSystem") != null)
		{
			ConsumeItem.eS = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		}
		if (ConsumeItem.eS != null)
		{
			this.itemTypeOfSlot = ConsumeItem.eS.itemTypeOfSlots;
		}
		Item item = null;
		if (this.duplication != null)
		{
			item = this.duplication.GetComponent<ItemOnObject>().item;
		}
		bool flag2 = false;
		if (ConsumeItem.eS != null)
		{
			int i = 0;
			while (i < ConsumeItem.eS.slotsInTotal)
			{
				if (this.itemTypeOfSlot[i].Equals(this.item.itemType) && ConsumeItem.eS.transform.GetChild(1).GetChild(i).childCount == 0)
				{
					flag2 = true;
					base.gameObject.transform.SetParent(ConsumeItem.eS.transform.GetChild(1).GetChild(i));
					base.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
					ConsumeItem.eS.gameObject.GetComponent<Inventory>().updateItemList();
					component.updateItemList();
					component.EquiptItem(this.item);
					flag = true;
					if (this.duplication != null)
					{
						Object.Destroy(this.duplication.gameObject);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			if (!flag2)
			{
				for (int j = 0; j < ConsumeItem.eS.slotsInTotal; j++)
				{
					if (this.itemTypeOfSlot[j].Equals(this.item.itemType) && ConsumeItem.eS.transform.GetChild(1).GetChild(j).childCount != 0)
					{
						GameObject gameObject = ConsumeItem.eS.transform.GetChild(1).GetChild(j).GetChild(0).gameObject;
						Item item2 = gameObject.GetComponent<ItemOnObject>().item;
						if (this.item.itemType == ItemType.UFPS_Weapon)
						{
							component.UnEquipItem1(gameObject.GetComponent<ItemOnObject>().item);
							component.EquiptItem(this.item);
						}
						else
						{
							component.EquiptItem(this.item);
							if (this.item.itemType != ItemType.Backpack)
							{
								component.UnEquipItem1(gameObject.GetComponent<ItemOnObject>().item);
							}
						}
						if (this == null)
						{
							GameObject gameObject2 = Object.Instantiate<GameObject>(item2.itemModel);
							gameObject2.AddComponent<PickUpItem>();
							gameObject2.GetComponent<PickUpItem>().item = item2;
							gameObject2.transform.localPosition = ((GameObject)KBEngineApp.app.player().renderObj).transform.localPosition;
							component.OnUpdateItemList();
						}
						else
						{
							gameObject.transform.SetParent(base.transform.parent);
							gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
							if (base.gameObject.transform.parent.parent.parent.GetComponent<Hotbar>() != null)
							{
								this.createDuplication(gameObject);
							}
							base.gameObject.transform.SetParent(ConsumeItem.eS.transform.GetChild(1).GetChild(j));
							base.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
						}
						flag = true;
						if (this.duplication != null)
						{
							Object.Destroy(this.duplication.gameObject);
						}
						ConsumeItem.eS.gameObject.GetComponent<Inventory>().updateItemList();
						component.OnUpdateItemList();
						break;
					}
				}
			}
		}
		if (!flag && this.item.itemType != ItemType.UFPS_Ammo && this.item.itemType != ItemType.UFPS_Grenade)
		{
			if (this.duplication != null)
			{
				item = this.duplication.GetComponent<ItemOnObject>().item;
			}
			component.ConsumeItem(this.item);
			this.item.itemValue--;
			if (item != null)
			{
				this.duplication.GetComponent<ItemOnObject>().item.itemValue--;
				if (item.itemValue <= 0)
				{
					if (ConsumeItem.tooltip != null)
					{
						ConsumeItem.tooltip.deactivateTooltip();
					}
					component.deleteItemFromInventory(this.item);
					Object.Destroy(this.duplication.gameObject);
				}
			}
			if (this.item.itemValue <= 0)
			{
				if (ConsumeItem.tooltip != null)
				{
					ConsumeItem.tooltip.deactivateTooltip();
				}
				component.deleteItemFromInventory(this.item);
				Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x00057FFC File Offset: 0x000561FC
	public void createDuplication(GameObject Item)
	{
		Item item = Item.GetComponent<ItemOnObject>().item;
		GameObject gameObject = ConsumeItem.mainInventory.GetComponent<Inventory>().addItemToInventory(item.itemID, item.itemValue);
		Item.GetComponent<ConsumeItem>().duplication = gameObject;
		gameObject.GetComponent<ConsumeItem>().duplication = Item;
	}

	// Token: 0x04000A9C RID: 2716
	public Item item;

	// Token: 0x04000A9D RID: 2717
	private static Tooltip tooltip;

	// Token: 0x04000A9E RID: 2718
	public ItemType[] itemTypeOfSlot;

	// Token: 0x04000A9F RID: 2719
	public static EquipmentSystem eS;

	// Token: 0x04000AA0 RID: 2720
	public GameObject duplication;

	// Token: 0x04000AA1 RID: 2721
	public static GameObject mainInventory;
}
