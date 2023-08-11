using System.Collections.Generic;
using UltimateSurvival.GUISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival;

public class InventoryController : MonoSingleton<InventoryController>
{
	public Value<ET.InventoryState> State = new Value<ET.InventoryState>(ET.InventoryState.Closed);

	public Attempt<ET.InventoryState> SetState = new Attempt<ET.InventoryState>();

	public Attempt<SmeltingStation> OpenFurnace = new Attempt<SmeltingStation>();

	public Attempt<SmeltingStation> OpenCampfire = new Attempt<SmeltingStation>();

	public Attempt<LootObject> OpenLootContainer = new Attempt<LootObject>();

	public Attempt<Anvil> OpenAnvil = new Attempt<Anvil>();

	public Attempt<CraftData> CraftItem = new Attempt<CraftData>();

	public Message<ItemHolder> EquipmentChanged = new Message<ItemHolder>();

	[SerializeField]
	[Tooltip("The inventory cannot function without this, as some operations, like ADD, LOAD require a database.")]
	private ItemDatabase m_ItemDatabase;

	[Header("Item Collections")]
	[SerializeField]
	[Range(1f, 48f)]
	private int m_InventorySize = 24;

	[SerializeField]
	[Range(1f, 12f)]
	private int m_HotbarSize = 6;

	[SerializeField]
	[Reorderable]
	private ReorderableStringList m_EquipmentList;

	[Header("Item Drop")]
	[SerializeField]
	private Vector3 m_DropOffset = new Vector3(0f, 0f, 0.8f);

	[SerializeField]
	private float m_DropAngularFactor = 150f;

	[SerializeField]
	private float m_DropSpeed = 8f;

	private PlayerEventHandler m_Player;

	private ItemContainer[] m_AllCollections;

	private float m_LastTimeToggledInventory;

	private List<ItemHolder> m_InventoryCollection;

	private List<ItemHolder> m_HotbarCollection;

	private List<ItemHolder> m_EquipmentHolders;

	public bool IsClosed => State.Is(ET.InventoryState.Closed);

	public ItemDatabase Database => m_ItemDatabase;

	public bool AddItemToCollection(int itemID, int amount, string collection, out int added)
	{
		added = 0;
		if (!((Behaviour)this).enabled)
		{
			return false;
		}
		for (int i = 0; i < m_AllCollections.Length; i++)
		{
			if (m_AllCollections[i].Name == collection)
			{
				bool result = false;
				if (m_ItemDatabase.FindItemById(itemID, out var itemData))
				{
					result = m_AllCollections[i].TryAddItem(itemData, amount, out added, 0uL);
				}
				return result;
			}
		}
		Debug.LogWarningFormat((Object)(object)this, "No collection with the name '{0}' was found! No item added.", new object[1] { collection });
		return false;
	}

	public bool AddItemToCollection(int itemID, ulong uuid, int amount, string collection, out int added, int index)
	{
		added = 0;
		if (!((Behaviour)this).enabled)
		{
			return false;
		}
		for (int i = 0; i < m_AllCollections.Length; i++)
		{
			if (m_AllCollections[i].Name == collection)
			{
				bool result = false;
				if (m_ItemDatabase.FindItemById(itemID, out var itemData))
				{
					result = m_AllCollections[i].TryAddItem(itemData, amount, out added, uuid, index);
				}
				return result;
			}
		}
		Debug.LogWarningFormat((Object)(object)this, "No collection with the name '{0}' was found! No item added.", new object[1] { collection });
		return false;
	}

	public bool AddItemToCollection(string itemName, int amount, string collection, out int added)
	{
		added = 0;
		if (!((Behaviour)this).enabled)
		{
			return false;
		}
		for (int i = 0; i < m_AllCollections.Length; i++)
		{
			if (m_AllCollections[i].Name == collection)
			{
				bool result = false;
				if (m_ItemDatabase.FindItemByName(itemName, out var itemData))
				{
					result = m_AllCollections[i].TryAddItem(itemData, amount, out added, 0uL);
				}
				return result;
			}
		}
		Debug.LogWarningFormat((Object)(object)this, "No collection with the name '{0}' was found! No item added.", new object[1] { collection });
		return false;
	}

	public int GetItemCount(string name)
	{
		return MonoSingleton<GUIController>.Instance.GetContainer("Inventory").GetItemCount(name);
	}

	public bool TryRemoveItem(SavableItem item)
	{
		if (!((Behaviour)this).enabled)
		{
			return false;
		}
		for (int i = 0; i < m_AllCollections.Length; i++)
		{
			if (m_AllCollections[i].TryRemoveItem(item))
			{
				return true;
			}
		}
		return false;
	}

	public void RemoveItems(int ID, ulong uuid)
	{
		ItemContainer[] containers = MonoSingleton<GUIController>.Instance.Containers;
		for (int i = 0; i < containers.Length; i++)
		{
			foreach (Slot slot in containers[i].Slots)
			{
				if (slot.HasItem && ((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemUUID == uuid)
				{
					Try_DropItem(slot.CurrentItem, slot);
					((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemUUID = 0uL;
					((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemID = 0;
					((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemIndex = 0;
				}
			}
		}
	}

	public int findItemCount(ulong uuid)
	{
		int num = 0;
		ItemContainer[] containers = MonoSingleton<GUIController>.Instance.Containers;
		for (int i = 0; i < containers.Length; i++)
		{
			foreach (Slot slot in containers[i].Slots)
			{
				if (slot.HasItem && ((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemUUID == uuid)
				{
					num += slot.CurrentItem.CurrentInStack;
				}
			}
		}
		return num;
	}

	public int findItemCount(string tiemName)
	{
		int num = 0;
		ItemContainer[] containers = MonoSingleton<GUIController>.Instance.Containers;
		for (int i = 0; i < containers.Length; i++)
		{
			foreach (Slot slot in containers[i].Slots)
			{
				if (slot.HasItem && ((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemName == tiemName)
				{
					num += slot.CurrentItem.CurrentInStack;
				}
			}
		}
		return num;
	}

	public string findItemInCintainersName(ulong uuid)
	{
		string result = "";
		ItemContainer[] containers = MonoSingleton<GUIController>.Instance.Containers;
		for (int i = 0; i < containers.Length; i++)
		{
			foreach (Slot slot in containers[i].Slots)
			{
				if (slot.HasItem && ((Component)slot).gameObject.GetComponent<ItemOnObject>().item.itemUUID == uuid)
				{
					result = containers[i].Name;
				}
			}
		}
		return result;
	}

	public void RemoveItems(string itemName, int amount = 1)
	{
		MonoSingleton<GUIController>.Instance.GetContainer("Inventory").RemoveItems(itemName, amount);
	}

	public bool Try_DropItem(SavableItem item, Slot parentSlot = null)
	{
		if ((bool)item && Object.op_Implicit((Object)(object)item.ItemData.WorldObject))
		{
			if (Object.op_Implicit((Object)(object)parentSlot))
			{
				parentSlot.ItemHolder.SetItem(null);
			}
			return true;
		}
		return false;
	}

	public List<ItemHolder> GetEquipmentHolders()
	{
		return m_EquipmentHolders;
	}

	private void Awake()
	{
		if (!Object.op_Implicit((Object)(object)m_ItemDatabase))
		{
			Debug.LogError((object)"No ItemDatabase specified, the inventory will be disabled!", (Object)(object)this);
			((Behaviour)this).enabled = false;
			return;
		}
		SetState.SetTryer(TryChange_State);
		m_AllCollections = MonoSingleton<GUIController>.Instance.Containers;
		m_InventoryCollection = CreateListOfHolders(m_InventorySize);
		MonoSingleton<GUIController>.Instance.GetContainer("Inventory").Setup(m_InventoryCollection);
		m_HotbarCollection = CreateListOfHolders(m_HotbarSize);
		MonoSingleton<GUIController>.Instance.GetContainer("Hotbar").Setup(m_HotbarCollection);
		m_EquipmentHolders = CreateListOfHolders(m_EquipmentList.Count);
		for (int i = 0; i < m_EquipmentList.Count; i++)
		{
			ItemContainer container = MonoSingleton<GUIController>.Instance.GetContainer(m_EquipmentList[i]);
			if (Object.op_Implicit((Object)(object)container))
			{
				container.Setup(new List<ItemHolder> { m_EquipmentHolders[i] });
			}
			else
			{
				Debug.LogErrorFormat((Object)(object)this, "No GUI collection with the name '{0}' was found!", new object[1] { m_EquipmentList[i] });
			}
		}
		m_Player = GameController.LocalPlayer;
		m_Player.ChangeHealth.AddListener(OnChanged_PlayerHealth);
		m_Player.Death.AddListener(On_PlayerDeath);
	}

	private void OnChanged_PlayerHealth(HealthEventData data)
	{
		if (!(data.Delta < 0f))
		{
			return;
		}
		for (int i = 0; i < m_EquipmentHolders.Count; i++)
		{
			if (m_EquipmentHolders[i].HasItem && m_EquipmentHolders[i].CurrentItem.HasProperty("Durability"))
			{
				ItemProperty.Float @float = m_EquipmentHolders[i].CurrentItem.GetPropertyValue("Durability").Float;
				@float.Current--;
				if (@float.Current <= 0f)
				{
					m_EquipmentHolders[i].SetItem(null);
				}
			}
		}
	}

	private void On_PlayerDeath()
	{
		if (State.Get() != 0)
		{
			SetState.Try(ET.InventoryState.Closed);
		}
		RemoveItemsFromCollection("Inventory");
		RemoveItemsFromCollection("Hotbar");
		foreach (string equipment in m_EquipmentList)
		{
			RemoveItemsFromCollection(equipment);
		}
	}

	private void RemoveItemsFromCollection(string collection)
	{
		ItemContainer container = MonoSingleton<GUIController>.Instance.GetContainer(collection);
		if (!Object.op_Implicit((Object)(object)container))
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

	private void DropItemsFromCollection(string collection)
	{
		ItemContainer container = MonoSingleton<GUIController>.Instance.GetContainer(collection);
		if (!Object.op_Implicit((Object)(object)container))
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
					Try_DropItem(slot.CurrentItem, slot);
				}
			}
		}
	}

	private bool TryChange_State(ET.InventoryState state)
	{
		bool flag = false;
		if (Time.time > m_LastTimeToggledInventory + 0.5f)
		{
			m_LastTimeToggledInventory = Time.time;
			flag = true;
		}
		if (flag)
		{
			State.Set(state);
			EventSystem.current.SetSelectedGameObject((GameObject)null);
		}
		return flag;
	}

	private List<ItemHolder> CreateListOfHolders(int size)
	{
		List<ItemHolder> list = new List<ItemHolder>();
		for (int i = 0; i < size; i++)
		{
			list.Add(new ItemHolder());
		}
		return list;
	}
}
