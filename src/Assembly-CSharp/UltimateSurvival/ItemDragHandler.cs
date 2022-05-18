using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival
{
	// Token: 0x020008AE RID: 2222
	public class ItemDragHandler : MonoSingleton<ItemDragHandler>
	{
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06003933 RID: 14643 RVA: 0x001A48E0 File Offset: 0x001A2AE0
		// (remove) Token: 0x06003934 RID: 14644 RVA: 0x001A4918 File Offset: 0x001A2B18
		public event DropAction PlayerDroppedItem;

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x00029825 File Offset: 0x00027A25
		public bool IsDragging
		{
			get
			{
				return this.m_Dragging;
			}
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x001A4950 File Offset: 0x001A2B50
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

		// Token: 0x06003937 RID: 14647 RVA: 0x001A49DC File Offset: 0x001A2BDC
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

		// Token: 0x06003938 RID: 14648 RVA: 0x001A4B00 File Offset: 0x001A2D00
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

		// Token: 0x06003939 RID: 14649 RVA: 0x001A4B64 File Offset: 0x001A2D64
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

		// Token: 0x0600393A RID: 14650 RVA: 0x001A4DD8 File Offset: 0x001A2FD8
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

		// Token: 0x0600393B RID: 14651 RVA: 0x001A4E74 File Offset: 0x001A3074
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

		// Token: 0x0600393C RID: 14652 RVA: 0x0002982D File Offset: 0x00027A2D
		private void PutItemBack(Slot initialSlot)
		{
			if (initialSlot.HasItem)
			{
				initialSlot.Parent.TryAddItem(this.m_DraggedItem);
				return;
			}
			initialSlot.ItemHolder.SetItem(this.m_DraggedItem);
		}

		// Token: 0x0400335B RID: 13147
		[SerializeField]
		private float m_DraggedItemScale = 0.85f;

		// Token: 0x0400335C RID: 13148
		[SerializeField]
		private float m_DraggedItemAlpha = 0.75f;

		// Token: 0x0400335D RID: 13149
		[SerializeField]
		private KeyCode m_SplitKey = 304;

		// Token: 0x0400335E RID: 13150
		private ItemContainer[] m_AllCollections;

		// Token: 0x0400335F RID: 13151
		private bool m_Dragging;

		// Token: 0x04003360 RID: 13152
		private SavableItem m_DraggedItem;

		// Token: 0x04003361 RID: 13153
		private RectTransform m_DraggedItemRT;

		// Token: 0x04003362 RID: 13154
		private Canvas m_Canvas;

		// Token: 0x04003363 RID: 13155
		private RectTransform m_ParentCanvasRT;

		// Token: 0x04003364 RID: 13156
		private Vector2 m_DragOffset;
	}
}
