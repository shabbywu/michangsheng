using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival
{
	// Token: 0x020005E0 RID: 1504
	public class ItemDragHandler : MonoSingleton<ItemDragHandler>
	{
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06003067 RID: 12391 RVA: 0x0015ADF4 File Offset: 0x00158FF4
		// (remove) Token: 0x06003068 RID: 12392 RVA: 0x0015AE2C File Offset: 0x0015902C
		public event DropAction PlayerDroppedItem;

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x0015AE61 File Offset: 0x00159061
		public bool IsDragging
		{
			get
			{
				return this.m_Dragging;
			}
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x0015AE6C File Offset: 0x0015906C
		private void Start()
		{
			this.m_AllCollections = MonoSingleton<GUIController>.Instance.Containers;
			this.m_Canvas = MonoSingleton<GUIController>.Instance.Canvas;
			this.m_ParentCanvasRT = this.m_Canvas.GetComponent<RectTransform>();
			foreach (ItemContainer itemContainer in this.m_AllCollections)
			{
				itemContainer.Slot_BeginDrag += this.On_Slot_BeginDrag;
				itemContainer.Slot_Drag += this.On_Slot_Drag;
				itemContainer.Slot_EndDrag += this.On_Slot_EndDrag;
			}
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0015AEF8 File Offset: 0x001590F8
		private void On_Slot_BeginDrag(PointerEventData data, Slot slot, ItemContainer collection)
		{
			if (!slot.HasItem || MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				return;
			}
			this.m_Dragging = true;
			SavableItem currentItem = slot.CurrentItem;
			currentItem.m_Item = slot.item;
			if (Input.GetKey(this.m_SplitKey) && currentItem.CurrentInStack > 1)
			{
				int currentInStack = currentItem.CurrentInStack;
				int num = currentInStack / 2;
				currentItem.CurrentInStack = currentInStack - num;
				this.m_DraggedItem = new SavableItem(currentItem.ItemData, num, currentItem.CurrentPropertyValues);
				slot.Refresh();
			}
			else
			{
				slot.ItemHolder.SetItem(null);
				this.m_DraggedItem = currentItem;
			}
			this.m_DraggedItemRT = slot.GetDragTemplate(this.m_DraggedItem, this.m_DraggedItemAlpha);
			this.m_DraggedItemRT.SetParent(this.m_ParentCanvasRT, true);
			this.m_DraggedItemRT.localScale = Vector3.one * this.m_DraggedItemScale;
			Camera pressEventCamera = data.pressEventCamera;
			Vector3 vector;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_ParentCanvasRT, data.position, pressEventCamera, ref vector))
			{
				this.m_DragOffset = slot.transform.position - vector;
			}
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x0015B01C File Offset: 0x0015921C
		private void On_Slot_Drag(PointerEventData data, Slot initialSlot, ItemContainer collection)
		{
			if (!this.m_Dragging)
			{
				return;
			}
			Camera pressEventCamera = data.pressEventCamera;
			Vector2 vector;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_ParentCanvasRT, data.position, pressEventCamera, ref vector))
			{
				this.m_DraggedItemRT.localPosition = vector + this.m_ParentCanvasRT.InverseTransformVector(this.m_DragOffset);
			}
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x0015B080 File Offset: 0x00159280
		private void On_Slot_EndDrag(PointerEventData data, Slot initialSlot, ItemContainer collection)
		{
			if (!this.m_Dragging)
			{
				return;
			}
			List<Slot> slots = collection.Slots;
			GameObject gameObject = data.pointerCurrentRaycast.gameObject;
			Slot slot = null;
			if (gameObject)
			{
				slot = gameObject.GetComponent<Slot>();
			}
			if (slot)
			{
				if (slot.AllowsItem(this.m_DraggedItem))
				{
					ItemOnObject itemOnObject = new ItemOnObject();
					ItemOnObject component = slot.gameObject.GetComponent<ItemOnObject>();
					itemOnObject.item = component.item;
					slot.gameObject.GetComponent<ItemOnObject>().item = initialSlot.gameObject.GetComponent<ItemOnObject>().item;
					slot.ItemHolder.uuid = slot.gameObject.GetComponent<ItemOnObject>().item.itemUUID;
					slot.ItemHolder.itemID = slot.gameObject.GetComponent<ItemOnObject>().item.itemID;
					slot.ItemHolder.index = slot.gameObject.GetComponent<ItemOnObject>().item.itemIndex;
					initialSlot.gameObject.GetComponent<ItemOnObject>().item = itemOnObject.item;
					initialSlot.ItemHolder.uuid = initialSlot.gameObject.GetComponent<ItemOnObject>().item.itemUUID;
					initialSlot.ItemHolder.itemID = initialSlot.gameObject.GetComponent<ItemOnObject>().item.itemID;
					initialSlot.ItemHolder.index = initialSlot.gameObject.GetComponent<ItemOnObject>().item.itemIndex;
					if (!slot.HasItem)
					{
						if (slot.ItemHolder)
						{
							slot.ItemHolder.SetItem(this.m_DraggedItem);
						}
						else
						{
							Debug.LogError("You tried to drop an item over a Slot which is not linked with any holder.", this);
						}
					}
					else
					{
						SavableItem currentItem = slot.CurrentItem;
						if (currentItem.Id == this.m_DraggedItem.Id && currentItem.ItemData.StackSize > 1 && currentItem.CurrentInStack < currentItem.ItemData.StackSize)
						{
							this.OnItemsAreStackable(slot, initialSlot);
						}
						else
						{
							this.On_ItemNotStackable(slot, initialSlot);
						}
					}
				}
				else
				{
					this.PutItemBack(initialSlot);
				}
				initialSlot.Refresh();
			}
			else
			{
				GameObject gameObject2 = GameObject.Find("8-Item Inspector");
				if (gameObject2 != null)
				{
					Tooltip component2 = gameObject2.GetComponent<Tooltip>();
					Avatar avatar = (Avatar)KBEngineApp.app.player();
					if (avatar != null)
					{
						avatar.dropRequest(component2.item.itemUUID);
					}
				}
			}
			Object.Destroy(this.m_DraggedItemRT.gameObject);
			this.m_DraggedItem = null;
			this.m_Dragging = false;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0015B2F4 File Offset: 0x001594F4
		private void OnItemsAreStackable(Slot slotUnderPointer, Slot initialSlot)
		{
			int num;
			slotUnderPointer.ItemHolder.TryAddItem(this.m_DraggedItem.ItemData, this.m_DraggedItem.CurrentInStack, out num, this.m_DraggedItem.CurrentPropertyValues, 0UL, 0);
			int num2 = this.m_DraggedItem.CurrentInStack - num;
			if (num2 > 0)
			{
				if (initialSlot.HasItem)
				{
					slotUnderPointer.Parent.TryAddItem(this.m_DraggedItem.ItemData, num2);
					return;
				}
				initialSlot.ItemHolder.SetItem(new SavableItem(this.m_DraggedItem.ItemData, num2, this.m_DraggedItem.CurrentPropertyValues));
			}
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x0015B390 File Offset: 0x00159590
		private void On_ItemNotStackable(Slot slotUnderPointer, Slot initialSlot)
		{
			if (!initialSlot.AllowsItem(slotUnderPointer.CurrentItem))
			{
				this.PutItemBack(initialSlot);
				return;
			}
			SavableItem currentItem = slotUnderPointer.CurrentItem;
			if (!initialSlot.HasItem)
			{
				slotUnderPointer.ItemHolder.SetItem(this.m_DraggedItem);
				initialSlot.ItemHolder.SetItem(currentItem);
				return;
			}
			int num;
			initialSlot.Parent.TryAddItem(this.m_DraggedItem.ItemData, this.m_DraggedItem.CurrentInStack, out num, 0UL, 0);
			int num2 = this.m_DraggedItem.CurrentInStack - num;
			if (num2 > 0)
			{
				initialSlot.Parent.TryAddItem(this.m_DraggedItem.ItemData, num2);
			}
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x0015B431 File Offset: 0x00159631
		private void PutItemBack(Slot initialSlot)
		{
			if (initialSlot.HasItem)
			{
				initialSlot.Parent.TryAddItem(this.m_DraggedItem);
				return;
			}
			initialSlot.ItemHolder.SetItem(this.m_DraggedItem);
		}

		// Token: 0x04002A9D RID: 10909
		[SerializeField]
		private float m_DraggedItemScale = 0.85f;

		// Token: 0x04002A9E RID: 10910
		[SerializeField]
		private float m_DraggedItemAlpha = 0.75f;

		// Token: 0x04002A9F RID: 10911
		[SerializeField]
		private KeyCode m_SplitKey = 304;

		// Token: 0x04002AA0 RID: 10912
		private ItemContainer[] m_AllCollections;

		// Token: 0x04002AA1 RID: 10913
		private bool m_Dragging;

		// Token: 0x04002AA2 RID: 10914
		private SavableItem m_DraggedItem;

		// Token: 0x04002AA3 RID: 10915
		private RectTransform m_DraggedItemRT;

		// Token: 0x04002AA4 RID: 10916
		private Canvas m_Canvas;

		// Token: 0x04002AA5 RID: 10917
		private RectTransform m_ParentCanvasRT;

		// Token: 0x04002AA6 RID: 10918
		private Vector2 m_DragOffset;
	}
}
