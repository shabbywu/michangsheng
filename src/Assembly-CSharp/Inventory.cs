using System;
using System.Collections;
using System.Collections.Generic;
using UltimateSurvival;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000209 RID: 521
public class Inventory : MonoBehaviour
{
	// Token: 0x1400002F RID: 47
	// (add) Token: 0x06001059 RID: 4185 RVA: 0x000A51AC File Offset: 0x000A33AC
	// (remove) Token: 0x0600105A RID: 4186 RVA: 0x000A51E0 File Offset: 0x000A33E0
	public static event Inventory.ItemDelegate ItemConsumed;

	// Token: 0x14000030 RID: 48
	// (add) Token: 0x0600105B RID: 4187 RVA: 0x000A5214 File Offset: 0x000A3414
	// (remove) Token: 0x0600105C RID: 4188 RVA: 0x000A5248 File Offset: 0x000A3448
	public static event Inventory.ItemDelegate ItemEquip;

	// Token: 0x14000031 RID: 49
	// (add) Token: 0x0600105D RID: 4189 RVA: 0x000A527C File Offset: 0x000A347C
	// (remove) Token: 0x0600105E RID: 4190 RVA: 0x000A52B0 File Offset: 0x000A34B0
	public static event Inventory.ItemDelegate UnEquipItem;

	// Token: 0x14000032 RID: 50
	// (add) Token: 0x0600105F RID: 4191 RVA: 0x000A52E4 File Offset: 0x000A34E4
	// (remove) Token: 0x06001060 RID: 4192 RVA: 0x000A5318 File Offset: 0x000A3518
	public static event Inventory.InventoryOpened InventoryOpen;

	// Token: 0x14000033 RID: 51
	// (add) Token: 0x06001061 RID: 4193 RVA: 0x000A534C File Offset: 0x000A354C
	// (remove) Token: 0x06001062 RID: 4194 RVA: 0x000A5380 File Offset: 0x000A3580
	public static event Inventory.InventoryOpened AllInventoriesClosed;

	// Token: 0x06001063 RID: 4195 RVA: 0x00010502 File Offset: 0x0000E702
	private void Start()
	{
		if (base.transform.GetComponent<global::Hotbar>() == null)
		{
			base.gameObject.SetActive(false);
		}
		this.updateItemList();
		this.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x000A53B4 File Offset: 0x000A35B4
	public void sortItems()
	{
		int num = -1;
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount == 0 && num == -1)
			{
				num = i;
			}
			else if (num > -1 && this.SlotContainer.transform.GetChild(i).childCount != 0)
			{
				Transform component = this.SlotContainer.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>();
				this.SlotContainer.transform.GetChild(i).GetChild(0).transform.SetParent(this.SlotContainer.transform.GetChild(num).transform);
				component.localPosition = Vector3.zero;
				i = num + 1;
				num = i;
			}
		}
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x0001053E File Offset: 0x0000E73E
	private void Update()
	{
		this.updateItemIndex();
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x00010546 File Offset: 0x0000E746
	public void setAsMain()
	{
		if (this.mainInventory)
		{
			base.gameObject.tag = "Untagged";
			return;
		}
		if (!this.mainInventory)
		{
			base.gameObject.tag = "MainInventory";
		}
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x00010579 File Offset: 0x0000E779
	public void OnUpdateItemList()
	{
		this.updateItemList();
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x00010581 File Offset: 0x0000E781
	public void closeInventory()
	{
		base.gameObject.SetActive(false);
		this.checkIfAllInventoryClosed();
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x00010595 File Offset: 0x0000E795
	public void openInventory()
	{
		base.gameObject.SetActive(true);
		if (Inventory.InventoryOpen != null)
		{
			Inventory.InventoryOpen();
		}
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x000A5484 File Offset: 0x000A3684
	public void checkIfAllInventoryClosed()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
			if (!gameObject2.activeSelf && (gameObject2.tag == "EquipmentSystem" || gameObject2.tag == "Panel" || gameObject2.tag == "MainInventory" || gameObject2.tag == "CraftSystem"))
			{
				if (Inventory.AllInventoriesClosed != null && i == gameObject.transform.childCount - 1)
				{
					Inventory.AllInventoriesClosed();
				}
			}
			else
			{
				if (gameObject2.activeSelf && (gameObject2.tag == "EquipmentSystem" || gameObject2.tag == "Panel" || gameObject2.tag == "MainInventory" || gameObject2.tag == "CraftSystem"))
				{
					break;
				}
				if (i == gameObject.transform.childCount - 1 && Inventory.AllInventoriesClosed != null)
				{
					Inventory.AllInventoriesClosed();
				}
			}
		}
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x000105B4 File Offset: 0x0000E7B4
	public void ConsumeItem(Item item)
	{
		if (Inventory.ItemConsumed != null)
		{
			Inventory.ItemConsumed(item);
		}
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x000105C8 File Offset: 0x0000E7C8
	public void EquiptItem(Item item)
	{
		if (Inventory.ItemEquip != null)
		{
			Inventory.ItemEquip(item);
		}
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x000105DC File Offset: 0x0000E7DC
	public void UnEquipItem1(Item item)
	{
		if (Inventory.UnEquipItem != null)
		{
			Inventory.UnEquipItem(item);
		}
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x000A55B4 File Offset: 0x000A37B4
	public void setImportantVariables()
	{
		this.PanelRectTransform = base.GetComponent<RectTransform>();
		this.SlotContainer = base.transform.GetChild(1).gameObject;
		this.SlotGridLayout = this.SlotContainer.GetComponent<GridLayoutGroup>();
		this.SlotGridRectTransform = this.SlotContainer.GetComponent<RectTransform>();
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x000A5608 File Offset: 0x000A3808
	public void getPrefabs()
	{
		if (this.prefabCanvasWithPanel == null)
		{
			this.prefabCanvasWithPanel = (Resources.Load("Prefabs/Canvas - Inventory") as GameObject);
		}
		if (this.prefabSlot == null)
		{
			this.prefabSlot = (Resources.Load("Prefabs/Slot - Inventory") as GameObject);
		}
		if (this.prefabSlotContainer == null)
		{
			this.prefabSlotContainer = (Resources.Load("Prefabs/Slots - Inventory") as GameObject);
		}
		if (this.prefabItem == null)
		{
			this.prefabItem = (Resources.Load("Prefabs/Item") as GameObject);
		}
		if (this.itemDatabase == null)
		{
			this.itemDatabase = (ItemDataBaseList)Resources.Load("ItemDatabase");
		}
		if (this.prefabDraggingItemContainer == null)
		{
			this.prefabDraggingItemContainer = (Resources.Load("Prefabs/DraggingItem") as GameObject);
		}
		if (this.prefabPanel == null)
		{
			this.prefabPanel = (Resources.Load("Prefabs/Panel - Inventory") as GameObject);
		}
		this.setImportantVariables();
		this.setDefaultSettings();
		this.adjustInventorySize();
		this.updateSlotAmount(this.width, this.height);
		this.updateSlotSize();
		this.updatePadding(this.paddingBetweenX, this.paddingBetweenY);
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x000A5748 File Offset: 0x000A3948
	public void updateItemList()
	{
		this.ItemsInInventory.Clear();
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			Transform child = this.SlotContainer.transform.GetChild(i);
			if (child.childCount != 0)
			{
				this.ItemsInInventory.Add(child.GetChild(0).GetComponent<ItemOnObject>().item);
			}
		}
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x000105F0 File Offset: 0x0000E7F0
	public bool characterSystem()
	{
		return base.GetComponent<EquipmentSystem>() != null;
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x000A57B4 File Offset: 0x000A39B4
	public void setDefaultSettings()
	{
		if (base.GetComponent<EquipmentSystem>() == null && base.GetComponent<global::Hotbar>() == null && base.GetComponent<CraftSystem>() == null)
		{
			this.height = 5;
			this.width = 5;
			this.slotSize = 50;
			this.iconSize = 45;
			this.paddingBetweenX = 5;
			this.paddingBetweenY = 5;
			this.paddingTop = 35;
			this.paddingBottom = 10;
			this.paddingLeft = 10;
			this.paddingRight = 10;
			return;
		}
		if (base.GetComponent<global::Hotbar>() != null)
		{
			this.height = 1;
			this.width = 9;
			this.slotSize = 50;
			this.iconSize = 45;
			this.paddingBetweenX = 5;
			this.paddingBetweenY = 5;
			this.paddingTop = 10;
			this.paddingBottom = 10;
			this.paddingLeft = 10;
			this.paddingRight = 10;
			return;
		}
		if (base.GetComponent<CraftSystem>() != null)
		{
			this.height = 3;
			this.width = 3;
			this.slotSize = 55;
			this.iconSize = 45;
			this.paddingBetweenX = 5;
			this.paddingBetweenY = 5;
			this.paddingTop = 35;
			this.paddingBottom = 95;
			this.paddingLeft = 25;
			this.paddingRight = 25;
			return;
		}
		this.height = 4;
		this.width = 2;
		this.slotSize = 50;
		this.iconSize = 45;
		this.paddingBetweenX = 100;
		this.paddingBetweenY = 20;
		this.paddingTop = 35;
		this.paddingBottom = 10;
		this.paddingLeft = 10;
		this.paddingRight = 10;
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x000A5940 File Offset: 0x000A3B40
	public void adjustInventorySize()
	{
		int num = this.width * this.slotSize + (this.width - 1) * this.paddingBetweenX + this.paddingLeft + this.paddingRight;
		int num2 = this.height * this.slotSize + (this.height - 1) * this.paddingBetweenY + this.paddingTop + this.paddingBottom;
		this.PanelRectTransform.sizeDelta = new Vector2((float)num, (float)num2);
		this.SlotGridRectTransform.sizeDelta = new Vector2((float)num, (float)num2);
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x000A59D0 File Offset: 0x000A3BD0
	public void updateSlotAmount(int width, int height)
	{
		if (this.prefabSlot == null)
		{
			this.prefabSlot = (Resources.Load("Prefabs/Slot - Inventory") as GameObject);
		}
		if (this.SlotContainer == null)
		{
			this.SlotContainer = Object.Instantiate<GameObject>(this.prefabSlotContainer);
			this.SlotContainer.transform.SetParent(this.PanelRectTransform.transform);
			this.SlotContainerRectTransform = this.SlotContainer.GetComponent<RectTransform>();
			this.SlotGridRectTransform = this.SlotContainer.GetComponent<RectTransform>();
			this.SlotGridLayout = this.SlotContainer.GetComponent<GridLayoutGroup>();
		}
		if (this.SlotContainerRectTransform == null)
		{
			this.SlotContainerRectTransform = this.SlotContainer.GetComponent<RectTransform>();
		}
		this.SlotContainerRectTransform.localPosition = Vector3.zero;
		List<Item> list = new List<Item>();
		List<GameObject> list2 = new List<GameObject>();
		using (IEnumerator enumerator = this.SlotContainer.transform.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.tag == "Slot")
				{
					list2.Add(transform.gameObject);
				}
			}
			goto IL_179;
		}
		IL_126:
		GameObject gameObject = list2[list2.Count - 1];
		ItemOnObject componentInChildren = gameObject.GetComponentInChildren<ItemOnObject>();
		if (componentInChildren != null)
		{
			list.Add(componentInChildren.item);
			this.ItemsInInventory.Remove(componentInChildren.item);
		}
		list2.Remove(gameObject);
		Object.DestroyImmediate(gameObject);
		IL_179:
		if (list2.Count <= width * height)
		{
			if (list2.Count < width * height)
			{
				for (int i = list2.Count; i < width * height; i++)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.prefabSlot);
					gameObject2.name = (list2.Count + 1).ToString();
					gameObject2.transform.SetParent(this.SlotContainer.transform);
					list2.Add(gameObject2);
				}
			}
			if (list != null && this.ItemsInInventory.Count < width * height)
			{
				foreach (Item item in list)
				{
					this.addItemToInventory(item.itemID);
				}
			}
			this.setImportantVariables();
			return;
		}
		goto IL_126;
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x000A5C38 File Offset: 0x000A3E38
	public void updateSlotAmount()
	{
		if (this.prefabSlot == null)
		{
			this.prefabSlot = (Resources.Load("Prefabs/Slot - Inventory") as GameObject);
		}
		if (this.SlotContainer == null)
		{
			this.SlotContainer = Object.Instantiate<GameObject>(this.prefabSlotContainer);
			this.SlotContainer.transform.SetParent(this.PanelRectTransform.transform);
			this.SlotContainerRectTransform = this.SlotContainer.GetComponent<RectTransform>();
			this.SlotGridRectTransform = this.SlotContainer.GetComponent<RectTransform>();
			this.SlotGridLayout = this.SlotContainer.GetComponent<GridLayoutGroup>();
		}
		if (this.SlotContainerRectTransform == null)
		{
			this.SlotContainerRectTransform = this.SlotContainer.GetComponent<RectTransform>();
		}
		this.SlotContainerRectTransform.localPosition = Vector3.zero;
		List<Item> list = new List<Item>();
		List<GameObject> list2 = new List<GameObject>();
		using (IEnumerator enumerator = this.SlotContainer.transform.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.tag == "Slot")
				{
					list2.Add(transform.gameObject);
				}
			}
			goto IL_179;
		}
		IL_126:
		GameObject gameObject = list2[list2.Count - 1];
		ItemOnObject componentInChildren = gameObject.GetComponentInChildren<ItemOnObject>();
		if (componentInChildren != null)
		{
			list.Add(componentInChildren.item);
			this.ItemsInInventory.Remove(componentInChildren.item);
		}
		list2.Remove(gameObject);
		Object.DestroyImmediate(gameObject);
		IL_179:
		if (list2.Count <= this.width * this.height)
		{
			if (list2.Count < this.width * this.height)
			{
				for (int i = list2.Count; i < this.width * this.height; i++)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.prefabSlot);
					gameObject2.name = (list2.Count + 1).ToString();
					gameObject2.transform.SetParent(this.SlotContainer.transform);
					list2.Add(gameObject2);
				}
			}
			if (list != null && this.ItemsInInventory.Count < this.width * this.height)
			{
				foreach (Item item in list)
				{
					this.addItemToInventory(item.itemID);
				}
			}
			this.setImportantVariables();
			return;
		}
		goto IL_126;
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x00010603 File Offset: 0x0000E803
	public void updateSlotSize(int slotSize)
	{
		this.SlotGridLayout.cellSize = new Vector2((float)slotSize, (float)slotSize);
		this.updateItemSize();
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x0001061F File Offset: 0x0000E81F
	public void updateSlotSize()
	{
		this.SlotGridLayout.cellSize = new Vector2((float)this.slotSize, (float)this.slotSize);
		this.updateItemSize();
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x000A5EC8 File Offset: 0x000A40C8
	private void updateItemSize()
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount > 0)
			{
				this.SlotContainer.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((float)this.slotSize, (float)this.slotSize);
				this.SlotContainer.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2((float)this.slotSize, (float)this.slotSize);
			}
		}
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x000A5F80 File Offset: 0x000A4180
	public void updatePadding(int spacingBetweenX, int spacingBetweenY)
	{
		this.SlotGridLayout.spacing = new Vector2((float)this.paddingBetweenX, (float)this.paddingBetweenY);
		this.SlotGridLayout.padding.bottom = this.paddingBottom;
		this.SlotGridLayout.padding.right = this.paddingRight;
		this.SlotGridLayout.padding.left = this.paddingLeft;
		this.SlotGridLayout.padding.top = this.paddingTop;
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x000A5F80 File Offset: 0x000A4180
	public void updatePadding()
	{
		this.SlotGridLayout.spacing = new Vector2((float)this.paddingBetweenX, (float)this.paddingBetweenY);
		this.SlotGridLayout.padding.bottom = this.paddingBottom;
		this.SlotGridLayout.padding.right = this.paddingRight;
		this.SlotGridLayout.padding.left = this.paddingLeft;
		this.SlotGridLayout.padding.top = this.paddingTop;
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x000A6004 File Offset: 0x000A4204
	public void addAllItemsToInventory()
	{
		for (int i = 0; i < this.ItemsInInventory.Count; i++)
		{
			for (int j = 0; j < this.SlotContainer.transform.childCount; j++)
			{
				if (this.SlotContainer.transform.GetChild(j).childCount == 0)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.prefabItem);
					gameObject.GetComponent<ItemOnObject>().item = this.ItemsInInventory[i];
					gameObject.transform.SetParent(this.SlotContainer.transform.GetChild(j));
					gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
					gameObject.transform.GetChild(0).GetComponent<Image>().sprite = this.ItemsInInventory[i].itemIcon;
					this.updateItemSize();
					break;
				}
			}
		}
		this.stackableSettings();
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x000A60E8 File Offset: 0x000A42E8
	public bool checkIfItemAllreadyExist(int itemID, int itemValue)
	{
		this.updateItemList();
		for (int i = 0; i < this.ItemsInInventory.Count; i++)
		{
			if (this.ItemsInInventory[i].itemID == itemID)
			{
				int num = this.ItemsInInventory[i].itemValue + itemValue;
				if (num <= this.ItemsInInventory[i].maxStack)
				{
					this.ItemsInInventory[i].itemValue = num;
					GameObject itemGameObject = this.getItemGameObject(this.ItemsInInventory[i]);
					if (itemGameObject != null && itemGameObject.GetComponent<ConsumeItem>().duplication != null)
					{
						itemGameObject.GetComponent<ConsumeItem>().duplication.GetComponent<ItemOnObject>().item.itemValue = num;
					}
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x000A61B8 File Offset: 0x000A43B8
	public void addItemToInventory(int id)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount == 0)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.prefabItem);
				gameObject.GetComponent<ItemOnObject>().item = this.itemDatabase.getItemByID(id);
				gameObject.transform.SetParent(this.SlotContainer.transform.GetChild(i));
				gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
				gameObject.transform.GetChild(0).GetComponent<Image>().sprite = gameObject.GetComponent<ItemOnObject>().item.itemIcon;
				gameObject.GetComponent<ItemOnObject>().item.indexItemInList = this.ItemsInInventory.Count - 1;
				break;
			}
		}
		this.stackableSettings();
		this.updateItemList();
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x000A62A4 File Offset: 0x000A44A4
	public GameObject addItemToInventory(int id, int value)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount == 0)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.prefabItem);
				ItemOnObject component = gameObject.GetComponent<ItemOnObject>();
				component.item = this.itemDatabase.getItemByID(id);
				if (component.item.itemValue <= component.item.maxStack && value <= component.item.maxStack)
				{
					component.item.itemValue = value;
				}
				else
				{
					component.item.itemValue = 1;
				}
				gameObject.transform.SetParent(this.SlotContainer.transform.GetChild(i));
				gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
				gameObject.transform.GetChild(0).GetComponent<Image>().sprite = component.item.itemIcon;
				component.item.indexItemInList = this.ItemsInInventory.Count - 1;
				if (this.inputManagerDatabase == null)
				{
					this.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
				}
				return gameObject;
			}
		}
		this.stackableSettings();
		this.updateItemList();
		return null;
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x000A63E8 File Offset: 0x000A45E8
	public GameObject addItemToInventory(int id, ulong itemUUID, int value)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount == 0)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.prefabItem);
				ItemOnObject component = gameObject.GetComponent<ItemOnObject>();
				component.item = this.itemDatabase.getItemByID(id);
				component.item.itemUUID = itemUUID;
				if (component.item.itemValue <= component.item.maxStack && value <= component.item.maxStack)
				{
					component.item.itemValue = value;
				}
				else
				{
					component.item.itemValue = 1;
				}
				gameObject.transform.SetParent(this.SlotContainer.transform.GetChild(i));
				gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
				gameObject.transform.GetChild(0).GetComponent<Image>().sprite = component.item.itemIcon;
				component.item.indexItemInList = this.ItemsInInventory.Count - 1;
				if (this.inputManagerDatabase == null)
				{
					this.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
				}
				return gameObject;
			}
		}
		this.stackableSettings();
		this.updateItemList();
		return null;
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x000A6538 File Offset: 0x000A4738
	public GameObject addItemToInventory(int itemId, ulong itemUUID, int value, int itemIndex)
	{
		if (itemIndex < 0 || itemIndex >= this.SlotContainer.transform.childCount)
		{
			return null;
		}
		GameObject gameObject;
		if (this.SlotContainer.transform.GetChild(itemIndex).childCount != 0)
		{
			gameObject = this.SlotContainer.transform.GetChild(itemIndex).GetChild(0).gameObject;
		}
		else
		{
			gameObject = Object.Instantiate<GameObject>(this.prefabItem);
		}
		ItemOnObject component = gameObject.GetComponent<ItemOnObject>();
		component.item = this.itemDatabase.getItemByID(itemId);
		component.item.itemUUID = itemUUID;
		component.item.itemIndex = itemIndex;
		if (component.item.itemValue <= component.item.maxStack && value <= component.item.maxStack)
		{
			component.item.itemValue = value;
		}
		else
		{
			component.item.itemValue = 1;
		}
		gameObject.transform.SetParent(this.SlotContainer.transform.GetChild(itemIndex));
		gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
		gameObject.transform.GetChild(0).GetComponent<Image>().sprite = component.item.itemIcon;
		component.item.indexItemInList = this.ItemsInInventory.Count - 1;
		if (this.inputManagerDatabase == null)
		{
			this.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
		}
		if (base.name == "Panel - Inventory(Clone)")
		{
			int num = UltimateSurvival.MonoSingleton<InventoryController>.Instance.findItemCount(itemUUID);
			string text = UltimateSurvival.MonoSingleton<InventoryController>.Instance.findItemInCintainersName(itemUUID);
			if (text == "")
			{
				text = "Inventory";
			}
			if (UltimateSurvival.MonoSingleton<GUIController>.Instance.GetContainer("Hotbar").getHasItemSlotCount() < 6 && num <= 0)
			{
				text = "Hotbar";
			}
			int num2;
			UltimateSurvival.MonoSingleton<InventoryController>.Instance.AddItemToCollection(itemId, itemUUID, value - num, text, out num2, itemIndex);
		}
		return gameObject;
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x000A671C File Offset: 0x000A491C
	public void addItemToInventoryStorage(int itemID, int value)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount == 0)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.prefabItem);
				ItemOnObject component = gameObject.GetComponent<ItemOnObject>();
				component.item = this.itemDatabase.getItemByID(itemID);
				if (component.item.itemValue < component.item.maxStack && value <= component.item.maxStack)
				{
					component.item.itemValue = value;
				}
				else
				{
					component.item.itemValue = 1;
				}
				gameObject.transform.SetParent(this.SlotContainer.transform.GetChild(i));
				gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
				component.item.indexItemInList = 999;
				if (this.inputManagerDatabase == null)
				{
					this.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
				}
				this.updateItemSize();
				this.stackableSettings();
				break;
			}
		}
		this.stackableSettings();
		this.updateItemList();
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x000A6844 File Offset: 0x000A4A44
	public void updateIconSize(int iconSize)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount > 0)
			{
				this.SlotContainer.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((float)iconSize, (float)iconSize);
			}
		}
		this.updateItemSize();
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x000A68BC File Offset: 0x000A4ABC
	public void updateIconSize()
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount > 0)
			{
				this.SlotContainer.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((float)this.iconSize, (float)this.iconSize);
			}
		}
		this.updateItemSize();
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x000A6940 File Offset: 0x000A4B40
	public void stackableSettings(bool stackable, Vector3 posi)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount > 0)
			{
				ItemOnObject component = this.SlotContainer.transform.GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
				if (component.item.maxStack > 1)
				{
					Transform component2 = this.SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<RectTransform>();
					Text component3 = this.SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Text>();
					component3.text = string.Concat(component.item.itemValue);
					component3.enabled = stackable;
					component2.localPosition = posi;
				}
			}
		}
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x000A6A20 File Offset: 0x000A4C20
	public void deleteAllItems()
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount != 0)
			{
				Object.Destroy(this.SlotContainer.transform.GetChild(i).GetChild(0).gameObject);
			}
		}
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x000A6A84 File Offset: 0x000A4C84
	public List<Item> getItemList()
	{
		List<Item> list = new List<Item>();
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount != 0)
			{
				list.Add(this.SlotContainer.transform.GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item);
			}
		}
		return list;
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x000A6AF4 File Offset: 0x000A4CF4
	public void stackableSettings()
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount > 0)
			{
				ItemOnObject component = this.SlotContainer.transform.GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
				if (component.item.maxStack > 1)
				{
					Transform component2 = this.SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<RectTransform>();
					Text component3 = this.SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Text>();
					component3.text = string.Concat(component.item.itemValue);
					component3.enabled = this.stackable;
					component2.localPosition = new Vector3((float)this.positionNumberX, (float)this.positionNumberY, 0f);
				}
				else
				{
					this.SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Text>().enabled = false;
				}
			}
		}
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x000A6C20 File Offset: 0x000A4E20
	public GameObject getItemGameObjectByName(Item item)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount != 0)
			{
				GameObject gameObject = this.SlotContainer.transform.GetChild(i).GetChild(0).gameObject;
				if (gameObject.GetComponent<ItemOnObject>().item.itemName.Equals(item.itemName))
				{
					return gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x000A6CA0 File Offset: 0x000A4EA0
	public GameObject getItemGameObject(Item item)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount != 0)
			{
				GameObject gameObject = this.SlotContainer.transform.GetChild(i).GetChild(0).gameObject;
				if (gameObject.GetComponent<ItemOnObject>().item.Equals(item))
				{
					return gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x000A6D14 File Offset: 0x000A4F14
	public GameObject getItemGameObject(int itemId)
	{
		int num = -1;
		foreach (Item item in this.ItemsInInventory)
		{
			if (item.itemID == itemId)
			{
				num = item.itemIndex;
				break;
			}
		}
		if (num != -1 && this.SlotContainer.transform.GetChild(num).childCount != 0)
		{
			return this.SlotContainer.transform.GetChild(num).GetChild(0).gameObject;
		}
		return null;
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x000A6DB0 File Offset: 0x000A4FB0
	public void changeInventoryPanelDesign(Image image)
	{
		Image component = base.transform.GetChild(0).GetChild(0).GetComponent<Image>();
		component.sprite = image.sprite;
		component.color = image.color;
		component.material = image.material;
		component.type = image.type;
		component.fillCenter = image.fillCenter;
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x000A6E10 File Offset: 0x000A5010
	public void deleteItem(Item item)
	{
		for (int i = 0; i < this.ItemsInInventory.Count; i++)
		{
			if (item.Equals(this.ItemsInInventory[i]))
			{
				this.ItemsInInventory.RemoveAt(item.indexItemInList);
			}
		}
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x00010645 File Offset: 0x0000E845
	public void deleteItemByIndex(int index)
	{
		if (this.SlotContainer.transform.GetChild(index).childCount != 0)
		{
			Object.Destroy(this.SlotContainer.transform.GetChild(index).GetChild(0).gameObject);
		}
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x000A6E58 File Offset: 0x000A5058
	public void deleteItemFromInventory(Item item)
	{
		for (int i = 0; i < this.ItemsInInventory.Count; i++)
		{
			if (item.Equals(this.ItemsInInventory[i]))
			{
				this.ItemsInInventory.RemoveAt(i);
			}
		}
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x000A6E9C File Offset: 0x000A509C
	public void deleteItemFromInventoryWithGameObject(Item item)
	{
		for (int i = 0; i < this.ItemsInInventory.Count; i++)
		{
			if (item.Equals(this.ItemsInInventory[i]))
			{
				this.ItemsInInventory.RemoveAt(i);
			}
		}
		for (int j = 0; j < this.SlotContainer.transform.childCount; j++)
		{
			if (this.SlotContainer.transform.GetChild(j).childCount != 0)
			{
				GameObject gameObject = this.SlotContainer.transform.GetChild(j).GetChild(0).gameObject;
				if (gameObject.GetComponent<ItemOnObject>().item.Equals(item))
				{
					Object.Destroy(gameObject);
					return;
				}
			}
		}
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x000A6F4C File Offset: 0x000A514C
	public int getPositionOfItem(Item item)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount != 0)
			{
				Item item2 = this.SlotContainer.transform.GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item;
				if (item.Equals(item2))
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x000A6FBC File Offset: 0x000A51BC
	public void addItemToInventory(int ignoreSlot, int itemID, int itemValue)
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount == 0 && i != ignoreSlot)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.prefabItem);
				ItemOnObject component = gameObject.GetComponent<ItemOnObject>();
				component.item = this.itemDatabase.getItemByID(itemID);
				if (component.item.itemValue < component.item.maxStack && itemValue <= component.item.maxStack)
				{
					component.item.itemValue = itemValue;
				}
				else
				{
					component.item.itemValue = 1;
				}
				gameObject.transform.SetParent(this.SlotContainer.transform.GetChild(i));
				gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
				component.item.indexItemInList = 999;
				this.updateItemSize();
				this.stackableSettings();
				break;
			}
		}
		this.stackableSettings();
		this.updateItemList();
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x000A70C8 File Offset: 0x000A52C8
	public int getFirstEmptyItemIndex()
	{
		for (int i = 0; i < this.SlotContainer.transform.childCount; i++)
		{
			if (this.SlotContainer.transform.GetChild(i).childCount == 0)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x000A710C File Offset: 0x000A530C
	public void updateItemIndex()
	{
		for (int i = 0; i < this.ItemsInInventory.Count; i++)
		{
			this.ItemsInInventory[i].indexItemInList = i;
		}
	}

	// Token: 0x04000CF0 RID: 3312
	[SerializeField]
	private GameObject prefabCanvasWithPanel;

	// Token: 0x04000CF1 RID: 3313
	[SerializeField]
	private GameObject prefabSlot;

	// Token: 0x04000CF2 RID: 3314
	[SerializeField]
	private GameObject prefabSlotContainer;

	// Token: 0x04000CF3 RID: 3315
	[SerializeField]
	private GameObject prefabItem;

	// Token: 0x04000CF4 RID: 3316
	[SerializeField]
	private GameObject prefabDraggingItemContainer;

	// Token: 0x04000CF5 RID: 3317
	[SerializeField]
	private GameObject prefabPanel;

	// Token: 0x04000CF6 RID: 3318
	[SerializeField]
	private ItemDataBaseList itemDatabase;

	// Token: 0x04000CF7 RID: 3319
	[SerializeField]
	private string inventoryTitle;

	// Token: 0x04000CF8 RID: 3320
	[SerializeField]
	private RectTransform PanelRectTransform;

	// Token: 0x04000CF9 RID: 3321
	[SerializeField]
	private Image PanelImage;

	// Token: 0x04000CFA RID: 3322
	[SerializeField]
	private GameObject SlotContainer;

	// Token: 0x04000CFB RID: 3323
	[SerializeField]
	private GameObject DraggingItemContainer;

	// Token: 0x04000CFC RID: 3324
	[SerializeField]
	private RectTransform SlotContainerRectTransform;

	// Token: 0x04000CFD RID: 3325
	[SerializeField]
	private GridLayoutGroup SlotGridLayout;

	// Token: 0x04000CFE RID: 3326
	[SerializeField]
	private RectTransform SlotGridRectTransform;

	// Token: 0x04000CFF RID: 3327
	[SerializeField]
	public bool mainInventory;

	// Token: 0x04000D00 RID: 3328
	[SerializeField]
	public List<Item> ItemsInInventory = new List<Item>();

	// Token: 0x04000D01 RID: 3329
	[SerializeField]
	public int height;

	// Token: 0x04000D02 RID: 3330
	[SerializeField]
	public int width;

	// Token: 0x04000D03 RID: 3331
	[SerializeField]
	public bool stackable;

	// Token: 0x04000D04 RID: 3332
	[SerializeField]
	public static bool inventoryOpen;

	// Token: 0x04000D05 RID: 3333
	[SerializeField]
	public int slotSize;

	// Token: 0x04000D06 RID: 3334
	[SerializeField]
	public int iconSize;

	// Token: 0x04000D07 RID: 3335
	[SerializeField]
	public int paddingBetweenX;

	// Token: 0x04000D08 RID: 3336
	[SerializeField]
	public int paddingBetweenY;

	// Token: 0x04000D09 RID: 3337
	[SerializeField]
	public int paddingLeft;

	// Token: 0x04000D0A RID: 3338
	[SerializeField]
	public int paddingRight;

	// Token: 0x04000D0B RID: 3339
	[SerializeField]
	public int paddingBottom;

	// Token: 0x04000D0C RID: 3340
	[SerializeField]
	public int paddingTop;

	// Token: 0x04000D0D RID: 3341
	[SerializeField]
	public int positionNumberX;

	// Token: 0x04000D0E RID: 3342
	[SerializeField]
	public int positionNumberY;

	// Token: 0x04000D0F RID: 3343
	private InputManager inputManagerDatabase;

	// Token: 0x0200020A RID: 522
	// (Invoke) Token: 0x06001096 RID: 4246
	public delegate void ItemDelegate(Item item);

	// Token: 0x0200020B RID: 523
	// (Invoke) Token: 0x0600109A RID: 4250
	public delegate void InventoryOpened();
}
