using System;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000213 RID: 531
public class DragItem : MonoBehaviour, IDragHandler, IEventSystemHandler, IPointerDownHandler, IEndDragHandler
{
	// Token: 0x14000034 RID: 52
	// (add) Token: 0x060010B8 RID: 4280 RVA: 0x000A8464 File Offset: 0x000A6664
	// (remove) Token: 0x060010B9 RID: 4281 RVA: 0x000A8498 File Offset: 0x000A6698
	public static event DragItem.ItemDelegate updateInventoryList;

	// Token: 0x060010BA RID: 4282 RVA: 0x000A84CC File Offset: 0x000A66CC
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.rectTransformSlot = GameObject.FindGameObjectWithTag("DraggingItem").GetComponent<RectTransform>();
		this.inventory = base.transform.parent.parent.parent.GetComponent<Inventory>();
		this.draggedItemBox = GameObject.FindGameObjectWithTag("DraggingItem").transform;
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x000A853C File Offset: 0x000A673C
	public void OnDrag(PointerEventData data)
	{
		if (this.rectTransform == null)
		{
			return;
		}
		if (data.button == null && base.transform.parent.GetComponent<CraftResultSlot>() == null)
		{
			this.rectTransform.SetAsLastSibling();
			base.transform.SetParent(this.draggedItemBox);
			this.canvasGroup.blocksRaycasts = false;
			Vector2 vector;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransformSlot, Input.mousePosition, data.pressEventCamera, ref vector))
			{
				this.rectTransform.localPosition = vector - this.pointerOffset;
				if (base.transform.GetComponent<ConsumeItem>().duplication != null)
				{
					Object.Destroy(base.transform.GetComponent<ConsumeItem>().duplication);
				}
			}
		}
		this.inventory.OnUpdateItemList();
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x000A861C File Offset: 0x000A681C
	public void OnPointerDown(PointerEventData data)
	{
		if (data.button == null)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, data.position, data.pressEventCamera, ref this.pointerOffset);
			this.oldSlot = base.transform.parent.gameObject;
		}
		if (DragItem.updateInventoryList != null)
		{
			DragItem.updateInventoryList();
		}
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x000A8678 File Offset: 0x000A6878
	public void createDuplication(GameObject Item)
	{
		Item item = Item.GetComponent<ItemOnObject>().item;
		GameObject gameObject = GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().addItemToInventory(item.itemID, item.itemValue);
		gameObject.transform.parent.parent.parent.GetComponent<Inventory>().stackableSettings();
		Item.GetComponent<ConsumeItem>().duplication = gameObject;
		gameObject.GetComponent<ConsumeItem>().duplication = Item;
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x000A86EC File Offset: 0x000A68EC
	public void OnEndDrag(PointerEventData data)
	{
		if (data.button == null)
		{
			this.canvasGroup.blocksRaycasts = true;
			Transform transform = null;
			if (data.pointerEnter != null)
			{
				transform = data.pointerEnter.transform;
			}
			if (transform != null && transform.tag != "Untagged")
			{
				GameObject gameObject = base.gameObject;
				GameObject gameObject2 = transform.parent.gameObject;
				RectTransform component = base.gameObject.GetComponent<RectTransform>();
				RectTransform component2 = transform.parent.GetComponent<RectTransform>();
				Item item = this.rectTransform.GetComponent<ItemOnObject>().item;
				Item item2 = new Item();
				if (transform.parent.GetComponent<ItemOnObject>() != null)
				{
					item2 = transform.parent.GetComponent<ItemOnObject>().item;
				}
				bool flag = item.itemName == item2.itemName;
				bool flag2 = item.Equals(item2);
				bool flag3 = false;
				bool flag4 = false;
				if (flag)
				{
					flag4 = (item.itemValue < item.maxStack);
					flag3 = (item2.itemValue < item2.maxStack);
				}
				if (component2.parent == null)
				{
					base.gameObject.transform.SetParent(this.oldSlot.transform);
					base.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
					return;
				}
				GameObject gameObject3 = component2.parent.gameObject;
				if (gameObject3.tag == "Slot")
				{
					gameObject3 = component2.parent.parent.parent.gameObject;
				}
				if (gameObject3.tag.Equals("Slot"))
				{
					gameObject3 = gameObject3.transform.parent.parent.gameObject;
				}
				if (gameObject3.GetComponent<Hotbar>() == null && gameObject3.GetComponent<EquipmentSystem>() == null && gameObject3.GetComponent<CraftSystem>() == null)
				{
					if (transform.transform.parent.tag == "ResultSlot" || transform.transform.tag == "ResultSlot" || transform.transform.parent.parent.tag == "ResultSlot")
					{
						gameObject.transform.SetParent(this.oldSlot.transform);
						component.localPosition = Vector3.zero;
					}
					else
					{
						int childCount = transform.transform.parent.childCount;
						bool flag5 = transform.transform.parent.GetChild(0).tag == "ItemIcon";
						if (childCount != 0 && flag5)
						{
							bool flag6 = false;
							if (flag)
							{
								flag6 = (item.itemValue + item2.itemValue <= item.maxStack);
							}
							if (this.inventory.stackable && flag && flag4 && flag3)
							{
								if (flag6 && !flag2)
								{
									item2.itemValue = item.itemValue + item2.itemValue;
									gameObject2.transform.SetParent(transform.parent.parent);
									Object.Destroy(gameObject);
									component2.localPosition = Vector3.zero;
									if (gameObject2.GetComponent<ConsumeItem>().duplication != null)
									{
										GameObject duplication = gameObject2.GetComponent<ConsumeItem>().duplication;
										duplication.GetComponent<ItemOnObject>().item.itemValue = item2.itemValue;
										duplication.GetComponent<SplitItem>().inv.stackableSettings();
										duplication.transform.parent.parent.parent.GetComponent<Inventory>().updateItemList();
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
										gameObject2.transform.SetParent(this.oldSlot.transform);
										component.localPosition = Vector3.zero;
										component2.localPosition = Vector3.zero;
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
									gameObject2.transform.SetParent(this.oldSlot.transform);
									component.localPosition = Vector3.zero;
									component2.localPosition = Vector3.zero;
								}
								else if (!flag6 && num2 == 0)
								{
									if (this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null && item.itemType == item2.itemType)
									{
										int.Parse(this.oldSlot.name);
										int.Parse(gameObject2.transform.parent.name);
										Avatar avatar = (Avatar)KBEngineApp.app.player();
										transform.transform.parent.parent.parent.parent.GetComponent<Inventory>().UnEquipItem1(item);
										this.oldSlot.transform.parent.parent.GetComponent<Inventory>().EquiptItem(item2);
										gameObject.transform.SetParent(gameObject2.transform.parent);
										gameObject2.transform.SetParent(this.oldSlot.transform);
										component2.localPosition = Vector3.zero;
										component.localPosition = Vector3.zero;
										if (gameObject2.GetComponent<ConsumeItem>().duplication != null)
										{
											Object.Destroy(gameObject2.GetComponent<ConsumeItem>().duplication);
										}
									}
									else if (this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null && item.itemType != item2.itemType)
									{
										gameObject.transform.SetParent(this.oldSlot.transform);
										component.localPosition = Vector3.zero;
									}
									else if (this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() == null)
									{
										int srcIndex = int.Parse(this.oldSlot.name) - 1;
										int dstIndex = int.Parse(gameObject2.transform.parent.name) - 1;
										gameObject.transform.SetParent(gameObject2.transform.parent);
										gameObject2.transform.SetParent(this.oldSlot.transform);
										component2.localPosition = Vector3.zero;
										component.localPosition = Vector3.zero;
										Avatar avatar2 = (Avatar)KBEngineApp.app.player();
										if (avatar2 != null)
										{
											avatar2.swapItemRequest(srcIndex, dstIndex);
										}
									}
								}
							}
						}
						else if (transform.tag != "Slot" && transform.tag != "ItemIcon")
						{
							gameObject.transform.SetParent(this.oldSlot.transform);
							component.localPosition = Vector3.zero;
						}
						else
						{
							gameObject.transform.SetParent(transform.transform);
							component.localPosition = Vector3.zero;
							int srcIndex2 = int.Parse(this.oldSlot.name) - 1;
							int dstIndex2 = int.Parse(transform.name) - 1;
							Avatar avatar3 = (Avatar)KBEngineApp.app.player();
							if (avatar3 != null && (!(transform.transform.parent.parent.GetComponent<EquipmentSystem>() == null) || !(this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null)))
							{
								avatar3.swapItemRequest(srcIndex2, dstIndex2);
							}
							if (transform.transform.parent.parent.GetComponent<EquipmentSystem>() == null && this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null)
							{
								this.oldSlot.transform.parent.parent.GetComponent<Inventory>().UnEquipItem1(item);
							}
						}
					}
				}
				if (gameObject3.GetComponent<Hotbar>() != null)
				{
					int childCount2 = transform.transform.parent.childCount;
					bool flag7 = transform.transform.parent.GetChild(0).tag == "ItemIcon";
					if (childCount2 != 0 && flag7)
					{
						bool flag8 = false;
						if (flag)
						{
							flag8 = (item.itemValue + item2.itemValue <= item.maxStack);
						}
						if (this.inventory.stackable && flag && flag4 && flag3)
						{
							if (flag8 && !flag2)
							{
								item2.itemValue = item.itemValue + item2.itemValue;
								gameObject2.transform.SetParent(transform.parent.parent);
								Object.Destroy(gameObject);
								component2.localPosition = Vector3.zero;
								if (gameObject2.GetComponent<ConsumeItem>().duplication != null)
								{
									GameObject duplication2 = gameObject2.GetComponent<ConsumeItem>().duplication;
									duplication2.GetComponent<ItemOnObject>().item.itemValue = item2.itemValue;
									gameObject3.GetComponent<Inventory>().stackableSettings();
									duplication2.transform.parent.parent.parent.GetComponent<Inventory>().updateItemList();
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
									gameObject2.transform.SetParent(this.oldSlot.transform);
									component.localPosition = Vector3.zero;
									component2.localPosition = Vector3.zero;
									this.createDuplication(base.gameObject);
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
							bool flag9 = this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null;
							if (!flag8 && num4 > 0)
							{
								item2.itemValue = item.maxStack;
								item.itemValue = num4;
								this.createDuplication(base.gameObject);
								gameObject.transform.SetParent(gameObject2.transform.parent);
								gameObject2.transform.SetParent(this.oldSlot.transform);
								component.localPosition = Vector3.zero;
								component2.localPosition = Vector3.zero;
							}
							else if (!flag8 && num4 == 0)
							{
								if (!flag9)
								{
									gameObject.transform.SetParent(gameObject2.transform.parent);
									gameObject2.transform.SetParent(this.oldSlot.transform);
									component2.localPosition = Vector3.zero;
									component.localPosition = Vector3.zero;
									if (this.oldSlot.transform.parent.parent.gameObject.Equals(GameObject.FindGameObjectWithTag("MainInventory")))
									{
										Object.Destroy(gameObject2.GetComponent<ConsumeItem>().duplication);
										this.createDuplication(gameObject);
									}
									else
									{
										this.createDuplication(gameObject);
									}
								}
								else
								{
									gameObject.transform.SetParent(this.oldSlot.transform);
									component.localPosition = Vector3.zero;
								}
							}
						}
					}
					else if (transform.tag != "Slot" && transform.tag != "ItemIcon")
					{
						gameObject.transform.SetParent(this.oldSlot.transform);
						component.localPosition = Vector3.zero;
					}
					else
					{
						gameObject.transform.SetParent(transform.transform);
						component.localPosition = Vector3.zero;
						if (transform.transform.parent.parent.GetComponent<EquipmentSystem>() == null && this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null)
						{
							this.oldSlot.transform.parent.parent.GetComponent<Inventory>().UnEquipItem1(item);
						}
						this.createDuplication(gameObject);
					}
				}
				if (gameObject3.GetComponent<EquipmentSystem>() != null)
				{
					ItemType[] itemTypeOfSlots = GameObject.FindGameObjectWithTag("EquipmentSystem").GetComponent<EquipmentSystem>().itemTypeOfSlots;
					int childCount3 = transform.transform.parent.childCount;
					bool flag10 = transform.transform.parent.GetChild(0).tag == "ItemIcon";
					bool flag11 = item.itemType == item2.itemType;
					bool flag12 = this.oldSlot.transform.parent.parent.GetComponent<Hotbar>() != null;
					if (childCount3 != 0 && flag10)
					{
						if (flag11 && !flag2)
						{
							int.Parse(this.oldSlot.name);
							int.Parse(gameObject2.transform.parent.name);
							object parent = gameObject2.transform.parent.parent.parent;
							Transform parent2 = this.oldSlot.transform.parent.parent;
							gameObject.transform.SetParent(gameObject2.transform.parent);
							gameObject2.transform.SetParent(this.oldSlot.transform);
							component2.localPosition = Vector3.zero;
							component.localPosition = Vector3.zero;
							if (!parent.Equals(parent2))
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
								this.createDuplication(gameObject2);
							}
							if ((Avatar)KBEngineApp.app.player() != null)
							{
							}
						}
						else
						{
							gameObject.transform.SetParent(this.oldSlot.transform);
							component.localPosition = Vector3.zero;
							if (flag12)
							{
								this.createDuplication(gameObject);
							}
						}
					}
					else
					{
						for (int i = 0; i < transform.parent.childCount; i++)
						{
							if (transform.Equals(transform.parent.GetChild(i)))
							{
								if (itemTypeOfSlots[i] == base.transform.GetComponent<ItemOnObject>().item.itemType)
								{
									base.transform.SetParent(transform);
									this.rectTransform.localPosition = Vector3.zero;
									if (!this.oldSlot.transform.parent.parent.Equals(transform.transform.parent.parent))
									{
										gameObject3.GetComponent<Inventory>().EquiptItem(item);
									}
									if ((Avatar)KBEngineApp.app.player() != null)
									{
										int.Parse(this.oldSlot.name);
										int.Parse(transform.name);
									}
								}
								else
								{
									base.transform.SetParent(this.oldSlot.transform);
									this.rectTransform.localPosition = Vector3.zero;
									if (flag12)
									{
										this.createDuplication(gameObject);
									}
								}
							}
						}
					}
				}
				if (gameObject3.GetComponent<CraftSystem>() != null)
				{
					CraftSystem component3 = gameObject3.GetComponent<CraftSystem>();
					int childCount4 = transform.transform.parent.childCount;
					bool flag13 = transform.transform.parent.GetChild(0).tag == "ItemIcon";
					if (childCount4 != 0 && flag13)
					{
						bool flag14 = false;
						if (flag)
						{
							flag14 = (item.itemValue + item2.itemValue <= item.maxStack);
						}
						if (this.inventory.stackable && flag && flag4 && flag3)
						{
							if (flag14 && !flag2)
							{
								item2.itemValue = item.itemValue + item2.itemValue;
								gameObject2.transform.SetParent(transform.parent.parent);
								Object.Destroy(gameObject);
								component2.localPosition = Vector3.zero;
								if (gameObject2.GetComponent<ConsumeItem>().duplication != null)
								{
									GameObject duplication3 = gameObject2.GetComponent<ConsumeItem>().duplication;
									duplication3.GetComponent<ItemOnObject>().item.itemValue = item2.itemValue;
									duplication3.GetComponent<SplitItem>().inv.stackableSettings();
									duplication3.transform.parent.parent.parent.GetComponent<Inventory>().updateItemList();
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
									gameObject2.transform.SetParent(this.oldSlot.transform);
									component.localPosition = Vector3.zero;
									component2.localPosition = Vector3.zero;
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
								gameObject2.transform.SetParent(this.oldSlot.transform);
								component.localPosition = Vector3.zero;
								component2.localPosition = Vector3.zero;
								component3.ListWithItem();
							}
							else if (!flag14 && num6 == 0)
							{
								if (this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null && item.itemType == item2.itemType)
								{
									gameObject.transform.SetParent(gameObject2.transform.parent);
									gameObject2.transform.SetParent(this.oldSlot.transform);
									component2.localPosition = Vector3.zero;
									component.localPosition = Vector3.zero;
									this.oldSlot.transform.parent.parent.GetComponent<Inventory>().EquiptItem(item2);
									transform.transform.parent.parent.parent.parent.GetComponent<Inventory>().UnEquipItem1(item);
								}
								else if (this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null && item.itemType != item2.itemType)
								{
									gameObject.transform.SetParent(this.oldSlot.transform);
									component.localPosition = Vector3.zero;
								}
								else if (this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() == null)
								{
									gameObject.transform.SetParent(gameObject2.transform.parent);
									gameObject2.transform.SetParent(this.oldSlot.transform);
									component2.localPosition = Vector3.zero;
									component.localPosition = Vector3.zero;
								}
							}
						}
					}
					else if (transform.tag != "Slot" && transform.tag != "ItemIcon")
					{
						gameObject.transform.SetParent(this.oldSlot.transform);
						component.localPosition = Vector3.zero;
					}
					else
					{
						gameObject.transform.SetParent(transform.transform);
						component.localPosition = Vector3.zero;
						if (transform.transform.parent.parent.GetComponent<EquipmentSystem>() == null && this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null)
						{
							this.oldSlot.transform.parent.parent.GetComponent<Inventory>().UnEquipItem1(item);
						}
					}
				}
			}
			else
			{
				if (this.oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null)
				{
					base.gameObject.transform.SetParent(this.oldSlot.transform);
					base.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
					return;
				}
				Avatar avatar4 = (Avatar)KBEngineApp.app.player();
				if (avatar4 != null)
				{
					avatar4.dropRequest(base.gameObject.GetComponent<ItemOnObject>().item.itemUUID);
					Object.Destroy(base.gameObject);
				}
			}
		}
		this.inventory.OnUpdateItemList();
	}

	// Token: 0x04000D40 RID: 3392
	private Vector2 pointerOffset;

	// Token: 0x04000D41 RID: 3393
	private RectTransform rectTransform;

	// Token: 0x04000D42 RID: 3394
	private RectTransform rectTransformSlot;

	// Token: 0x04000D43 RID: 3395
	private CanvasGroup canvasGroup;

	// Token: 0x04000D44 RID: 3396
	private GameObject oldSlot;

	// Token: 0x04000D45 RID: 3397
	private Inventory inventory;

	// Token: 0x04000D46 RID: 3398
	private Transform draggedItemBox;

	// Token: 0x02000214 RID: 532
	// (Invoke) Token: 0x060010C1 RID: 4289
	public delegate void ItemDelegate();
}
