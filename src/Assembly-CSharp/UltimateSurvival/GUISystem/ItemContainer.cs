using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000930 RID: 2352
	public sealed class ItemContainer : GUIBehaviour
	{
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06003BE4 RID: 15332 RVA: 0x001AF328 File Offset: 0x001AD528
		// (remove) Token: 0x06003BE5 RID: 15333 RVA: 0x001AF360 File Offset: 0x001AD560
		public event Action<PointerEventData, Slot> Slot_PointerDown;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06003BE6 RID: 15334 RVA: 0x001AF398 File Offset: 0x001AD598
		// (remove) Token: 0x06003BE7 RID: 15335 RVA: 0x001AF3D0 File Offset: 0x001AD5D0
		public event Action<PointerEventData, Slot> Slot_PointerUp;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06003BE8 RID: 15336 RVA: 0x001AF408 File Offset: 0x001AD608
		// (remove) Token: 0x06003BE9 RID: 15337 RVA: 0x001AF440 File Offset: 0x001AD640
		public event Action<BaseEventData, Slot> Slot_Select;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06003BEA RID: 15338 RVA: 0x001AF478 File Offset: 0x001AD678
		// (remove) Token: 0x06003BEB RID: 15339 RVA: 0x001AF4B0 File Offset: 0x001AD6B0
		public event DragAction Slot_BeginDrag;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06003BEC RID: 15340 RVA: 0x001AF4E8 File Offset: 0x001AD6E8
		// (remove) Token: 0x06003BED RID: 15341 RVA: 0x001AF520 File Offset: 0x001AD720
		public event DragAction Slot_Drag;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06003BEE RID: 15342 RVA: 0x001AF558 File Offset: 0x001AD758
		// (remove) Token: 0x06003BEF RID: 15343 RVA: 0x001AF590 File Offset: 0x001AD790
		public event DragAction Slot_EndDrag;

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x0002B50D File Offset: 0x0002970D
		public bool IsOpen
		{
			get
			{
				return this.m_SetUp && (!this.m_Window || this.m_Window.IsOpen);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x0002B533 File Offset: 0x00029733
		public string Name
		{
			get
			{
				return this._Name;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x0002B53B File Offset: 0x0002973B
		// (set) Token: 0x06003BF3 RID: 15347 RVA: 0x0002B543 File Offset: 0x00029743
		public List<Slot> Slots { get; private set; }

		// Token: 0x06003BF4 RID: 15348 RVA: 0x001AF5C8 File Offset: 0x001AD7C8
		public int getHasItemSlotCount()
		{
			int num = 0;
			using (List<Slot>.Enumerator enumerator = this.Slots.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasItem)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x0002B54C File Offset: 0x0002974C
		public void Setup(ItemHolder itemholder)
		{
			this.Setup(new List<ItemHolder>
			{
				itemholder
			});
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x001AF624 File Offset: 0x001AD824
		public void Setup(List<ItemHolder> itemHolders)
		{
			if (!Application.isPlaying)
			{
				Debug.LogError("You can't create the container when the application is not playing.", this);
				return;
			}
			if (this.m_SlotTemplate == null)
			{
				Debug.LogError("You tried to create slots for this container, but the slot template is null / not assigned in the inspector!", this);
				return;
			}
			if (!this.m_SlotsParent || !this.m_SlotTemplate)
			{
				return;
			}
			if (this.Slots == null)
			{
				this.Slots = new List<Slot>();
				base.GetComponentsInChildren<Slot>(this.Slots);
			}
			this.m_ItemHolders = itemHolders;
			bool activeSelf = this.m_SlotTemplate.gameObject.activeSelf;
			this.m_SlotTemplate.gameObject.SetActive(true);
			this.PrepareGUIForSlots(this.m_SlotsParent, this.m_SlotTemplate);
			this.m_SlotTemplate.gameObject.SetActive(activeSelf);
			this.m_SetUp = true;
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x001AF6EC File Offset: 0x001AD8EC
		public bool HasItem(SavableItem item)
		{
			for (int i = 0; i < this.m_ItemHolders.Count; i++)
			{
				if (item == this.m_ItemHolders[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x0002B560 File Offset: 0x00029760
		public bool TryAddItem(ItemData itemData, int amount, out int added, ulong uuid = 0UL, int index = 0)
		{
			added = 0;
			CollectionUtils.AddItem(itemData, amount, this.Slots, out added, null, uuid, index);
			return added > 0;
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x001AF72C File Offset: 0x001AD92C
		public bool TryAddItem(ItemData itemData, int amount)
		{
			int num = 0;
			CollectionUtils.AddItem(itemData, amount, this.Slots, out num, null, 0UL, 0);
			return num > 0;
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x001AF754 File Offset: 0x001AD954
		public bool TryAddItem(string name, int amount, out int added)
		{
			added = 0;
			ItemData itemData;
			if (MonoSingleton<InventoryController>.Instance.Database.FindItemByName(name, out itemData))
			{
				CollectionUtils.AddItem(itemData, amount, this.Slots, out added, null, 0UL, 0);
			}
			return added > 0;
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x001AF790 File Offset: 0x001AD990
		public bool TryAddItem(string name, int amount)
		{
			int num = 0;
			ItemData itemData;
			if (MonoSingleton<InventoryController>.Instance.Database.FindItemByName(name, out itemData))
			{
				CollectionUtils.AddItem(itemData, amount, this.Slots, out num, null, 0UL, 0);
			}
			return num > 0;
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x001AF7CC File Offset: 0x001AD9CC
		public bool TryAddItem(SavableItem item)
		{
			if (item == null)
			{
				return false;
			}
			int num = 0;
			CollectionUtils.AddItem(item.ItemData, item.CurrentInStack, this.Slots, out num, item.CurrentPropertyValues, 0UL, 0);
			return num > 0;
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x001AF808 File Offset: 0x001ADA08
		public bool TryRemoveItem(SavableItem item)
		{
			Slot slot2 = this.Slots.Find((Slot slot) => slot.CurrentItem == item);
			if (slot2)
			{
				slot2.ItemHolder.SetItem(null);
				return true;
			}
			return false;
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x0002B57D File Offset: 0x0002977D
		public void RemoveItems(string itemName, int amount, out int removed)
		{
			CollectionUtils.RemoveItems(itemName, amount, this.Slots, out removed);
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x000042DD File Offset: 0x000024DD
		public void RemoveItems(ulong uuid)
		{
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x001AF854 File Offset: 0x001ADA54
		public void RemoveItems(string itemName, int amount)
		{
			int num;
			CollectionUtils.RemoveItems(itemName, amount, this.Slots, out num);
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x001AF870 File Offset: 0x001ADA70
		public void AddAllFrom(ItemContainer container)
		{
			for (int i = 0; i < container.Slots.Count; i++)
			{
				if (container.Slots[i].HasItem && this.TryAddItem(container.Slots[i].CurrentItem))
				{
					container.Slots[i].ItemHolder.SetItem(null);
				}
			}
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x001AF8D8 File Offset: 0x001ADAD8
		public int GetItemCount(int itemID)
		{
			int num = 0;
			for (int i = 0; i < this.Slots.Count; i++)
			{
				if (this.Slots[i].HasItem && this.Slots[i].CurrentItem.Id == itemID)
				{
					num += this.Slots[i].CurrentItem.CurrentInStack;
				}
			}
			return num;
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x001AF944 File Offset: 0x001ADB44
		public int GetItemCount(string itemName)
		{
			int num = 0;
			for (int i = 0; i < this.Slots.Count; i++)
			{
				if (this.Slots[i].HasItem && this.Slots[i].CurrentItem.ItemData.Name == itemName)
				{
					num += this.Slots[i].CurrentItem.CurrentInStack;
				}
			}
			return num;
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x0002B58D File Offset: 0x0002978D
		public void ApplyAll()
		{
			if (Application.isPlaying)
			{
				return;
			}
			this.ApplyTemplate();
			this.ApplyRequiredStuff();
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x001AF9BC File Offset: 0x001ADBBC
		public void ApplyTemplate()
		{
			if (Application.isPlaying)
			{
				return;
			}
			Transform slotsParent = this.m_SlotsParent;
			Slot slotTemplate = this.m_SlotTemplate;
			if (!slotsParent || !slotTemplate)
			{
				return;
			}
			bool activeSelf = slotTemplate.gameObject.activeSelf;
			slotTemplate.gameObject.SetActive(true);
			this.RemoveSlots(slotsParent, slotTemplate);
			this.CreateSlots(slotsParent, slotTemplate);
			slotTemplate.gameObject.SetActive(activeSelf);
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x001AFA24 File Offset: 0x001ADC24
		public void ApplyRequiredStuff()
		{
			if (Application.isPlaying)
			{
				return;
			}
			foreach (Slot slot in base.GetComponentsInChildren<Slot>())
			{
				slot.RequiredCategories = this.m_RequiredCategories;
				slot.RequiredProperties = this.m_RequiredProperties;
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x0002B5A3 File Offset: 0x000297A3
		private void Awake()
		{
			this.Slots = new List<Slot>();
			base.GetComponentsInChildren<Slot>(this.Slots);
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x001AFA68 File Offset: 0x001ADC68
		private void On_Slot_PointerDown(PointerEventData data, Slot slot)
		{
			if (this.Slot_PointerDown != null)
			{
				this.Slot_PointerDown(data, slot);
			}
			if (Input.GetKey(304) && data.button == null)
			{
				if (this._Name == "Inventory")
				{
					ItemContainer container = base.Controller.GetContainer("Loot");
					if (container.IsOpen)
					{
						if (container.TryAddItem(slot.CurrentItem))
						{
							slot.ItemHolder.SetItem(null);
							return;
						}
					}
					else if (base.Controller.GetContainer("Hotbar").TryAddItem(slot.CurrentItem))
					{
						slot.ItemHolder.SetItem(null);
						return;
					}
				}
				else if (base.Controller.GetContainer("Inventory").TryAddItem(slot.CurrentItem))
				{
					slot.ItemHolder.SetItem(null);
				}
			}
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x0002B5BC File Offset: 0x000297BC
		private void On_Slot_PointerUp(PointerEventData data, Slot slot)
		{
			if (this.Slot_PointerUp != null)
			{
				this.Slot_PointerUp(data, slot);
			}
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x0002B5D3 File Offset: 0x000297D3
		private void On_Slot_Select(BaseEventData data, Slot slot)
		{
			if (this.Slot_Select != null)
			{
				this.Slot_Select(data, slot);
			}
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x0002B5EA File Offset: 0x000297EA
		private void On_Slot_BeginDrag(PointerEventData data, Slot slot)
		{
			if (this.Slot_BeginDrag != null)
			{
				this.Slot_BeginDrag(data, slot, this);
			}
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x0002B602 File Offset: 0x00029802
		private void On_Slot_Drag(PointerEventData data, Slot slot)
		{
			if (this.Slot_Drag != null)
			{
				this.Slot_Drag(data, slot, this);
			}
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x0002B61A File Offset: 0x0002981A
		private void On_Slot_EndDrag(PointerEventData data, Slot slot)
		{
			if (this.Slot_EndDrag != null)
			{
				this.Slot_EndDrag(data, slot, this);
			}
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x0002B632 File Offset: 0x00029832
		private void On_Slot_Refreshed(Slot slot)
		{
			this.Slot_Refreshed.Send(slot);
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x001AFB40 File Offset: 0x001ADD40
		private void ActivateSlots(Transform parent, Slot template, int count, bool active)
		{
			for (int i = 0; i < count; i++)
			{
				int num = parent.childCount - i - 1;
				if (parent.GetChild(num) != template.transform)
				{
					parent.GetChild(num).gameObject.SetActive(active);
				}
			}
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x001AFB8C File Offset: 0x001ADD8C
		private void PrepareGUIForSlots(Transform parent, Slot template)
		{
			this.OnSlotsDiscarded();
			this.ActivateSlots(parent, template, Mathf.Clamp(this.m_ItemHolders.Count, 0, this.Slots.Count), true);
			if (this.m_ItemHolders.Count > this.Slots.Count)
			{
				int num = this.m_ItemHolders.Count - this.Slots.Count;
				for (int i = 0; i < num; i++)
				{
					Slot slot = Object.Instantiate<Slot>(template);
					slot.transform.SetParent(parent);
					slot.transform.localPosition = Vector3.zero;
					slot.transform.localScale = Vector3.one;
					this.Slots.Add(slot);
				}
			}
			else if (this.m_ItemHolders.Count < this.Slots.Count)
			{
				this.ActivateSlots(parent, template, this.Slots.Count - this.m_ItemHolders.Count, false);
			}
			for (int j = 0; j < this.m_ItemHolders.Count; j++)
			{
				this.Slots[j].LinkWithHolder(this.m_ItemHolders[j]);
			}
			this.OnSlotsCreated();
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x001AFCB4 File Offset: 0x001ADEB4
		private void OnSlotsDiscarded()
		{
			foreach (Slot slot in this.Slots)
			{
				slot.PointerDown -= this.On_Slot_PointerDown;
				slot.PointerUp -= this.On_Slot_PointerUp;
				slot.E_Select -= this.On_Slot_Select;
				slot.BeginDrag -= this.On_Slot_BeginDrag;
				slot.Drag -= this.On_Slot_Drag;
				slot.EndDrag -= this.On_Slot_EndDrag;
				slot.Refreshed.RemoveListener(new Action<Slot>(this.On_Slot_Refreshed));
			}
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x001AFD88 File Offset: 0x001ADF88
		private void OnSlotsCreated()
		{
			foreach (Slot slot in this.Slots)
			{
				slot.PointerDown += this.On_Slot_PointerDown;
				slot.PointerUp += this.On_Slot_PointerUp;
				slot.E_Select += this.On_Slot_Select;
				slot.BeginDrag += this.On_Slot_BeginDrag;
				slot.Drag += this.On_Slot_Drag;
				slot.EndDrag += this.On_Slot_EndDrag;
				slot.Refreshed.AddListener(new Action<Slot>(this.On_Slot_Refreshed));
			}
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x001AFE5C File Offset: 0x001AE05C
		private void RemoveSlots(Transform parent, Slot template)
		{
			int childCount = parent.childCount;
			for (int i = 0; i < childCount; i++)
			{
				if (parent.GetChild(parent.childCount - 1) != template.transform)
				{
					Object.DestroyImmediate(parent.GetChild(parent.childCount - 1).gameObject);
				}
			}
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x001AFEB0 File Offset: 0x001AE0B0
		private void CreateSlots(Transform parent, Slot template)
		{
			for (int i = 0; i < this.m_PreviewSize; i++)
			{
				Slot slot = Object.Instantiate<Slot>(template);
				slot.transform.SetParent(parent);
				slot.transform.localPosition = Vector3.zero;
				slot.transform.localScale = Vector3.one;
			}
		}

		// Token: 0x04003665 RID: 13925
		public Message<Slot> Slot_Refreshed = new Message<Slot>();

		// Token: 0x0400366D RID: 13933
		[SerializeField]
		private string _Name = "";

		// Token: 0x0400366E RID: 13934
		[SerializeField]
		[Tooltip("It is optional. If you assign a window, the open state will be taken from the window, otherwise the container will always be considered open.")]
		private Window m_Window;

		// Token: 0x0400366F RID: 13935
		[Header("Slots")]
		[SerializeField]
		[Tooltip("All the created slots will be based on this template.")]
		private Slot m_SlotTemplate;

		// Token: 0x04003670 RID: 13936
		[SerializeField]
		[Tooltip("The parent of the slots, usually it has attached a GridLayoutGroup, HorizontalLayoutGroup, etc, so they are automatically arranged.")]
		private Transform m_SlotsParent;

		// Token: 0x04003671 RID: 13937
		[SerializeField]
		[Range(0f, 100f)]
		private int m_PreviewSize;

		// Token: 0x04003672 RID: 13938
		[Header("Required Stuff")]
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_RequiredCategories;

		// Token: 0x04003673 RID: 13939
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_RequiredProperties;

		// Token: 0x04003674 RID: 13940
		private List<ItemHolder> m_ItemHolders;

		// Token: 0x04003675 RID: 13941
		private List<Slot> m_Slots;

		// Token: 0x04003676 RID: 13942
		private bool m_SetUp;
	}
}
