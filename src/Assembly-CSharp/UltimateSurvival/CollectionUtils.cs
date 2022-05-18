using System;
using System.Collections.Generic;
using UltimateSurvival.GUISystem;

namespace UltimateSurvival
{
	// Token: 0x02000905 RID: 2309
	public static class CollectionUtils
	{
		// Token: 0x06003B14 RID: 15124 RVA: 0x001AB1F8 File Offset: 0x001A93F8
		public static void AddItem(ItemData itemData, int amount, List<ItemHolder> itemHolders, out int added, List<ItemProperty.Value> customPropertyValues = null)
		{
			added = 0;
			for (int i = 0; i < itemHolders.Count; i++)
			{
				int num;
				itemHolders[i].TryAddItem(itemData, amount, out num, customPropertyValues, 0UL, 0);
				added += num;
				if (added == amount)
				{
					return;
				}
			}
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x001AB23C File Offset: 0x001A943C
		public static void AddItem(ItemData itemData, int amount, List<ItemHolder> itemHolders, List<ItemProperty.Value> customPropertyValues = null)
		{
			int num = 0;
			for (int i = 0; i < itemHolders.Count; i++)
			{
				int num2;
				itemHolders[i].TryAddItem(itemData, amount, out num2, customPropertyValues, 0UL, 0);
				num += num2;
				amount -= num2;
				if (amount == 0)
				{
					return;
				}
			}
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x001AB280 File Offset: 0x001A9480
		public static void AddItem(ItemData itemData, int amount, List<Slot> slots, out int added, List<ItemProperty.Value> customPropertyValues = null, ulong uuid = 0UL, int index = 0)
		{
			added = 0;
			for (int i = 0; i < slots.Count; i++)
			{
				int num;
				slots[i].ItemHolder.TryAddItem(itemData, amount, out num, customPropertyValues, uuid, index);
				added += num;
				amount -= num;
				if (amount == 0)
				{
					return;
				}
			}
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x001AB2CC File Offset: 0x001A94CC
		public static void RemoveItems(string itemName, int amount, List<ItemHolder> holders, out int removed)
		{
			removed = 0;
			for (int i = 0; i < holders.Count; i++)
			{
				if (holders[i].HasItem && holders[i].CurrentItem.Name == itemName)
				{
					int num;
					holders[i].RemoveFromStack(amount - removed, out num);
					removed += num;
					if (removed == amount)
					{
						return;
					}
				}
			}
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x001AB334 File Offset: 0x001A9534
		public static void RemoveItems(string itemName, int amount, List<Slot> slots, out int removed)
		{
			removed = 0;
			for (int i = 0; i < slots.Count; i++)
			{
				if (slots[i].HasItem && slots[i].CurrentItem.Name == itemName)
				{
					int num;
					slots[i].ItemHolder.RemoveFromStack(amount - removed, out num);
					removed += num;
					if (removed == amount)
					{
						return;
					}
				}
			}
		}
	}
}
