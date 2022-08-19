using System;
using System.Collections.Generic;
using UltimateSurvival.GUISystem;

namespace UltimateSurvival
{
	// Token: 0x0200061E RID: 1566
	public static class CollectionUtils
	{
		// Token: 0x060031E0 RID: 12768 RVA: 0x001618DC File Offset: 0x0015FADC
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

		// Token: 0x060031E1 RID: 12769 RVA: 0x00161920 File Offset: 0x0015FB20
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

		// Token: 0x060031E2 RID: 12770 RVA: 0x00161964 File Offset: 0x0015FB64
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

		// Token: 0x060031E3 RID: 12771 RVA: 0x001619B0 File Offset: 0x0015FBB0
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

		// Token: 0x060031E4 RID: 12772 RVA: 0x00161A18 File Offset: 0x0015FC18
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
