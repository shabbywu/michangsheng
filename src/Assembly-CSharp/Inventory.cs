using System;
using System.Collections;
using System.Collections.Generic;
using UltimateSurvival;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000138 RID: 312
public class Inventory : MonoBehaviour
{
	// Token: 0x1400002F RID: 47
	// (add) Token: 0x06000E4B RID: 3659 RVA: 0x00054C20 File Offset: 0x00052E20
	// (remove) Token: 0x06000E4C RID: 3660 RVA: 0x00054C54 File Offset: 0x00052E54
	public static event Inventory.ItemDelegate ItemConsumed;

	// Token: 0x14000030 RID: 48
	// (add) Token: 0x06000E4D RID: 3661 RVA: 0x00054C88 File Offset: 0x00052E88
	// (remove) Token: 0x06000E4E RID: 3662 RVA: 0x00054CBC File Offset: 0x00052EBC
	public static event Inventory.ItemDelegate ItemEquip;

	// Token: 0x14000031 RID: 49
	// (add) Token: 0x06000E4F RID: 3663 RVA: 0x00054CF0 File Offset: 0x00052EF0
	// (remove) Token: 0x06000E50 RID: 3664 RVA: 0x00054D24 File Offset: 0x00052F24
	public static event Inventory.ItemDelegate UnEquipItem;

	// Token: 0x14000032 RID: 50
	// (add) Token: 0x06000E51 RID: 3665 RVA: 0x00054D58 File Offset: 0x00052F58
	// (remove) Token: 0x06000E52 RID: 3666 RVA: 0x00054D8C File Offset: 0x00052F8C
	public static event Inventory.InventoryOpened InventoryOpen;

	// Token: 0x14000033 RID: 51
	// (add) Token: 0x06000E53 RID: 3667 RVA: 0x00054DC0 File Offset: 0x00052FC0
	// (remove) Token: 0x06000E54 RID: 3668 RVA: 0x00054DF4 File Offset: 0x00052FF4
	public static event Inventory.InventoryOpened AllInventoriesClosed;

	// Token: 0x06000E55 RID: 3669 RVA: 0x00054E27 File Offset: 0x00053027
	private void Start()
	{
		if (base.transform.GetComponent<global::Hotbar>() == null)
		{
			base.gameObject.SetActive(false);
		}
		this.updateItemList();
		this.inputManagerDatabase = (InputManager)Resources.Load("InputManager");
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x00054E64 File Offset: 0x00053064
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

	// Token: 0x06000E57 RID: 3671 RVA: 0x00054F33 File Offset: 0x00053133
	private void Update()
	{
		this.updateItemIndex();
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x00054F3B File Offset: 0x0005313B
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

	// Token: 0x06000E59 RID: 3673 RVA: 0x00054F6E File Offset: 0x0005316E
	public void OnUpdateItemList()
	{
		this.updateItemList();
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x00054F76 File Offset: 0x00053176
	public void closeInventory()
	{
		base.gameObject.SetActive(false);
		this.checkIfAllInventoryClosed();
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x00054F8A File Offset: 0x0005318A
	public void openInventory()
	{
		base.gameObject.SetActive(true);
		if (Inventory.InventoryOpen != null)
		{
			Inventory.InventoryOpen();
		}
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x00054FAC File Offset: 0x000531AC
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

	// Token: 0x06000E5D RID: 3677 RVA: 0x000550D9 File Offset: 0x000532D9
	public void ConsumeItem(Item item)
	{
		if (Inventory.ItemConsumed != null)
		{
			Inventory.ItemConsumed(item);
		}
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x000550ED File Offset: 0x000532ED
	public void EquiptItem(Item item)
	{
		if (Inventory.ItemEquip != null)
		{
			Inventory.ItemEquip(item);
		}
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x00055101 File Offset: 0x00053301
	public void UnEquipItem1(Item item)
	{
		if (Inventory.UnEquipItem != null)
		{
			Inventory.UnEquipItem(item);
		}
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x00055118 File Offset: 0x00053318
	public void setImportantVariables()
	{
		this.PanelRectTransform = base.GetComponent<RectTransform>();
		this.SlotContainer = base.transform.GetChild(1).gameObject;
		this.SlotGridLayout = this.SlotContainer.GetComponent<GridLayoutGroup>();
		this.SlotGridRectTransform = this.SlotContainer.GetComponent<RectTransform>();
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x0005516C File Offset: 0x0005336C
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

	// Token: 0x06000E62 RID: 3682 RVA: 0x000552AC File Offset: 0x000534AC
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

	// Token: 0x06000E63 RID: 3683 RVA: 0x00055315 File Offset: 0x00053515
	public bool characterSystem()
	{
		return base.GetComponent<EquipmentSystem>() != null;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x00055328 File Offset: 0x00053528
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

	// Token: 0x06000E65 RID: 3685 RVA: 0x000554B4 File Offset: 0x000536B4
	public void adjustInventorySize()
	{
		int num = this.width * this.slotSize + (this.width - 1) * this.paddingBetweenX + this.paddingLeft + this.paddingRight;
		int num2 = this.height * this.slotSize + (this.height - 1) * this.paddingBetweenY + this.paddingTop + this.paddingBottom;
		this.PanelRectTransform.sizeDelta = new Vector2((float)num, (float)num2);
		this.SlotGridRectTransform.sizeDelta = new Vector2((float)num, (float)num2);
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x00055544 File Offset: 0x00053744
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

	// Token: 0x06000E67 RID: 3687 RVA: 0x000557AC File Offset: 0x000539AC
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

	// Token: 0x06000E68 RID: 3688 RVA: 0x00055A3C File Offset: 0x00053C3C
	public void updateSlotSize(int slotSize)
	{
		this.SlotGridLayout.cellSize = new Vector2((float)slotSize, (float)slotSize);
		this.updateItemSize();
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x00055A58 File Offset: 0x00053C58
	public void updateSlotSize()
	{
		this.SlotGridLayout.cellSize = new Vector2((float)this.slotSize, (float)this.slotSize);
		this.updateItemSize();
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x00055A80 File Offset: 0x00053C80
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

	// Token: 0x06000E6B RID: 3691 RVA: 0x00055B38 File Offset: 0x00053D38
	public void updatePadding(int spacingBetweenX, int spacingBetweenY)
	{
		this.SlotGridLayout.spacing = new Vector2((float)this.paddingBetweenX, (float)this.paddingBetweenY);
		this.SlotGridLayout.padding.bottom = this.paddingBottom;
		this.SlotGridLayout.padding.right = this.paddingRight;
		this.SlotGridLayout.padding.left = this.paddingLeft;
		this.SlotGridLayout.padding.top = this.paddingTop;
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x00055BBC File Offset: 0x00053DBC
	public void updatePadding()
	{
		this.SlotGridLayout.spacing = new Vector2((float)this.paddingBetweenX, (float)this.paddingBetweenY);
		this.SlotGridLayout.padding.bottom = this.paddingBottom;
		this.SlotGridLayout.padding.right = this.paddingRight;
		this.SlotGridLayout.padding.left = this.paddingLeft;
		this.SlotGridLayout.padding.top = this.paddingTop;
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x00055C40 File Offset: 0x00053E40
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

	// Token: 0x06000E6E RID: 3694 RVA: 0x00055D24 File Offset: 0x00053F24
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

	// Token: 0x06000E6F RID: 3695 RVA: 0x00055DF4 File Offset: 0x00053FF4
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

	// Token: 0x06000E70 RID: 3696 RVA: 0x00055EE0 File Offset: 0x000540E0
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

	// Token: 0x06000E71 RID: 3697 RVA: 0x00056024 File Offset: 0x00054224
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

	// Token: 0x06000E72 RID: 3698 RVA: 0x00056174 File Offset: 0x00054374
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

	// Token: 0x06000E73 RID: 3699 RVA: 0x00056358 File Offset: 0x00054558
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

	// Token: 0x06000E74 RID: 3700 RVA: 0x00056480 File Offset: 0x00054680
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

	// Token: 0x06000E75 RID: 3701 RVA: 0x000564F8 File Offset: 0x000546F8
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

	// Token: 0x06000E76 RID: 3702 RVA: 0x0005657C File Offset: 0x0005477C
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

	// Token: 0x06000E77 RID: 3703 RVA: 0x0005665C File Offset: 0x0005485C
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

	// Token: 0x06000E78 RID: 3704 RVA: 0x000566C0 File Offset: 0x000548C0
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

	// Token: 0x06000E79 RID: 3705 RVA: 0x00056730 File Offset: 0x00054930
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

	// Token: 0x06000E7A RID: 3706 RVA: 0x0005685C File Offset: 0x00054A5C
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

	// Token: 0x06000E7B RID: 3707 RVA: 0x000568DC File Offset: 0x00054ADC
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

	// Token: 0x06000E7C RID: 3708 RVA: 0x00056950 File Offset: 0x00054B50
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

	// Token: 0x06000E7D RID: 3709 RVA: 0x000569EC File Offset: 0x00054BEC
	public void changeInventoryPanelDesign(Image image)
	{
		Image component = base.transform.GetChild(0).GetChild(0).GetComponent<Image>();
		component.sprite = image.sprite;
		component.color = image.color;
		component.material = image.material;
		component.type = image.type;
		component.fillCenter = image.fillCenter;
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x00056A4C File Offset: 0x00054C4C
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

	// Token: 0x06000E7F RID: 3711 RVA: 0x00056A94 File Offset: 0x00054C94
	public void deleteItemByIndex(int index)
	{
		if (this.SlotContainer.transform.GetChild(index).childCount != 0)
		{
			Object.Destroy(this.SlotContainer.transform.GetChild(index).GetChild(0).gameObject);
		}
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x00056AD0 File Offset: 0x00054CD0
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

	// Token: 0x06000E81 RID: 3713 RVA: 0x00056B14 File Offset: 0x00054D14
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

	// Token: 0x06000E82 RID: 3714 RVA: 0x00056BC4 File Offset: 0x00054DC4
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

	// Token: 0x06000E83 RID: 3715 RVA: 0x00056C34 File Offset: 0x00054E34
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

	// Token: 0x06000E84 RID: 3716 RVA: 0x00056D40 File Offset: 0x00054F40
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

	// Token: 0x06000E85 RID: 3717 RVA: 0x00056D84 File Offset: 0x00054F84
	public void updateItemIndex()
	{
		for (int i = 0; i < this.ItemsInInventory.Count; i++)
		{
			this.ItemsInInventory[i].indexItemInList = i;
		}
	}

	// Token: 0x04000A58 RID: 2648
	[SerializeField]
	private GameObject prefabCanvasWithPanel;

	// Token: 0x04000A59 RID: 2649
	[SerializeField]
	private GameObject prefabSlot;

	// Token: 0x04000A5A RID: 2650
	[SerializeField]
	private GameObject prefabSlotContainer;

	// Token: 0x04000A5B RID: 2651
	[SerializeField]
	private GameObject prefabItem;

	// Token: 0x04000A5C RID: 2652
	[SerializeField]
	private GameObject prefabDraggingItemContainer;

	// Token: 0x04000A5D RID: 2653
	[SerializeField]
	private GameObject prefabPanel;

	// Token: 0x04000A5E RID: 2654
	[SerializeField]
	private ItemDataBaseList itemDatabase;

	// Token: 0x04000A5F RID: 2655
	[SerializeField]
	private string inventoryTitle;

	// Token: 0x04000A60 RID: 2656
	[SerializeField]
	private RectTransform PanelRectTransform;

	// Token: 0x04000A61 RID: 2657
	[SerializeField]
	private Image PanelImage;

	// Token: 0x04000A62 RID: 2658
	[SerializeField]
	private GameObject SlotContainer;

	// Token: 0x04000A63 RID: 2659
	[SerializeField]
	private GameObject DraggingItemContainer;

	// Token: 0x04000A64 RID: 2660
	[SerializeField]
	private RectTransform SlotContainerRectTransform;

	// Token: 0x04000A65 RID: 2661
	[SerializeField]
	private GridLayoutGroup SlotGridLayout;

	// Token: 0x04000A66 RID: 2662
	[SerializeField]
	private RectTransform SlotGridRectTransform;

	// Token: 0x04000A67 RID: 2663
	[SerializeField]
	public bool mainInventory;

	// Token: 0x04000A68 RID: 2664
	[SerializeField]
	public List<Item> ItemsInInventory = new List<Item>();

	// Token: 0x04000A69 RID: 2665
	[SerializeField]
	public int height;

	// Token: 0x04000A6A RID: 2666
	[SerializeField]
	public int width;

	// Token: 0x04000A6B RID: 2667
	[SerializeField]
	public bool stackable;

	// Token: 0x04000A6C RID: 2668
	[SerializeField]
	public static bool inventoryOpen;

	// Token: 0x04000A6D RID: 2669
	[SerializeField]
	public int slotSize;

	// Token: 0x04000A6E RID: 2670
	[SerializeField]
	public int iconSize;

	// Token: 0x04000A6F RID: 2671
	[SerializeField]
	public int paddingBetweenX;

	// Token: 0x04000A70 RID: 2672
	[SerializeField]
	public int paddingBetweenY;

	// Token: 0x04000A71 RID: 2673
	[SerializeField]
	public int paddingLeft;

	// Token: 0x04000A72 RID: 2674
	[SerializeField]
	public int paddingRight;

	// Token: 0x04000A73 RID: 2675
	[SerializeField]
	public int paddingBottom;

	// Token: 0x04000A74 RID: 2676
	[SerializeField]
	public int paddingTop;

	// Token: 0x04000A75 RID: 2677
	[SerializeField]
	public int positionNumberX;

	// Token: 0x04000A76 RID: 2678
	[SerializeField]
	public int positionNumberY;

	// Token: 0x04000A77 RID: 2679
	private InputManager inputManagerDatabase;

	// Token: 0x0200128E RID: 4750
	// (Invoke) Token: 0x060079B1 RID: 31153
	public delegate void ItemDelegate(Item item);

	// Token: 0x0200128F RID: 4751
	// (Invoke) Token: 0x060079B5 RID: 31157
	public delegate void InventoryOpened();
}
