using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
	public GameObject[] ItemEmbedSlot;

	public ItemSlot[] ItemsEquiped;

	public List<ItemSlot> ItemSlots = new List<ItemSlot>();

	public ItemManager itemManager;

	private CharacterStatus character;

	private CharacterAttack characterAttack;

	private CharacterSystem characterSystem;

	private void Start()
	{
		character = ((Component)this).gameObject.GetComponent<CharacterStatus>();
		characterAttack = ((Component)this).gameObject.GetComponent<CharacterAttack>();
		characterSystem = ((Component)this).gameObject.GetComponent<CharacterSystem>();
		itemManager = (ItemManager)(object)Object.FindObjectOfType(typeof(ItemManager));
		ItemsEquiped = new ItemSlot[ItemEmbedSlot.Length];
	}

	private void removeAllChild(GameObject parent)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		foreach (Transform item in parent.transform)
		{
			Transform val = item;
			if ((Object)(object)val != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}

	public void AddItem(int index, int num)
	{
		foreach (ItemSlot itemSlot2 in ItemSlots)
		{
			if (itemSlot2 != null && itemSlot2.Index == index)
			{
				itemSlot2.Num += num;
				return;
			}
		}
		ItemSlot itemSlot = new ItemSlot();
		itemSlot.Index = index;
		itemSlot.Num = num;
		ItemSlots.Add(itemSlot);
		EquipItem(itemSlot);
	}

	public void RemoveItem(ItemSlot item, int num)
	{
		if (item != null)
		{
			if (item.Num >= num)
			{
				item.Num -= num;
			}
			if (item.Num <= 0)
			{
				ItemSlots.Remove(item);
			}
		}
	}

	public void EquipItem(ItemSlot indexEquip)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)itemManager != (Object)null && indexEquip.Index >= itemManager.Items.Length)
		{
			return;
		}
		ItemCollector itemCollector = itemManager.Items[indexEquip.Index];
		if ((Object)(object)itemCollector.ItemPrefab != (Object)null)
		{
			ItemInventory component = itemCollector.ItemPrefab.GetComponent<ItemInventory>();
			if ((Object)(object)component != (Object)null)
			{
				int itemEmbedSlotIndex = component.ItemEmbedSlotIndex;
				GameObject obj = Object.Instantiate<GameObject>(itemCollector.ItemPrefab, ItemEmbedSlot[itemEmbedSlotIndex].transform.position, ItemEmbedSlot[itemEmbedSlotIndex].transform.rotation);
				removeAllChild(ItemEmbedSlot[itemEmbedSlotIndex]);
				obj.transform.parent = ItemEmbedSlot[itemEmbedSlotIndex].transform;
				ItemsEquiped[itemEmbedSlotIndex] = indexEquip;
				Debug.Log((object)("Equiped " + component));
			}
		}
	}

	public void UnEquipItem(ItemSlot indexEquip)
	{
		if (indexEquip.Index < itemManager.Items.Length)
		{
			ItemInventory component = itemManager.Items[indexEquip.Index].ItemPrefab.GetComponent<ItemInventory>();
			int itemEmbedSlotIndex = ((Component)component).GetComponent<ItemInventory>().ItemEmbedSlotIndex;
			removeAllChild(ItemEmbedSlot[itemEmbedSlotIndex]);
			ItemsEquiped[itemEmbedSlotIndex] = null;
			Debug.Log((object)("UnEquipped " + component));
		}
	}

	public void UseItem(ItemSlot indexItem)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (indexItem.Num > 0 && Object.op_Implicit((Object)(object)itemManager.Items[indexItem.Index].ItemPrefab))
		{
			GameObject obj = Object.Instantiate<GameObject>(itemManager.Items[indexItem.Index].ItemPrefab, ((Component)this).transform.position, ((Component)this).transform.rotation);
			obj.transform.parent = ((Component)this).gameObject.transform;
			RemoveItem(indexItem, 1);
			Debug.Log((object)string.Concat(obj, " Removed"));
		}
	}

	public bool CheckEquiped(ItemSlot indexEquip)
	{
		int itemEmbedSlotIndex = ((Component)itemManager.Items[indexEquip.Index].ItemPrefab.GetComponent<ItemInventory>()).GetComponent<ItemInventory>().ItemEmbedSlotIndex;
		if (ItemsEquiped[itemEmbedSlotIndex] != null)
		{
			return ItemsEquiped[itemEmbedSlotIndex].Index == indexEquip.Index;
		}
		return false;
	}

	private void Update()
	{
		if (ItemEmbedSlot.Length == 0)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < ItemsEquiped.Length; i++)
		{
			if (ItemsEquiped[i] != null)
			{
				num += itemManager.Items[ItemsEquiped[i].Index].ItemPrefab.GetComponent<ItemInventory>().Damage;
				num2 += itemManager.Items[ItemsEquiped[i].Index].ItemPrefab.GetComponent<ItemInventory>().Defend;
			}
		}
		if (Object.op_Implicit((Object)(object)character))
		{
			character.Damage = num;
			character.Defend = num2;
		}
		if ((Object)(object)itemManager != (Object)null && ItemsEquiped[0] != null)
		{
			int index = ItemsEquiped[0].Index;
			ItemInventory component = itemManager.Items[index].ItemPrefab.GetComponent<ItemInventory>();
			if (Object.op_Implicit((Object)(object)characterSystem))
			{
				characterSystem.SpeedAttack = component.SpeedAttack;
			}
			if (Object.op_Implicit((Object)(object)characterAttack))
			{
				characterAttack.SoundHit = component.SoundHit;
			}
		}
	}
}
