using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200063B RID: 1595
	public sealed class ItemContainer : GUIBehaviour
	{
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600329E RID: 12958 RVA: 0x00165EF8 File Offset: 0x001640F8
		// (remove) Token: 0x0600329F RID: 12959 RVA: 0x00165F30 File Offset: 0x00164130
		public event Action<PointerEventData, Slot> Slot_PointerDown;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060032A0 RID: 12960 RVA: 0x00165F68 File Offset: 0x00164168
		// (remove) Token: 0x060032A1 RID: 12961 RVA: 0x00165FA0 File Offset: 0x001641A0
		public event Action<PointerEventData, Slot> Slot_PointerUp;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060032A2 RID: 12962 RVA: 0x00165FD8 File Offset: 0x001641D8
		// (remove) Token: 0x060032A3 RID: 12963 RVA: 0x00166010 File Offset: 0x00164210
		public event Action<BaseEventData, Slot> Slot_Select;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060032A4 RID: 12964 RVA: 0x00166048 File Offset: 0x00164248
		// (remove) Token: 0x060032A5 RID: 12965 RVA: 0x00166080 File Offset: 0x00164280
		public event DragAction Slot_BeginDrag;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060032A6 RID: 12966 RVA: 0x001660B8 File Offset: 0x001642B8
		// (remove) Token: 0x060032A7 RID: 12967 RVA: 0x001660F0 File Offset: 0x001642F0
		public event DragAction Slot_Drag;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060032A8 RID: 12968 RVA: 0x00166128 File Offset: 0x00164328
		// (remove) Token: 0x060032A9 RID: 12969 RVA: 0x00166160 File Offset: 0x00164360
		public event DragAction Slot_EndDrag;

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x00166195 File Offset: 0x00164395
		public bool IsOpen
		{
			get
			{
				return this.m_SetUp && (!this.m_Window || this.m_Window.IsOpen);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x001661BB File Offset: 0x001643BB
		public string Name
		{
			get
			{
				return this._Name;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x001661C3 File Offset: 0x001643C3
		// (set) Token: 0x060032AD RID: 12973 RVA: 0x001661CB File Offset: 0x001643CB
		public List<Slot> Slots { get; private set; }

		// Token: 0x060032AE RID: 12974 RVA: 0x001661D4 File Offset: 0x001643D4
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

		// Token: 0x060032AF RID: 12975 RVA: 0x00166230 File Offset: 0x00164430
		public void Setup(ItemHolder itemholder)
		{
			this.Setup(new List<ItemHolder>
			{
				itemholder
			});
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x00166244 File Offset: 0x00164444
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

		// Token: 0x060032B1 RID: 12977 RVA: 0x0016630C File Offset: 0x0016450C
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

		// Token: 0x060032B2 RID: 12978 RVA: 0x0016634B File Offset: 0x0016454B
		public bool TryAddItem(ItemData itemData, int amount, out int added, ulong uuid = 0UL, int index = 0)
		{
			added = 0;
			CollectionUtils.AddItem(itemData, amount, this.Slots, out added, null, uuid, index);
			return added > 0;
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x00166368 File Offset: 0x00164568
		public bool TryAddItem(ItemData itemData, int amount)
		{
			int num = 0;
			CollectionUtils.AddItem(itemData, amount, this.Slots, out num, null, 0UL, 0);
			return num > 0;
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x00166390 File Offset: 0x00164590
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

		// Token: 0x060032B5 RID: 12981 RVA: 0x001663CC File Offset: 0x001645CC
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

		// Token: 0x060032B6 RID: 12982 RVA: 0x00166408 File Offset: 0x00164608
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

		// Token: 0x060032B7 RID: 12983 RVA: 0x00166444 File Offset: 0x00164644
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

		// Token: 0x060032B8 RID: 12984 RVA: 0x0016648D File Offset: 0x0016468D
		public void RemoveItems(string itemName, int amount, out int removed)
		{
			CollectionUtils.RemoveItems(itemName, amount, this.Slots, out removed);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x00004095 File Offset: 0x00002295
		public void RemoveItems(ulong uuid)
		{
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x001664A0 File Offset: 0x001646A0
		public void RemoveItems(string itemName, int amount)
		{
			int num;
			CollectionUtils.RemoveItems(itemName, amount, this.Slots, out num);
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x001664BC File Offset: 0x001646BC
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

		// Token: 0x060032BC RID: 12988 RVA: 0x00166524 File Offset: 0x00164724
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

		// Token: 0x060032BD RID: 12989 RVA: 0x00166590 File Offset: 0x00164790
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

		// Token: 0x060032BE RID: 12990 RVA: 0x00166605 File Offset: 0x00164805
		public void ApplyAll()
		{
			if (Application.isPlaying)
			{
				return;
			}
			this.ApplyTemplate();
			this.ApplyRequiredStuff();
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x0016661C File Offset: 0x0016481C
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

		// Token: 0x060032C0 RID: 12992 RVA: 0x00166684 File Offset: 0x00164884
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

		// Token: 0x060032C1 RID: 12993 RVA: 0x001666C8 File Offset: 0x001648C8
		private void Awake()
		{
			this.Slots = new List<Slot>();
			base.GetComponentsInChildren<Slot>(this.Slots);
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x001666E4 File Offset: 0x001648E4
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

		// Token: 0x060032C3 RID: 12995 RVA: 0x001667B9 File Offset: 0x001649B9
		private void On_Slot_PointerUp(PointerEventData data, Slot slot)
		{
			if (this.Slot_PointerUp != null)
			{
				this.Slot_PointerUp(data, slot);
			}
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x001667D0 File Offset: 0x001649D0
		private void On_Slot_Select(BaseEventData data, Slot slot)
		{
			if (this.Slot_Select != null)
			{
				this.Slot_Select(data, slot);
			}
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x001667E7 File Offset: 0x001649E7
		private void On_Slot_BeginDrag(PointerEventData data, Slot slot)
		{
			if (this.Slot_BeginDrag != null)
			{
				this.Slot_BeginDrag(data, slot, this);
			}
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x001667FF File Offset: 0x001649FF
		private void On_Slot_Drag(PointerEventData data, Slot slot)
		{
			if (this.Slot_Drag != null)
			{
				this.Slot_Drag(data, slot, this);
			}
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x00166817 File Offset: 0x00164A17
		private void On_Slot_EndDrag(PointerEventData data, Slot slot)
		{
			if (this.Slot_EndDrag != null)
			{
				this.Slot_EndDrag(data, slot, this);
			}
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x0016682F File Offset: 0x00164A2F
		private void On_Slot_Refreshed(Slot slot)
		{
			this.Slot_Refreshed.Send(slot);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x00166840 File Offset: 0x00164A40
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

		// Token: 0x060032CA RID: 13002 RVA: 0x0016688C File Offset: 0x00164A8C
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

		// Token: 0x060032CB RID: 13003 RVA: 0x001669B4 File Offset: 0x00164BB4
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

		// Token: 0x060032CC RID: 13004 RVA: 0x00166A88 File Offset: 0x00164C88
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

		// Token: 0x060032CD RID: 13005 RVA: 0x00166B5C File Offset: 0x00164D5C
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

		// Token: 0x060032CE RID: 13006 RVA: 0x00166BB0 File Offset: 0x00164DB0
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

		// Token: 0x04002D09 RID: 11529
		public Message<Slot> Slot_Refreshed = new Message<Slot>();

		// Token: 0x04002D11 RID: 11537
		[SerializeField]
		private string _Name = "";

		// Token: 0x04002D12 RID: 11538
		[SerializeField]
		[Tooltip("It is optional. If you assign a window, the open state will be taken from the window, otherwise the container will always be considered open.")]
		private Window m_Window;

		// Token: 0x04002D13 RID: 11539
		[Header("Slots")]
		[SerializeField]
		[Tooltip("All the created slots will be based on this template.")]
		private Slot m_SlotTemplate;

		// Token: 0x04002D14 RID: 11540
		[SerializeField]
		[Tooltip("The parent of the slots, usually it has attached a GridLayoutGroup, HorizontalLayoutGroup, etc, so they are automatically arranged.")]
		private Transform m_SlotsParent;

		// Token: 0x04002D15 RID: 11541
		[SerializeField]
		[Range(0f, 100f)]
		private int m_PreviewSize;

		// Token: 0x04002D16 RID: 11542
		[Header("Required Stuff")]
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_RequiredCategories;

		// Token: 0x04002D17 RID: 11543
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_RequiredProperties;

		// Token: 0x04002D18 RID: 11544
		private List<ItemHolder> m_ItemHolders;

		// Token: 0x04002D19 RID: 11545
		private List<Slot> m_Slots;

		// Token: 0x04002D1A RID: 11546
		private bool m_SetUp;
	}
}
