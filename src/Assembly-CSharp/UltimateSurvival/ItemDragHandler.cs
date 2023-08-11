using KBEngine;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival;

public class ItemDragHandler : MonoSingleton<ItemDragHandler>
{
	[SerializeField]
	private float m_DraggedItemScale = 0.85f;

	[SerializeField]
	private float m_DraggedItemAlpha = 0.75f;

	[SerializeField]
	private KeyCode m_SplitKey = (KeyCode)304;

	private ItemContainer[] m_AllCollections;

	private bool m_Dragging;

	private SavableItem m_DraggedItem;

	private RectTransform m_DraggedItemRT;

	private Canvas m_Canvas;

	private RectTransform m_ParentCanvasRT;

	private Vector2 m_DragOffset;

	public bool IsDragging => m_Dragging;

	public event DropAction PlayerDroppedItem;

	private void Start()
	{
		m_AllCollections = MonoSingleton<GUIController>.Instance.Containers;
		m_Canvas = MonoSingleton<GUIController>.Instance.Canvas;
		m_ParentCanvasRT = ((Component)m_Canvas).GetComponent<RectTransform>();
		ItemContainer[] allCollections = m_AllCollections;
		foreach (ItemContainer obj in allCollections)
		{
			obj.Slot_BeginDrag += On_Slot_BeginDrag;
			obj.Slot_Drag += On_Slot_Drag;
			obj.Slot_EndDrag += On_Slot_EndDrag;
		}
	}

	private void On_Slot_BeginDrag(PointerEventData data, Slot slot, ItemContainer collection)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		if (slot.HasItem && !MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			m_Dragging = true;
			SavableItem currentItem = slot.CurrentItem;
			currentItem.m_Item = slot.item;
			if (Input.GetKey(m_SplitKey) && currentItem.CurrentInStack > 1)
			{
				int currentInStack = currentItem.CurrentInStack;
				int num = currentInStack / 2;
				currentItem.CurrentInStack = currentInStack - num;
				m_DraggedItem = new SavableItem(currentItem.ItemData, num, currentItem.CurrentPropertyValues);
				slot.Refresh();
			}
			else
			{
				slot.ItemHolder.SetItem(null);
				m_DraggedItem = currentItem;
			}
			m_DraggedItemRT = slot.GetDragTemplate(m_DraggedItem, m_DraggedItemAlpha);
			((Transform)m_DraggedItemRT).SetParent((Transform)(object)m_ParentCanvasRT, true);
			((Transform)m_DraggedItemRT).localScale = Vector3.one * m_DraggedItemScale;
			Camera pressEventCamera = data.pressEventCamera;
			Vector3 val = default(Vector3);
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_ParentCanvasRT, Vector2.op_Implicit(Vector2.op_Implicit(data.position)), pressEventCamera, ref val))
			{
				m_DragOffset = Vector2.op_Implicit(((Component)slot).transform.position - val);
			}
		}
	}

	private void On_Slot_Drag(PointerEventData data, Slot initialSlot, ItemContainer collection)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (m_Dragging)
		{
			Camera pressEventCamera = data.pressEventCamera;
			Vector2 val = default(Vector2);
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_ParentCanvasRT, data.position, pressEventCamera, ref val))
			{
				((Transform)m_DraggedItemRT).localPosition = Vector2.op_Implicit(val + Vector2.op_Implicit(((Transform)m_ParentCanvasRT).InverseTransformVector(Vector2.op_Implicit(m_DragOffset))));
			}
		}
	}

	private void On_Slot_EndDrag(PointerEventData data, Slot initialSlot, ItemContainer collection)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (!m_Dragging)
		{
			return;
		}
		_ = collection.Slots;
		RaycastResult pointerCurrentRaycast = data.pointerCurrentRaycast;
		GameObject gameObject = ((RaycastResult)(ref pointerCurrentRaycast)).gameObject;
		Slot slot = null;
		if (Object.op_Implicit((Object)(object)gameObject))
		{
			slot = gameObject.GetComponent<Slot>();
		}
		if (Object.op_Implicit((Object)(object)slot))
		{
			if (slot.AllowsItem(m_DraggedItem))
			{
				ItemOnObject itemOnObject = new ItemOnObject();
				ItemOnObject component = ((Component)slot).gameObject.GetComponent<ItemOnObject>();
				itemOnObject.item = component.item;
				((Component)slot).gameObject.GetComponent<ItemOnObject>().item = ((Component)initialSlot).gameObject.GetComponent<ItemOnObject>().item;
				slot.ItemHolder.uuid = ((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemUUID;
				slot.ItemHolder.itemID = ((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemID;
				slot.ItemHolder.index = ((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemIndex;
				((Component)initialSlot).gameObject.GetComponent<ItemOnObject>().item = itemOnObject.item;
				initialSlot.ItemHolder.uuid = ((Component)initialSlot).gameObject.GetComponent<ItemOnObject>().item.itemUUID;
				initialSlot.ItemHolder.itemID = ((Component)initialSlot).gameObject.GetComponent<ItemOnObject>().item.itemID;
				initialSlot.ItemHolder.index = ((Component)initialSlot).gameObject.GetComponent<ItemOnObject>().item.itemIndex;
				itemOnObject = null;
				if (!slot.HasItem)
				{
					if ((bool)slot.ItemHolder)
					{
						slot.ItemHolder.SetItem(m_DraggedItem);
					}
					else
					{
						Debug.LogError((object)"You tried to drop an item over a Slot which is not linked with any holder.", (Object)(object)this);
					}
				}
				else
				{
					SavableItem currentItem = slot.CurrentItem;
					if (currentItem.Id == m_DraggedItem.Id && currentItem.ItemData.StackSize > 1 && currentItem.CurrentInStack < currentItem.ItemData.StackSize)
					{
						OnItemsAreStackable(slot, initialSlot);
					}
					else
					{
						On_ItemNotStackable(slot, initialSlot);
					}
				}
			}
			else
			{
				PutItemBack(initialSlot);
			}
			initialSlot.Refresh();
		}
		else
		{
			GameObject val = GameObject.Find("8-Item Inspector");
			if ((Object)(object)val != (Object)null)
			{
				Tooltip component2 = val.GetComponent<Tooltip>();
				((Avatar)KBEngineApp.app.player())?.dropRequest(component2.item.itemUUID);
			}
		}
		Object.Destroy((Object)(object)((Component)m_DraggedItemRT).gameObject);
		m_DraggedItem = null;
		m_Dragging = false;
	}

	private void OnItemsAreStackable(Slot slotUnderPointer, Slot initialSlot)
	{
		slotUnderPointer.ItemHolder.TryAddItem(m_DraggedItem.ItemData, m_DraggedItem.CurrentInStack, out var added, m_DraggedItem.CurrentPropertyValues, 0uL);
		int num = m_DraggedItem.CurrentInStack - added;
		if (num > 0)
		{
			if (initialSlot.HasItem)
			{
				slotUnderPointer.Parent.TryAddItem(m_DraggedItem.ItemData, num);
			}
			else
			{
				initialSlot.ItemHolder.SetItem(new SavableItem(m_DraggedItem.ItemData, num, m_DraggedItem.CurrentPropertyValues));
			}
		}
	}

	private void On_ItemNotStackable(Slot slotUnderPointer, Slot initialSlot)
	{
		if (!initialSlot.AllowsItem(slotUnderPointer.CurrentItem))
		{
			PutItemBack(initialSlot);
			return;
		}
		SavableItem currentItem = slotUnderPointer.CurrentItem;
		if (!initialSlot.HasItem)
		{
			slotUnderPointer.ItemHolder.SetItem(m_DraggedItem);
			initialSlot.ItemHolder.SetItem(currentItem);
			return;
		}
		initialSlot.Parent.TryAddItem(m_DraggedItem.ItemData, m_DraggedItem.CurrentInStack, out var added, 0uL);
		int num = m_DraggedItem.CurrentInStack - added;
		if (num > 0)
		{
			initialSlot.Parent.TryAddItem(m_DraggedItem.ItemData, num);
		}
	}

	private void PutItemBack(Slot initialSlot)
	{
		if (initialSlot.HasItem)
		{
			initialSlot.Parent.TryAddItem(m_DraggedItem);
		}
		else
		{
			initialSlot.ItemHolder.SetItem(m_DraggedItem);
		}
	}
}
