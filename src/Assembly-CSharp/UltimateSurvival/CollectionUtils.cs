using System.Collections.Generic;
using UltimateSurvival.GUISystem;

namespace UltimateSurvival;

public static class CollectionUtils
{
	public static void AddItem(ItemData itemData, int amount, List<ItemHolder> itemHolders, out int added, List<ItemProperty.Value> customPropertyValues = null)
	{
		added = 0;
		for (int i = 0; i < itemHolders.Count; i++)
		{
			itemHolders[i].TryAddItem(itemData, amount, out var added2, customPropertyValues, 0uL);
			added += added2;
			if (added == amount)
			{
				break;
			}
		}
	}

	public static void AddItem(ItemData itemData, int amount, List<ItemHolder> itemHolders, List<ItemProperty.Value> customPropertyValues = null)
	{
		int num = 0;
		for (int i = 0; i < itemHolders.Count; i++)
		{
			itemHolders[i].TryAddItem(itemData, amount, out var added, customPropertyValues, 0uL);
			num += added;
			amount -= added;
			if (amount == 0)
			{
				break;
			}
		}
	}

	public static void AddItem(ItemData itemData, int amount, List<Slot> slots, out int added, List<ItemProperty.Value> customPropertyValues = null, ulong uuid = 0uL, int index = 0)
	{
		added = 0;
		for (int i = 0; i < slots.Count; i++)
		{
			slots[i].ItemHolder.TryAddItem(itemData, amount, out var added2, customPropertyValues, uuid, index);
			added += added2;
			amount -= added2;
			if (amount == 0)
			{
				break;
			}
		}
	}

	public static void RemoveItems(string itemName, int amount, List<ItemHolder> holders, out int removed)
	{
		removed = 0;
		for (int i = 0; i < holders.Count; i++)
		{
			if (holders[i].HasItem && holders[i].CurrentItem.Name == itemName)
			{
				holders[i].RemoveFromStack(amount - removed, out var removed2);
				removed += removed2;
				if (removed == amount)
				{
					break;
				}
			}
		}
	}

	public static void RemoveItems(string itemName, int amount, List<Slot> slots, out int removed)
	{
		removed = 0;
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i].HasItem && slots[i].CurrentItem.Name == itemName)
			{
				slots[i].ItemHolder.RemoveFromStack(amount - removed, out var removed2);
				removed += removed2;
				if (removed == amount)
				{
					break;
				}
			}
		}
	}
}
