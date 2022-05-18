using System;
using System.Collections.Generic;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival
{
	// Token: 0x020008AB RID: 2219
	public class InventoryController : MonoSingleton<InventoryController>
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000297A5 File Offset: 0x000279A5
		public bool IsClosed
		{
			get
			{
				return this.State.Is(ET.InventoryState.Closed);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06003916 RID: 14614 RVA: 0x000297B3 File Offset: 0x000279B3
		public ItemDatabase Database
		{
			get
			{
				return this.m_ItemDatabase;
			}
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x001A3F64 File Offset: 0x001A2164
		public bool AddItemToCollection(int itemID, int amount, string collection, out int added)
		{
			added = 0;
			if (!base.enabled)
			{
				return false;
			}
			for (int i = 0; i < this.m_AllCollections.Length; i++)
			{
				if (this.m_AllCollections[i].Name == collection)
				{
					bool result = false;
					ItemData itemData;
					if (this.m_ItemDatabase.FindItemById(itemID, out itemData))
					{
						result = this.m_AllCollections[i].TryAddItem(itemData, amount, out added, 0UL, 0);
					}
					return result;
				}
			}
			Debug.LogWarningFormat(this, "No collection with the name '{0}' was found! No item added.", new object[]
			{
				collection
			});
			return false;
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x001A3FE8 File Offset: 0x001A21E8
		public bool AddItemToCollection(int itemID, ulong uuid, int amount, string collection, out int added, int index)
		{
			added = 0;
			if (!base.enabled)
			{
				return false;
			}
			for (int i = 0; i < this.m_AllCollections.Length; i++)
			{
				if (this.m_AllCollections[i].Name == collection)
				{
					bool result = false;
					ItemData itemData;
					if (this.m_ItemDatabase.FindItemById(itemID, out itemData))
					{
						result = this.m_AllCollections[i].TryAddItem(itemData, amount, out added, uuid, index);
					}
					return result;
				}
			}
			Debug.LogWarningFormat(this, "No collection with the name '{0}' was found! No item added.", new object[]
			{
				collection
			});
			return false;
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x001A406C File Offset: 0x001A226C
		public bool AddItemToCollection(string itemName, int amount, string collection, out int added)
		{
			added = 0;
			if (!base.enabled)
			{
				return false;
			}
			for (int i = 0; i < this.m_AllCollections.Length; i++)
			{
				if (this.m_AllCollections[i].Name == collection)
				{
					bool result = false;
					ItemData itemData;
					if (this.m_ItemDatabase.FindItemByName(itemName, out itemData))
					{
						result = this.m_AllCollections[i].TryAddItem(itemData, amount, out added, 0UL, 0);
					}
					return result;
				}
			}
			Debug.LogWarningFormat(this, "No collection with the name '{0}' was found! No item added.", new object[]
			{
				collection
			});
			return false;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000297BB File Offset: 0x000279BB
		public int GetItemCount(string name)
		{
			return MonoSingleton<GUIController>.Instance.GetContainer("Inventory").GetItemCount(name);
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x001A40F0 File Offset: 0x001A22F0
		public bool TryRemoveItem(SavableItem item)
		{
			if (!base.enabled)
			{
				return false;
			}
			for (int i = 0; i < this.m_AllCollections.Length; i++)
			{
				if (this.m_AllCollections[i].TryRemoveItem(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x001A4130 File Offset: 0x001A2330
		public void RemoveItems(int ID, ulong uuid)
		{
			ItemContainer[] containers = MonoSingleton<GUIController>.Instance.Containers;
			for (int i = 0; i < containers.Length; i++)
			{
				foreach (Slot slot in containers[i].Slots)
				{
					if (slot.HasItem && slot.gameObject.GetComponent<ItemOnObject>().item.itemUUID == uuid)
					{
						this.Try_DropItem(slot.CurrentItem, slot);
						slot.gameObject.GetComponent<ItemOnObject>().item.itemUUID = 0UL;
						slot.gameObject.GetComponent<ItemOnObject>().item.itemID = 0;
						slot.gameObject.GetComponent<ItemOnObject>().item.itemIndex = 0;
					}
				}
			}
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x001A4214 File Offset: 0x001A2414
		public int findItemCount(ulong uuid)
		{
			int num = 0;
			ItemContainer[] containers = MonoSingleton<GUIController>.Instance.Containers;
			for (int i = 0; i < containers.Length; i++)
			{
				foreach (Slot slot in containers[i].Slots)
				{
					if (slot.HasItem && slot.gameObject.GetComponent<ItemOnObject>().item.itemUUID == uuid)
					{
						num += slot.CurrentItem.CurrentInStack;
					}
				}
			}
			return num;
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x001A42B0 File Offset: 0x001A24B0
		public int findItemCount(string tiemName)
		{
			int num = 0;
			ItemContainer[] containers = MonoSingleton<GUIController>.Instance.Containers;
			for (int i = 0; i < containers.Length; i++)
			{
				foreach (Slot slot in containers[i].Slots)
				{
					if (slot.HasItem && slot.gameObject.GetComponent<ItemOnObject>().item.itemName == tiemName)
					{
						num += slot.CurrentItem.CurrentInStack;
					}
				}
			}
			return num;
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x001A4354 File Offset: 0x001A2554
		public string findItemInCintainersName(ulong uuid)
		{
			string result = "";
			ItemContainer[] containers = MonoSingleton<GUIController>.Instance.Containers;
			for (int i = 0; i < containers.Length; i++)
			{
				foreach (Slot slot in containers[i].Slots)
				{
					if (slot.HasItem && slot.gameObject.GetComponent<ItemOnObject>().item.itemUUID == uuid)
					{
						result = containers[i].Name;
					}
				}
			}
			return result;
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000297D2 File Offset: 0x000279D2
		public void RemoveItems(string itemName, int amount = 1)
		{
			MonoSingleton<GUIController>.Instance.GetContainer("Inventory").RemoveItems(itemName, amount);
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000297EA File Offset: 0x000279EA
		public bool Try_DropItem(SavableItem item, Slot parentSlot = null)
		{
			if (item && item.ItemData.WorldObject)
			{
				if (parentSlot)
				{
					parentSlot.ItemHolder.SetItem(null);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x0002981D File Offset: 0x00027A1D
		public List<ItemHolder> GetEquipmentHolders()
		{
			return this.m_EquipmentHolders;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x001A43F0 File Offset: 0x001A25F0
		private void Awake()
		{
			if (!this.m_ItemDatabase)
			{
				Debug.LogError("No ItemDatabase specified, the inventory will be disabled!", this);
				base.enabled = false;
				return;
			}
			this.SetState.SetTryer(new Attempt<ET.InventoryState>.GenericTryerDelegate(this.TryChange_State));
			this.m_AllCollections = MonoSingleton<GUIController>.Instance.Containers;
			this.m_InventoryCollection = this.CreateListOfHolders(this.m_InventorySize);
			MonoSingleton<GUIController>.Instance.GetContainer("Inventory").Setup(this.m_InventoryCollection);
			this.m_HotbarCollection = this.CreateListOfHolders(this.m_HotbarSize);
			MonoSingleton<GUIController>.Instance.GetContainer("Hotbar").Setup(this.m_HotbarCollection);
			this.m_EquipmentHolders = this.CreateListOfHolders(this.m_EquipmentList.Count);
			for (int i = 0; i < this.m_EquipmentList.Count; i++)
			{
				ItemContainer container = MonoSingleton<GUIController>.Instance.GetContainer(this.m_EquipmentList[i]);
				if (container)
				{
					container.Setup(new List<ItemHolder>
					{
						this.m_EquipmentHolders[i]
					});
				}
				else
				{
					Debug.LogErrorFormat(this, "No GUI collection with the name '{0}' was found!", new object[]
					{
						this.m_EquipmentList[i]
					});
				}
			}
			this.m_Player = GameController.LocalPlayer;
			this.m_Player.ChangeHealth.AddListener(new Action<HealthEventData>(this.OnChanged_PlayerHealth));
			this.m_Player.Death.AddListener(new Action(this.On_PlayerDeath));
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x001A456C File Offset: 0x001A276C
		private void OnChanged_PlayerHealth(HealthEventData data)
		{
			if (data.Delta < 0f)
			{
				for (int i = 0; i < this.m_EquipmentHolders.Count; i++)
				{
					if (this.m_EquipmentHolders[i].HasItem && this.m_EquipmentHolders[i].CurrentItem.HasProperty("Durability"))
					{
						ItemProperty.Float @float = this.m_EquipmentHolders[i].CurrentItem.GetPropertyValue("Durability").Float;
						float num = @float.Current;
						@float.Current = num - 1f;
						if (@float.Current <= 0f)
						{
							this.m_EquipmentHolders[i].SetItem(null);
						}
					}
				}
			}
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x001A462C File Offset: 0x001A282C
		private void On_PlayerDeath()
		{
			if (this.State.Get() != ET.InventoryState.Closed)
			{
				this.SetState.Try(ET.InventoryState.Closed);
			}
			this.RemoveItemsFromCollection("Inventory");
			this.RemoveItemsFromCollection("Hotbar");
			foreach (string collection in this.m_EquipmentList)
			{
				this.RemoveItemsFromCollection(collection);
			}
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x001A46AC File Offset: 0x001A28AC
		private void RemoveItemsFromCollection(string collection)
		{
			ItemContainer container = MonoSingleton<GUIController>.Instance.GetContainer(collection);
			if (!container)
			{
				return;
			}
			foreach (Slot slot in container.Slots)
			{
				if (slot.HasItem)
				{
					slot.ItemHolder.SetItem(null);
				}
			}
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x001A4724 File Offset: 0x001A2924
		private void DropItemsFromCollection(string collection)
		{
			ItemContainer container = MonoSingleton<GUIController>.Instance.GetContainer(collection);
			if (!container)
			{
				return;
			}
			foreach (Slot slot in container.Slots)
			{
				if (slot.HasItem)
				{
					if (slot.CurrentItem.ItemData.IsBuildable)
					{
						slot.ItemHolder.SetItem(null);
					}
					else
					{
						this.Try_DropItem(slot.CurrentItem, slot);
					}
				}
			}
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x001A47BC File Offset: 0x001A29BC
		private bool TryChange_State(ET.InventoryState state)
		{
			bool flag = false;
			if (Time.time > this.m_LastTimeToggledInventory + 0.5f)
			{
				this.m_LastTimeToggledInventory = Time.time;
				flag = true;
			}
			if (flag)
			{
				this.State.Set(state);
				EventSystem.current.SetSelectedGameObject(null);
			}
			return flag;
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x001A4808 File Offset: 0x001A2A08
		private List<ItemHolder> CreateListOfHolders(int size)
		{
			List<ItemHolder> list = new List<ItemHolder>();
			for (int i = 0; i < size; i++)
			{
				list.Add(new ItemHolder());
			}
			return list;
		}

		// Token: 0x04003345 RID: 13125
		public Value<ET.InventoryState> State = new Value<ET.InventoryState>(ET.InventoryState.Closed);

		// Token: 0x04003346 RID: 13126
		public Attempt<ET.InventoryState> SetState = new Attempt<ET.InventoryState>();

		// Token: 0x04003347 RID: 13127
		public Attempt<SmeltingStation> OpenFurnace = new Attempt<SmeltingStation>();

		// Token: 0x04003348 RID: 13128
		public Attempt<SmeltingStation> OpenCampfire = new Attempt<SmeltingStation>();

		// Token: 0x04003349 RID: 13129
		public Attempt<LootObject> OpenLootContainer = new Attempt<LootObject>();

		// Token: 0x0400334A RID: 13130
		public Attempt<Anvil> OpenAnvil = new Attempt<Anvil>();

		// Token: 0x0400334B RID: 13131
		public Attempt<CraftData> CraftItem = new Attempt<CraftData>();

		// Token: 0x0400334C RID: 13132
		public Message<ItemHolder> EquipmentChanged = new Message<ItemHolder>();

		// Token: 0x0400334D RID: 13133
		[SerializeField]
		[Tooltip("The inventory cannot function without this, as some operations, like ADD, LOAD require a database.")]
		private ItemDatabase m_ItemDatabase;

		// Token: 0x0400334E RID: 13134
		[Header("Item Collections")]
		[SerializeField]
		[Range(1f, 48f)]
		private int m_InventorySize = 24;

		// Token: 0x0400334F RID: 13135
		[SerializeField]
		[Range(1f, 12f)]
		private int m_HotbarSize = 6;

		// Token: 0x04003350 RID: 13136
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_EquipmentList;

		// Token: 0x04003351 RID: 13137
		[Header("Item Drop")]
		[SerializeField]
		private Vector3 m_DropOffset = new Vector3(0f, 0f, 0.8f);

		// Token: 0x04003352 RID: 13138
		[SerializeField]
		private float m_DropAngularFactor = 150f;

		// Token: 0x04003353 RID: 13139
		[SerializeField]
		private float m_DropSpeed = 8f;

		// Token: 0x04003354 RID: 13140
		private PlayerEventHandler m_Player;

		// Token: 0x04003355 RID: 13141
		private ItemContainer[] m_AllCollections;

		// Token: 0x04003356 RID: 13142
		private float m_LastTimeToggledInventory;

		// Token: 0x04003357 RID: 13143
		private List<ItemHolder> m_InventoryCollection;

		// Token: 0x04003358 RID: 13144
		private List<ItemHolder> m_HotbarCollection;

		// Token: 0x04003359 RID: 13145
		private List<ItemHolder> m_EquipmentHolders;
	}
}
