using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class ItemToGenerate
{
	[SerializeField]
	private bool m_Random;

	[SerializeField]
	private string m_CustomName;

	[SerializeField]
	[Clamp(1f, 9999999f)]
	private int m_StackSize = 1;

	public bool TryGenerate(out SavableItem runtimeItem)
	{
		runtimeItem = null;
		ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
		ItemData itemData;
		if (m_Random)
		{
			if (database.FindItemById(Random.Range(0, database.GetItemCount() - 1), out itemData))
			{
				runtimeItem = new SavableItem(itemData, (int)((float)itemData.StackSize * 0.1f) + 1);
				return true;
			}
		}
		else if (database.FindItemByName(m_CustomName, out itemData))
		{
			runtimeItem = new SavableItem(itemData, m_StackSize);
			return true;
		}
		return false;
	}

	public ItemData GenerateItemData()
	{
		ItemDatabase database = MonoSingleton<InventoryController>.Instance.Database;
		ItemData itemData = null;
		if (m_Random)
		{
			database.FindItemById(Random.Range(0, database.GetItemCount() - 1), out itemData);
		}
		else
		{
			database.FindItemByName(m_CustomName, out itemData);
		}
		return itemData;
	}
}
