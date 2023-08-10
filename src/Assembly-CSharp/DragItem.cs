using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IEventSystemHandler, IPointerDownHandler, IEndDragHandler
{
	public delegate void ItemDelegate();

	private Vector2 pointerOffset;

	private RectTransform rectTransform;

	private RectTransform rectTransformSlot;

	private CanvasGroup canvasGroup;

	private GameObject oldSlot;

	private Inventory inventory;

	private Transform draggedItemBox;

	public static event ItemDelegate updateInventoryList;

	private void Start()
	{
		rectTransform = ((Component)this).GetComponent<RectTransform>();
		canvasGroup = ((Component)this).GetComponent<CanvasGroup>();
		rectTransformSlot = GameObject.FindGameObjectWithTag("DraggingItem").GetComponent<RectTransform>();
		inventory = ((Component)((Component)this).transform.parent.parent.parent).GetComponent<Inventory>();
		draggedItemBox = GameObject.FindGameObjectWithTag("DraggingItem").transform;
	}

	public void OnDrag(PointerEventData data)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)rectTransform == (Object)null)
		{
			return;
		}
		if ((int)data.button == 0 && (Object)(object)((Component)((Component)this).transform.parent).GetComponent<CraftResultSlot>() == (Object)null)
		{
			((Transform)rectTransform).SetAsLastSibling();
			((Component)this).transform.SetParent(draggedItemBox);
			canvasGroup.blocksRaycasts = false;
			Vector2 val = default(Vector2);
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransformSlot, Vector2.op_Implicit(Input.mousePosition), data.pressEventCamera, ref val))
			{
				((Transform)rectTransform).localPosition = Vector2.op_Implicit(val - pointerOffset);
				if ((Object)(object)((Component)((Component)this).transform).GetComponent<ConsumeItem>().duplication != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)((Component)this).transform).GetComponent<ConsumeItem>().duplication);
				}
			}
		}
		inventory.OnUpdateItemList();
	}

	public void OnPointerDown(PointerEventData data)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if ((int)data.button == 0)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, ref pointerOffset);
			oldSlot = ((Component)((Component)this).transform.parent).gameObject;
		}
		if (DragItem.updateInventoryList != null)
		{
			DragItem.updateInventoryList();
		}
	}

	public void createDuplication(GameObject Item)
	{
		Item item = Item.GetComponent<ItemOnObject>().item;
		GameObject val = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().addItemToInventory(item.itemID, item.itemValue);
		((Component)val.transform.parent.parent.parent).GetComponent<Inventory>().stackableSettings();
		Item.GetComponent<ConsumeItem>().duplication = val;
		val.GetComponent<ConsumeItem>().duplication = Item;
	}

	public void OnEndDrag(PointerEventData data)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_1498: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bea: Unknown result type (might be due to invalid IL or missing references)
		//IL_13db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daf: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_104f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1134: Unknown result type (might be due to invalid IL or missing references)
		//IL_1140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_1259: Unknown result type (might be due to invalid IL or missing references)
		//IL_1264: Unknown result type (might be due to invalid IL or missing references)
		//IL_1365: Unknown result type (might be due to invalid IL or missing references)
		//IL_1370: Unknown result type (might be due to invalid IL or missing references)
		//IL_1303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		if ((int)data.button == 0)
		{
			canvasGroup.blocksRaycasts = true;
			Transform val = null;
			if ((Object)(object)data.pointerEnter != (Object)null)
			{
				val = data.pointerEnter.transform;
			}
			if ((Object)(object)val != (Object)null && ((Component)val).tag != "Untagged")
			{
				GameObject gameObject = ((Component)this).gameObject;
				GameObject gameObject2 = ((Component)val.parent).gameObject;
				RectTransform component = ((Component)this).gameObject.GetComponent<RectTransform>();
				RectTransform component2 = ((Component)val.parent).GetComponent<RectTransform>();
				Item item = ((Component)rectTransform).GetComponent<ItemOnObject>().item;
				Item item2 = new Item();
				if ((Object)(object)((Component)val.parent).GetComponent<ItemOnObject>() != (Object)null)
				{
					item2 = ((Component)val.parent).GetComponent<ItemOnObject>().item;
				}
				bool flag = item.itemName == item2.itemName;
				bool flag2 = item.Equals(item2);
				bool flag3 = false;
				bool flag4 = false;
				if (flag)
				{
					flag4 = item.itemValue < item.maxStack;
					flag3 = item2.itemValue < item2.maxStack;
				}
				if ((Object)(object)((Transform)component2).parent == (Object)null)
				{
					((Component)this).gameObject.transform.SetParent(oldSlot.transform);
					((Transform)((Component)this).gameObject.GetComponent<RectTransform>()).localPosition = Vector3.zero;
					return;
				}
				GameObject gameObject3 = ((Component)((Transform)component2).parent).gameObject;
				if (gameObject3.tag == "Slot")
				{
					gameObject3 = ((Component)((Transform)component2).parent.parent.parent).gameObject;
				}
				if (gameObject3.tag.Equals("Slot"))
				{
					gameObject3 = ((Component)gameObject3.transform.parent.parent).gameObject;
				}
				if ((Object)(object)gameObject3.GetComponent<Hotbar>() == (Object)null && (Object)(object)gameObject3.GetComponent<EquipmentSystem>() == (Object)null && (Object)(object)gameObject3.GetComponent<CraftSystem>() == (Object)null)
				{
					if (((Component)((Component)val).transform.parent).tag == "ResultSlot" || ((Component)((Component)val).transform).tag == "ResultSlot" || ((Component)((Component)val).transform.parent.parent).tag == "ResultSlot")
					{
						gameObject.transform.SetParent(oldSlot.transform);
						((Transform)component).localPosition = Vector3.zero;
					}
					else
					{
						int childCount = ((Component)val).transform.parent.childCount;
						bool flag5 = ((Component)((Component)val).transform.parent.GetChild(0)).tag == "ItemIcon";
						if (childCount != 0 && flag5)
						{
							bool flag6 = false;
							if (flag)
							{
								flag6 = item.itemValue + item2.itemValue <= item.maxStack;
							}
							if (inventory.stackable && flag && flag4 && flag3)
							{
								if (flag6 && !flag2)
								{
									item2.itemValue = item.itemValue + item2.itemValue;
									gameObject2.transform.SetParent(val.parent.parent);
									Object.Destroy((Object)(object)gameObject);
									((Transform)component2).localPosition = Vector3.zero;
									if ((Object)(object)gameObject2.GetComponent<ConsumeItem>().duplication != (Object)null)
									{
										GameObject duplication = gameObject2.GetComponent<ConsumeItem>().duplication;
										duplication.GetComponent<ItemOnObject>().item.itemValue = item2.itemValue;
										duplication.GetComponent<SplitItem>().inv.stackableSettings();
										((Component)duplication.transform.parent.parent.parent).GetComponent<Inventory>().updateItemList();
									}
								}
								else
								{
									int num = (item.itemValue + item2.itemValue) % item.maxStack;
									if (!flag6 && num > 0)
									{
										item.itemValue = item.maxStack;
										item2.itemValue = num;
										gameObject.transform.SetParent(gameObject2.transform.parent);
										gameObject2.transform.SetParent(oldSlot.transform);
										((Transform)component).localPosition = Vector3.zero;
										((Transform)component2).localPosition = Vector3.zero;
									}
								}
							}
							else
							{
								int num2 = 0;
								if (flag)
								{
									num2 = (item.itemValue + item2.itemValue) % item.maxStack;
								}
								if (!flag6 && num2 > 0)
								{
									item2.itemValue = item.maxStack;
									item.itemValue = num2;
									gameObject.transform.SetParent(gameObject2.transform.parent);
									gameObject2.transform.SetParent(oldSlot.transform);
									((Transform)component).localPosition = Vector3.zero;
									((Transform)component2).localPosition = Vector3.zero;
								}
								else if (!flag6 && num2 == 0)
								{
									if ((Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null && item.itemType == item2.itemType)
									{
										int.Parse(((Object)oldSlot).name);
										int.Parse(((Object)gameObject2.transform.parent).name);
										_ = (Avatar)KBEngineApp.app.player();
										((Component)((Component)val).transform.parent.parent.parent.parent).GetComponent<Inventory>().UnEquipItem1(item);
										((Component)oldSlot.transform.parent.parent).GetComponent<Inventory>().EquiptItem(item2);
										gameObject.transform.SetParent(gameObject2.transform.parent);
										gameObject2.transform.SetParent(oldSlot.transform);
										((Transform)component2).localPosition = Vector3.zero;
										((Transform)component).localPosition = Vector3.zero;
										if ((Object)(object)gameObject2.GetComponent<ConsumeItem>().duplication != (Object)null)
										{
											Object.Destroy((Object)(object)gameObject2.GetComponent<ConsumeItem>().duplication);
										}
									}
									else if ((Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null && item.itemType != item2.itemType)
									{
										gameObject.transform.SetParent(oldSlot.transform);
										((Transform)component).localPosition = Vector3.zero;
									}
									else if ((Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() == (Object)null)
									{
										int srcIndex = int.Parse(((Object)oldSlot).name) - 1;
										int dstIndex = int.Parse(((Object)gameObject2.transform.parent).name) - 1;
										gameObject.transform.SetParent(gameObject2.transform.parent);
										gameObject2.transform.SetParent(oldSlot.transform);
										((Transform)component2).localPosition = Vector3.zero;
										((Transform)component).localPosition = Vector3.zero;
										((Avatar)KBEngineApp.app.player())?.swapItemRequest(srcIndex, dstIndex);
									}
								}
							}
						}
						else if (((Component)val).tag != "Slot" && ((Component)val).tag != "ItemIcon")
						{
							gameObject.transform.SetParent(oldSlot.transform);
							((Transform)component).localPosition = Vector3.zero;
						}
						else
						{
							gameObject.transform.SetParent(((Component)val).transform);
							((Transform)component).localPosition = Vector3.zero;
							int srcIndex2 = int.Parse(((Object)oldSlot).name) - 1;
							int dstIndex2 = int.Parse(((Object)val).name) - 1;
							Avatar avatar = (Avatar)KBEngineApp.app.player();
							if (avatar != null && (!((Object)(object)((Component)((Component)val).transform.parent.parent).GetComponent<EquipmentSystem>() == (Object)null) || !((Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null)))
							{
								avatar.swapItemRequest(srcIndex2, dstIndex2);
							}
							if ((Object)(object)((Component)((Component)val).transform.parent.parent).GetComponent<EquipmentSystem>() == (Object)null && (Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null)
							{
								((Component)oldSlot.transform.parent.parent).GetComponent<Inventory>().UnEquipItem1(item);
							}
						}
					}
				}
				if ((Object)(object)gameObject3.GetComponent<Hotbar>() != (Object)null)
				{
					int childCount2 = ((Component)val).transform.parent.childCount;
					bool flag7 = ((Component)((Component)val).transform.parent.GetChild(0)).tag == "ItemIcon";
					if (childCount2 != 0 && flag7)
					{
						bool flag8 = false;
						if (flag)
						{
							flag8 = item.itemValue + item2.itemValue <= item.maxStack;
						}
						if (inventory.stackable && flag && flag4 && flag3)
						{
							if (flag8 && !flag2)
							{
								item2.itemValue = item.itemValue + item2.itemValue;
								gameObject2.transform.SetParent(val.parent.parent);
								Object.Destroy((Object)(object)gameObject);
								((Transform)component2).localPosition = Vector3.zero;
								if ((Object)(object)gameObject2.GetComponent<ConsumeItem>().duplication != (Object)null)
								{
									GameObject duplication2 = gameObject2.GetComponent<ConsumeItem>().duplication;
									duplication2.GetComponent<ItemOnObject>().item.itemValue = item2.itemValue;
									gameObject3.GetComponent<Inventory>().stackableSettings();
									((Component)duplication2.transform.parent.parent.parent).GetComponent<Inventory>().updateItemList();
								}
							}
							else
							{
								int num3 = (item.itemValue + item2.itemValue) % item.maxStack;
								if (!flag8 && num3 > 0)
								{
									item.itemValue = item.maxStack;
									item2.itemValue = num3;
									gameObject.transform.SetParent(gameObject2.transform.parent);
									gameObject2.transform.SetParent(oldSlot.transform);
									((Transform)component).localPosition = Vector3.zero;
									((Transform)component2).localPosition = Vector3.zero;
									createDuplication(((Component)this).gameObject);
									gameObject2.GetComponent<ConsumeItem>().duplication.GetComponent<ItemOnObject>().item = item2;
									gameObject2.GetComponent<SplitItem>().inv.stackableSettings();
								}
							}
						}
						else
						{
							int num4 = 0;
							if (flag)
							{
								num4 = (item.itemValue + item2.itemValue) % item.maxStack;
							}
							bool flag9 = (Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null;
							if (!flag8 && num4 > 0)
							{
								item2.itemValue = item.maxStack;
								item.itemValue = num4;
								createDuplication(((Component)this).gameObject);
								gameObject.transform.SetParent(gameObject2.transform.parent);
								gameObject2.transform.SetParent(oldSlot.transform);
								((Transform)component).localPosition = Vector3.zero;
								((Transform)component2).localPosition = Vector3.zero;
							}
							else if (!flag8 && num4 == 0)
							{
								if (!flag9)
								{
									gameObject.transform.SetParent(gameObject2.transform.parent);
									gameObject2.transform.SetParent(oldSlot.transform);
									((Transform)component2).localPosition = Vector3.zero;
									((Transform)component).localPosition = Vector3.zero;
									if (((object)((Component)oldSlot.transform.parent.parent).gameObject).Equals((object?)GameObject.FindGameObjectWithTag("MainInventory")))
									{
										Object.Destroy((Object)(object)gameObject2.GetComponent<ConsumeItem>().duplication);
										createDuplication(gameObject);
									}
									else
									{
										createDuplication(gameObject);
									}
								}
								else
								{
									gameObject.transform.SetParent(oldSlot.transform);
									((Transform)component).localPosition = Vector3.zero;
								}
							}
						}
					}
					else if (((Component)val).tag != "Slot" && ((Component)val).tag != "ItemIcon")
					{
						gameObject.transform.SetParent(oldSlot.transform);
						((Transform)component).localPosition = Vector3.zero;
					}
					else
					{
						gameObject.transform.SetParent(((Component)val).transform);
						((Transform)component).localPosition = Vector3.zero;
						if ((Object)(object)((Component)((Component)val).transform.parent.parent).GetComponent<EquipmentSystem>() == (Object)null && (Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null)
						{
							((Component)oldSlot.transform.parent.parent).GetComponent<Inventory>().UnEquipItem1(item);
						}
						createDuplication(gameObject);
					}
				}
				if ((Object)(object)gameObject3.GetComponent<EquipmentSystem>() != (Object)null)
				{
					ItemType[] itemTypeOfSlots = GameObject.FindGameObjectWithTag("EquipmentSystem").GetComponent<EquipmentSystem>().itemTypeOfSlots;
					int childCount3 = ((Component)val).transform.parent.childCount;
					bool flag10 = ((Component)((Component)val).transform.parent.GetChild(0)).tag == "ItemIcon";
					bool flag11 = item.itemType == item2.itemType;
					bool flag12 = (Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<Hotbar>() != (Object)null;
					if (childCount3 != 0 && flag10)
					{
						if (flag11 && !flag2)
						{
							int.Parse(((Object)oldSlot).name);
							int.Parse(((Object)gameObject2.transform.parent).name);
							Transform parent = gameObject2.transform.parent.parent.parent;
							Transform parent2 = oldSlot.transform.parent.parent;
							gameObject.transform.SetParent(gameObject2.transform.parent);
							gameObject2.transform.SetParent(oldSlot.transform);
							((Transform)component2).localPosition = Vector3.zero;
							((Transform)component).localPosition = Vector3.zero;
							if (!((object)parent).Equals((object?)parent2))
							{
								if (item.itemType == ItemType.UFPS_Weapon)
								{
									gameObject3.GetComponent<Inventory>().UnEquipItem1(item2);
									gameObject3.GetComponent<Inventory>().EquiptItem(item);
								}
								else
								{
									gameObject3.GetComponent<Inventory>().EquiptItem(item);
									if (item2.itemType != ItemType.Backpack)
									{
										gameObject3.GetComponent<Inventory>().UnEquipItem1(item2);
									}
								}
							}
							if (flag12)
							{
								createDuplication(gameObject2);
							}
							if ((Avatar)KBEngineApp.app.player() == null)
							{
							}
						}
						else
						{
							gameObject.transform.SetParent(oldSlot.transform);
							((Transform)component).localPosition = Vector3.zero;
							if (flag12)
							{
								createDuplication(gameObject);
							}
						}
					}
					else
					{
						for (int i = 0; i < val.parent.childCount; i++)
						{
							if (!((object)val).Equals((object?)val.parent.GetChild(i)))
							{
								continue;
							}
							if (itemTypeOfSlots[i] == ((Component)((Component)this).transform).GetComponent<ItemOnObject>().item.itemType)
							{
								((Component)this).transform.SetParent(val);
								((Transform)rectTransform).localPosition = Vector3.zero;
								if (!((object)oldSlot.transform.parent.parent).Equals((object?)((Component)val).transform.parent.parent))
								{
									gameObject3.GetComponent<Inventory>().EquiptItem(item);
								}
								if ((Avatar)KBEngineApp.app.player() != null)
								{
									int.Parse(((Object)oldSlot).name);
									int.Parse(((Object)val).name);
								}
							}
							else
							{
								((Component)this).transform.SetParent(oldSlot.transform);
								((Transform)rectTransform).localPosition = Vector3.zero;
								if (flag12)
								{
									createDuplication(gameObject);
								}
							}
						}
					}
				}
				if ((Object)(object)gameObject3.GetComponent<CraftSystem>() != (Object)null)
				{
					CraftSystem component3 = gameObject3.GetComponent<CraftSystem>();
					int childCount4 = ((Component)val).transform.parent.childCount;
					bool flag13 = ((Component)((Component)val).transform.parent.GetChild(0)).tag == "ItemIcon";
					if (childCount4 != 0 && flag13)
					{
						bool flag14 = false;
						if (flag)
						{
							flag14 = item.itemValue + item2.itemValue <= item.maxStack;
						}
						if (inventory.stackable && flag && flag4 && flag3)
						{
							if (flag14 && !flag2)
							{
								item2.itemValue = item.itemValue + item2.itemValue;
								gameObject2.transform.SetParent(val.parent.parent);
								Object.Destroy((Object)(object)gameObject);
								((Transform)component2).localPosition = Vector3.zero;
								if ((Object)(object)gameObject2.GetComponent<ConsumeItem>().duplication != (Object)null)
								{
									GameObject duplication3 = gameObject2.GetComponent<ConsumeItem>().duplication;
									duplication3.GetComponent<ItemOnObject>().item.itemValue = item2.itemValue;
									duplication3.GetComponent<SplitItem>().inv.stackableSettings();
									((Component)duplication3.transform.parent.parent.parent).GetComponent<Inventory>().updateItemList();
								}
								component3.ListWithItem();
							}
							else
							{
								int num5 = (item.itemValue + item2.itemValue) % item.maxStack;
								if (!flag14 && num5 > 0)
								{
									item.itemValue = item.maxStack;
									item2.itemValue = num5;
									gameObject.transform.SetParent(gameObject2.transform.parent);
									gameObject2.transform.SetParent(oldSlot.transform);
									((Transform)component).localPosition = Vector3.zero;
									((Transform)component2).localPosition = Vector3.zero;
									component3.ListWithItem();
								}
							}
						}
						else
						{
							int num6 = 0;
							if (flag)
							{
								num6 = (item.itemValue + item2.itemValue) % item.maxStack;
							}
							if (!flag14 && num6 > 0)
							{
								item2.itemValue = item.maxStack;
								item.itemValue = num6;
								gameObject.transform.SetParent(gameObject2.transform.parent);
								gameObject2.transform.SetParent(oldSlot.transform);
								((Transform)component).localPosition = Vector3.zero;
								((Transform)component2).localPosition = Vector3.zero;
								component3.ListWithItem();
							}
							else if (!flag14 && num6 == 0)
							{
								if ((Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null && item.itemType == item2.itemType)
								{
									gameObject.transform.SetParent(gameObject2.transform.parent);
									gameObject2.transform.SetParent(oldSlot.transform);
									((Transform)component2).localPosition = Vector3.zero;
									((Transform)component).localPosition = Vector3.zero;
									((Component)oldSlot.transform.parent.parent).GetComponent<Inventory>().EquiptItem(item2);
									((Component)((Component)val).transform.parent.parent.parent.parent).GetComponent<Inventory>().UnEquipItem1(item);
								}
								else if ((Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null && item.itemType != item2.itemType)
								{
									gameObject.transform.SetParent(oldSlot.transform);
									((Transform)component).localPosition = Vector3.zero;
								}
								else if ((Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() == (Object)null)
								{
									gameObject.transform.SetParent(gameObject2.transform.parent);
									gameObject2.transform.SetParent(oldSlot.transform);
									((Transform)component2).localPosition = Vector3.zero;
									((Transform)component).localPosition = Vector3.zero;
								}
							}
						}
					}
					else if (((Component)val).tag != "Slot" && ((Component)val).tag != "ItemIcon")
					{
						gameObject.transform.SetParent(oldSlot.transform);
						((Transform)component).localPosition = Vector3.zero;
					}
					else
					{
						gameObject.transform.SetParent(((Component)val).transform);
						((Transform)component).localPosition = Vector3.zero;
						if ((Object)(object)((Component)((Component)val).transform.parent.parent).GetComponent<EquipmentSystem>() == (Object)null && (Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null)
						{
							((Component)oldSlot.transform.parent.parent).GetComponent<Inventory>().UnEquipItem1(item);
						}
					}
				}
			}
			else
			{
				if ((Object)(object)((Component)oldSlot.transform.parent.parent).GetComponent<EquipmentSystem>() != (Object)null)
				{
					((Component)this).gameObject.transform.SetParent(oldSlot.transform);
					((Transform)((Component)this).gameObject.GetComponent<RectTransform>()).localPosition = Vector3.zero;
					return;
				}
				Avatar avatar2 = (Avatar)KBEngineApp.app.player();
				if (avatar2 != null)
				{
					avatar2.dropRequest(((Component)this).gameObject.GetComponent<ItemOnObject>().item.itemUUID);
					Object.Destroy((Object)(object)((Component)this).gameObject);
				}
			}
		}
		inventory.OnUpdateItemList();
	}
}
