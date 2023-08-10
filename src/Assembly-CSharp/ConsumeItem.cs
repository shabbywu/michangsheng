using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConsumeItem : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public Item item;

	private static Tooltip tooltip;

	public ItemType[] itemTypeOfSlot;

	public static EquipmentSystem eS;

	public GameObject duplication;

	public static GameObject mainInventory;

	private void Start()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		item = ((Component)this).GetComponent<ItemOnObject>().item;
		if ((Object)(object)GameObject.FindGameObjectWithTag("Tooltip") != (Object)null)
		{
			tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
		}
		if ((Object)(object)GameObject.FindGameObjectWithTag("EquipmentSystem") != (Object)null)
		{
			eS = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		}
		if ((Object)(object)GameObject.FindGameObjectWithTag("MainInventory") != (Object)null)
		{
			mainInventory = GameObject.FindGameObjectWithTag("MainInventory");
		}
	}

	public void OnPointerDown(PointerEventData data)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Invalid comparison between Unknown and I4
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)((Component)((Component)this).gameObject.transform.parent.parent.parent).GetComponent<EquipmentSystem>() == (Object)null))
		{
			return;
		}
		bool flag = false;
		Inventory component = ((Component)((Component)this).transform.parent.parent.parent).GetComponent<Inventory>();
		if ((Object)(object)eS != (Object)null)
		{
			itemTypeOfSlot = eS.itemTypeOfSlots;
		}
		if ((int)data.button != 1)
		{
			return;
		}
		if ((Object)(object)((Component)((Component)this).transform.parent).GetComponent<CraftResultSlot>() != (Object)null)
		{
			if (!((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().checkIfItemAllreadyExist(this.item.itemID, this.item.itemValue))
			{
				((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().addItemToInventory(this.item.itemID, this.item.itemValue);
				((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().stackableSettings();
			}
			((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().craftSystem.GetComponent<CraftSystem>().deleteItems(this.item);
			((Component)((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().craftSystem.transform.GetChild(3)).GetComponent<CraftResultSlot>().temp = 0;
			tooltip.deactivateTooltip();
			flag = true;
			GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().updateItemList();
		}
		else
		{
			bool flag2 = false;
			if ((Object)(object)eS != (Object)null)
			{
				for (int i = 0; i < eS.slotsInTotal; i++)
				{
					if (itemTypeOfSlot[i].Equals(this.item.itemType) && ((Component)eS).transform.GetChild(1).GetChild(i).childCount == 0)
					{
						flag2 = true;
						if (!((Object)(object)((Component)((Component)eS).transform.GetChild(1).GetChild(i).parent.parent).GetComponent<EquipmentSystem>() != (Object)null) || !((Object)(object)((Component)((Component)this).gameObject.transform.parent.parent.parent).GetComponent<EquipmentSystem>() != (Object)null))
						{
							component.EquiptItem(this.item);
						}
						((Component)this).gameObject.transform.SetParent(((Component)eS).transform.GetChild(1).GetChild(i));
						((Transform)((Component)this).gameObject.GetComponent<RectTransform>()).localPosition = Vector3.zero;
						((Component)eS).gameObject.GetComponent<Inventory>().updateItemList();
						component.updateItemList();
						flag = true;
						if ((Object)(object)duplication != (Object)null)
						{
							Object.Destroy((Object)(object)duplication.gameObject);
						}
						break;
					}
				}
				if (!flag2)
				{
					for (int j = 0; j < eS.slotsInTotal; j++)
					{
						if (!itemTypeOfSlot[j].Equals(this.item.itemType) || ((Component)eS).transform.GetChild(1).GetChild(j).childCount == 0)
						{
							continue;
						}
						GameObject gameObject = ((Component)((Component)eS).transform.GetChild(1).GetChild(j).GetChild(0)).gameObject;
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
						if ((Object)(object)this == (Object)null)
						{
							GameObject obj = Object.Instantiate<GameObject>(item.itemModel);
							obj.AddComponent<PickUpItem>();
							obj.GetComponent<PickUpItem>().item = item;
							obj.transform.localPosition = ((GameObject)KBEngineApp.app.player().renderObj).transform.localPosition;
							component.OnUpdateItemList();
						}
						else
						{
							gameObject.transform.SetParent(((Component)this).transform.parent);
							((Transform)gameObject.GetComponent<RectTransform>()).localPosition = Vector3.zero;
							if ((Object)(object)((Component)((Component)this).gameObject.transform.parent.parent.parent).GetComponent<Hotbar>() != (Object)null)
							{
								createDuplication(gameObject);
							}
							((Component)this).gameObject.transform.SetParent(((Component)eS).transform.GetChild(1).GetChild(j));
							((Transform)((Component)this).gameObject.GetComponent<RectTransform>()).localPosition = Vector3.zero;
						}
						flag = true;
						if ((Object)(object)duplication != (Object)null)
						{
							Object.Destroy((Object)(object)duplication.gameObject);
						}
						((Component)eS).gameObject.GetComponent<Inventory>().updateItemList();
						component.OnUpdateItemList();
						break;
					}
				}
			}
		}
		if (flag || this.item.itemType == ItemType.UFPS_Ammo || this.item.itemType == ItemType.UFPS_Grenade)
		{
			return;
		}
		Item item2 = null;
		if ((Object)(object)duplication != (Object)null)
		{
			item2 = duplication.GetComponent<ItemOnObject>().item;
		}
		component.ConsumeItem(this.item);
		this.item.itemValue--;
		if (item2 != null)
		{
			duplication.GetComponent<ItemOnObject>().item.itemValue--;
			if (item2.itemValue <= 0)
			{
				if ((Object)(object)tooltip != (Object)null)
				{
					tooltip.deactivateTooltip();
				}
				component.deleteItemFromInventory(this.item);
				Object.Destroy((Object)(object)duplication.gameObject);
			}
		}
		if (this.item.itemValue <= 0)
		{
			if ((Object)(object)tooltip != (Object)null)
			{
				tooltip.deactivateTooltip();
			}
			component.deleteItemFromInventory(this.item);
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}

	public void consumeIt()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		Inventory component = ((Component)((Component)this).transform.parent.parent.parent).GetComponent<Inventory>();
		bool flag = false;
		if ((Object)(object)GameObject.FindGameObjectWithTag("EquipmentSystem") != (Object)null)
		{
			eS = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		}
		if ((Object)(object)eS != (Object)null)
		{
			itemTypeOfSlot = eS.itemTypeOfSlots;
		}
		Item item = null;
		if ((Object)(object)duplication != (Object)null)
		{
			item = duplication.GetComponent<ItemOnObject>().item;
		}
		bool flag2 = false;
		if ((Object)(object)eS != (Object)null)
		{
			for (int i = 0; i < eS.slotsInTotal; i++)
			{
				if (itemTypeOfSlot[i].Equals(this.item.itemType) && ((Component)eS).transform.GetChild(1).GetChild(i).childCount == 0)
				{
					flag2 = true;
					((Component)this).gameObject.transform.SetParent(((Component)eS).transform.GetChild(1).GetChild(i));
					((Transform)((Component)this).gameObject.GetComponent<RectTransform>()).localPosition = Vector3.zero;
					((Component)eS).gameObject.GetComponent<Inventory>().updateItemList();
					component.updateItemList();
					component.EquiptItem(this.item);
					flag = true;
					if ((Object)(object)duplication != (Object)null)
					{
						Object.Destroy((Object)(object)duplication.gameObject);
					}
					break;
				}
			}
			if (!flag2)
			{
				for (int j = 0; j < eS.slotsInTotal; j++)
				{
					if (!itemTypeOfSlot[j].Equals(this.item.itemType) || ((Component)eS).transform.GetChild(1).GetChild(j).childCount == 0)
					{
						continue;
					}
					GameObject gameObject = ((Component)((Component)eS).transform.GetChild(1).GetChild(j).GetChild(0)).gameObject;
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
					if ((Object)(object)this == (Object)null)
					{
						GameObject obj = Object.Instantiate<GameObject>(item2.itemModel);
						obj.AddComponent<PickUpItem>();
						obj.GetComponent<PickUpItem>().item = item2;
						obj.transform.localPosition = ((GameObject)KBEngineApp.app.player().renderObj).transform.localPosition;
						component.OnUpdateItemList();
					}
					else
					{
						gameObject.transform.SetParent(((Component)this).transform.parent);
						((Transform)gameObject.GetComponent<RectTransform>()).localPosition = Vector3.zero;
						if ((Object)(object)((Component)((Component)this).gameObject.transform.parent.parent.parent).GetComponent<Hotbar>() != (Object)null)
						{
							createDuplication(gameObject);
						}
						((Component)this).gameObject.transform.SetParent(((Component)eS).transform.GetChild(1).GetChild(j));
						((Transform)((Component)this).gameObject.GetComponent<RectTransform>()).localPosition = Vector3.zero;
					}
					flag = true;
					if ((Object)(object)duplication != (Object)null)
					{
						Object.Destroy((Object)(object)duplication.gameObject);
					}
					((Component)eS).gameObject.GetComponent<Inventory>().updateItemList();
					component.OnUpdateItemList();
					break;
				}
			}
		}
		if (flag || this.item.itemType == ItemType.UFPS_Ammo || this.item.itemType == ItemType.UFPS_Grenade)
		{
			return;
		}
		if ((Object)(object)duplication != (Object)null)
		{
			item = duplication.GetComponent<ItemOnObject>().item;
		}
		component.ConsumeItem(this.item);
		this.item.itemValue--;
		if (item != null)
		{
			duplication.GetComponent<ItemOnObject>().item.itemValue--;
			if (item.itemValue <= 0)
			{
				if ((Object)(object)tooltip != (Object)null)
				{
					tooltip.deactivateTooltip();
				}
				component.deleteItemFromInventory(this.item);
				Object.Destroy((Object)(object)duplication.gameObject);
			}
		}
		if (this.item.itemValue <= 0)
		{
			if ((Object)(object)tooltip != (Object)null)
			{
				tooltip.deactivateTooltip();
			}
			component.deleteItemFromInventory(this.item);
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}

	public void createDuplication(GameObject Item)
	{
		Item item = Item.GetComponent<ItemOnObject>().item;
		GameObject val = mainInventory.GetComponent<Inventory>().addItemToInventory(item.itemID, item.itemValue);
		Item.GetComponent<ConsumeItem>().duplication = val;
		val.GetComponent<ConsumeItem>().duplication = Item;
	}
}
