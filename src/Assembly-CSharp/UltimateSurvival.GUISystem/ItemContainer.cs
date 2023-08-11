using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival.GUISystem;

public sealed class ItemContainer : GUIBehaviour
{
	public Message<Slot> Slot_Refreshed = new Message<Slot>();

	[SerializeField]
	private string _Name = "";

	[SerializeField]
	[Tooltip("It is optional. If you assign a window, the open state will be taken from the window, otherwise the container will always be considered open.")]
	private Window m_Window;

	[Header("Slots")]
	[SerializeField]
	[Tooltip("All the created slots will be based on this template.")]
	private Slot m_SlotTemplate;

	[SerializeField]
	[Tooltip("The parent of the slots, usually it has attached a GridLayoutGroup, HorizontalLayoutGroup, etc, so they are automatically arranged.")]
	private Transform m_SlotsParent;

	[SerializeField]
	[Range(0f, 100f)]
	private int m_PreviewSize;

	[Header("Required Stuff")]
	[SerializeField]
	[Reorderable]
	private ReorderableStringList m_RequiredCategories;

	[SerializeField]
	[Reorderable]
	private ReorderableStringList m_RequiredProperties;

	private List<ItemHolder> m_ItemHolders;

	private List<Slot> m_Slots;

	private bool m_SetUp;

	public bool IsOpen
	{
		get
		{
			if (m_SetUp)
			{
				if (Object.op_Implicit((Object)(object)m_Window))
				{
					return m_Window.IsOpen;
				}
				return true;
			}
			return false;
		}
	}

	public string Name => _Name;

	public List<Slot> Slots { get; private set; }

	public event Action<PointerEventData, Slot> Slot_PointerDown;

	public event Action<PointerEventData, Slot> Slot_PointerUp;

	public event Action<BaseEventData, Slot> Slot_Select;

	public event DragAction Slot_BeginDrag;

	public event DragAction Slot_Drag;

	public event DragAction Slot_EndDrag;

	public int getHasItemSlotCount()
	{
		int num = 0;
		foreach (Slot slot in Slots)
		{
			if (slot.HasItem)
			{
				num++;
			}
		}
		return num;
	}

	public void Setup(ItemHolder itemholder)
	{
		Setup(new List<ItemHolder> { itemholder });
	}

	public void Setup(List<ItemHolder> itemHolders)
	{
		if (!Application.isPlaying)
		{
			Debug.LogError((object)"You can't create the container when the application is not playing.", (Object)(object)this);
		}
		else if ((Object)(object)m_SlotTemplate == (Object)null)
		{
			Debug.LogError((object)"You tried to create slots for this container, but the slot template is null / not assigned in the inspector!", (Object)(object)this);
		}
		else if (Object.op_Implicit((Object)(object)m_SlotsParent) && Object.op_Implicit((Object)(object)m_SlotTemplate))
		{
			if (Slots == null)
			{
				Slots = new List<Slot>();
				((Component)this).GetComponentsInChildren<Slot>(Slots);
			}
			m_ItemHolders = itemHolders;
			bool activeSelf = ((Component)m_SlotTemplate).gameObject.activeSelf;
			((Component)m_SlotTemplate).gameObject.SetActive(true);
			PrepareGUIForSlots(m_SlotsParent, m_SlotTemplate);
			((Component)m_SlotTemplate).gameObject.SetActive(activeSelf);
			m_SetUp = true;
		}
	}

	public bool HasItem(SavableItem item)
	{
		for (int i = 0; i < m_ItemHolders.Count; i++)
		{
			if ((bool)item == (bool)m_ItemHolders[i])
			{
				return true;
			}
		}
		return false;
	}

	public bool TryAddItem(ItemData itemData, int amount, out int added, ulong uuid = 0uL, int index = 0)
	{
		added = 0;
		CollectionUtils.AddItem(itemData, amount, Slots, out added, null, uuid, index);
		return added > 0;
	}

	public bool TryAddItem(ItemData itemData, int amount)
	{
		int added = 0;
		CollectionUtils.AddItem(itemData, amount, Slots, out added, null, 0uL);
		return added > 0;
	}

	public bool TryAddItem(string name, int amount, out int added)
	{
		added = 0;
		if (MonoSingleton<InventoryController>.Instance.Database.FindItemByName(name, out var itemData))
		{
			CollectionUtils.AddItem(itemData, amount, Slots, out added, null, 0uL);
		}
		return added > 0;
	}

	public bool TryAddItem(string name, int amount)
	{
		int added = 0;
		if (MonoSingleton<InventoryController>.Instance.Database.FindItemByName(name, out var itemData))
		{
			CollectionUtils.AddItem(itemData, amount, Slots, out added, null, 0uL);
		}
		return added > 0;
	}

	public bool TryAddItem(SavableItem item)
	{
		if (item == null)
		{
			return false;
		}
		int added = 0;
		CollectionUtils.AddItem(item.ItemData, item.CurrentInStack, Slots, out added, item.CurrentPropertyValues, 0uL);
		return added > 0;
	}

	public bool TryRemoveItem(SavableItem item)
	{
		Slot slot2 = Slots.Find((Slot slot) => slot.CurrentItem == item);
		if (Object.op_Implicit((Object)(object)slot2))
		{
			slot2.ItemHolder.SetItem(null);
			return true;
		}
		return false;
	}

	public void RemoveItems(string itemName, int amount, out int removed)
	{
		CollectionUtils.RemoveItems(itemName, amount, Slots, out removed);
	}

	public void RemoveItems(ulong uuid)
	{
	}

	public void RemoveItems(string itemName, int amount)
	{
		CollectionUtils.RemoveItems(itemName, amount, Slots, out var _);
	}

	public void AddAllFrom(ItemContainer container)
	{
		for (int i = 0; i < container.Slots.Count; i++)
		{
			if (container.Slots[i].HasItem && TryAddItem(container.Slots[i].CurrentItem))
			{
				container.Slots[i].ItemHolder.SetItem(null);
			}
		}
	}

	public int GetItemCount(int itemID)
	{
		int num = 0;
		for (int i = 0; i < Slots.Count; i++)
		{
			if (Slots[i].HasItem && Slots[i].CurrentItem.Id == itemID)
			{
				num += Slots[i].CurrentItem.CurrentInStack;
			}
		}
		return num;
	}

	public int GetItemCount(string itemName)
	{
		int num = 0;
		for (int i = 0; i < Slots.Count; i++)
		{
			if (Slots[i].HasItem && Slots[i].CurrentItem.ItemData.Name == itemName)
			{
				num += Slots[i].CurrentItem.CurrentInStack;
			}
		}
		return num;
	}

	public void ApplyAll()
	{
		if (!Application.isPlaying)
		{
			ApplyTemplate();
			ApplyRequiredStuff();
		}
	}

	public void ApplyTemplate()
	{
		if (!Application.isPlaying)
		{
			Transform slotsParent = m_SlotsParent;
			Slot slotTemplate = m_SlotTemplate;
			if (Object.op_Implicit((Object)(object)slotsParent) && Object.op_Implicit((Object)(object)slotTemplate))
			{
				bool activeSelf = ((Component)slotTemplate).gameObject.activeSelf;
				((Component)slotTemplate).gameObject.SetActive(true);
				RemoveSlots(slotsParent, slotTemplate);
				CreateSlots(slotsParent, slotTemplate);
				((Component)slotTemplate).gameObject.SetActive(activeSelf);
			}
		}
	}

	public void ApplyRequiredStuff()
	{
		if (!Application.isPlaying)
		{
			Slot[] componentsInChildren = ((Component)this).GetComponentsInChildren<Slot>();
			foreach (Slot obj in componentsInChildren)
			{
				obj.RequiredCategories = m_RequiredCategories;
				obj.RequiredProperties = m_RequiredProperties;
			}
		}
	}

	private void Awake()
	{
		Slots = new List<Slot>();
		((Component)this).GetComponentsInChildren<Slot>(Slots);
	}

	private void On_Slot_PointerDown(PointerEventData data, Slot slot)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (this.Slot_PointerDown != null)
		{
			this.Slot_PointerDown(data, slot);
		}
		if (!Input.GetKey((KeyCode)304) || (int)data.button != 0)
		{
			return;
		}
		if (_Name == "Inventory")
		{
			ItemContainer container = base.Controller.GetContainer("Loot");
			if (container.IsOpen)
			{
				if (container.TryAddItem(slot.CurrentItem))
				{
					slot.ItemHolder.SetItem(null);
				}
			}
			else if (base.Controller.GetContainer("Hotbar").TryAddItem(slot.CurrentItem))
			{
				slot.ItemHolder.SetItem(null);
			}
		}
		else if (base.Controller.GetContainer("Inventory").TryAddItem(slot.CurrentItem))
		{
			slot.ItemHolder.SetItem(null);
		}
	}

	private void On_Slot_PointerUp(PointerEventData data, Slot slot)
	{
		if (this.Slot_PointerUp != null)
		{
			this.Slot_PointerUp(data, slot);
		}
	}

	private void On_Slot_Select(BaseEventData data, Slot slot)
	{
		if (this.Slot_Select != null)
		{
			this.Slot_Select(data, slot);
		}
	}

	private void On_Slot_BeginDrag(PointerEventData data, Slot slot)
	{
		if (this.Slot_BeginDrag != null)
		{
			this.Slot_BeginDrag(data, slot, this);
		}
	}

	private void On_Slot_Drag(PointerEventData data, Slot slot)
	{
		if (this.Slot_Drag != null)
		{
			this.Slot_Drag(data, slot, this);
		}
	}

	private void On_Slot_EndDrag(PointerEventData data, Slot slot)
	{
		if (this.Slot_EndDrag != null)
		{
			this.Slot_EndDrag(data, slot, this);
		}
	}

	private void On_Slot_Refreshed(Slot slot)
	{
		Slot_Refreshed.Send(slot);
	}

	private void ActivateSlots(Transform parent, Slot template, int count, bool active)
	{
		for (int i = 0; i < count; i++)
		{
			int num = parent.childCount - i - 1;
			if ((Object)(object)parent.GetChild(num) != (Object)(object)((Component)template).transform)
			{
				((Component)parent.GetChild(num)).gameObject.SetActive(active);
			}
		}
	}

	private void PrepareGUIForSlots(Transform parent, Slot template)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		OnSlotsDiscarded();
		ActivateSlots(parent, template, Mathf.Clamp(m_ItemHolders.Count, 0, Slots.Count), active: true);
		if (m_ItemHolders.Count > Slots.Count)
		{
			int num = m_ItemHolders.Count - Slots.Count;
			for (int i = 0; i < num; i++)
			{
				Slot slot = Object.Instantiate<Slot>(template);
				((Component)slot).transform.SetParent(parent);
				((Component)slot).transform.localPosition = Vector3.zero;
				((Component)slot).transform.localScale = Vector3.one;
				Slots.Add(slot);
			}
		}
		else if (m_ItemHolders.Count < Slots.Count)
		{
			ActivateSlots(parent, template, Slots.Count - m_ItemHolders.Count, active: false);
		}
		for (int j = 0; j < m_ItemHolders.Count; j++)
		{
			Slots[j].LinkWithHolder(m_ItemHolders[j]);
		}
		OnSlotsCreated();
	}

	private void OnSlotsDiscarded()
	{
		foreach (Slot slot in Slots)
		{
			slot.PointerDown -= On_Slot_PointerDown;
			slot.PointerUp -= On_Slot_PointerUp;
			slot.E_Select -= On_Slot_Select;
			slot.BeginDrag -= On_Slot_BeginDrag;
			slot.Drag -= On_Slot_Drag;
			slot.EndDrag -= On_Slot_EndDrag;
			slot.Refreshed.RemoveListener(On_Slot_Refreshed);
		}
	}

	private void OnSlotsCreated()
	{
		foreach (Slot slot in Slots)
		{
			slot.PointerDown += On_Slot_PointerDown;
			slot.PointerUp += On_Slot_PointerUp;
			slot.E_Select += On_Slot_Select;
			slot.BeginDrag += On_Slot_BeginDrag;
			slot.Drag += On_Slot_Drag;
			slot.EndDrag += On_Slot_EndDrag;
			slot.Refreshed.AddListener(On_Slot_Refreshed);
		}
	}

	private void RemoveSlots(Transform parent, Slot template)
	{
		int childCount = parent.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if ((Object)(object)parent.GetChild(parent.childCount - 1) != (Object)(object)((Component)template).transform)
			{
				Object.DestroyImmediate((Object)(object)((Component)parent.GetChild(parent.childCount - 1)).gameObject);
			}
		}
	}

	private void CreateSlots(Transform parent, Slot template)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < m_PreviewSize; i++)
		{
			Slot slot = Object.Instantiate<Slot>(template);
			((Component)slot).transform.SetParent(parent);
			((Component)slot).transform.localPosition = Vector3.zero;
			((Component)slot).transform.localScale = Vector3.one;
		}
	}
}
