using System.Collections.Generic;
using UltimateSurvival;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public delegate void ItemDelegate(Item item);

	public delegate void InventoryOpened();

	[SerializeField]
	private GameObject prefabCanvasWithPanel;

	[SerializeField]
	private GameObject prefabSlot;

	[SerializeField]
	private GameObject prefabSlotContainer;

	[SerializeField]
	private GameObject prefabItem;

	[SerializeField]
	private GameObject prefabDraggingItemContainer;

	[SerializeField]
	private GameObject prefabPanel;

	[SerializeField]
	private ItemDataBaseList itemDatabase;

	[SerializeField]
	private string inventoryTitle;

	[SerializeField]
	private RectTransform PanelRectTransform;

	[SerializeField]
	private Image PanelImage;

	[SerializeField]
	private GameObject SlotContainer;

	[SerializeField]
	private GameObject DraggingItemContainer;

	[SerializeField]
	private RectTransform SlotContainerRectTransform;

	[SerializeField]
	private GridLayoutGroup SlotGridLayout;

	[SerializeField]
	private RectTransform SlotGridRectTransform;

	[SerializeField]
	public bool mainInventory;

	[SerializeField]
	public List<Item> ItemsInInventory = new List<Item>();

	[SerializeField]
	public int height;

	[SerializeField]
	public int width;

	[SerializeField]
	public bool stackable;

	[SerializeField]
	public static bool inventoryOpen;

	[SerializeField]
	public int slotSize;

	[SerializeField]
	public int iconSize;

	[SerializeField]
	public int paddingBetweenX;

	[SerializeField]
	public int paddingBetweenY;

	[SerializeField]
	public int paddingLeft;

	[SerializeField]
	public int paddingRight;

	[SerializeField]
	public int paddingBottom;

	[SerializeField]
	public int paddingTop;

	[SerializeField]
	public int positionNumberX;

	[SerializeField]
	public int positionNumberY;

	private InputManager inputManagerDatabase;

	public static event ItemDelegate ItemConsumed;

	public static event ItemDelegate ItemEquip;

	public static event ItemDelegate UnEquipItem;

	public static event InventoryOpened InventoryOpen;

	public static event InventoryOpened AllInventoriesClosed;

	private void Start()
	{
		if ((Object)(object)((Component)((Component)this).transform).GetComponent<Hotbar>() == (Object)null)
		{
			((Component)this).gameObject.SetActive(false);
		}
		updateItemList();
		inputManagerDatabase = (InputManager)(object)Resources.Load("InputManager");
	}

	public void sortItems()
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		int num = -1;
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount == 0 && num == -1)
			{
				num = i;
			}
			else if (num > -1 && SlotContainer.transform.GetChild(i).childCount != 0)
			{
				RectTransform component = ((Component)SlotContainer.transform.GetChild(i).GetChild(0)).GetComponent<RectTransform>();
				((Component)SlotContainer.transform.GetChild(i).GetChild(0)).transform.SetParent(((Component)SlotContainer.transform.GetChild(num)).transform);
				((Transform)component).localPosition = Vector3.zero;
				i = num + 1;
				num = i;
			}
		}
	}

	private void Update()
	{
		updateItemIndex();
	}

	public void setAsMain()
	{
		if (mainInventory)
		{
			((Component)this).gameObject.tag = "Untagged";
		}
		else if (!mainInventory)
		{
			((Component)this).gameObject.tag = "MainInventory";
		}
	}

	public void OnUpdateItemList()
	{
		updateItemList();
	}

	public void closeInventory()
	{
		((Component)this).gameObject.SetActive(false);
		checkIfAllInventoryClosed();
	}

	public void openInventory()
	{
		((Component)this).gameObject.SetActive(true);
		if (Inventory.InventoryOpen != null)
		{
			Inventory.InventoryOpen();
		}
	}

	public void checkIfAllInventoryClosed()
	{
		GameObject val = GameObject.FindGameObjectWithTag("Canvas");
		for (int i = 0; i < val.transform.childCount; i++)
		{
			GameObject gameObject = ((Component)val.transform.GetChild(i)).gameObject;
			if (!gameObject.activeSelf && (gameObject.tag == "EquipmentSystem" || gameObject.tag == "Panel" || gameObject.tag == "MainInventory" || gameObject.tag == "CraftSystem"))
			{
				if (Inventory.AllInventoriesClosed != null && i == val.transform.childCount - 1)
				{
					Inventory.AllInventoriesClosed();
				}
				continue;
			}
			if (!gameObject.activeSelf || (!(gameObject.tag == "EquipmentSystem") && !(gameObject.tag == "Panel") && !(gameObject.tag == "MainInventory") && !(gameObject.tag == "CraftSystem")))
			{
				if (i == val.transform.childCount - 1 && Inventory.AllInventoriesClosed != null)
				{
					Inventory.AllInventoriesClosed();
				}
				continue;
			}
			break;
		}
	}

	public void ConsumeItem(Item item)
	{
		if (Inventory.ItemConsumed != null)
		{
			Inventory.ItemConsumed(item);
		}
	}

	public void EquiptItem(Item item)
	{
		if (Inventory.ItemEquip != null)
		{
			Inventory.ItemEquip(item);
		}
	}

	public void UnEquipItem1(Item item)
	{
		if (Inventory.UnEquipItem != null)
		{
			Inventory.UnEquipItem(item);
		}
	}

	public void setImportantVariables()
	{
		PanelRectTransform = ((Component)this).GetComponent<RectTransform>();
		SlotContainer = ((Component)((Component)this).transform.GetChild(1)).gameObject;
		SlotGridLayout = SlotContainer.GetComponent<GridLayoutGroup>();
		SlotGridRectTransform = SlotContainer.GetComponent<RectTransform>();
	}

	public void getPrefabs()
	{
		if ((Object)(object)prefabCanvasWithPanel == (Object)null)
		{
			ref GameObject reference = ref prefabCanvasWithPanel;
			Object obj = Resources.Load("Prefabs/Canvas - Inventory");
			reference = (GameObject)(object)((obj is GameObject) ? obj : null);
		}
		if ((Object)(object)prefabSlot == (Object)null)
		{
			ref GameObject reference2 = ref prefabSlot;
			Object obj2 = Resources.Load("Prefabs/Slot - Inventory");
			reference2 = (GameObject)(object)((obj2 is GameObject) ? obj2 : null);
		}
		if ((Object)(object)prefabSlotContainer == (Object)null)
		{
			ref GameObject reference3 = ref prefabSlotContainer;
			Object obj3 = Resources.Load("Prefabs/Slots - Inventory");
			reference3 = (GameObject)(object)((obj3 is GameObject) ? obj3 : null);
		}
		if ((Object)(object)prefabItem == (Object)null)
		{
			ref GameObject reference4 = ref prefabItem;
			Object obj4 = Resources.Load("Prefabs/Item");
			reference4 = (GameObject)(object)((obj4 is GameObject) ? obj4 : null);
		}
		if ((Object)(object)itemDatabase == (Object)null)
		{
			itemDatabase = (ItemDataBaseList)(object)Resources.Load("ItemDatabase");
		}
		if ((Object)(object)prefabDraggingItemContainer == (Object)null)
		{
			ref GameObject reference5 = ref prefabDraggingItemContainer;
			Object obj5 = Resources.Load("Prefabs/DraggingItem");
			reference5 = (GameObject)(object)((obj5 is GameObject) ? obj5 : null);
		}
		if ((Object)(object)prefabPanel == (Object)null)
		{
			ref GameObject reference6 = ref prefabPanel;
			Object obj6 = Resources.Load("Prefabs/Panel - Inventory");
			reference6 = (GameObject)(object)((obj6 is GameObject) ? obj6 : null);
		}
		setImportantVariables();
		setDefaultSettings();
		adjustInventorySize();
		updateSlotAmount(width, height);
		updateSlotSize();
		updatePadding(paddingBetweenX, paddingBetweenY);
	}

	public void updateItemList()
	{
		ItemsInInventory.Clear();
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			Transform child = SlotContainer.transform.GetChild(i);
			if (child.childCount != 0)
			{
				ItemsInInventory.Add(((Component)child.GetChild(0)).GetComponent<ItemOnObject>().item);
			}
		}
	}

	public bool characterSystem()
	{
		if ((Object)(object)((Component)this).GetComponent<EquipmentSystem>() != (Object)null)
		{
			return true;
		}
		return false;
	}

	public void setDefaultSettings()
	{
		if ((Object)(object)((Component)this).GetComponent<EquipmentSystem>() == (Object)null && (Object)(object)((Component)this).GetComponent<Hotbar>() == (Object)null && (Object)(object)((Component)this).GetComponent<CraftSystem>() == (Object)null)
		{
			height = 5;
			width = 5;
			slotSize = 50;
			iconSize = 45;
			paddingBetweenX = 5;
			paddingBetweenY = 5;
			paddingTop = 35;
			paddingBottom = 10;
			paddingLeft = 10;
			paddingRight = 10;
		}
		else if ((Object)(object)((Component)this).GetComponent<Hotbar>() != (Object)null)
		{
			height = 1;
			width = 9;
			slotSize = 50;
			iconSize = 45;
			paddingBetweenX = 5;
			paddingBetweenY = 5;
			paddingTop = 10;
			paddingBottom = 10;
			paddingLeft = 10;
			paddingRight = 10;
		}
		else if ((Object)(object)((Component)this).GetComponent<CraftSystem>() != (Object)null)
		{
			height = 3;
			width = 3;
			slotSize = 55;
			iconSize = 45;
			paddingBetweenX = 5;
			paddingBetweenY = 5;
			paddingTop = 35;
			paddingBottom = 95;
			paddingLeft = 25;
			paddingRight = 25;
		}
		else
		{
			height = 4;
			width = 2;
			slotSize = 50;
			iconSize = 45;
			paddingBetweenX = 100;
			paddingBetweenY = 20;
			paddingTop = 35;
			paddingBottom = 10;
			paddingLeft = 10;
			paddingRight = 10;
		}
	}

	public void adjustInventorySize()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		int num = width * slotSize + (width - 1) * paddingBetweenX + paddingLeft + paddingRight;
		int num2 = height * slotSize + (height - 1) * paddingBetweenY + paddingTop + paddingBottom;
		PanelRectTransform.sizeDelta = new Vector2((float)num, (float)num2);
		SlotGridRectTransform.sizeDelta = new Vector2((float)num, (float)num2);
	}

	public void updateSlotAmount(int width, int height)
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		if ((Object)(object)prefabSlot == (Object)null)
		{
			ref GameObject reference = ref prefabSlot;
			Object obj = Resources.Load("Prefabs/Slot - Inventory");
			reference = (GameObject)(object)((obj is GameObject) ? obj : null);
		}
		if ((Object)(object)SlotContainer == (Object)null)
		{
			SlotContainer = Object.Instantiate<GameObject>(prefabSlotContainer);
			SlotContainer.transform.SetParent(((Component)PanelRectTransform).transform);
			SlotContainerRectTransform = SlotContainer.GetComponent<RectTransform>();
			SlotGridRectTransform = SlotContainer.GetComponent<RectTransform>();
			SlotGridLayout = SlotContainer.GetComponent<GridLayoutGroup>();
		}
		if ((Object)(object)SlotContainerRectTransform == (Object)null)
		{
			SlotContainerRectTransform = SlotContainer.GetComponent<RectTransform>();
		}
		((Transform)SlotContainerRectTransform).localPosition = Vector3.zero;
		List<Item> list = new List<Item>();
		List<GameObject> list2 = new List<GameObject>();
		foreach (Transform item in SlotContainer.transform)
		{
			Transform val = item;
			if (((Component)val).tag == "Slot")
			{
				list2.Add(((Component)val).gameObject);
			}
		}
		while (list2.Count > width * height)
		{
			GameObject val2 = list2[list2.Count - 1];
			ItemOnObject componentInChildren = val2.GetComponentInChildren<ItemOnObject>();
			if ((Object)(object)componentInChildren != (Object)null)
			{
				list.Add(componentInChildren.item);
				ItemsInInventory.Remove(componentInChildren.item);
			}
			list2.Remove(val2);
			Object.DestroyImmediate((Object)(object)val2);
		}
		if (list2.Count < width * height)
		{
			for (int i = list2.Count; i < width * height; i++)
			{
				GameObject val3 = Object.Instantiate<GameObject>(prefabSlot);
				((Object)val3).name = (list2.Count + 1).ToString();
				val3.transform.SetParent(SlotContainer.transform);
				list2.Add(val3);
			}
		}
		if (list != null && ItemsInInventory.Count < width * height)
		{
			foreach (Item item2 in list)
			{
				addItemToInventory(item2.itemID);
			}
		}
		setImportantVariables();
	}

	public void updateSlotAmount()
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		if ((Object)(object)prefabSlot == (Object)null)
		{
			ref GameObject reference = ref prefabSlot;
			Object obj = Resources.Load("Prefabs/Slot - Inventory");
			reference = (GameObject)(object)((obj is GameObject) ? obj : null);
		}
		if ((Object)(object)SlotContainer == (Object)null)
		{
			SlotContainer = Object.Instantiate<GameObject>(prefabSlotContainer);
			SlotContainer.transform.SetParent(((Component)PanelRectTransform).transform);
			SlotContainerRectTransform = SlotContainer.GetComponent<RectTransform>();
			SlotGridRectTransform = SlotContainer.GetComponent<RectTransform>();
			SlotGridLayout = SlotContainer.GetComponent<GridLayoutGroup>();
		}
		if ((Object)(object)SlotContainerRectTransform == (Object)null)
		{
			SlotContainerRectTransform = SlotContainer.GetComponent<RectTransform>();
		}
		((Transform)SlotContainerRectTransform).localPosition = Vector3.zero;
		List<Item> list = new List<Item>();
		List<GameObject> list2 = new List<GameObject>();
		foreach (Transform item in SlotContainer.transform)
		{
			Transform val = item;
			if (((Component)val).tag == "Slot")
			{
				list2.Add(((Component)val).gameObject);
			}
		}
		while (list2.Count > width * height)
		{
			GameObject val2 = list2[list2.Count - 1];
			ItemOnObject componentInChildren = val2.GetComponentInChildren<ItemOnObject>();
			if ((Object)(object)componentInChildren != (Object)null)
			{
				list.Add(componentInChildren.item);
				ItemsInInventory.Remove(componentInChildren.item);
			}
			list2.Remove(val2);
			Object.DestroyImmediate((Object)(object)val2);
		}
		if (list2.Count < width * height)
		{
			for (int i = list2.Count; i < width * height; i++)
			{
				GameObject val3 = Object.Instantiate<GameObject>(prefabSlot);
				((Object)val3).name = (list2.Count + 1).ToString();
				val3.transform.SetParent(SlotContainer.transform);
				list2.Add(val3);
			}
		}
		if (list != null && ItemsInInventory.Count < width * height)
		{
			foreach (Item item2 in list)
			{
				addItemToInventory(item2.itemID);
			}
		}
		setImportantVariables();
	}

	public void updateSlotSize(int slotSize)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		SlotGridLayout.cellSize = new Vector2((float)slotSize, (float)slotSize);
		updateItemSize();
	}

	public void updateSlotSize()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		SlotGridLayout.cellSize = new Vector2((float)slotSize, (float)slotSize);
		updateItemSize();
	}

	private void updateItemSize()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount > 0)
			{
				((Component)SlotContainer.transform.GetChild(i).GetChild(0)).GetComponent<RectTransform>().sizeDelta = new Vector2((float)slotSize, (float)slotSize);
				((Component)SlotContainer.transform.GetChild(i).GetChild(0).GetChild(2)).GetComponent<RectTransform>().sizeDelta = new Vector2((float)slotSize, (float)slotSize);
			}
		}
	}

	public void updatePadding(int spacingBetweenX, int spacingBetweenY)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		SlotGridLayout.spacing = new Vector2((float)paddingBetweenX, (float)paddingBetweenY);
		((LayoutGroup)SlotGridLayout).padding.bottom = paddingBottom;
		((LayoutGroup)SlotGridLayout).padding.right = paddingRight;
		((LayoutGroup)SlotGridLayout).padding.left = paddingLeft;
		((LayoutGroup)SlotGridLayout).padding.top = paddingTop;
	}

	public void updatePadding()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		SlotGridLayout.spacing = new Vector2((float)paddingBetweenX, (float)paddingBetweenY);
		((LayoutGroup)SlotGridLayout).padding.bottom = paddingBottom;
		((LayoutGroup)SlotGridLayout).padding.right = paddingRight;
		((LayoutGroup)SlotGridLayout).padding.left = paddingLeft;
		((LayoutGroup)SlotGridLayout).padding.top = paddingTop;
	}

	public void addAllItemsToInventory()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < ItemsInInventory.Count; i++)
		{
			for (int j = 0; j < SlotContainer.transform.childCount; j++)
			{
				if (SlotContainer.transform.GetChild(j).childCount == 0)
				{
					GameObject obj = Object.Instantiate<GameObject>(prefabItem);
					obj.GetComponent<ItemOnObject>().item = ItemsInInventory[i];
					obj.transform.SetParent(SlotContainer.transform.GetChild(j));
					((Transform)obj.GetComponent<RectTransform>()).localPosition = Vector3.zero;
					((Component)obj.transform.GetChild(0)).GetComponent<Image>().sprite = ItemsInInventory[i].itemIcon;
					updateItemSize();
					break;
				}
			}
		}
		stackableSettings();
	}

	public bool checkIfItemAllreadyExist(int itemID, int itemValue)
	{
		updateItemList();
		for (int i = 0; i < ItemsInInventory.Count; i++)
		{
			if (ItemsInInventory[i].itemID != itemID)
			{
				continue;
			}
			int num = ItemsInInventory[i].itemValue + itemValue;
			if (num <= ItemsInInventory[i].maxStack)
			{
				ItemsInInventory[i].itemValue = num;
				GameObject itemGameObject = getItemGameObject(ItemsInInventory[i]);
				if ((Object)(object)itemGameObject != (Object)null && (Object)(object)itemGameObject.GetComponent<ConsumeItem>().duplication != (Object)null)
				{
					itemGameObject.GetComponent<ConsumeItem>().duplication.GetComponent<ItemOnObject>().item.itemValue = num;
				}
				return true;
			}
		}
		return false;
	}

	public void addItemToInventory(int id)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount == 0)
			{
				GameObject val = Object.Instantiate<GameObject>(prefabItem);
				val.GetComponent<ItemOnObject>().item = itemDatabase.getItemByID(id);
				val.transform.SetParent(SlotContainer.transform.GetChild(i));
				((Transform)val.GetComponent<RectTransform>()).localPosition = Vector3.zero;
				((Component)val.transform.GetChild(0)).GetComponent<Image>().sprite = val.GetComponent<ItemOnObject>().item.itemIcon;
				val.GetComponent<ItemOnObject>().item.indexItemInList = ItemsInInventory.Count - 1;
				break;
			}
		}
		stackableSettings();
		updateItemList();
	}

	public GameObject addItemToInventory(int id, int value)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount == 0)
			{
				GameObject obj = Object.Instantiate<GameObject>(prefabItem);
				ItemOnObject component = obj.GetComponent<ItemOnObject>();
				component.item = itemDatabase.getItemByID(id);
				if (component.item.itemValue <= component.item.maxStack && value <= component.item.maxStack)
				{
					component.item.itemValue = value;
				}
				else
				{
					component.item.itemValue = 1;
				}
				obj.transform.SetParent(SlotContainer.transform.GetChild(i));
				((Transform)obj.GetComponent<RectTransform>()).localPosition = Vector3.zero;
				((Component)obj.transform.GetChild(0)).GetComponent<Image>().sprite = component.item.itemIcon;
				component.item.indexItemInList = ItemsInInventory.Count - 1;
				if ((Object)(object)inputManagerDatabase == (Object)null)
				{
					inputManagerDatabase = (InputManager)(object)Resources.Load("InputManager");
				}
				return obj;
			}
		}
		stackableSettings();
		updateItemList();
		return null;
	}

	public GameObject addItemToInventory(int id, ulong itemUUID, int value)
	{
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount == 0)
			{
				GameObject obj = Object.Instantiate<GameObject>(prefabItem);
				ItemOnObject component = obj.GetComponent<ItemOnObject>();
				component.item = itemDatabase.getItemByID(id);
				component.item.itemUUID = itemUUID;
				if (component.item.itemValue <= component.item.maxStack && value <= component.item.maxStack)
				{
					component.item.itemValue = value;
				}
				else
				{
					component.item.itemValue = 1;
				}
				obj.transform.SetParent(SlotContainer.transform.GetChild(i));
				((Transform)obj.GetComponent<RectTransform>()).localPosition = Vector3.zero;
				((Component)obj.transform.GetChild(0)).GetComponent<Image>().sprite = component.item.itemIcon;
				component.item.indexItemInList = ItemsInInventory.Count - 1;
				if ((Object)(object)inputManagerDatabase == (Object)null)
				{
					inputManagerDatabase = (InputManager)(object)Resources.Load("InputManager");
				}
				return obj;
			}
		}
		stackableSettings();
		updateItemList();
		return null;
	}

	public GameObject addItemToInventory(int itemId, ulong itemUUID, int value, int itemIndex)
	{
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		if (itemIndex < 0 || itemIndex >= SlotContainer.transform.childCount)
		{
			return null;
		}
		GameObject val = null;
		val = ((SlotContainer.transform.GetChild(itemIndex).childCount == 0) ? Object.Instantiate<GameObject>(prefabItem) : ((Component)SlotContainer.transform.GetChild(itemIndex).GetChild(0)).gameObject);
		ItemOnObject component = val.GetComponent<ItemOnObject>();
		component.item = itemDatabase.getItemByID(itemId);
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
		val.transform.SetParent(SlotContainer.transform.GetChild(itemIndex));
		((Transform)val.GetComponent<RectTransform>()).localPosition = Vector3.zero;
		((Component)val.transform.GetChild(0)).GetComponent<Image>().sprite = component.item.itemIcon;
		component.item.indexItemInList = ItemsInInventory.Count - 1;
		if ((Object)(object)inputManagerDatabase == (Object)null)
		{
			inputManagerDatabase = (InputManager)(object)Resources.Load("InputManager");
		}
		if (((Object)this).name == "Panel - Inventory(Clone)")
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
			UltimateSurvival.MonoSingleton<InventoryController>.Instance.AddItemToCollection(itemId, itemUUID, value - num, text, out var _, itemIndex);
		}
		return val;
	}

	public void addItemToInventoryStorage(int itemID, int value)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount == 0)
			{
				GameObject obj = Object.Instantiate<GameObject>(prefabItem);
				ItemOnObject component = obj.GetComponent<ItemOnObject>();
				component.item = itemDatabase.getItemByID(itemID);
				if (component.item.itemValue < component.item.maxStack && value <= component.item.maxStack)
				{
					component.item.itemValue = value;
				}
				else
				{
					component.item.itemValue = 1;
				}
				obj.transform.SetParent(SlotContainer.transform.GetChild(i));
				((Transform)obj.GetComponent<RectTransform>()).localPosition = Vector3.zero;
				component.item.indexItemInList = 999;
				if ((Object)(object)inputManagerDatabase == (Object)null)
				{
					inputManagerDatabase = (InputManager)(object)Resources.Load("InputManager");
				}
				updateItemSize();
				stackableSettings();
				break;
			}
		}
		stackableSettings();
		updateItemList();
	}

	public void updateIconSize(int iconSize)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount > 0)
			{
				((Component)SlotContainer.transform.GetChild(i).GetChild(0).GetChild(0)).GetComponent<RectTransform>().sizeDelta = new Vector2((float)iconSize, (float)iconSize);
			}
		}
		updateItemSize();
	}

	public void updateIconSize()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount > 0)
			{
				((Component)SlotContainer.transform.GetChild(i).GetChild(0).GetChild(0)).GetComponent<RectTransform>().sizeDelta = new Vector2((float)iconSize, (float)iconSize);
			}
		}
		updateItemSize();
	}

	public void stackableSettings(bool stackable, Vector3 posi)
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount > 0)
			{
				ItemOnObject component = ((Component)SlotContainer.transform.GetChild(i).GetChild(0)).GetComponent<ItemOnObject>();
				if (component.item.maxStack > 1)
				{
					RectTransform component2 = ((Component)SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1)).GetComponent<RectTransform>();
					Text component3 = ((Component)SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1)).GetComponent<Text>();
					component3.text = string.Concat(component.item.itemValue);
					((Behaviour)component3).enabled = stackable;
					((Transform)component2).localPosition = posi;
				}
			}
		}
	}

	public void deleteAllItems()
	{
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount != 0)
			{
				Object.Destroy((Object)(object)((Component)SlotContainer.transform.GetChild(i).GetChild(0)).gameObject);
			}
		}
	}

	public List<Item> getItemList()
	{
		List<Item> list = new List<Item>();
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount != 0)
			{
				list.Add(((Component)SlotContainer.transform.GetChild(i).GetChild(0)).GetComponent<ItemOnObject>().item);
			}
		}
		return list;
	}

	public void stackableSettings()
	{
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount > 0)
			{
				ItemOnObject component = ((Component)SlotContainer.transform.GetChild(i).GetChild(0)).GetComponent<ItemOnObject>();
				if (component.item.maxStack > 1)
				{
					RectTransform component2 = ((Component)SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1)).GetComponent<RectTransform>();
					Text component3 = ((Component)SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1)).GetComponent<Text>();
					component3.text = string.Concat(component.item.itemValue);
					((Behaviour)component3).enabled = stackable;
					((Transform)component2).localPosition = new Vector3((float)positionNumberX, (float)positionNumberY, 0f);
				}
				else
				{
					((Behaviour)((Component)SlotContainer.transform.GetChild(i).GetChild(0).GetChild(1)).GetComponent<Text>()).enabled = false;
				}
			}
		}
	}

	public GameObject getItemGameObjectByName(Item item)
	{
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount != 0)
			{
				GameObject gameObject = ((Component)SlotContainer.transform.GetChild(i).GetChild(0)).gameObject;
				if (gameObject.GetComponent<ItemOnObject>().item.itemName.Equals(item.itemName))
				{
					return gameObject;
				}
			}
		}
		return null;
	}

	public GameObject getItemGameObject(Item item)
	{
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount != 0)
			{
				GameObject gameObject = ((Component)SlotContainer.transform.GetChild(i).GetChild(0)).gameObject;
				if (gameObject.GetComponent<ItemOnObject>().item.Equals(item))
				{
					return gameObject;
				}
			}
		}
		return null;
	}

	public GameObject getItemGameObject(int itemId)
	{
		int num = -1;
		foreach (Item item in ItemsInInventory)
		{
			if (item.itemID == itemId)
			{
				num = item.itemIndex;
				break;
			}
		}
		if (num != -1 && SlotContainer.transform.GetChild(num).childCount != 0)
		{
			return ((Component)SlotContainer.transform.GetChild(num).GetChild(0)).gameObject;
		}
		return null;
	}

	public void changeInventoryPanelDesign(Image image)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Image component = ((Component)((Component)this).transform.GetChild(0).GetChild(0)).GetComponent<Image>();
		component.sprite = image.sprite;
		((Graphic)component).color = ((Graphic)image).color;
		((Graphic)component).material = ((Graphic)image).material;
		component.type = image.type;
		component.fillCenter = image.fillCenter;
	}

	public void deleteItem(Item item)
	{
		for (int i = 0; i < ItemsInInventory.Count; i++)
		{
			if (item.Equals(ItemsInInventory[i]))
			{
				ItemsInInventory.RemoveAt(item.indexItemInList);
			}
		}
	}

	public void deleteItemByIndex(int index)
	{
		if (SlotContainer.transform.GetChild(index).childCount != 0)
		{
			Object.Destroy((Object)(object)((Component)SlotContainer.transform.GetChild(index).GetChild(0)).gameObject);
		}
	}

	public void deleteItemFromInventory(Item item)
	{
		for (int i = 0; i < ItemsInInventory.Count; i++)
		{
			if (item.Equals(ItemsInInventory[i]))
			{
				ItemsInInventory.RemoveAt(i);
			}
		}
	}

	public void deleteItemFromInventoryWithGameObject(Item item)
	{
		for (int i = 0; i < ItemsInInventory.Count; i++)
		{
			if (item.Equals(ItemsInInventory[i]))
			{
				ItemsInInventory.RemoveAt(i);
			}
		}
		for (int j = 0; j < SlotContainer.transform.childCount; j++)
		{
			if (SlotContainer.transform.GetChild(j).childCount != 0)
			{
				GameObject gameObject = ((Component)SlotContainer.transform.GetChild(j).GetChild(0)).gameObject;
				if (gameObject.GetComponent<ItemOnObject>().item.Equals(item))
				{
					Object.Destroy((Object)(object)gameObject);
					break;
				}
			}
		}
	}

	public int getPositionOfItem(Item item)
	{
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount != 0)
			{
				Item item2 = ((Component)SlotContainer.transform.GetChild(i).GetChild(0)).GetComponent<ItemOnObject>().item;
				if (item.Equals(item2))
				{
					return i;
				}
			}
		}
		return -1;
	}

	public void addItemToInventory(int ignoreSlot, int itemID, int itemValue)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount == 0 && i != ignoreSlot)
			{
				GameObject obj = Object.Instantiate<GameObject>(prefabItem);
				ItemOnObject component = obj.GetComponent<ItemOnObject>();
				component.item = itemDatabase.getItemByID(itemID);
				if (component.item.itemValue < component.item.maxStack && itemValue <= component.item.maxStack)
				{
					component.item.itemValue = itemValue;
				}
				else
				{
					component.item.itemValue = 1;
				}
				obj.transform.SetParent(SlotContainer.transform.GetChild(i));
				((Transform)obj.GetComponent<RectTransform>()).localPosition = Vector3.zero;
				component.item.indexItemInList = 999;
				updateItemSize();
				stackableSettings();
				break;
			}
		}
		stackableSettings();
		updateItemList();
	}

	public int getFirstEmptyItemIndex()
	{
		for (int i = 0; i < SlotContainer.transform.childCount; i++)
		{
			if (SlotContainer.transform.GetChild(i).childCount == 0)
			{
				return i;
			}
		}
		return -1;
	}

	public void updateItemIndex()
	{
		for (int i = 0; i < ItemsInInventory.Count; i++)
		{
			ItemsInInventory[i].indexItemInList = i;
		}
	}
}
