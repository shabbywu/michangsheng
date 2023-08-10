using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class LootItem
{
	[SerializeField]
	private string m_ItemName;

	[SerializeField]
	[Range(0f, 100f)]
	private float m_SpawnChance;

	[SerializeField]
	private int m_MinAmount;

	[SerializeField]
	private int m_MaxAmount;

	public string ItemName => m_ItemName;

	public float SpawnChance => m_SpawnChance;

	public void AddToInventory(out int added, float amountFactor)
	{
		added = 0;
		int num = Mathf.CeilToInt((float)Random.Range(m_MinAmount, m_MaxAmount) * amountFactor);
		if (num > 0)
		{
			MonoSingleton<InventoryController>.Instance.AddItemToCollection(m_ItemName, num, "Inventory", out added);
		}
	}

	public GameObject CreatePickup(Vector3 position, Quaternion rotation)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (MonoSingleton<InventoryController>.Instance.Database.FindItemByName(m_ItemName, out var itemData) && Object.op_Implicit((Object)(object)itemData.WorldObject))
		{
			GameObject obj = Object.Instantiate<GameObject>(itemData.WorldObject, position, rotation);
			ItemPickup component = obj.GetComponent<ItemPickup>();
			if (Object.op_Implicit((Object)(object)component))
			{
				component.ItemToAdd.CurrentInStack = Random.Range(m_MinAmount, m_MaxAmount);
			}
			return obj;
		}
		return null;
	}
}
