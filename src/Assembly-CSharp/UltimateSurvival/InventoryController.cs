using System;
using System.Collections.Generic;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival
{
	// Token: 0x020005DD RID: 1501
	public class InventoryController : MonoSingleton<InventoryController>
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x0015A3F4 File Offset: 0x001585F4
		public bool IsClosed
		{
			get
			{
				return this.State.Is(ET.InventoryState.Closed);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x0015A402 File Offset: 0x00158602
		public ItemDatabase Database
		{
			get
			{
				return this.m_ItemDatabase;
			}
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x0015A40C File Offset: 0x0015860C
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

		// Token: 0x0600304C RID: 12364 RVA: 0x0015A490 File Offset: 0x00158690
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

		// Token: 0x0600304D RID: 12365 RVA: 0x0015A514 File Offset: 0x00158714
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

		// Token: 0x0600304E RID: 12366 RVA: 0x0015A596 File Offset: 0x00158796
		public int GetItemCount(string name)
		{
			return MonoSingleton<GUIController>.Instance.GetContainer("Inventory").GetItemCount(name);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x0015A5B0 File Offset: 0x001587B0
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

		// Token: 0x06003050 RID: 12368 RVA: 0x0015A5F0 File Offset: 0x001587F0
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

		// Token: 0x06003051 RID: 12369 RVA: 0x0015A6D4 File Offset: 0x001588D4
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

		// Token: 0x06003052 RID: 12370 RVA: 0x0015A770 File Offset: 0x00158970
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

		// Token: 0x06003053 RID: 12371 RVA: 0x0015A814 File Offset: 0x00158A14
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

		// Token: 0x06003054 RID: 12372 RVA: 0x0015A8B0 File Offset: 0x00158AB0
		public void RemoveItems(string itemName, int amount = 1)
		{
			MonoSingleton<GUIController>.Instance.GetContainer("Inventory").RemoveItems(itemName, amount);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x0015A8C8 File Offset: 0x00158AC8
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

		// Token: 0x06003056 RID: 12374 RVA: 0x0015A8FB File Offset: 0x00158AFB
		public List<ItemHolder> GetEquipmentHolders()
		{
			return this.m_EquipmentHolders;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x0015A904 File Offset: 0x00158B04
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

		// Token: 0x06003058 RID: 12376 RVA: 0x0015AA80 File Offset: 0x00158C80
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

		// Token: 0x06003059 RID: 12377 RVA: 0x0015AB40 File Offset: 0x00158D40
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

		// Token: 0x0600305A RID: 12378 RVA: 0x0015ABC0 File Offset: 0x00158DC0
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

		// Token: 0x0600305B RID: 12379 RVA: 0x0015AC38 File Offset: 0x00158E38
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

		// Token: 0x0600305C RID: 12380 RVA: 0x0015ACD0 File Offset: 0x00158ED0
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

		// Token: 0x0600305D RID: 12381 RVA: 0x0015AD1C File Offset: 0x00158F1C
		private List<ItemHolder> CreateListOfHolders(int size)
		{
			List<ItemHolder> list = new List<ItemHolder>();
			for (int i = 0; i < size; i++)
			{
				list.Add(new ItemHolder());
			}
			return list;
		}

		// Token: 0x04002A87 RID: 10887
		public Value<ET.InventoryState> State = new Value<ET.InventoryState>(ET.InventoryState.Closed);

		// Token: 0x04002A88 RID: 10888
		public Attempt<ET.InventoryState> SetState = new Attempt<ET.InventoryState>();

		// Token: 0x04002A89 RID: 10889
		public Attempt<SmeltingStation> OpenFurnace = new Attempt<SmeltingStation>();

		// Token: 0x04002A8A RID: 10890
		public Attempt<SmeltingStation> OpenCampfire = new Attempt<SmeltingStation>();

		// Token: 0x04002A8B RID: 10891
		public Attempt<LootObject> OpenLootContainer = new Attempt<LootObject>();

		// Token: 0x04002A8C RID: 10892
		public Attempt<Anvil> OpenAnvil = new Attempt<Anvil>();

		// Token: 0x04002A8D RID: 10893
		public Attempt<CraftData> CraftItem = new Attempt<CraftData>();

		// Token: 0x04002A8E RID: 10894
		public Message<ItemHolder> EquipmentChanged = new Message<ItemHolder>();

		// Token: 0x04002A8F RID: 10895
		[SerializeField]
		[Tooltip("The inventory cannot function without this, as some operations, like ADD, LOAD require a database.")]
		private ItemDatabase m_ItemDatabase;

		// Token: 0x04002A90 RID: 10896
		[Header("Item Collections")]
		[SerializeField]
		[Range(1f, 48f)]
		private int m_InventorySize = 24;

		// Token: 0x04002A91 RID: 10897
		[SerializeField]
		[Range(1f, 12f)]
		private int m_HotbarSize = 6;

		// Token: 0x04002A92 RID: 10898
		[SerializeField]
		[Reorderable]
		private ReorderableStringList m_EquipmentList;

		// Token: 0x04002A93 RID: 10899
		[Header("Item Drop")]
		[SerializeField]
		private Vector3 m_DropOffset = new Vector3(0f, 0f, 0.8f);

		// Token: 0x04002A94 RID: 10900
		[SerializeField]
		private float m_DropAngularFactor = 150f;

		// Token: 0x04002A95 RID: 10901
		[SerializeField]
		private float m_DropSpeed = 8f;

		// Token: 0x04002A96 RID: 10902
		private PlayerEventHandler m_Player;

		// Token: 0x04002A97 RID: 10903
		private ItemContainer[] m_AllCollections;

		// Token: 0x04002A98 RID: 10904
		private float m_LastTimeToggledInventory;

		// Token: 0x04002A99 RID: 10905
		private List<ItemHolder> m_InventoryCollection;

		// Token: 0x04002A9A RID: 10906
		private List<ItemHolder> m_HotbarCollection;

		// Token: 0x04002A9B RID: 10907
		private List<ItemHolder> m_EquipmentHolders;
	}
}
