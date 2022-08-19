using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000603 RID: 1539
	[Serializable]
	public class LootItem
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x0015EBDA File Offset: 0x0015CDDA
		public string ItemName
		{
			get
			{
				return this.m_ItemName;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x0015EBE2 File Offset: 0x0015CDE2
		public float SpawnChance
		{
			get
			{
				return this.m_SpawnChance;
			}
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x0015EBEC File Offset: 0x0015CDEC
		public void AddToInventory(out int added, float amountFactor)
		{
			added = 0;
			int num = Mathf.CeilToInt((float)Random.Range(this.m_MinAmount, this.m_MaxAmount) * amountFactor);
			if (num > 0)
			{
				MonoSingleton<InventoryController>.Instance.AddItemToCollection(this.m_ItemName, num, "Inventory", out added);
			}
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x0015EC34 File Offset: 0x0015CE34
		public GameObject CreatePickup(Vector3 position, Quaternion rotation)
		{
			ItemData itemData;
			if (MonoSingleton<InventoryController>.Instance.Database.FindItemByName(this.m_ItemName, out itemData) && itemData.WorldObject)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(itemData.WorldObject, position, rotation);
				ItemPickup component = gameObject.GetComponent<ItemPickup>();
				if (component)
				{
					component.ItemToAdd.CurrentInStack = Random.Range(this.m_MinAmount, this.m_MaxAmount);
				}
				return gameObject;
			}
			return null;
		}

		// Token: 0x04002B79 RID: 11129
		[SerializeField]
		private string m_ItemName;

		// Token: 0x04002B7A RID: 11130
		[SerializeField]
		[Range(0f, 100f)]
		private float m_SpawnChance;

		// Token: 0x04002B7B RID: 11131
		[SerializeField]
		private int m_MinAmount;

		// Token: 0x04002B7C RID: 11132
		[SerializeField]
		private int m_MaxAmount;
	}
}
